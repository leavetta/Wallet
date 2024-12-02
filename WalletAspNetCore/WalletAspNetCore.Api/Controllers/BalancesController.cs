using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories;
using Microsoft.EntityFrameworkCore;
using WalletAspNetCore.Api.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Net.Http.Headers;
using WalletAspNetCore.Auth;
using System.ComponentModel.DataAnnotations;


namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BalancesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BalanceRepository _balanceRepository;
        private readonly JwtParser _jwtParser;


        public BalancesController(ApplicationDbContext dbContext, BalanceRepository balanceRepository, JwtParser jwtParser)
        {
            _dbContext = dbContext;
            _balanceRepository = balanceRepository;
            _jwtParser = jwtParser;
        }

        [HttpGet]
        public async Task<IActionResult> GetById()
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var balance = await _balanceRepository.GetByUserId(userId);

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

                await _balanceRepository.Update(userId, currentAmount);

                return Ok();
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }

    }
}
