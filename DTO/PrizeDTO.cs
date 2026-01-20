using ChineseAuctionAPI.Models;
using System.ComponentModel.DataAnnotations;
using ChineseAuctionAPI.DTO;
using static ChineseAuctionAPI.DTO.CategotyDTO;

namespace ChineseAuctionAPI.DTO
{
    public class CreatePrizeDTO
    {

        [MaxLength(100),Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }


        [Required(ErrorMessage = "Donor ID is required")]
        public int DonorId { get; set; }


        [Required(ErrorMessage = "Category ID is required")]
        public int CategoryId { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 50)]
        public int Qty { get; set; }

    }

    public class ReadPrizeDTO
    {

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DonorForReadPrizesDTO Donor { get; set; }

        public CategoryDTOWithId Category { get; set; }

        public string ImagePath { get; set; }

        [Range(0, 50)]
        public int Qty { get; set; }
        public int NumOfTickets { get; set; } = 0;
    }

    public class UpdatePrizeDTO
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int DonorId { get; set; }

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

        [MaxLength(500)]
        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImagePath { get; set; }
    }
    

    public class ReadSimplePrizeDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required"), MaxLength(100)]
        public string Name { get; set; }
        public string Category{ get; set; }

        public string ImagePath { get; set; }

        
        
    }
    public class PrizeForWinnerDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required"), MaxLength(100)]
        public string Name { get; set; }
        
        //donor

        public string CategoryName { get; set; }

        public string ImagePath { get; set; }
    }
    

}


