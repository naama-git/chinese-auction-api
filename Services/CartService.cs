using AutoMapper;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.CartDTO;

namespace ChineseAuctionAPI.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _repo;
        private readonly IMapper _mapper;

        public CartService(ICartRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddPrizeToCart(int userId, int prizeId, int quantity = 1)
        {
            var cart = await _repo.GetCartByUserId(userId);

            if (cart == null)
            {
                var newCart = new Cart { UserId = userId , CartItems = new List<CartItem>() };
                await _repo.addcart(newCart);
                cart = await _repo.GetCartByUserId(userId);
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
            await _repo.UpdateCart(cart);
        }


        public async Task RemovePrizeFromCart(int userId, int prizeId)
        {
            var cart = await _repo.GetCartByUserId(userId);

            if (cart != null)
            {
                var item = cart.CartItems.FirstOrDefault(ci => ci.PrizeId == prizeId);
                if (item != null)
                {
                    item.Quantity -= 1;
                    if (item.Quantity <= 0)
                    {
                        cart.CartItems.Remove(item);
                    }
                }

            }
            await _repo.UpdateCart(cart);
            
        }



        public async Task addcart(addCartDTO cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _repo.addcart(cart);
        }



        public async Task<ReadCartDTO> GetCartByUserId(int userId)
        {
            var cart = await _repo.GetCartByUserId(userId);
            return _mapper.Map<ReadCartDTO>(cart);
        }

        public async Task PurchaseCart(int userId)
        {
            var cart = await _repo.GetCartByUserId(userId);
            if (cart != null)
            {
                cart.CartItems.Clear();
                await _repo.UpdateCart(cart);
            }
        }
    }
}