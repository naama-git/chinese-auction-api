using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.CategoryDTO;

namespace ChineseAuctionAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoriesDTO>> GetAllCategory()
        {
            var categories = await _repo.GetAllCategory();
            return _mapper.Map<IEnumerable<CategoriesDTO>>(categories);
        }
        public async Task AddCategory(CategoriesDTO categoryName)
        {
            Category categoryEntity = _mapper.Map<Category>(categoryName);
            await _repo.AddCategory(categoryEntity);
        }
        public async Task UpdateCategory(UpdateCategory category)
        {
            Category categoryEntity = _mapper.Map<Category>(category);
            await _repo.UpdateCategory(categoryEntity);
        }
        public async Task DeleteCategory(int id)
        {
            await _repo.DeleteCategory(id);
        }
    }
}
