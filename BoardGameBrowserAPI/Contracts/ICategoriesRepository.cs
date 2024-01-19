using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.Category;

namespace BoardGameBrowserAPI.Contracts
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Task<List<GetCategoryListDTO>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
    }
}
