using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.Models
{
    public class Prize
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; }

        public int DonorId { get; set; }
        public Donor Donor { get; set; }

        public bool IsPremium { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ImagePath {  get; set; }

        [Range(0,50)]
        public int Qty { get; set; }


    }
}
