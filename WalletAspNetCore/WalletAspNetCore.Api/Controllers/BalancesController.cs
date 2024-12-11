using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.Models.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using WalletAspNetCore.Auth;
using System.ComponentModel.DataAnnotations;
using WalletAspNetCore.Services.Interfaces;


namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BalancesController : ControllerBase
    {
        private readonly IBalancesService _balanceService;
        private readonly JwtParser _jwtParser;


        public BalancesController(IBalancesService balanceService, JwtParser jwtParser)
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

                var balance = await _balanceService.GetByUserIdAsync(userId);

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

                var balanceId = await _balanceService.UpdateBalanceAsync(userId, currentAmount);

                return Ok(balanceId);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }
    }
}
