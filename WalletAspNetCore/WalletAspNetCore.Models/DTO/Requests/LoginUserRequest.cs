using System.ComponentModel.DataAnnotations;

namespace WalletAspNetCore.Models.DTO.Requests
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password
    );
}
