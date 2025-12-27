using static ChineseAuctionAPI.DTO.CategoryDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoriesDTO>> GetAllCategory();
        public Task AddCategory(CategoriesDTO category);
        public Task UpdateCategory(UpdateCategory category);
        public Task DeleteCategory(int id);
    }
}
