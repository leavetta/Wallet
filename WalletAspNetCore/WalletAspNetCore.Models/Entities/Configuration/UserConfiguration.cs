namespace WalletAspNetCore.Models.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Transactions)
                .WithOne(t => t.UserNavigation);

            builder.HasOne(u => u.BalanceNavigation)
                .WithOne(b=>b.UserNavigation);
        }
    }
}
