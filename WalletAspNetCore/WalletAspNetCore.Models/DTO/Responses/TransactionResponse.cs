namespace WalletAspNetCore.Models.DTO.Responses
{
    public record TransactionResponse
    (
        Guid id,
        decimal Amount,
        string OperationDate,
        string CategoryName
    );
}
