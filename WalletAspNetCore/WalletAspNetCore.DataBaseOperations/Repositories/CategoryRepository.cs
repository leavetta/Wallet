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

        public async Task<Category> Create(string name)
        {
            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = name
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

        public async Task<Category> Delete(Category category)
        {
            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync();
            return category;
        }
        
    }
}
