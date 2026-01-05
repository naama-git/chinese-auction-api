using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
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
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = await _context.categories.ToListAsync();

            if (categories == null || !categories.Any())
                {
                
                    throw new ErrorResponse(500, "GetAllCategories", "Internal Server Error", "Couldn't get categories" , null, "CategoryRepository");
                }

            return categories;
        }

        // add new category
        public async Task AddCategory(Category category)
        {
            try
            {
                await _context.categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new ErrorResponse(500, "AddCategory", "Internal Server Error", "Couldn't add category", null, "CategoryRepository");
            }
           
        }

        // update category
        public async Task UpdateCategory(Category category)
        {
            try
            {
                _context.categories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "UpdateCategory", "Internal Server Error", "Couldn't update category", null, "CategoryRepository");
            }
        }
        
        // delete category
        public async Task DeleteCategory(int id )
        {
            var category = await _context.categories.FindAsync(id);
            if (category == null)
            {
                throw new ErrorResponse(404, "DeleteCategory", "Category not found", "Couldn't find category",  null, "CategoryRepository");
            }

            try
            {
                _context.categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new ErrorResponse(500, "DeleteCategory", "Internal Server Error", "Couldn't delete category", null, "CategoryRepository");
            }

        }
    }
}
