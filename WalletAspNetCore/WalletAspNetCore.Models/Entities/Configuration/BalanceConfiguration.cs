
namespace WalletAspNetCore.Models.Entities.Configuration
{
    public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(b => b.UserNavigation)
                .WithOne(u => u.BalanceNavigation)
                .HasForeignKey<User>("FK_Balance_User_UserId");
            
        }
    }
}
