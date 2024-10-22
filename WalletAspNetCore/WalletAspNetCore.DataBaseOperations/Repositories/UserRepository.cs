using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BalanceRepository _balanceRepository;

        public UserRepository(ApplicationDbContext dbContext, BalanceRepository balanceRepository)
        {
            _dbContext = dbContext;
            _balanceRepository = balanceRepository;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> Update(User user)
        {

        }

        public async Task<User> Create(Balance balance, string name, string email, string password)
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            user.BalanceNavigation = balance;

            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
