using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.Designer;

namespace BoardGameBrowserAPI.Contracts
{
    public interface IDesignersRepository : IGenericRepository<Designer>
    {
        Task<List<GetDesignerListDTO>> GetDesignersAsync();
        Task<Designer> GetDesignerAsync(int id);
        Task<List<DesignersFilteredDTO>> GetSearchDesignersAsync(string term);
        Task<List<DesignersFilteredDTO>> GetFilteredDesignersAsync(string term);
    }
}
