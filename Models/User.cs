using System.ComponentModel; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace ChineseAuctionAPI.Models
{
    public class User
    {
        public int Id { get; set; }


        [DefaultValue("User"),Required(ErrorMessage ="Role is required")]
        public string Role { get; set; }="User";

        [MaxLength(100)]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Phone, MaxLength(15)]
        public string PhoneNumber { get; set; }


    }
}
