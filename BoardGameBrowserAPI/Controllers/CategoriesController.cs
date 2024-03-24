using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Models.Category;
using BoardGameBrowserAPI.Models.BoardGame;
using BoardGameBrowserAPI.Repository;
using AutoMapper;
using Microsoft.AspNetCore.OData.Query;

namespace BoardGameBrowserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _context;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesRepository context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        [EnableQuery(PageSize = 25)]
        public async Task<ActionResult<IEnumerable<GetCategoryListDTO>>> GetCategories()
        {
            return await _context.GetCategoriesAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _context.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return categoryDTO;
        }

        [HttpGet("Search:{term}")]
        [EnableQuery(PageSize = 5)]
        public async Task<ActionResult<IEnumerable<CategoriesFilteredDTO>>> GetCategoriesSearch(string term)
        {
            var categories = await _context.GetSearchCategoriesAsync(term);

            return categories;
        }

        [HttpGet("Filter:{term}")]
        [EnableQuery(PageSize = 25)]
        public async Task<ActionResult<IEnumerable<CategoriesFilteredDTO>>> GetCategoriesFiltered(string term)
        {
            var categories = await _context.GetFilteredCategoriesAsync(term);

            return categories;
        }

    }
}
