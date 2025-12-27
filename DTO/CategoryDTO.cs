namespace ChineseAuctionAPI.DTO
{
    public class CategoryDTO
    {
        public class CategoriesDTO{
            public string Name { get; set; }
        }
        public class UpdateCategory
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
