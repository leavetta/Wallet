
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

            builder.HasOne(t => t.UserNavigation)
                .WithMany(u => u.Transactions)
                .HasForeignKey("FK_Transactions_User_UserId");
        }
    }
}
