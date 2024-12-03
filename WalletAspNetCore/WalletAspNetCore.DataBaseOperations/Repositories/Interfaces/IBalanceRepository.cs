using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories
{
    public interface IBalanceRepository
    {
        Task<Guid> ApplyTransaction(User user, Transaction transaction);
        Task<Balance> Create();
        Task<Balance> GetByUserId(Guid id);
        Task<Guid> Update(Guid userId, decimal currentAmount);
    }
}