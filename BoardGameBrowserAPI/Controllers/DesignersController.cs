using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardGameBrowserAPI.Data;
using AutoMapper;
using BoardGameBrowserAPI.Contracts;
using BoardGameBrowserAPI.Models.Designer;
using BoardGameBrowserAPI.Models.Category;
using Microsoft.AspNetCore.OData.Query;

namespace BoardGameBrowserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignersController : ControllerBase
    {
        private readonly IDesignersRepository _context;
        private readonly IMapper _mapper;

        public DesignersController(IDesignersRepository context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Designers
        [HttpGet]
        [EnableQuery(PageSize = 25)]
        public async Task<ActionResult<IEnumerable<GetDesignerListDTO>>> GetDesigners()
        {
            return await _context.GetDesignersAsync();
        }

        // GET: api/Designers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DesignerDTO>> GetDesigner(int id)
        {
            var designer = await _context.GetDesignerAsync(id);

            if (designer == null)
            {
                return NotFound();
            }

            var designerDTO = _mapper.Map<DesignerDTO>(designer);

            return designerDTO;
        }

        [HttpGet("Search:{term}")]
        [EnableQuery(PageSize = 5)]
        public async Task<ActionResult<IEnumerable<DesignersFilteredDTO>>> GetDesignersSearch(string term)
        {
            var designers = await _context.GetSearchDesignersAsync(term);

            return designers;
        }

        [HttpGet("Filter:{term}")]
        [EnableQuery(PageSize = 25)]
        public async Task<ActionResult<IEnumerable<DesignersFilteredDTO>>> GetDesignersFiltered(string term)
        {
            var designers = await _context.GetFilteredDesignersAsync(term);

            return designers;
        }

    }
}
