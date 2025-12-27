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
            //User
            CreateMap<SignInDTO, User>();
            CreateMap<LogInDTO, User>();
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 345624fd9c498db560c9f029a11f6cd9b9014b63
            //Category
            CreateMap<Category, CategoriesDTO>();
            CreateMap<CategoriesDTO, Category>();
            CreateMap<UpdateCategory, Category>();
<<<<<<< HEAD
=======

=======
>>>>>>> 345624fd9c498db560c9f029a11f6cd9b9014b63
            //Prizes
            CreateMap<Prize, ReadPrizeDTO>();
            CreateMap<CreatePrizeDTO,Prize>();
            CreateMap<UpdatePrizeDTO, Prize>();



<<<<<<< HEAD
>>>>>>> 5152a63e98f732a86c30ebcb4a424031a389883c
=======
>>>>>>> 345624fd9c498db560c9f029a11f6cd9b9014b63
        }
    }
}
