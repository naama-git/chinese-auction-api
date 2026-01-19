using ChineseAuctionAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChineseAuctionAPI.DTO
{
    public class DonorReadDTO
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }
        public string? Company { get; set; }
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public List<ReadPrizeForDonorsDTO> Prizes { get; set; }
    }
    public class DonorCreateDTO
    {

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }
        public string? Company { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class DonorUpdateDTO

    {
        [JsonPropertyName("id")]
        [Required (ErrorMessage ="id is required")]
        public int Id { get; set; }

        [MaxLength(100)]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("company")]
        public string? Company { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [Phone]
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public class DonorForReadPrizesDTO
    {
   
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }
        public string? Company { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }





}
