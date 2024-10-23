using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.Api.DTO;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories;
using WalletAspNetCore.Models.Entities;


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

        public TransactionsController(ApplicationDbContext dbContext, TransactionRepository transactionRepository, UserRepository userRepository, CategoryRepository categoryRepository)
        {
            _dbContext = dbContext;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(Guid userId, decimal amount, Guid categoryId)
        {
            var user = await _userRepository.GetById(userId);
            var category = await _categoryRepository.GetById(categoryId);

            var transaction = await _transactionRepository.Create(user, category, amount);

            return Ok(transaction.Id);
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionsResponse>>> GetAll(Guid id)
        {
            var transactions = await _transactionRepository.GetAll(id);
            var transactionsResponse = transactions.Select(t => new TransactionsResponse(t.Id, t.Amount, t.OperationDate, t.CategoryNavigation.Name));

            return Ok(transactionsResponse);
        }
    }
}
