namespace ChineseAuctionAPI.DTO
{
   
    public class CategotyDTO
    {
        public class CategoryCreateDTO
        {
            public string Name { get; set; }
        }
        public class CategoryDTOWithId
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

}