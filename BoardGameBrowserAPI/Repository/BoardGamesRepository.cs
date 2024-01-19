using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace BoardGameBrowserAPI.Repository
{
    public class BoardGamesRepository : GenericRepository<BoardGame>, IBoardGamesRepository
    {
        private readonly BoardGameBrowserDbContext _context;
        public BoardGamesRepository(BoardGameBrowserDbContext context) : base(context)
        {
            this._context = context;
        }


        public async Task DeleteBoardGame(int id)
        {
            var entity = await _context.BoardGames.Include(g => g.Designers).Include(g => g.Categories).FirstOrDefaultAsync(g => g.Id == id);

            foreach (var child in entity.Designers.ToList())
            {
                var count = child.BoardGames.Count;

                if (count == 1)
                {
                    _context.Designers.Remove(child);
                }
            }
            foreach (var child in entity.Categories.ToList())
            {
                var count = child.BoardGames.Count;

                if (count == 1)
                {
                    _context.Categories.Remove(child);
                }
            }

            _context.BoardGames.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<BoardGame> GetBoardGameAsync(int id)
        {
            return await _context.BoardGames.Include(g => g.Designers).Include(g => g.Categories).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<BoardGame>> GetBoardGamesAsync()
        {
            return await _context.BoardGames.Include(g => g.Designers).Include(g => g.Categories).ToListAsync();
        }

    }
}
