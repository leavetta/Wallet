using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Guid> CreateAsync(Guid userId, Guid categoryId, decimal amount);
        Task<Dictionary<Category, decimal>> GetReportSelectedKindTransactionsAsync(Guid userId, bool selectedKey);
        Task<List<Transaction>> GetTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate);
    }
}