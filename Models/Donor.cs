using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.Models


{
    public class Donor
    {
        public int Id { get; set; }

        [MaxLength(100),Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [MaxLength(100), Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string? Company { get; set; }

        public string Address { get; set; }

        [EmailAddress, Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public List<Prize> Prizes { get; set; } = new List<Prize>();
    }
}
