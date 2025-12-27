using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.UserDTO;

namespace ChineseAuctionAPI.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //Donors
            CreateMap<Donor, DonorReadDTO>();
            CreateMap<DonorCreateDTO,Donor>();
            //User
            CreateMap<SignInDTO, User>();
            CreateMap<LogInDTO, User>();
        }
    }
}
