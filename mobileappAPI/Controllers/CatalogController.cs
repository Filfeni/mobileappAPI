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
        public async Task<ActionResult<IEnumerable<Post>>> GetCatalog()
        {
            ApplicationUser user = await _context.Users.Include(x => x.Usuario).SingleAsync(u => u.NormalizedUserName == User.Identity.Name.ToUpper());
            int userid = user.Usuario.Idusuario;
            return await _context.Posts.Include(x => x.IdcarroNavigation)
                                       .Where(x => x.IdcarroNavigation.Idpropietario != userid)
                                       .ToListAsync();
        }

        // GET: api/Catalogo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetCatalogSpecific(int id)
        {
            var posts = await _context.Posts.SingleAsync(x => x.Idcarro == id);

            if (posts == null)
            {
                return NotFound();
            }

            return posts;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Post>>> SearchCatalog([FromQuery] string? search)
        {
            ApplicationUser user = await _context.Users.Include(x => x.Usuario).SingleAsync(u => u.NormalizedUserName == User.Identity.Name.ToUpper());
            int userid = user.Usuario.Idusuario;
            if (string.IsNullOrEmpty(search))
                return await _context.Posts.Include(x => x.IdcarroNavigation)
                                           .Where(x => x.IdcarroNavigation.Idpropietario != userid)
                                           .ToListAsync();

            return await _context.Posts.Include(p => p.IdcarroNavigation)
                                       .Include(p => p.IdcarroNavigation.IdmarcaNavigation)
                                       .Where(c => (c.IdcarroNavigation.Modelo.Contains(search) || c.IdcarroNavigation.IdmarcaNavigation.Marca1.Contains(search) || c.Descripcion.Contains(search)) && c.IdcarroNavigation.Idpropietario != userid)
                                       .ToListAsync();
        }
        
        //[HttpGet("search")]
        //public async Task<ActionResult<IEnumerable<Post>>> SearchCatalog([FromQuery] string? search, [FromQuery] string? marca)
        //{
        //    int? userid = _context.Users.Include(x => x.Usuario).Single(u => u.NormalizedUserName == User.Identity.Name.ToUpper()).Usuario.Idusuario;
        //    if (search == null && marca == null)
        //        return RedirectToAction("GetCatalog");
            
        //    if (search == null && marca != null)
        //    {
        //        return await _context.Posts.Include(p => p.IdcarroNavigation)
        //        .Where(c => c.IdcarroNavigation.IdmarcaNavigation.Marca1 == marca && c.IdcarroNavigation.Idpropietario != userid)?
        //        .ToListAsync();
        //    }
        //    if (search != null && marca == null)
        //    {
        //        return await _context.Posts.Include(p => p.IdcarroNavigation)
        //        .Where(c => (c.IdcarroNavigation.Modelo.Contains(search) || c.IdcarroNavigation.IdmarcaNavigation.Marca1 == marca) && c.IdcarroNavigation.Idpropietario != userid)
        //        .ToListAsync();
        //    }
        //    return await _context.Posts.Include(p => p.IdcarroNavigation)
        //        .Where(c => (c.IdcarroNavigation.Modelo.Contains(search) || c.IdcarroNavigation.IdmarcaNavigation.Marca1 == marca) && c.IdcarroNavigation.Idpropietario != userid)
        //        .ToListAsync();
        //}

        //[HttpGet("search")]
        //public async Task<ActionResult<IEnumerable<Post>>> AlternateSearchCatalog([FromQuery] string? search, [FromQuery] string? marca)
        //{
        //     var result = await _context.Posts.Include(p => p.IdcarroNavigation)
        //    .Where(c => c.IdcarroNavigation.IdmarcaNavigation.Marca1 == marca)?
        //    .Concat(_context.Posts.Include(p => p.IdcarroNavigation)
        //    .Where(c => c.IdcarroNavigation.Modelo.Contains(search)))?
        //    .ToListAsync();
            
        //    if (result != null)
        //        return result;

        //    return NotFound();
        //}

    }
}
