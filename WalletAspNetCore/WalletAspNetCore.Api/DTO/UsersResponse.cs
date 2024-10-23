namespace WalletAspNetCore.Api.DTO
{
    public record UsersResponse
    (
        Guid id,
        string Name,
        string Email,
        string Password
    );
}
