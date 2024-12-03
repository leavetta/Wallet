using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoriesService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> CreateCategory(string cateoryName, bool isIncome)
        {
            var category = await _categoryRepository.Create(cateoryName, isIncome);
            return category.Id;
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);
            return category;
        }

        public async Task<List<Category>> GetSelectedCategories(bool selectedKey)
        {
            var categories = await _categoryRepository.GetCategories(selectedKey);

            return categories;
        }
    }
}
