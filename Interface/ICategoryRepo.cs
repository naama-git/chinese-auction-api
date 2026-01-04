using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Interface
{
    public interface ICategoryRepo
    {
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task AddCategory(Category category);
        public Task UpdateCategory(Category category);
        public Task DeleteCategory(int id);

    }
}
