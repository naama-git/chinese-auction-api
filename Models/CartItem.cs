namespace ChineseAuctionAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; } 
        public int CartId { get; set; }
        public int PrizeId { get; set; }
        public Prize Prize { get; set; }
        public int Quantity { get; set; }
    }
}
