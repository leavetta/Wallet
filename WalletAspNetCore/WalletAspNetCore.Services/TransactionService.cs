using Microsoft.EntityFrameworkCore;
using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly UserRepository _userRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly BalanceRepository _balanceRepository;

        public TransactionService(
            TransactionRepository transactionRepository,
            UserRepository userRepository,
            CategoryRepository categoryRepository,
            BalanceRepository balanceRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _balanceRepository = balanceRepository;
        }

        public async Task<Guid> Create(Guid userId, Guid categoryId, decimal amount)
        {
            var user = await _userRepository.GetById(userId);
            var category = await _categoryRepository.GetById(categoryId);

            var transaction = await _transactionRepository.Create(user, category, amount);

            await _balanceRepository.ApplyTransaction(user, transaction);

            return transaction.Id;
        }

        public async Task<Dictionary<Category, decimal>> GetReportSelectedKindTransactions(Guid userId, bool selectedKey)
        {
            Dictionary<Category, decimal> categoriesAmount = new Dictionary<Category, decimal>();

            var categories = await _categoryRepository.GetSelectedCategories(userId, selectedKey);

            foreach (var category in categories)
            {
                decimal amount = Math.Abs(category.Transactions.Sum(t => t.Amount));
                categoriesAmount[category] = amount;
            }

            return categoriesAmount;
        }

        //public async Task<decimal> GetSelectedKindTransactionsAmount(Guid userId, bool isIncome)
        //{
        //    var transactions = await _transactionRepository.GetSelectedKindTransactions(userId, isIncome);

        //    var amount = Math.Abs(transactions.Sum(x => x.Amount));
        //    return amount;
        //}

        //public async Task<decimal> GetSelectedCategoryTransactionsAmount(Guid userId, Guid categoryId)
        //{
        //    var transactions = await _transactionRepository.GetSelectedCategoryTransactions(userId, categoryId);

        //    var amount = Math.Abs(transactions.Sum(x => x.Amount));
        //    return amount;
        //}

        public async Task<List<Transaction>> GetTransactions(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null)
            {
                return await _transactionRepository.GetAll(userId);
            }
            DateTime notNullStartDate = (DateTime)startDate;

            if (endDate == null)
            {

                return await _transactionRepository.GetTransactionsOfRangeDate(userId, notNullStartDate.AddHours(7), notNullStartDate.AddHours(7));
            }
            DateTime notNullEndDate = (DateTime)endDate;

            return await _transactionRepository.GetTransactionsOfRangeDate(userId, notNullStartDate.AddHours(7), notNullEndDate.AddHours(7));
        }
    }
}
