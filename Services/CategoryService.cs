using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using static ChineseAuctionAPI.DTO.CategotyDTO;


namespace ChineseAuctionAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private const string Location = "CategoryService";

        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDTOWithId>> GetAllCategory()
        {
            var categories = await _repo.GetAllCategories();
            if (categories == null) { 
                 return Enumerable.Empty<CategoryDTOWithId>();
            }
            return _mapper.Map<IEnumerable<CategoryDTOWithId>>(categories);
        }
        public async Task AddCategory(CategoryCreateDTO categoryName)
        {

            Category categoryEntity = _mapper.Map<Category>(categoryName);
            await _repo.AddCategory(categoryEntity);
        }
        public async Task UpdateCategory(CategoryDTOWithId category)
        {
            var categories= await _repo.GetAllCategories();
            bool exist = categories.Any(c => c.Id == category.Id);
            if (!exist)
            {
                throw new ErrorResponse(404, "UpdateCategory", "Category not found.", $"Update failed: Category ID {category.Id} does not exist.", "PUT", Location);
            }

            Category categoryEntity = _mapper.Map<Category>(category);
            await _repo.UpdateCategory(categoryEntity);
        }

        public async Task DeleteCategory(int id)
        {
            var categories = await _repo.GetAllCategories();
            bool exist = categories.Any(c => c.Id == id);
            if (!exist)
            {
                throw new ErrorResponse(404, "UpdateCategory", "Category not found.", $"Update failed: Category ID {id} does not exist.", "PUT", Location);
            }
            await _repo.DeleteCategory(id);
        }
    }
}
