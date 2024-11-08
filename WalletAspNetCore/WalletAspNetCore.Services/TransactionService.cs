using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletAspNetCore.Auth;
using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionService(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<decimal> GetSelectedKindTransactionsAmount(Guid userId, bool isIncome)
        {
            var transactions = await _transactionRepository.GetSelectedKindTransactions(userId, isIncome);

            var amount = Math.Abs(transactions.Sum(x => x.Amount));
            return amount;
        }

        public async Task<decimal> GetSelectedCategoryTransactionsAmount(Guid userId, Guid categoryId)
        {
            var transactions = await _transactionRepository.GetSelectedCategoryTransactions(userId, categoryId);

            var amount = Math.Abs(transactions.Sum(x => x.Amount));
            return amount;
        }

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
