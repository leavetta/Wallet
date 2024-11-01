namespace WalletAspNetCore.Api.DTO.Responses
{
    public record TransactionResponse
    (
        Guid id,
        decimal Amount,
        string OperationDate,
        string CategoryName
    );
}
