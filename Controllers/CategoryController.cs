using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionAPI.DTO.CategoryDTO;
using Microsoft.AspNetCore.Authorization;


namespace ChineseAuctionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
           
            var categories = await _categoryService.GetAllCategory();
            return Ok(categories);
        }
        [HttpPut]
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(UpdateCategory category)
        {
            await _categoryService.UpdateCategory(category);
            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory(CategoriesDTO category)
        {
            await _categoryService.AddCategory(category);
            return Ok(201);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
