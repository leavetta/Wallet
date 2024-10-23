using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories;
using Microsoft.EntityFrameworkCore;
using WalletAspNetCore.Api.DTO;


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
        public async Task<ActionResult<BalancesResponse>> GetById(Guid id)
        {
            var balance = await _balanceRepository.GetByUserId(id);
            if (balance == null)
            {
                return NotFound("Balance not found");
            }

            var balanceResponse = new BalancesResponse(balance.Id, balance.CurrentAmount);

            return Ok(balanceResponse);
        }




    }
}
