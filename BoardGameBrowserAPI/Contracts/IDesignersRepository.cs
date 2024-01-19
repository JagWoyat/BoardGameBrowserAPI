using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.Designer;

namespace BoardGameBrowserAPI.Contracts
{
    public interface IDesignersRepository : IGenericRepository<Designer>
    {
        Task<List<GetDesignerListDTO>> GetDesignersAsync();
        Task<Designer> GetDesignerAsync(int id);
    }
}
