using AutoMapper;
using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.BoardGame;
using BoardGameBrowserAPI.Models.Category;
using BoardGameBrowserAPI.Models.Designer;
using Microsoft.EntityFrameworkCore;

namespace BoardGameBrowserAPI.Repository
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly BoardGameBrowserDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesRepository(BoardGameBrowserDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<GetCategoryListDTO>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.Include(d => d.BoardGames).ToListAsync();
            var categoriesDTO = _mapper.Map<List<GetCategoryListDTO>>(categories);
            foreach (var category in categories)
            {
                var boardGameCount = category.BoardGames.Count;
                categoriesDTO.Where(c => c.Id == category.Id).First().BoardGameCount = boardGameCount;
            }

            return categoriesDTO;
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.Categories.Include(c => c.BoardGames).FirstOrDefaultAsync(g => g.Id == id);
            
        }

        public async Task<List<CategoriesFilteredDTO>> GetFilteredCategoriesAsync(string term)
        {
            var startsWith = await _context.Categories.Where(c => c.Name.ToLower().StartsWith(term.ToLower())).Include(c => c.BoardGames).ToListAsync();
            var contains = await _context.Categories.Where(c => c.Name.ToLower().Contains(term.ToLower())).Include(c => c.BoardGames).ToListAsync();

            var startsWithF = _mapper.Map<List<CategoriesFilteredDTO>>(startsWith);
            foreach (var f in startsWithF)
            {
                var index = startsWithF.IndexOf(f);
                startsWithF[index].FilterValue = 1;
                var toRemove = contains.Single(bg => startsWithF[index].Id == bg.Id);
                contains.Remove(toRemove);
            }
            var containsF = _mapper.Map<List<CategoriesFilteredDTO>>(contains);
            foreach (var f in containsF)
            {
                var index = containsF.IndexOf(f);
                containsF[index].FilterValue = 10;
            }

            var results = startsWithF.Union(containsF).ToList();

            return results;
        }

        public async Task<List<CategoriesFilteredDTO>> GetSearchCategoriesAsync(string term)
        {
            var results = new List<CategoriesFilteredDTO>();
            var startsWith = await _context.Categories.Where(c => c.Name.ToLower().StartsWith(term.ToLower())).ToListAsync();
            if (startsWith.Count < 25)
            {
                var contains = await _context.Categories.Where(c => c.Name.ToLower().Contains(term.ToLower())).ToListAsync();
                var startsWithF = _mapper.Map<List<CategoriesFilteredDTO>>(startsWith);
                foreach (var f in startsWithF)
                {
                    var index = startsWithF.IndexOf(f);
                    startsWithF[index].FilterValue = 1;
                    var toRemove = contains.Single(bg => startsWithF[index].Id == bg.Id);
                    contains.Remove(toRemove);
                }
                var containsF = _mapper.Map<List<CategoriesFilteredDTO>>(contains);
                foreach (var f in containsF)
                {
                    var index = containsF.IndexOf(f);
                    containsF[index].FilterValue = 10;
                }
                results = startsWithF.Union(containsF).ToList();
            }
            else
            {
                results = _mapper.Map<List<CategoriesFilteredDTO>>(startsWith);
            }


            return results;
        }
    }
}
