using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories.Interfaces
{
    public interface IBalanceRepository
    {
        Task<Guid?> ApplyTransactionAsync(User user, Transaction transaction);
        Task<Balance> CreateAsync();
        Task<Balance> GetByUserIdAsync(Guid id);
        Task<Guid?> UpdateAsync(Guid userId, decimal currentAmount);
    }
}