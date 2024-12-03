using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.Api.DTO.Requests;
using WalletAspNetCore.Api.DTO.Responses;
using WalletAspNetCore.Services;
using Microsoft.Net.Http.Headers;
using WalletAspNetCore.Auth;


namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        private readonly JwtParser _jwtParser;

        public TransactionsController(
            TransactionService transactionService,
            JwtParser jwtParser)
        {
            _transactionService = transactionService;
            _jwtParser = jwtParser;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionRequest transactionRequest)
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var transactionId = await _transactionService.CreateAsync(userId, transactionRequest.CategoryId, transactionRequest.Amount);

                return Ok(transactionId);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var transactions = await _transactionService.GetTransactionsAsync(userId, startDate, endDate);
                var transactionsResponse = transactions.Select(t => new TransactionResponse(t.Id, t.Amount, t.OperationDate.ToString("dd.MM.yyyy hh:mm:ss"), t.CategoryNavigation.Name));

                return Ok(transactionsResponse);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }

        [HttpGet]
        [Route("selected")]
        public async Task<IActionResult> GetReportSelectedKindTransactions(bool selectedKey)
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var categoriesAmount = await _transactionService.GetReportSelectedKindTransactionsAsync(userId, selectedKey);

                return Ok(categoriesAmount);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }
    }
}
