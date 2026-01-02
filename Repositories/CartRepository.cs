using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

public class CartRepository : ICartRepo
{
    private readonly ChineseAuctionDBcontext _context;

    public CartRepository(ChineseAuctionDBcontext context)
    {
        _context = context;
    }


    public async Task<Cart> GetCartByUserId(int userId)
    {
        return await _context.carts
            .Include(c => c.CartItems) 
                .ThenInclude(ci => ci.Prize)
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }


    public async Task addcart(Cart _cart)
    {
        await _context.carts.AddAsync(_cart);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateCart(Cart cart)
    {
        _context.carts.Update(cart);
        await _context.SaveChangesAsync();
    }

    
}