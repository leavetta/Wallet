using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.Api.DTO.Requests;
using WalletAspNetCore.Api.DTO.Responses;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.Models.Entities;
using WalletAspNetCore.Services;

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
        [Route("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest registerUserRequest, 
            UsersService usersService)
        {
            Balance balance =  await _balanceRepository.Create();
            await usersService.Register(balance, registerUserRequest.Name, registerUserRequest.Email, registerUserRequest.Password);
            
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserRequest loginUserRequest, 
            UsersService usersService)
        {
            var token = await usersService.Login(loginUserRequest.Email, loginUserRequest.Password);
            HttpContext.Response.Cookies.Append("secretCookie", token);
            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            var userResponse = new UsersResponse(user.Id, user.Name, user.Email, user.Password);

            return Ok(userResponse);
        }

    }
}
