using static ChineseAuctionAPI.DTO.CategoryDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTOWithId>> GetAllCategory();
        public Task AddCategory(CategoryCreateDTO category);
        public Task UpdateCategory(CategoryDTOWithId category);
        public Task DeleteCategory(int id);
    }
}
