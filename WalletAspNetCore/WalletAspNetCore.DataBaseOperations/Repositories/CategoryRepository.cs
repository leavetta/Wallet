using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories.Interfaces;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(string name, bool isIncome)
        {
            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                IsIncome = isIncome
            };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            var category = await _dbContext.Categories
                //.Include(c=>c.Transactions)
                .FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<List<Category>> GetSelectedCategoriesAsync(Guid userId, bool selectedKey)
        {
            var categories = await _dbContext.Categories
                .Include(c => c.Transactions.Where(u => u.UserNavigation.Id == userId))
                .ThenInclude(u => u.UserNavigation)
                .Where(c => c.IsIncome == selectedKey)

                .ToListAsync();
            return categories;
        }

        public async Task<List<Category>> GetCategoriesAsync(bool selectedKey)
        {
            var categories = await _dbContext.Categories
                .Where(c => c.IsIncome == selectedKey)

                .ToListAsync();
            return categories;
        }

        public async Task<Category> DeleteAsync(Category category)
        {
            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync();
            return category;
        }

    }
}
