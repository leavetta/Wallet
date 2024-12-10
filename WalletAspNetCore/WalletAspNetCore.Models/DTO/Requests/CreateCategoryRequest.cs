using System.ComponentModel.DataAnnotations;
namespace WalletAspNetCore.Models.DTO.Requests
{
    public record CreateCategoryRequest(
        [Required] string Name,
        [Required] bool IsIncome
    );
}
