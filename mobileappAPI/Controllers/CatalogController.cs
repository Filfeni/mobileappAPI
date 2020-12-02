using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mobileappAPI.Authentication;
using mobileappAPI.Models;

namespace mobileappAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatalogController : ControllerBase
    {
        private readonly AuthContext _context;
        public CatalogController(AuthContext context)
        {
            _context = context;
        }
        //_context.Users.Single(u => u.UserName == User.Identity.Name).Usuario.Idusuario)
        // GET: api/Catalogo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carro>>> GetCatalog()
        {
            return await _context.Carros.ToListAsync();
        }

        // GET: api/Catalogo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> GetCatalogSpecific(int id)
        {
            var carro = await _context.Carros.FindAsync(id);

            if (carro == null)
            {
                return NotFound();
            }

            return carro;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Carro>>> SearchCatalog([FromQuery] string? filter, [FromQuery] string? marca)
        {
            if (filter == null && marca == null)
            {
                RedirectToAction("GetCarros");
            }
            return await _context.Carros.Where(c => c.Modelo.Contains("search") || c.IdmarcaNavigation.Marca1 == marca).ToListAsync();
        }
    }
}
