using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserRepository _userRepository;

        public UsersService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            return user;

        }
    }
}
