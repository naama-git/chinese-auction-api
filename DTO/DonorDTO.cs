using ChineseAuctionAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.DTO
{
    public class DonorReadDTO
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public List<Prize> Prizes { get; set; }
    }
    public class DonorCreateDTO
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
  

}
