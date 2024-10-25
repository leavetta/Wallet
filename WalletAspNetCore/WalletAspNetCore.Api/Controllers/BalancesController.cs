using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories;
using Microsoft.EntityFrameworkCore;
using WalletAspNetCore.Api.DTO.Responses;


namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BalancesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly BalanceRepository _balanceRepository;


        public BalancesController(ApplicationDbContext dbContext, BalanceRepository balanceRepository)
        {
            _dbContext = dbContext;
            _balanceRepository = balanceRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var balance = await _balanceRepository.GetByUserId(id);
            if (balance == null)
            {
                return NotFound("Balance not found");
            }

            var balanceResponse = new BalancesResponse(balance.Id, balance.CurrentAmount);

            return Ok(balanceResponse);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateBalance(Guid userId, decimal currentAmount)
        {
            await _balanceRepository.Update(userId, currentAmount);

            return Ok();
        }

    }
}
