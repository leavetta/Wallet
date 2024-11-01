using System.ComponentModel.DataAnnotations;

namespace WalletAspNetCore.Api.DTO.Requests
{
    public record CreateTransactionRequest(
        [Required] Guid UserId,
        [Required] decimal Amount,
        [Required] Guid CategoryId
    );
}
