using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Guid> CreateAsync(Guid userId, Guid categoryId, decimal amount);
        Task<List<Transaction>> GetSelectedKindTransactionsAmountAsync(Guid userId, bool isIncome);
        Task<List<Transaction>> GetTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate);
    }
}