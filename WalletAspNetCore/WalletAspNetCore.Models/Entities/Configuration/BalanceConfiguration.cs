
namespace WalletAspNetCore.Models.Entities.Configuration
{
    public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasData(new Balance
            {
                Id = Guid.NewGuid(),
                CurrentAmount = 0
            });
            
        }
    }
}
