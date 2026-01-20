using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.Models
{
    public class Prize
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required"), MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required"), MaxLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Donor ID is required")]
        public int DonorId { get; set; }
        public Donor Donor { get; set; }
        
        public List<Category> Categories {get;set;}=new List<Category>();

        public string ImagePath {  get; set; }

        [Required(ErrorMessage = "Quantity is required"), Range(1, 50)]
        public int Qty { get; set; }

        public List<Order> Orders { get; set; }

        public List<Ticket> Tickets { get; set; }

         


    }
}
