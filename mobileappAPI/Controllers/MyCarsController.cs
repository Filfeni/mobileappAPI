using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mobileappAPI.Authentication;
using mobileappAPI.Models;

namespace mobileappAPI.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    [Authorize]
    public class MyCarsController : ControllerBase
    {
        /*  /mycars		        GET
            /mycars/	        POST
            /mycars/:id	        PATCH
            /mycars/:id	        DELETE
            /mycars/:id/post    GET
            /mycars/:id/post    POST
            /mycars/:id/post    PUT
            /mycars/:id/post    DELETE
        */
        private readonly AuthContext _context;
        public MyCarsController(AuthContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carro>>> GetMyCars()
        {
            int userid = _context.Users.Single(u => u.UserName == User.Identity.Name).Usuario.Idusuario;
            var myCars = await _context.Carros.Where(x => x.Idpropietario == userid).ToListAsync();
            if (myCars == null)
            {
                return NotFound();
            }
            return myCars;        
        }

        [HttpPost]
        public async Task<ObjectResult> RegisterCar(Carro car)
        {
            try
            {
                _context.Carros.Add(car);
                await _context.SaveChangesAsync();
                
            }
            catch (System.Exception)
            {

                return BadRequest(new Response() { Status = "Error", Message = "This car could not be created, please try again" });
            }
            return Ok(new Response() { Status = "Success", Message = "This car was registered!" });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> GetMyCarsSpecific(int id)
        {
            var carro = await _context.Carros.FindAsync(id);

            if (carro == null)
            {
                return NotFound();
            }

            return carro;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMyCar(int id, Carro car)
        {
            int userid = _context.Users.Single(u => u.UserName == User.Identity.Name).Usuario.Idusuario;
            if (userid != car.Idpropietario)
            {
                return BadRequest();
            }
            if (id != car.Idcarro)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyCarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok( new Response(){Status = "Success", Message = "This car was updated!"});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyCar(int id)
        {
            if (!MyCarExists(id))
            {
                return NotFound();
            }

            var car = await _context.Carros.FindAsync();
            int userid = _context.Users.Single(u => u.UserName == User.Identity.Name).Usuario.Idusuario;
            
            if (userid != car.Idpropietario)
            {
                return BadRequest();
            }
            
            
            try
            {
                _context.Carros.Remove(car);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok( new Response{ Status = "Succeded", Message = "This Car was deleted"});

        }

        public bool MyCarExists(int id)
        {
            return _context.Carros.Any(c => c.Idcarro == id);
        }
        public bool BrandExists(string marca)
        {
            return _context.Marcas.Any(b => b.Marca1.ToLower() == marca.ToLower());
        }
    }
}