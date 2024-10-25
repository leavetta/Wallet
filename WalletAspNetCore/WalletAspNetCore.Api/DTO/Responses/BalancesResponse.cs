namespace WalletAspNetCore.Api.DTO.Responses
{
    public record BalancesResponse(
        Guid Id,
        decimal CurrentAmount
    );
}
