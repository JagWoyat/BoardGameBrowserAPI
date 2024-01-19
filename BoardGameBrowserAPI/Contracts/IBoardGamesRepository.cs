using BoardGameBrowserAPI.Data;

namespace BoardGameBrowserAPI.Contracts
{
    public interface IBoardGamesRepository : IGenericRepository<BoardGame>
    {
        Task<List<BoardGame>> GetBoardGamesAsync();
        Task<BoardGame> GetBoardGameAsync(int id);

        Task DeleteBoardGame(int id);

    }
}
