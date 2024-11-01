using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.DataBaseOperations.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> Create(string name, bool isIncome)
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

        public async Task<Category?> GetById(Guid id)
        {
            var category = await _dbContext.Categories
                //.Include(c=>c.Transactions)
                .FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<List<Category>> GetSelectedCategories(Guid id, bool selectedKey)
        {
            var categories = await _dbContext.Categories
                .Include(c=>c.Transactions.Where(u => u.UserNavigation.Id == id))
                .ThenInclude(u => u.UserNavigation)
                .Where(c => c.IsIncome == selectedKey)
                
                .ToListAsync();
            return categories;
        }

        public async Task<List<Category>> GetCategories(bool selectedKey)
        {
            var categories = await _dbContext.Categories
                .Where(c => c.IsIncome == selectedKey)

                .ToListAsync();
            return categories;
        }

        public async Task<Category> Delete(Category category)
        {
            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync();
            return category;
        }
        
    }
}
