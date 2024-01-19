namespace BoardGameBrowserAPI.Data
{
    public class BoardGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int Year { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set;}
        public int PlayingTime { get; set; }
        public List<Designer> Designers { get; set; }
        public List<Category> Categories { get; set; }
    }
}
