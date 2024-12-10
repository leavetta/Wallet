namespace WalletAspNetCore.Models.DTO.Responses
{
    public record TransactionDto
    (
        Guid id,
        decimal Amount,
        string OperationDate,
        string CategoryName
    );
}
