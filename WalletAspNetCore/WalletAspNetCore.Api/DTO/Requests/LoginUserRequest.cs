using System.ComponentModel.DataAnnotations;

namespace WalletAspNetCore.Api.DTO.Requests
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password
    );
}
