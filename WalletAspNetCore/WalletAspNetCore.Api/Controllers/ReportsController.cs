using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WalletAspNetCore.Auth;
using WalletAspNetCore.Models.DTO.Responses;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly JwtParser _jwtParser;

        public ReportsController(IReportService reportService, JwtParser jwtParser)
        {
            _reportService = reportService;
            _jwtParser = jwtParser;
        }

        [HttpGet]
        [Route("income")]
        public async Task<IActionResult> GetIncomeReportAsync()
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var reportResponse = await _reportService.GenerateReportAboutIncomeByCtegoriesAsync(userId);

                return Ok(reportResponse);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }

        [HttpGet]
        [Route("expenses")]
        public async Task<IActionResult> GetReportAboutExpensesAsync()
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var reportResponse = await _reportService.GenerateReportAboutExpensesByCategoriesAsync(userId);

                return Ok(reportResponse);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }
    }
}
