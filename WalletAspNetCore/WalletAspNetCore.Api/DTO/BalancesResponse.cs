namespace WalletAspNetCore.Api.DTO
{
    public record BalancesResponse(
        Guid Id,
        decimal CurrentAmount
    );
}
