using System.ComponentModel.DataAnnotations;
using System.ComponentModel; 

namespace ChineseAuctionAPI.Models
{
    public class Package
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Range(0, 999), Required(ErrorMessage ="Number of classic tickets is required"), DefaultValue(0)]
        public int NumOfClassicTickets { get; set; }

        [Range(0, 999), Required(ErrorMessage ="Number of premium tickets is required"), DefaultValue(0)]
        public int NumberOfPremiumTickets { get;  set; }


        [Range(0, 99999999999999), Required(ErrorMessage ="Price is required")]
        public int Price { get; set; }
    }
}
