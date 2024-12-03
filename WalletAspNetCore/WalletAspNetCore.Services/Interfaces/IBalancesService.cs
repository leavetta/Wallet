using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services.Interfaces
{
    public interface IBalancesService
    {
        Task<Balance> GetByUserId(Guid userId);
        Task<Guid> UpdateBalance(Guid userId, decimal currentAmount);
    }
}