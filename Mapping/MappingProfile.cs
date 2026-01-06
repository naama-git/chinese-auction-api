using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.UserDTO;
using static ChineseAuctionAPI.DTO.CategoryDTO;
using static ChineseAuctionAPI.DTO.TicketDTO;
using static ChineseAuctionAPI.DTO.PackageDTO;
using static ChineseAuctionAPI.DTO.WinnerDTO;
using static ChineseAuctionAPI.DTO.CartDTO;


namespace ChineseAuctionAPI.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //Donors
            CreateMap<Donor, DonorReadDTO>();
            CreateMap<DonorCreateDTO,Donor>();
            CreateMap<DonorUpdateDTO, Donor>();
            CreateMap<Donor, DonorForReadPrizesDTO>();

            //User
            CreateMap<SignInDTO, User>();
            CreateMap<LogInDTO, User>();
            //CreateMap<ResponseUserDTO, User>();
            CreateMap<User, ReadUserDTO>();

            //Category
            CreateMap<Category, CategoryDTOWithId>();
            CreateMap<CategoryCreateDTO, Category>();
            

            //Prizes
            CreateMap<Prize, ReadPrizeDTO>();
            CreateMap<CreatePrizeDTO,Prize>();
            CreateMap<UpdatePrizeDTO, Prize>();
            CreateMap<Prize, ReadPrizeForDonorsDTO>();

            //Tickets
            CreateMap<TicketCreateDTO, Ticket >();
            CreateMap<TicketReadDTO,Ticket>();
            CreateMap<Ticket, TicketReadDTO>();
            CreateMap<User,ResponseUserDTO>();
            CreateMap<Prize, ReadPrizeDTO>();

            //Package
            CreateMap<Package,ReadPackageDTO>().ReverseMap();
            CreateMap<CreatePackageDTO,Package>();

            //Winner
            CreateMap<CreateWinnerDTO,Winner>();
            CreateMap<Winner, ReadWinnerDTO>();
            CreateMap<User, ResponseUserDTO>();
            CreateMap<Prize, PrizeForWinnerDTO>();


            //Order
            CreateMap<Order,ReadOrderDTO>();

            //Cart
            CreateMap<addCartDTO, Cart>();
            CreateMap<Cart, ReadCartDTO>();
            CreateMap<CartItem, CartItemReadDTO>();
            CreateMap<User, ResponseUserDTO>();
            CreateMap<Prize, ReadSimplePrizeDTO>().ReverseMap();

        }
    }
}
