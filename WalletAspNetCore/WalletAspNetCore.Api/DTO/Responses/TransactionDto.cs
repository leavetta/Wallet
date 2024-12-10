namespace WalletAspNetCore.Api.DTO.Responses
{
    public record TransactionDto
    (
        Guid id,
        decimal Amount,
        string OperationDate,
        string CategoryName
    );
}
