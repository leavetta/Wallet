using System.ComponentModel.DataAnnotations;

namespace WalletAspNetCore.Models.DTO.Requests
{
    public record RegisterUserRequest(
        [Required] string Name,
        [Required] string Email,
        [Required] string Password
    );
}
