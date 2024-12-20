﻿using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var userEntity = await _dbContext.Users
                .Include(u => u.BalanceNavigation)
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception();
            return userEntity;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.BalanceNavigation)
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
            return userEntity;
        }

        //public async Task<User> Update(User user)
        //{

        //}

        public async Task<User> CreateAsync(Balance balance, string name, string email, string password)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password,
                BalanceNavigation = balance
            };

            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
