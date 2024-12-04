using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services.Interfaces
{
    public interface IBalancesService
    {
        Task<Balance> GetByUserIdAsync(Guid userId);
        Task<Guid?> UpdateBalanceAsync(Guid userId, decimal currentAmount);
    }
}