using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;

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
        }
    }
}
