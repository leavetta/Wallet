using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(Balance balance, string name, string email, string password);
        Task<User> GetByEmail(string email);
        Task<User> GetById(Guid id);
    }
}