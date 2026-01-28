using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;

namespace ChineseAuctionAPI.Models.QueryParams
{
    public class DonorQParams : FilterBase

    {
        [StringFilterOptions(StringFilterOption.Contains)]
        public string? Email { get; set; } = string.Empty;

        [StringFilterOptions(StringFilterOption.Contains)]
        public string? Name { get; set; } = string.Empty;

        public List<int>? PrizesIds { get; set; } = new List<int>();

        
    }
}
