using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<Guid> CreateCategoryAsync(string cateoryName, bool isIncome);
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task<List<Category>> GetSelectedCategoriesAsync(bool selectedKey);
    }
}