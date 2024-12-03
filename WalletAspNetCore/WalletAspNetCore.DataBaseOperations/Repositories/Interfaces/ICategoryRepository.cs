using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> Create(string name, bool isIncome);
        Task<Category> Delete(Category category);
        Task<Category?> GetById(Guid id);
        Task<List<Category>> GetCategories(bool selectedKey);
        Task<List<Category>> GetSelectedCategories(Guid userId, bool selectedKey);
    }
}