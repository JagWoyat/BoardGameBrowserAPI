using BoardGameBrowserAPI.Models.Category;
using BoardGameBrowserAPI.Models.Designer;

namespace BoardGameBrowserAPI.Models.BoardGame
{
    public class BoardGamesFilteredDTO : BoardGameDTO
    {
        public int FilterValue { get; set; }
    }
}
