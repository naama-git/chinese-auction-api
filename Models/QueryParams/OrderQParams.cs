using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;
using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.Models
{


    public class OrderQParams : FilterBase
    {

        public string? UserEmail { get; set; } = string.Empty;
        public List<int>? PackagesIds { get; set; } = new List<int>();
        public List<int>? PrizesIds { get; set; } = new List<int>();

        public Range<DateTime>? OrderDate { get; set; }

        [CompareTo("TotalPrice")]
        public Range<double>? TotalPrice { get; set; }
    }
}
