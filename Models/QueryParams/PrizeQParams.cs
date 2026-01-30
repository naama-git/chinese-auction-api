using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;
using AutoMapper.Configuration.Annotations;

namespace ChineseAuctionAPI.Models.QueryParams
{
    public class PrizeQParams : FilterBase
    {
        [StringFilterOptions(StringFilterOption.Contains)]
        public string? Name { get; set; }
        public List<int>? CategoriesIds { get; set; } 
        public int? DonorId { get; set; }

        [Ignore]
        public int? NumOfTickets {  get; set; }




    }
}
