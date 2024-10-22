using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.DataBaseOperations.EFStructures;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WalletAspNetCore.Api.Controllers
{
    public class BalancesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BalancesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> UpdateBalance()
        {

        }

        // GET: api/<BalancesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BalancesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BalancesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BalancesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BalancesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
