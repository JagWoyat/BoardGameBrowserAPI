using BoardGameBrowserAPI.Models.BoardGame;

namespace BoardGameBrowserAPI.Models.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetBoardGameDTO> BoardGames { get; set; }
    }
}
