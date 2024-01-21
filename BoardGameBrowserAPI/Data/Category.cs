namespace BoardGameBrowserAPI.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BoardGame> BoardGames { get; set; } = new List<BoardGame>() { };
    }
}
