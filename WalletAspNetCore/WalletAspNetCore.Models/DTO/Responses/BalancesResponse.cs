namespace WalletAspNetCore.Models.DTO.Responses
{
    public record BalancesResponse(
        Guid Id,
        decimal CurrentAmount
    );
}
