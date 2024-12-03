using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.Api.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using WalletAspNetCore.Auth;
using System.ComponentModel.DataAnnotations;
using WalletAspNetCore.Services;


namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BalancesController : ControllerBase
    {
        private readonly BalancesService _balanceService;
        private readonly JwtParser _jwtParser;


        public BalancesController(BalancesService balanceService, JwtParser jwtParser)
        {
            _balanceService = balanceService;
            _jwtParser = jwtParser;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBalance()
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var balance = await _balanceService.GetByUserId(userId);

                if (balance == null)
                {
                    return NotFound("Balance not found");
                }

                var balanceResponse = new BalancesResponse(balance.Id, balance.CurrentAmount);

                return Ok(balanceResponse);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBalance([Required] decimal currentAmount)
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var balanceId = await _balanceService.UpdateBalance(userId, currentAmount);

                return Ok(balanceId);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }
    }
}
