using AutoMapper;
using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Data;
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
    }
}
