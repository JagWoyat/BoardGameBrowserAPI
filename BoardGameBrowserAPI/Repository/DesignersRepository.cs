using AutoMapper;
using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.Designer;
using Microsoft.EntityFrameworkCore;

namespace BoardGameBrowserAPI.Repository
{
    public class DesignersRepository : GenericRepository<Designer>, IDesignersRepository
    {
        private readonly BoardGameBrowserDbContext _context;
        private readonly IMapper _mapper;

        public DesignersRepository(BoardGameBrowserDbContext context, IMapper mapper) : base(context)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<Designer> GetDesignerAsync(int id)
        {
            return await _context.Designers.Include(d => d.BoardGames).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<GetDesignerListDTO>> GetDesignersAsync()
        {
            var designers = await _context.Designers.Include(d => d.BoardGames).ToListAsync();
            var designersDTO = _mapper.Map<List<GetDesignerListDTO>>(designers);
            foreach (var designer in designers)
            {
                var boardGameCount = designer.BoardGames.Count;
                designersDTO.Where(d => d.Id == designer.Id).First().BoardGameCount = boardGameCount;
            }

            return designersDTO;
        }
    }
}
