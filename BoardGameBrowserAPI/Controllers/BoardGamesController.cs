﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardGameBrowserAPI.Data;
using AutoMapper;
using BoardGameBrowserAPI.Models.BoardGame;
using BoardGameBrowserAPI.Models;
using BoardGameBrowserAPI.Models.Category;
using BoardGameBrowserAPI.Models.Designer;
using System.Diagnostics.Metrics;
using BoardGameBrowserAPI.Contracts;

namespace BoardGameBrowserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly IBoardGamesRepository _boardGamesRepository;
        private readonly IMapper _mapper;

        public BoardGamesController(IBoardGamesRepository boardGamesRepository, IMapper mapper)
        {
            _boardGamesRepository = boardGamesRepository;
            this._mapper = mapper;
        }

        // GET: api/BoardGames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardGameDTO>>> GetBoardGames()
        {
            //var boardGames = await _context.BoardGames.Include(g => g.Designers).Include(g => g.Categories).ToListAsync();

            var boardGames = await _boardGamesRepository.GetBoardGamesAsync();

            //var boardGamesDTO = boardGames.Select(g => new BoardGameDTO
            //{
            //    Id = g.Id,
            //    Name = g.Name,
            //    Description = g.Description,
            //    Rating = g.Rating,
            //    MinPlayers = g.MinPlayers,
            //    MaxPlayers = g.MaxPlayers,
            //    PlayingTime = g.PlayingTime,
            //    Categories = g.Categories.Select(c => new GetCategoryDTO { Id = c.Id, Name = c.Name }).ToList(),
            //    Designers = g.Designers.Select(d => new GetDesignerDTO { Id = d.Id, Name = d.Name }).ToList(),
            //}).ToList();

            var boardGamesDTO = _mapper.Map<List<BoardGameDTO>>(boardGames);

            return boardGamesDTO;
        }

        // GET: api/BoardGames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardGameDTO>> GetBoardGame(int id)
        {
            var boardGame = await _boardGamesRepository.GetBoardGameAsync(id);

            if (boardGame == null)
            {
                return NotFound();
            }

            var boardGameDTO = _mapper.Map<BoardGameDTO>(boardGame);

            //var boardGameDTO = new BoardGameDTO
            //{
            //    Id = boardGame.Id,
            //    Name = boardGame.Name,
            //    Description = boardGame.Description,
            //   Rating = boardGame.Rating,
            //    MinPlayers = boardGame.MinPlayers,
            //    MaxPlayers = boardGame.MaxPlayers,
            //    PlayingTime = boardGame.PlayingTime,
            //    Categories = boardGame.Categories.Select(c => new CategoryDTO { Id = c.Id, Name = c.Name }).ToList(),
            //    Designers = boardGame.Designers.Select(d => new DesignerDTO { Id = d.Id, Name = d.Name }).ToList(),
            //};

            return boardGameDTO;
        }

        // PUT: api/BoardGames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoardGame(int id, BoardGameDTO boardGameDTO)
        {
            var boardGame = await _boardGamesRepository.GetAsync(id);
            if (boardGame == null)
            {
                return NotFound();
            }

            _mapper.Map(boardGameDTO, boardGame);

            try
            {
                await _boardGamesRepository.UpdateAsync(boardGame);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BoardGameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BoardGames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoardGameDTO>> PostBoardGame(CreateBoardGameDTO boardGameDTO)
        {
            var boardGame = _mapper.Map<BoardGame>(boardGameDTO);

            var filteredBoardGame = _boardGamesRepository.FilterExistingElements(boardGame);
            if(filteredBoardGame == null)
            {
                return NoContent();
            }

            //await _context.BoardGames.Add(boardGame);
            await _boardGamesRepository.AddAsync(filteredBoardGame);

            return CreatedAtAction("GetBoardGame", new { id = filteredBoardGame.Id }, boardGameDTO);
        }

        // DELETE: api/BoardGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoardGame(int id)
        {
            var boardGame = await _boardGamesRepository.GetAsync(id);
            if (boardGame == null)
            {
                return NotFound();
            }

            await _boardGamesRepository.DeleteAsync(id);
            await _boardGamesRepository.DeleteOrphanedChildren();

            return NoContent();
        }

        private async Task<bool> BoardGameExists(int id)
        {
            return await _boardGamesRepository.Exists(id);
        }
    }
}
