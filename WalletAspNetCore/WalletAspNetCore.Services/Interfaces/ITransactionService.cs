using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Guid> Create(Guid userId, Guid categoryId, decimal amount);
        Task<Dictionary<Category, decimal>> GetReportSelectedKindTransactions(Guid userId, bool selectedKey);
        Task<List<Transaction>> GetTransactions(Guid userId, DateTime? startDate, DateTime? endDate);
    }
}