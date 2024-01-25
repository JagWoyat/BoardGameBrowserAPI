﻿using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.BoardGame;
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



        public async Task DeleteOrphanedChildren()
        {
            foreach (var child in _context.Designers.Include(c => c.BoardGames).ToList())
            {
                var count = child.BoardGames.Count;

                if (count == 0)
                {
                    _context.Designers.Remove(child);
                }
            }
            foreach (var child in _context.Categories.Include(c => c.BoardGames).ToList())
            {
                var count = child.BoardGames.Count;

                if (count == 0)
                {
                    _context.Categories.Remove(child);
                }
            }
            await _context.SaveChangesAsync();
        }

        public BoardGame FilterExistingElements(BoardGame boardGame)
        {
            var boardGameExists = _context.BoardGames.Where(b => b.Name == boardGame.Name).Any();

            if (boardGameExists)
            {
                return null;
            }

            //var newBoardGame = boardGame;
            
            //newBoardGame.Categories.Clear();
            var newCategories = new List<Category>();
            foreach (var category in boardGame.Categories)
            {
                var categoryExists = _context.Categories.Where(c => c.Name == category.Name).FirstOrDefault();
                if (categoryExists != null)
                {
                    newCategories.Add(categoryExists);
                }else
                {
                    newCategories.Add(category);
                }
            }
            boardGame.Categories = newCategories;

            //newBoardGame.Designers.Clear();
            var newDesigners  = new List<Designer>();
            foreach (var designer in boardGame.Designers)
            {
                var designerExists = _context.Designers.Where(c => c.Name == designer.Name).FirstOrDefault(); ;
                if (designerExists != null)
                {
                    newDesigners.Add(designerExists);
                }else
                {
                    newDesigners.Add(designer);
                }
            }
            boardGame.Designers = newDesigners;

            return boardGame;
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
