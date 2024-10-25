using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}