using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> Create(User user, Category category, decimal amount);
        Task<List<Transaction>> GetAll(Guid id);
        Task<List<Transaction>> GetTransactionsOfRangeDate(Guid userId, DateTime startDate, DateTime endDate);
    }
}