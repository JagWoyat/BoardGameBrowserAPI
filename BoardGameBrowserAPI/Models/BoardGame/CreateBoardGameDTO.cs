using BoardGameBrowserAPI.Models.Category;
using BoardGameBrowserAPI.Models.Designer;

namespace BoardGameBrowserAPI.Models.BoardGame
{
    public class CreateBoardGameDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int Year { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int PlayingTime { get; set; }
        public List<CreateDesignerDTO> Designers { get; set; }
        public List<CreateCategoryDTO> Categories { get; set; }
    }
}
