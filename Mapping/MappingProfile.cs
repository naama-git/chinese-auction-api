using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.UserDTO;
using static ChineseAuctionAPI.DTO.TicketDTO;
using static ChineseAuctionAPI.DTO.PackageDTO;
using static ChineseAuctionAPI.DTO.WinnerDTO;
using static ChineseAuctionAPI.DTO.CartDTO;
using static ChineseAuctionAPI.DTO.CategotyDTO;


namespace ChineseAuctionAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Donors
            CreateMap<Donor, DonorReadDTO>();
            CreateMap<DonorCreateDTO, Donor>();
            CreateMap<DonorUpdateDTO, Donor>();
            CreateMap<Donor, DonorForReadPrizesDTO>();

            //User
            CreateMap<SignInDTO, User>();
            CreateMap<LogInDTO, User>();
            //CreateMap<ResponseUserDTO, User>();
            CreateMap<User, ReadUserDTO>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            //Category
            CreateMap<Category, CategoryDTOWithId>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>();


            //Prizes
            CreateMap<Prize, ReadPrizeDTO>()
                .ForMember(dest => dest.NumOfTickets,
               opt => opt.MapFrom(src => src.Tickets.Count));
            CreateMap<CreatePrizeDTO, Prize>();
            CreateMap<UpdatePrizeDTO, Prize>();
            CreateMap<Prize, ReadPrizeForDonorsDTO>();

            //Tickets
            CreateMap<TicketCreateDTO, Ticket>();
            CreateMap<TicketReadDTO, Ticket>();
            CreateMap<Ticket, TicketReadDTO>();
            CreateMap<User, ResponseUserDTO>();


            //Package
            CreateMap<Package, ReadPackageDTO>();
            CreateMap<CreatePackageDTO, Package>();
            CreateMap<UpdatePackageDTO, Package>();


            //Winner
            CreateMap<CreateWinnerDTO, Winner>();
            CreateMap<Winner, ReadWinnerDTO>();
            CreateMap<User, ResponseUserDTO>();
            CreateMap<Prize, PrizeForWinnerDTO>();
            CreateMap<Winner, ReadWinnerInPrizeDTO>();


            //Order
            CreateMap<Order, ReadOrderDTO>();
            CreateMap<Order,ReadSimpleOrderDTO>();

            //Cart
            CreateMap<addCartDTO, Cart>();
            CreateMap<Cart, ReadCartDTO>();
            CreateMap<CartItem, CartItemReadDTO>();
            CreateMap<User, ResponseUserDTO>();
            CreateMap<Prize, ReadSimplePrizeDTO>().ReverseMap();

        }
    }
}
