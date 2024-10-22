namespace WalletAspNetCore.Models.Entities
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime OperationDate { get; set; }

        [Required]
        [DisplayName("Category")]
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Category.Transactions))]
        public Category CategoryNavigation { get; set; }

        [InverseProperty(nameof(User.Transactions))]
        public User UserNavigation { get; set; }
    }
}
