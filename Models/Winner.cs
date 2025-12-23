namespace ChineseAuctionAPI.Models
{
    public class Winner
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PrizeId { get; set;}
        public Prize Prize { get; set;}
    }
}
