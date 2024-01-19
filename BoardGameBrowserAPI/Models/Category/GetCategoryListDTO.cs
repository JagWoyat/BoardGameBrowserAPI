namespace BoardGameBrowserAPI.Models.Category
{
    public class GetCategoryListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BoardGameCount { get; set; }
    }
}
