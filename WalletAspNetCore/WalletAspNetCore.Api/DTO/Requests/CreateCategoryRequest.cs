using System.ComponentModel.DataAnnotations;
namespace WalletAspNetCore.Api.DTO.Requests
{
    public record CreateCategoryRequest(
        [Required] string Name,
        [Required] bool IsIncome
    );
}
