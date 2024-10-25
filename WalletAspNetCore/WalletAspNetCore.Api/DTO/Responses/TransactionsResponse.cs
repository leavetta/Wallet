namespace WalletAspNetCore.Api.DTO.Responses
{
    public record TransactionsResponse
    (
        Guid id,
        decimal Amount,
        DateTime OperationDate,
        string CategoryName
    );
}
