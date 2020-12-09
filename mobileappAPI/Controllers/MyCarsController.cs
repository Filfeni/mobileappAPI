using System;
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
        /*  /mycars		        GET *
            /mycars/	        POST *
            /mycars/:id	        PUT *
            /mycars/:id	        DELETE *
            /mycars/:id/post    GET *
            /mycars/:id/post    POST *
            /mycars/:id/post    PUT *
            /mycars/:id/post    DELETE *
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
        public async Task<ActionResult> RegisterCar(Carro car)
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
            int userid = _context.Users.Single(u => u.UserName == User.Identity.Name).Usuario.Idusuario;
            var myCar = await _context.Carros.SingleAsync(x => x.Idpropietario == userid && x.Idcarro == id);

            if (myCar == null)
            {
                return NotFound();
            }

            return myCar;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMyCar(int id, Carro car)
        {
            int userid = _context.Users.Single(u => u.UserName == User.Identity.Name).Usuario.Idusuario;
            if (userid != car.Idpropietario || id != car.Idcarro)
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

            return Ok(new Response() { Status = "Success", Message = "This car was updated!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyCar(int id)
        {
            if (!MyCarExists(id))
                return NotFound();

            var car = await _context.Carros.FindAsync(id);
            int userid = _context.Users.Single(u => u.UserName == User.Identity.Name).Usuario.Idusuario;

            if (userid != car.Idpropietario)
                return BadRequest();

            try
            {
                _context.Carros.Remove(car);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok(new Response { Status = "Succeded", Message = "This Car was deleted" });

        }

        [HttpGet("{id}/post")]
        public async Task<ActionResult<Post>> GetCarPost(int id)
        {
            if (MyCarExists(id) && PostExists(id))
            {
                return await _context.Posts.SingleAsync(x => x.Idcarro == id);
            }

            return NotFound(new Response() { Status = "Error", Message = "This car is not registered or hasn't been posted yet" });
        }

        [HttpPost("{id}/post")]
        public async Task<ActionResult> PostCarPost(Post post)
        {
            if (!MyCarExists(post.Idcarro) || PostExists(post.Idcarro))
                return NotFound(new Response() { Status = "Error", Message = "This car already has a post or hasn't been registered yet" });
            
            try
            {
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(new Response() { Status = "Error", Message = "This post could not be created, please try again" });
            }
            return Ok(new Response { Status = "Succeded", Message = "The post was successfully created" });
        }

        [HttpPut("{id}/post")]
        public async Task<IActionResult> UpdatePost(int id, Post post)
        {
            if (id != post.Idcarro)
            {
                return BadRequest();
            }
            _context.Entry(post).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyCarExists(id) || !PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Response() { Status = "Success", Message = "This car was updated!" });
        }


        [HttpDelete("{id}/post")]
        public async Task<IActionResult> DeleteCarPost(int id) //Test
        {
            if (!MyCarExists(id) || !PostExists(id))
                return NotFound();
            
            var post = await _context.Posts.Include(x => x.IdcarroNavigation).SingleAsync(x => x.Idcarro == id);
            int userid = _context.Users.Single(u => u.UserName == User.Identity.Name).Usuario.Idusuario;

            if (userid != post.IdcarroNavigation.Idpropietario)
                return BadRequest();

            try
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok(new Response { Status = "Succeded", Message = "This Car was deleted" });

        }

        [HttpGet("posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetMyPosts()
        {
            int userid = _context.Users.Single(u => u.UserName == User.Identity.Name).Usuario.Idusuario;
            var myCars = await _context.Posts.Include(x => x.IdcarroNavigation)
                .Where(x => x.IdcarroNavigation.Idpropietario == userid).ToListAsync();
            if (myCars == null)
            {
                return NotFound();
            }
            return myCars;
        }
        public bool MyCarExists(int id)
        {
            return _context.Carros.Any(c => c.Idcarro == id);
        }
        public bool PostExists(int id)
        {
            return _context.Posts.Any(x => x.Idcarro == id);
        }
    }
}