namespace WalletAspNetCore.Models.Entities
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name {  get; set; }

        public bool? IsIncome {  get; set; }

        [InverseProperty(nameof(Transaction.CategoryNavigation))]
        public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
