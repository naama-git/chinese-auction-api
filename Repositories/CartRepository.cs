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

    public async Task RemovePrizeFromCart(int userId, int prizeId)
    {
        var cart = await _context.carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart != null)
        {
            var item = cart.CartItems.FirstOrDefault(ci => ci.PrizeId == prizeId);
            if (item != null)
            {
                item.Quantity--;
                await _context.SaveChangesAsync();
            }
        }
    }
    public async Task AddPrizeToCart(int userId, int prizeId, int quantity = 1)
    {
        var cart = await GetCartByUserId(userId);

        if (cart == null)
        {
            var newCart = new Cart { UserId = userId };
            await addcart(newCart);
            cart = await GetCartByUserId(userId);
        }

        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.PrizeId == prizeId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                CartId = cart.Id,
                PrizeId = prizeId,
                Quantity = quantity
            });
        }

        await UpdateCart(cart);
    }
}