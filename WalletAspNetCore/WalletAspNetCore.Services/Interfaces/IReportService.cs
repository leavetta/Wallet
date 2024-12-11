using WalletAspNetCore.Models.DTO.Responses;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportResponse> GenerateReportAboutExpensesByCategoriesAsync(Guid userId);
        Task<ReportResponse> GenerateReportAboutIncomeByCtegoriesAsync(Guid userId);
    }
}