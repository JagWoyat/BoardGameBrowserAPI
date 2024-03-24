using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.Category;

namespace BoardGameBrowserAPI.Contracts
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Task<List<GetCategoryListDTO>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Task<List<CategoriesFilteredDTO>> GetSearchCategoriesAsync(string term);
        Task<List<CategoriesFilteredDTO>> GetFilteredCategoriesAsync(string term);
    }
}
