using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class CategoryRepository : ICategoryRepo
    {
        private readonly ChineseAuctionDBcontext _context;
        private const string RepoLocation = "CategoryRepository";
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

                throw new ErrorResponse(500, "GetAllCategories", "Internal Server Error", "Couldn't get categories", "Get", RepoLocation);
            }

            return categories;
        }

        // get categories by Id's
        public async Task<IEnumerable<Category>> GetCategoriesByIds(List<int> ids)
        {
            var categories = await _context.categories
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();

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
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "AddCategory", "Internal Server Error", ex.Message, "POST", RepoLocation);
            }

        }

        // update category
        public async Task UpdateCategory(Category category)
        {
            var categoryInDb = await _context.categories.FindAsync(category.Id);
            if (categoryInDb == null)
            {
                throw new ErrorResponse(404, "UpdateCategory", "Category not found", "Couldn't find category", "PUT", RepoLocation);
            }
            try
            {
                _context.Entry(categoryInDb).CurrentValues.SetValues(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "UpdateCategory", "Internal Server Error", ex.Message, "PUT", RepoLocation);
            }
        }

        // delete category
        public async Task DeleteCategory(int id)
        {
            var category = await _context.categories.FindAsync(id);
            if (category == null)
            {
                throw new ErrorResponse(404, "DeleteCategory", "Category not found", "Couldn't find category", "DELETE", RepoLocation);
            }

            try
            {
                _context.categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "DeleteCategory", "Internal Server Error", ex.Message, "DELETE", RepoLocation);
            }

        }
    }
}
