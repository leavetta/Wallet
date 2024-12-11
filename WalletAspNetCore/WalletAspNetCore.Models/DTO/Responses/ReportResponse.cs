namespace WalletAspNetCore.Models.DTO.Responses
{
    public record ReportResponse
    (
        List<ReportDto> ReportDtos,
        decimal TotalAmount
    );
}
