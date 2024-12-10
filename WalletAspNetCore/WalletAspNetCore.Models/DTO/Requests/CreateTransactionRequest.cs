using System.ComponentModel.DataAnnotations;

namespace WalletAspNetCore.Models.DTO.Requests
{
    public record CreateTransactionRequest(
        [Required] decimal Amount,
        [Required] Guid CategoryId
    );
}
