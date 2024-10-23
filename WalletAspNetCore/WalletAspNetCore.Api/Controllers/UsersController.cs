using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.Api.DTO;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserRepository _userRepository;
        private readonly BalanceRepository _balanceRepository;

        public UsersController(ApplicationDbContext dbContext, UserRepository userRepository, BalanceRepository balanceRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _balanceRepository = balanceRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> RegisterUser(string name, string email, string password)
        {
            Balance balance =  await _balanceRepository.Create();

            User user = await _userRepository.Create(balance, name, email, password);
            

            return Ok(user.Id);
        }

        [HttpGet]
        public async Task<ActionResult<UsersResponse>> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            var userResponse = new UsersResponse(user.Id, user.Name, user.Email, user.Password);

            return Ok(userResponse);
        }

    }
}
