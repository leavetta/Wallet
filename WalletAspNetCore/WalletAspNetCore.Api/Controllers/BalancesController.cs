using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using Microsoft.EntityFrameworkCore;


namespace WalletAspNetCore.Api.Controllers
{
    public class BalancesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BalancesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateBalance(Guid id)
        //{
        //    var balance = await _dbContext.Balances.FirstOrDefaultAsync(b => b.Id == id);
        //    if (balance == null)
        //    {
        //        return NotFound("Balance not found");
        //    }


        //}

        
    }
}
