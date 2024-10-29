using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.Api.DTO.Requests;
using WalletAspNetCore.Api.DTO.Responses;
using WalletAspNetCore.DataBaseOperations.EFStructures;
using WalletAspNetCore.DataBaseOperations.Repositories;

namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly CategoryRepository _categoryRepository;

        public CategoriesController(ApplicationDbContext dbContext, CategoryRepository categoryRepository)
        {
            _dbContext = dbContext;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest createCategoryRequest) 
        { 
            var category = await _categoryRepository.Create(createCategoryRequest.Name, createCategoryRequest.IsIncome);
            return Ok(category.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _categoryRepository.GetById(id);
            var categoryResponse = new CategoriesResponse(category.Id, category.Name);

            return Ok(categoryResponse);
        }
    }
}
