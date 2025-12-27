using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class CategoryRepository:ICategoryRepo
    {
        private readonly ChineseAuctionDBcontext _context;

        public CategoryRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }
        // get all categories
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _context.categories.ToListAsync();
        }
        // add new category
        public async Task AddCategory(Category category)
        {
            await _context.categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        // update category
        public async Task UpdateCategory(Category category)
        {
            _context.categories.Update(category);
            await _context.SaveChangesAsync();
        }
        // delete category
        public async Task DeleteCategory(int id )
        {
           _context.categories.Remove( await _context.categories.FindAsync(id));
            await _context.SaveChangesAsync();
        }
    }
}
