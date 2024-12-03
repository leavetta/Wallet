using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WalletAspNetCore.Api.DTO.Requests;
using WalletAspNetCore.Api.DTO.Responses;
using WalletAspNetCore.Auth;
using WalletAspNetCore.Services;

namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UsersService _userService;
        private readonly JwtParser _jwtParser;

        public UsersController(AuthService authService, UsersService userService, JwtParser jwtParser)
        {
            _jwtParser = jwtParser;
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUserRequest)
        {
            var userId = await _authService.Register(registerUserRequest.Name, registerUserRequest.Email, registerUserRequest.Password);
            
            return Ok(userId);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUserRequest)
        {
            var token = await _authService.Login(loginUserRequest.Email, loginUserRequest.Password);
            //HttpContext.Response.Cookies.Append("secretCookie", token.Item1);
            
            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetById()
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var user = await _userService.GetUserById(userId);
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
