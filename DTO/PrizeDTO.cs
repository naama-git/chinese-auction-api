using ChineseAuctionAPI.Models;
using System.ComponentModel.DataAnnotations;
using static ChineseAuctionAPI.DTO.CategoryDTO;

namespace ChineseAuctionAPI.DTO
{
    public class CreatePrizeDTO
    {

        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }

        public int DonorId { get; set; }

        public bool IsPremium { get; set; }

        public int CategoryId { get; set; }

        public string ImagePath { get; set; }

        [Range(0, 50)]
        public int Qty { get; set; }

    }

    public class ReadPrizeDTO
    {

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }


        public DonorForReadPrizesDTO Donor { get; set; }

        public bool IsPremium { get; set; }
        public CategoriesDTO Category { get; set; }

        public string ImagePath { get; set; }

        [Range(0, 50)]
        public int Qty { get; set; }
    }

    public class UpdatePrizeDTO
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public int DonorId { get; set; }

        public bool IsPremium { get; set; }

        public int CategoryId { get; set; }

        public string ImagePath { get; set; }

        [Range(0, 50)]
        public int Qty { get; set; }

    }

    public class ReadPrizeForDonorsDTO
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }

        public bool IsPremium { get; set; }

        public string CategoryName { get; set; }

        public string ImagePath { get; set; }
    }
    public class PrizeForWinnerDTO
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsPremium { get; set; }

        public string CategoryName { get; set; }

        public string ImagePath { get; set; }

    }

}


