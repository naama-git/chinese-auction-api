using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.UserDTO;
using static ChineseAuctionAPI.DTO.CategoryDTO;


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
            //Category
            CreateMap<Category, CategoriesDTO>();
            CreateMap<CategoriesDTO, Category>();
            CreateMap<UpdateCategory, Category>();

            //Prizes
            CreateMap<Prize, ReadPrizeDTO>();
            CreateMap<CreatePrizeDTO,Prize>();
            CreateMap<UpdatePrizeDTO, Prize>();
            CreateMap<Prize, ReadPrizeForDonorsDTO>();

        }
    }
}
