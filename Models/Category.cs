
using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        public List<Prize> Prizes { get; set; } = new List<Prize>();
    }
}
