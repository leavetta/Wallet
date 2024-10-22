using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories
{
    public class BalanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BalanceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Update(User user, Transaction transaction)
        {
            var balance = await _dbContext.Balances.FirstOrDefaultAsync(b => b.UserNavigation.Id == user.Id);
            
            if (balance == null)
            {
                return Guid.Empty;
            }

            balance.CurrentAmount += transaction.Amount;

            await _dbContext.SaveChangesAsync();
            return balance.Id;
        }

        public async Task<Balance> Create()
        {
            Balance balance = new Balance();
            balance.CurrentAmount = 0;
            balance.UserNavigation = null;
            _dbContext.Balances.Attach(balance);

            await _dbContext.SaveChangesAsync();

            return balance;
        }
    }
}
