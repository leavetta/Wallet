
namespace WalletAspNetCore.Models.Entities.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.Transactions)
                .WithOne(t => t.CategoryNavigation);
        }
    }
}
