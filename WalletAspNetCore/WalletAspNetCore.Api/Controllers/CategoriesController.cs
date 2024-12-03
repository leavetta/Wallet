using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.Api.DTO.Requests;
using WalletAspNetCore.Api.DTO.Responses;
using WalletAspNetCore.Services;

namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesController(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCategoryRequest createCategoryRequest) 
        {
            var categoryId = await _categoriesService.CreateCategory(createCategoryRequest.Name, createCategoryRequest.IsIncome);
            return Ok(categoryId);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _categoriesService.GetCategoryById(id);
            var categoryResponse = new CategoriesResponse(category.Id, category.Name);

            return Ok(categoryResponse);
        }

        [HttpGet]
        [Route("selected")]
        public async Task<IActionResult> GetCategories(bool selectedKey)
        {
            var categories = await _categoriesService.GetSelectedCategories(selectedKey);

            return Ok(categories);
        }
    }
}
