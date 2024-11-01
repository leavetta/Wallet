using Microsoft.AspNetCore.Authorization;
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
    
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TransactionRepository _transactionRepository;
        private readonly UserRepository _userRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly BalanceRepository _balanceRepository;
        private readonly TransactionService _transactionService;

        public TransactionsController(ApplicationDbContext dbContext, 
            TransactionRepository transactionRepository, 
            UserRepository userRepository, 
            CategoryRepository categoryRepository,
            BalanceRepository balanceRepository,
            TransactionService transactionService)
        {
            _dbContext = dbContext;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _balanceRepository = balanceRepository;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionRequest transactionRequest)
        {
            var user = await _userRepository.GetById(transactionRequest.UserId);
            var category = await _categoryRepository.GetById(transactionRequest.CategoryId);

            var transaction = await _transactionRepository.Create(user, category, transactionRequest.Amount);

            await _balanceRepository.ApplyTransaction(user, transaction);

            return Ok(transaction.Id);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var transactions = await _transactionRepository.GetAll(id);
            var transactionsResponse = transactions.Select(t => new TransactionResponse(t.Id, t.Amount, t.OperationDate.ToString("dd.MM.yyyy hh:mm:ss"), t.CategoryNavigation.Name));

            return Ok(transactionsResponse);
        }

        [HttpGet]
        [Route("selected")]
        public async Task<IActionResult> GetReportSelectedKindTransactions(Guid id, bool selectedKey)
        {
            Dictionary<Category, decimal> categoriesAmount = new Dictionary<Category, decimal>();

            var categories = await _categoryRepository.GetSelectedCategories(id, selectedKey);

            foreach (var category in categories)
            {
                decimal amount = Math.Abs(category.Transactions.Sum(t => t.Amount));
                categoriesAmount[category] = amount;
            }

            return Ok(categoriesAmount);
        }
    }
}
