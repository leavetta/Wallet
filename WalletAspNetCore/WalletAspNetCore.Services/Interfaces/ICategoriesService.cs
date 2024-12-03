using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<Guid> CreateCategory(string cateoryName, bool isIncome);
        Task<Category> GetCategoryById(Guid categoryId);
        Task<List<Category>> GetSelectedCategories(bool selectedKey);
    }
}