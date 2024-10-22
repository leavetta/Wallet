namespace WalletAspNetCore.Models.Entities
{
    public class Balance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public decimal CurrentAmount { get; set; } = 0;
    }
}
