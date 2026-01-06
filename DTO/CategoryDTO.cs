namespace ChineseAuctionAPI.DTO
{
    public class CategoryDTO
    {
        public class CategoriesDTO{
            
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class UpdateCategory
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
