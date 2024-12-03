using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Services
{
    public class BalancesService : IBalancesService
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalancesService(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        public async Task<Balance> GetByUserIdAsync(Guid userId)
        {
            var balance = await _balanceRepository.GetByUserIdAsync(userId);
            return balance;
        }

        public async Task<Guid> UpdateBalanceAsync(Guid userId, decimal currentAmount)
        {
            var balanceId = await _balanceRepository.UpdateAsync(userId, currentAmount);
            return balanceId;
        }
    }
}
