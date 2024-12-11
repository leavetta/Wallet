using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Models.DTO.Responses;
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

        public async Task<ReportResponse> GenerateReportAboutIncomeByCtegoriesAsync(Guid userId)
        {
            bool isIncome = true;
            var transactions = await _transactionService.GetSelectedKindTransactionsAmountAsync(userId, isIncome);

            var reportResponse = CreateReportResponse(transactions);
            return reportResponse;
        }

        public async Task<ReportResponse> GenerateReportAboutExpensesByCategoriesAsync(Guid userId)
        {
            bool isIncome = false;
            var transactions = await _transactionService.GetSelectedKindTransactionsAmountAsync(userId, isIncome);

            var reportResponse = CreateReportResponse(transactions);
            return reportResponse;
        }

        private static ReportResponse CreateReportResponse(List<Transaction> transactions)
        {
            var reportDtos = FillReportDtos(transactions);

            var totalAmount = CalculateTotalAmount(transactions);

            var reportResponse =
                new ReportResponse(
                    reportDtos,
                    totalAmount);

            return reportResponse;
        }

        private static List<ReportDto> FillReportDtos(List<Transaction> transactions)
        {
            List<ReportDto> reportDtos = new();

            var categories = transactions.Select(x => x.CategoryNavigation).GroupBy(c => c.Name).Select(grp => grp.First());

            foreach (var category in categories)
            {
                var transactionsOfCategory = transactions
                    .Where(x => x.CategoryNavigation.Name == category.Name)
                    .Select(t => new TransactionDto(
                        t.Id,
                        t.Amount,
                        t.OperationDate.ToString("dd.MM.yyyy hh:mm:ss"),
                        category.Name));

                var transactionsOfCategoryAmount = transactionsOfCategory.Sum(t => t.Amount);

                reportDtos.Add(
                    new ReportDto(
                        new CategoryDto(
                            category.Id,
                            category.Name
                        ),
                        transactionsOfCategoryAmount,
                        transactionsOfCategory
                    ));
            }

            return reportDtos;
        }

        private static decimal CalculateTotalAmount(List<Transaction> transactions)
        {
            return transactions.Sum(t => t.Amount);
        }
    }
}
