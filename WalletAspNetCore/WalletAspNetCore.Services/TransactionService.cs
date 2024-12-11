using Microsoft.EntityFrameworkCore;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly ApplicationDbContext _applicationDbContext;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            IBalanceRepository balanceRepository,
            ApplicationDbContext applicationDbContext)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _balanceRepository = balanceRepository;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Guid> CreateAsync(Guid userId, Guid categoryId, decimal amount)
        {
            using var ts = _applicationDbContext.Database.BeginTransaction();
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                var category = await _categoryRepository.GetByIdAsync(categoryId);

                var transaction = await _transactionRepository.CreateAsync(user, category, amount);

                await _balanceRepository.ApplyTransactionAsync(user, transaction);

                await ts.CommitAsync();

                return transaction.Id;
            }
            catch (Exception)
            {
                await ts.RollbackAsync();

                throw;
            }
        }

        public async Task<List<Transaction>> GetSelectedKindTransactionsAmountAsync(Guid userId, bool isIncome)
        {
            var transactions = await _transactionRepository.GetSelectedKindTransactionsAsync(userId, isIncome);

            //var amount = Math.Abs(transactions.Sum(x => x.Amount));
            return transactions;
        }

        //public async Task<decimal> GetSelectedCategoryTransactionsAmount(Guid userId, Guid categoryId)
        //{
        //    var transactions = await _transactionRepository.GetSelectedCategoryTransactions(userId, categoryId);

        //    var amount = Math.Abs(transactions.Sum(x => x.Amount));
        //    return amount;
        //}

        public async Task<List<Transaction>> GetTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null)
            {
                return await _transactionRepository.GetAllAsync(userId);
            }
            DateTime notNullStartDate = (DateTime)startDate;

            if (endDate == null)
            {

                return await _transactionRepository.GetTransactionsOfRangeDateAsync(userId, notNullStartDate.AddHours(7), notNullStartDate.AddHours(7));
            }
            DateTime notNullEndDate = (DateTime)endDate;

            return await _transactionRepository.GetTransactionsOfRangeDateAsync(userId, notNullStartDate.AddHours(7), notNullEndDate.AddHours(7));
        }
    }
}
