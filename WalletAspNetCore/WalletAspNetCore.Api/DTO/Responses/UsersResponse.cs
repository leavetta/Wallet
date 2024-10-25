namespace WalletAspNetCore.Api.DTO.Responses
{
    public record UsersResponse
    (
        Guid id,
        string Name,
        string Email,
        string Password
    );
}
