using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Transaction>> GetAllAsync(Guid id)
        {
            var userTransactions = await _dbContext.Transactions
                .Include(u => u.UserNavigation)
                .Where(u => u.UserNavigation.Id == id)
                .Include(c => c.CategoryNavigation)
                .OrderByDescending(t => t.OperationDate)

                .AsNoTracking()
                .ToListAsync();

            return userTransactions;
        }

        //public async Task<List<Transaction>> GetSelectedKindTransactions(Guid id, bool isIncome)
        //{
        //    var userTransactions = await _dbContext.Transactions
        //        .Include(u => u.UserNavigation)
        //        .Where(u => u.UserNavigation.Id == id)
        //        .Include(c => c.CategoryNavigation)
        //        .Where(c => c.CategoryNavigation.IsIncome == isIncome)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return userTransactions;
        //}

        //public async Task<List<Transaction>> GetSelectedCategoryTransactions(Guid userId, Guid categoryId)
        //{
        //    var userTransactions = await _dbContext.Transactions
        //        .Include(u => u.UserNavigation)
        //        .Where(u => u.UserNavigation.Id == userId)
        //        .Include(c => c.CategoryNavigation)
        //        .Where(c => c.CategoryNavigation.Id == categoryId)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return userTransactions;
        //}

        public async Task<Transaction> CreateAsync(User user, Category category, decimal amount)
        {
            Transaction transaction = new()
            {
                UserNavigation = user,
                Id = Guid.NewGuid(),
                CategoryId = category.Id,
                CategoryNavigation = category,
                OperationDate = DateTime.Now.ToUniversalTime(),
                Amount = category.IsIncome == true ? amount : -amount
            };

            await _dbContext.Transactions.AddAsync(transaction);

            await _dbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task<List<Transaction>> GetTransactionsOfRangeDateAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            var userTransactions = await _dbContext.Transactions
                .Include(u => u.UserNavigation)
                .Where(u => u.UserNavigation.Id == userId)
                .Include(c => c.CategoryNavigation)
                .Where(c => c.OperationDate.Date >= startDate.Date && c.OperationDate.Date <= endDate.Date)
                .OrderByDescending(t => t.OperationDate)
                .AsNoTracking()
                .ToListAsync();

            return userTransactions;
        }
    }
}
