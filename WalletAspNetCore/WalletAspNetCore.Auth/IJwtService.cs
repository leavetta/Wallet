using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Auth
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}