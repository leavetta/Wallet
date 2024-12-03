using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
        Task<User> Register(string name, string email, string password);
    }
}