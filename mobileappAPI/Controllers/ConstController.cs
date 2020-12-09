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
    public class ConstController : ControllerBase
    {
        private readonly AuthContext _context;

        public ConstController(AuthContext context)
        {
            _context = context;
        }

        // GET: api/Const
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
            return await _context.Marcas.ToListAsync();
        }

        // GET: api/Const/5
        [HttpGet("brands/{id}")]
        public async Task<ActionResult<Marca>> GetMarca(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            return marca;
        }

        // GET: api/Const
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Categorium>>> GetCategorias()
        {
            return await _context.Categoria.ToListAsync();
        }

        // GET: api/Const/5
        [HttpGet("categories/{id}")]
        public async Task<ActionResult<Categorium>> GetCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // GET: api/Const
        [HttpGet("fueltype")]
        public async Task<ActionResult<IEnumerable<Combustible>>> GetCombustibles()
        {
            return await _context.Combustibles.ToListAsync();
        }

        // GET: api/Const/5
        [HttpGet("fueltype/{id}")]
        public async Task<ActionResult<Combustible>> GetCombustible(int id)
        {
            var combustible = await _context.Combustibles.FindAsync(id);

            if (combustible == null)
            {
                return NotFound();
            }

            return combustible;
        }

        // GET: api/Const
        [HttpGet("reservationtype")]
        public async Task<ActionResult<IEnumerable<TipoReservacion>>> GetTipoReservaciones()
        {
            return await _context.TipoReservacions.ToListAsync();
        }

        // GET: api/Const/5
        [HttpGet("reservationtype/{id}")]
        public async Task<ActionResult<TipoReservacion>> GetTipoReservacion(int id)
        {
            var tipoReservacion = await _context.TipoReservacions.FindAsync(id);

            if (tipoReservacion == null)
            {
                return NotFound();
            }

            return tipoReservacion;
        }


    }
}
