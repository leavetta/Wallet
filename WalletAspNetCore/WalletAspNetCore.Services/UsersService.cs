using WalletAspNetCore.Auth;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.DataBaseOperations.Repositories;


namespace WalletAspNetCore.Services
{
    public class UsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(
            IPasswordHasher passwordHasher, 
            UserRepository userRepository,
            IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(Balance balance, string name, string email, string password)
        {
            string hashedPassword = _passwordHasher.Generate(password);

            var user = await _userRepository.Create(balance, name, email, hashedPassword);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            var result = _passwordHasher.Verify(password, user.Password);

            if(result == false)
            {
                throw new Exception("Failed to login!");
            }

            var token = _jwtProvider.GenerateToken(user);
            return token;
        }
    }
}
