using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletAspNetCore.Models.DTO.Requests;
using WalletAspNetCore.Models.DTO.Responses;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCategoryRequest createCategoryRequest) 
        {
            var categoryId = await _categoriesService.CreateCategoryAsync(createCategoryRequest.Name, createCategoryRequest.IsIncome);
            return Ok(categoryId);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _categoriesService.GetCategoryByIdAsync(id);
            var categoryResponse = new CategoriesResponse(category.Id, category.Name);

            return Ok(categoryResponse);
        }

        [HttpGet]
        [Route("selected")]
        public async Task<IActionResult> GetCategories(bool selectedKey)
        {
            var categories = await _categoriesService.GetSelectedCategoriesAsync(selectedKey);

            return Ok(categories);
        }
    }
}
