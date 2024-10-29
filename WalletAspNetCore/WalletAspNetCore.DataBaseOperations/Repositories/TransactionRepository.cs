﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories
{
    public class TransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Transaction>> GetAll(Guid id)
        {
            var userTransactions = await _dbContext.Transactions
                .Include(u => u.UserNavigation)
                .Where(u => u.UserNavigation.Id == id)
                .Include(c => c.CategoryNavigation)
                
                .AsNoTracking()
                .ToListAsync();

            return userTransactions;
        }

        public async Task<List<Transaction>> GetSelectedKindTransactions(Guid id, bool isIncome)
        {
            var userTransactions = await _dbContext.Transactions
                .Include(u => u.UserNavigation)
                .Where(u => u.UserNavigation.Id == id)
                .Include(c => c.CategoryNavigation)
                .Where(c => c.CategoryNavigation.IsIncome == isIncome)
                .AsNoTracking()
                .ToListAsync();

            return userTransactions;
        }

        public async Task<List<Transaction>> GetSelectedCategoryTransactions(Guid userId, Guid categoryId)
        {
            var userTransactions = await _dbContext.Transactions
                .Include(u => u.UserNavigation)
                .Where(u => u.UserNavigation.Id == userId)
                .Include(c => c.CategoryNavigation)
                .Where(c => c.CategoryNavigation.Id == categoryId)
                .AsNoTracking()
                .ToListAsync();

            return userTransactions;
        }

        public async Task<Transaction> Create(User user, Category category, decimal amount)
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
    }
}
