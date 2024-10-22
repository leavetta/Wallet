
namespace WalletAspNetCore.Models.Entities.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(t => t.CategoryNavigation)
                .WithMany(c => c.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey("FK_Transactions_Category_CategoryId");
        }
    }
}
