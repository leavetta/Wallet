using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Services
{
    public class BalancesService : IBalancesService
    {
        private readonly BalanceRepository _balanceRepository;

        public BalancesService(BalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        public async Task<Balance> GetByUserId(Guid userId)
        {
            var balance = await _balanceRepository.GetByUserId(userId);
            return balance;
        }

        public async Task<Guid> UpdateBalance(Guid userId, decimal currentAmount)
        {
            var balanceId = await _balanceRepository.Update(userId, currentAmount);
            return balanceId;
        }
    }
}
