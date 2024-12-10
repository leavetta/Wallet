namespace WalletAspNetCore.Api.DTO.Responses
{
    public record ReportDto
    (
        CategoryDto CategoryDto, 
        decimal Amount, 
        List<TransactionDto> TransactionDtos
    );
}
