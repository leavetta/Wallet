using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Services
{
    public class ReportService : IReportService
    {
        private readonly ITransactionService _transactionService;

        public ReportService(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        //public async Task<Dictionary<Category, decimal>> GetReportAboutIncomeByCtegoriesAsync(Guid userId)
        //{
        //    Dictionary<Category, decimal> categoriesAmount = new Dictionary<Category, decimal>();
        //    List<ReportDto>

        //    bool isIncome = true;
        //    var transactions = await _transactionService.GetSelectedKindTransactionsAmountAsync(userId, isIncome);

        //    foreach (var category in categories)
        //    {
        //        decimal amount = Math.Abs(category.Transactions.Sum(t => t.Amount));
        //        categoriesAmount[category] = amount;
        //    }

        //    return categoriesAmount;
        //}

        //public async Task<Dictionary<Category, decimal>> GetReportAboutExpensesByCategoriesAsync(Guid userId)
        //{
        //    Dictionary<Category, decimal> categoriesAmount = new Dictionary<Category, decimal>();
        //    bool isIncome = false;
        //    var transactions = await _transactionService.GetSelectedKindTransactionsAmountAsync(userId, isIncome);

        //    foreach (var category in categories)
        //    {
        //        decimal amount = Math.Abs(category.Transactions.Sum(t => t.Amount));
        //        categoriesAmount[category] = amount;
        //    }

        //    return categoriesAmount;
        //}
    }
}
