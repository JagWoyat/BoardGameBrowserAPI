using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.Category;
using BoardGameBrowserAPI.Models.Designer;
using System.ComponentModel.DataAnnotations;

namespace BoardGameBrowserAPI.Models.BoardGame
{
    public class BoardGameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int Year { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int PlayingTime { get; set; }
        public List<GetDesignerDTO> Designers { get; set; }
        public List<GetCategoryDTO> Categories { get; set; }
    }
}
