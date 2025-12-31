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
                var newCart = new Cart { UserId = userId };
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
            await _repo.RemovePrizeFromCart(userId, prizeId);
        }

        public async Task addcart(addCartDTO _cartDto)
        {
            var cart = _mapper.Map<Cart>(_cartDto);
            await _repo.addcart(cart);
        }

        public async Task<ReadCartDTO> GetCartByUserId(int userId)
        {
            var cart = await _repo.GetCartByUserId(userId);
            return _mapper.Map<ReadCartDTO>(cart);
        }
    }
}