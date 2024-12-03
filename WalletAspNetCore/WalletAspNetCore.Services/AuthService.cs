using WalletAspNetCore.Auth;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services.Interfaces;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;


namespace WalletAspNetCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IJwtService _jwtProvider;

        public AuthService(
            IPasswordHasher passwordHasher,
            IUserRepository userRepository,
            IBalanceRepository balanceRepository,
            IJwtService jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _balanceRepository = balanceRepository;
            _jwtProvider = jwtProvider;

        }

        public async Task<User> RegisterAsync(string name, string email, string password)
        {
            Balance balance = await _balanceRepository.CreateAsync();

            string hashedPassword = _passwordHasher.Generate(password);

            var user = await _userRepository.CreateAsync(balance, name, email, hashedPassword);
            return user;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
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
