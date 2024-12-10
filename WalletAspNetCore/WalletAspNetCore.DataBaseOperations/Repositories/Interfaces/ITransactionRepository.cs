using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateAsync(User user, Category category, decimal amount);
        Task<List<Transaction>> GetAllAsync(Guid id);
        Task<List<Transaction>> GetSelectedKindTransactionsAsync(Guid id, bool isIncome);
        Task<List<Transaction>> GetTransactionsOfRangeDateAsync(Guid userId, DateTime startDate, DateTime endDate);
    }
}