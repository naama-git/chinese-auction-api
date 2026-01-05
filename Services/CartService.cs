using AutoMapper;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.CartDTO;

namespace ChineseAuctionAPI.Services
{
    public class CartService : ICartService
    {

        public const string Location = "CartService";
        private readonly ICartRepo _repo;
        private readonly IPrizeService _prizeService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CartService(ICartRepo repo, IMapper mapper, IPrizeService prizeService, IUserService userService)
        {
            _repo = repo;
            _mapper = mapper;
            _prizeService = prizeService;
            _userService = userService;
        }

        public async Task AddPrizeToCart(int userId, int prizeId, int quantity = 1)
        {

                var user= await _userService.GetUserById(userId);
                if (user == null){
                    throw new ErrorResponse(404, "AddPrizeToCart", "User not found", $"User with the provided ID {userId} does not exist.", "POST", Location);
                }
            var prize = await _prizeService.GetPrizeById(prizeId);
            if (prize == null){
                throw new ErrorResponse(404, "AddPrizeToCart", "Prize not found", $"Prize with the provided ID {prizeId} does not exist.", "POST", Location);
            }

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

            var user= await _userService.GetUserById(userId);
            if (user == null){
                throw new ErrorResponse(404, "RemovePrizeFromCart", "User not found", $"User with the provided ID {userId} does not exist.", "DELETE", Location);
            }
            var prize = await _prizeService.GetPrizeById(prizeId);
            if (prize == null){
                throw new ErrorResponse(404, "RemovePrizeFromCart", "Prize not found", $"Prize with the provided ID {prizeId} does not exist.", "DELETE", Location);
            }
            var cart = await _repo.GetCartByUserId(userId);

            if(cart == null)
            {
                throw new ErrorResponse(404, "RemovePrizeFromCart", "Cart not found", $"Cart for user with ID {userId} does not exist.", "DELETE", Location);
            }

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
            var user= await _userService.GetUserById(userId);
            if (user == null){
                throw new ErrorResponse(404, "GetCartByUserId", "User not found", $"User with the provided ID {userId} does not exist.", "GET", Location);
            }
            var cart = await _repo.GetCartByUserId(userId);
            return _mapper.Map<ReadCartDTO>(cart);
        }

        public async Task PurchaseCart(int userId)
        {
            var user= await _userService.GetUserById(userId);
            if (user == null){
                throw new ErrorResponse(404, "PurchaseCart", "User not found", $"User with the provided ID {userId} does not exist.", null, Location);
            }
            var cart = await _repo.GetCartByUserId(userId);
            if (cart != null)
            {
                cart.CartItems.Clear();
                await _repo.UpdateCart(cart);
            }
        }
    }
}