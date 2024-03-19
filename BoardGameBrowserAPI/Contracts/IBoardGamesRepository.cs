﻿using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.BoardGame;

namespace BoardGameBrowserAPI.Contracts
{
    public interface IBoardGamesRepository : IGenericRepository<BoardGame>
    {
        Task<List<BoardGame>> GetBoardGamesAsync();
        Task<List<BoardGame>> GetSearchBoardGamesAsync(string term);
        Task<List<BoardGame>> GetFilteredBoardGamesAsync(string term);
        Task<BoardGame> GetBoardGameAsync(int id);
        BoardGame FilterExistingElements(BoardGame boardGame);
        Task DeleteOrphanedChildren();

    }
}
