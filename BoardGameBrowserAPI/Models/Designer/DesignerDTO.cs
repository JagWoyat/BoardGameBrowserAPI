using BoardGameBrowserAPI.Models.BoardGame;

namespace BoardGameBrowserAPI.Models.Designer
{
    public class DesignerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetBoardGameDTO> BoardGames { get; set; }
    }
}
