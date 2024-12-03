using WalletAspNetCore.Auth;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.Services.Interfaces;


namespace WalletAspNetCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserRepository _userRepository;
        private readonly BalanceRepository _balanceRepository;
        private readonly IJwtService _jwtProvider;

        public AuthService(
            IPasswordHasher passwordHasher,
            UserRepository userRepository,
            BalanceRepository balanceRepository,
            IJwtService jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _balanceRepository = balanceRepository;
            _jwtProvider = jwtProvider;

        }

        public async Task<User> Register(string name, string email, string password)
        {
            Balance balance = await _balanceRepository.Create();

            string hashedPassword = _passwordHasher.Generate(password);

            var user = await _userRepository.Create(balance, name, email, hashedPassword);
            return user;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            var result = _passwordHasher.Verify(password, user.Password);

            if (result == false)
            {
                throw new Exception("Failed to login!");
            }

            var token = _jwtProvider.GenerateToken(user);
            return token;
        }
    }
}
