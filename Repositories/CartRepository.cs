using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

public class CartRepository : ICartRepo
{
    private readonly ChineseAuctionDBcontext _context;
    private const string RepoLocation = "CartRepository";
    public CartRepository(ChineseAuctionDBcontext context)
    {
        _context = context;
    }


    public async Task<Cart> GetCartByUserId(int userId)
    {
        try
        {
            var cart = await _context.carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Prize)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            //if (cart == null)
            //{
            //    throw new ErrorResponse(404, "GetCartByUserId", "Cart not found.", $"No cart was found for User ID {userId}.", "GET", RepoLocation);
            //}

            return cart;
        }
        catch (ErrorResponse) { throw; } 
        catch (Exception ex)
        {
            throw new ErrorResponse(500, "GetCartByUserId", "An error occurred while fetching the cart.", ex.Message, "GET", RepoLocation);
        }
    }


    public async Task addcart(Cart _cart)
    {
        try
        {
            await _context.carts.AddAsync(_cart);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ErrorResponse(500, "addcart", "Failed to create the cart.", ex.Message, "POST", RepoLocation);
        }
    }


    public async Task UpdateCart(Cart cart)
    {
        try
        {
            _context.carts.Update(cart);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ErrorResponse(500, "UpdateCart", "Failed to update the cart in the database.", ex.Message, "PUT", RepoLocation);
        }
    }

    
}