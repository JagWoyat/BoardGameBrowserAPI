using AutoMapper;
using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.Category;
using BoardGameBrowserAPI.Models.Designer;
using Microsoft.EntityFrameworkCore;

namespace BoardGameBrowserAPI.Repository
{
    public class DesignersRepository : GenericRepository<Designer>, IDesignersRepository
    {
        private readonly BoardGameBrowserDbContext _context;
        private readonly IMapper _mapper;

        public DesignersRepository(BoardGameBrowserDbContext context, IMapper mapper) : base(context, mapper)
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

        public async Task<List<DesignersFilteredDTO>> GetFilteredDesignersAsync(string term)
        {
            var startsWith = await _context.Categories.Where(d => d.Name.ToLower().StartsWith(term.ToLower())).Include(d => d.BoardGames).ToListAsync();
            var contains = await _context.Categories.Where(d => d.Name.ToLower().Contains(term.ToLower())).Include(d => d.BoardGames).ToListAsync();

            var startsWithF = _mapper.Map<List<DesignersFilteredDTO>>(startsWith);
            foreach (var f in startsWithF)
            {
                var index = startsWithF.IndexOf(f);
                startsWithF[index].FilterValue = 1;
                var toRemove = contains.Single(bg => startsWithF[index].Id == bg.Id);
                contains.Remove(toRemove);
            }
            var containsF = _mapper.Map<List<DesignersFilteredDTO>>(contains);
            foreach (var f in containsF)
            {
                var index = containsF.IndexOf(f);
                containsF[index].FilterValue = 10;
            }

            var results = startsWithF.Union(containsF).ToList();

            return results;
        }

        public async Task<List<DesignersFilteredDTO>> GetSearchDesignersAsync(string term)
        {
            var results = new List<DesignersFilteredDTO>();
            var startsWith = await _context.Designers.Where(d => d.Name.ToLower().StartsWith(term.ToLower())).ToListAsync();
            if (startsWith.Count < 25)
            {
                var contains = await _context.Designers.Where(d => d.Name.ToLower().Contains(term.ToLower())).ToListAsync();
                var startsWithF = _mapper.Map<List<DesignersFilteredDTO>>(startsWith);
                foreach (var f in startsWithF)
                {
                    var index = startsWithF.IndexOf(f);
                    startsWithF[index].FilterValue = 1;
                    var toRemove = contains.Single(bg => startsWithF[index].Id == bg.Id);
                    contains.Remove(toRemove);
                }
                var containsF = _mapper.Map<List<DesignersFilteredDTO>>(contains);
                foreach (var f in containsF)
                {
                    var index = containsF.IndexOf(f);
                    containsF[index].FilterValue = 10;
                }
                results = startsWithF.Union(containsF).ToList();
            }
            else
            {
                results = _mapper.Map<List<DesignersFilteredDTO>>(startsWith);
            }


            return results;
        }
    }
}
