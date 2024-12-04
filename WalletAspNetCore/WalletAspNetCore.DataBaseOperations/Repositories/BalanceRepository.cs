using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BalanceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid?> ApplyTransactionAsync(User user, Transaction transaction)
        {
            var balance = await _dbContext.Balances
                .Include(b => b.UserNavigation)
                .FirstOrDefaultAsync(b => b.UserNavigation.Id == user.Id);

            if (balance == null)
            {
                return null;
            }

            balance.CurrentAmount += transaction.Amount;

            await _dbContext.SaveChangesAsync();
            return balance.Id;
        }

        public async Task<Guid?> UpdateAsync(Guid userId, decimal currentAmount)
        {
            var balance = await _dbContext.Balances
                .Include(b => b.UserNavigation)
                .FirstOrDefaultAsync(b => b.UserNavigation.Id == userId);

            if (balance == null)
            {
                return null;
            }

            balance.CurrentAmount += currentAmount;

            await _dbContext.SaveChangesAsync();
            return balance.Id;
        }

        public async Task<Balance> CreateAsync()
        {
            Balance balance = new()
            {
                CurrentAmount = 0,
                UserNavigation = null
            };
            await _dbContext.Balances.AddAsync(balance);

            await _dbContext.SaveChangesAsync();

            return balance;
        }

        public async Task<Balance> GetByUserIdAsync(Guid id)
        {
            var balance = await _dbContext.Balances
                .Include(b => b.UserNavigation)
                .FirstOrDefaultAsync(b => b.UserNavigation.Id == id);
            return balance;
        }
    }
}
