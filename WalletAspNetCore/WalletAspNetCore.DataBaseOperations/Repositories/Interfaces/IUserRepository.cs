using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(Balance balance, string name, string email, string password);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid id);
    }
}