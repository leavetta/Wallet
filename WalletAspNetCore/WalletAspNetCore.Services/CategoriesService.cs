using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> CreateCategoryAsync(string cateoryName, bool isIncome)
        {
            var category = await _categoryRepository.CreateAsync(cateoryName, isIncome);
            return category.Id;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            return category;
        }

        public async Task<List<Category>> GetSelectedCategoriesAsync(bool selectedKey)
        {
            var categories = await _categoryRepository.GetCategoriesAsync(selectedKey);

            return categories;
        }
    }
}
