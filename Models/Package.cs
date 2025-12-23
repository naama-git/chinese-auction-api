using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfClassicTickets { get; set; }
        public int NumberOfPremiumTickets { get;  set; }
        [Range(0, 99999999999999)]
        public int Price { get; set; }
    }
}
