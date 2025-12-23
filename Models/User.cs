using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }


    }
}
