namespace WalletAspNetCore.Api.DTO
{
    public record TransactionsResponse
    (
        Guid id,
        decimal Amount,
        DateTime OperationDate,
        string CategoryName
    );
}
