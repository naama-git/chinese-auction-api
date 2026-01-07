using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using  ChineseAuctionAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using static ChineseAuctionAPI.DTO.CategotyDTO;


namespace ChineseAuctionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryCreateDTO> _createValidator;
        private readonly IValidator<CategoryDTOWithId> _updateValidator;
        public CategoryController(ICategoryService categoryService, IValidator<CategoryCreateDTO> createValidator, IValidator<CategoryDTOWithId> updateValidator   )
        {
            _categoryService = categoryService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
           
            var categories = await _categoryService.GetAllCategory();
            return Ok(categories);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(CategoryDTOWithId category)
        {
            var validationResult = await _updateValidator.ValidateAsync(category);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await _categoryService.UpdateCategory(category);
            return Ok();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory(CategoryCreateDTO category)
        {
            var validationResult = await _createValidator.ValidateAsync(category);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
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
