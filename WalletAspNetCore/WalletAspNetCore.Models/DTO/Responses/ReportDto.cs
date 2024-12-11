namespace WalletAspNetCore.Models.DTO.Responses
{
    public record ReportDto
    (
        CategoryDto CategoryDto, 
        decimal Amount, 
        IEnumerable<TransactionDto> TransactionDtos
    );
}
