using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using WalletAspNetCore.Api.DTO.Requests;
using WalletAspNetCore.Api.DTO.Responses;
using WalletAspNetCore.Auth;
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
        private readonly JwtParser _jwtParser;

        public UsersController(ApplicationDbContext dbContext, UserRepository userRepository, BalanceRepository balanceRepository, JwtParser jwtParser)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _balanceRepository = balanceRepository;
            _jwtParser = jwtParser;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest registerUserRequest, 
            UsersService usersService)
        {
            Balance balance =  await _balanceRepository.Create();
            var userId = await usersService.Register(balance, registerUserRequest.Name, registerUserRequest.Email, registerUserRequest.Password);
            
            return Ok(userId);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserRequest loginUserRequest, 
            UsersService usersService)
        {
            var token = await usersService.Login(loginUserRequest.Email, loginUserRequest.Password);
            //HttpContext.Response.Cookies.Append("secretCookie", token.Item1);
            
            return Ok(token.Item1);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetById()
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var user = await _userRepository.GetById(userId);
                var userResponse = new UsersResponse(user.Id, user.Name, user.Email, user.Password);

                return Ok(userResponse);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }

    }
}
