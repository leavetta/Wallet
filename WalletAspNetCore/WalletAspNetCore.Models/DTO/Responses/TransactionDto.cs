namespace WalletAspNetCore.Models.DTO.Responses
{
    public record TransactionDto
    (
        Guid Id,
        decimal Amount,
        string OperationDate,
        string CategoryName
    );
}
