using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(string name, bool isIncome);
        Task<Category> DeleteAsync(Category category);
        Task<Category?> GetByIdAsync(Guid id);
        Task<List<Category>> GetCategoriesAsync(bool selectedKey);
        Task<List<Category>> GetSelectedCategoriesAsync(Guid userId, bool selectedKey);
    }
}