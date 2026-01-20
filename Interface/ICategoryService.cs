using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.CategotyDTO;



namespace ChineseAuctionAPI.Interface
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTOWithId>> GetAllCategory();

        public Task<IEnumerable<Category>> GetCategoriesByIds(List<int> ids);
        public Task AddCategory(CategoryCreateDTO category);
        public Task UpdateCategory(CategoryDTOWithId category);
        public Task DeleteCategory(int id);
    }
}
