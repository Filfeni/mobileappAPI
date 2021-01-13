using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mobileappAPI.Authentication;
using mobileappAPI.Models;

namespace mobileappAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AuthContext _context;

        public ReservationsController(AuthContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservacion>>> GetReservacions()
        {
            var userid = GetUserId();

            var reservations = await _context.Reservacions.Include(x => x.IdcarroNavigation)
                                                          .Where(x => x.Idcliente == userid)
                                                          .ToListAsync();
            if (reservations != null)
                return reservations;

            return NotFound();
        }

        [HttpGet("{id}/reservation-dates")]
        public async Task<ActionResult<IEnumerable<DateTime>>> GetReservationDates(int? id)
        {
            if (!CarExists(id))
            {
                return NotFound();
            }
            List<DateTime> dates = new List<DateTime>();
            await _context.Reservacions.Where(x => x.Idcarro == id).ForEachAsync(x =>
            {
                dates.AddRange(EachCalendarDay(x.FechaSalida,x.FechaEntrega));  
            });

            return dates;
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservacion>> GetReservacion(int? id)
        {
            var reservacion = await _context.Reservacions.FindAsync(id);

            if (reservacion == null)
            {
                return NotFound();
            }

            return reservacion;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservacion(int? id, Reservacion reservacion)
        {
            if (id != reservacion.Idreservacion)
            {
                return BadRequest();
            }

            _context.Entry(reservacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/Reservations
        //To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostReservacion(Reservacion reservacion)
        {
            try
            {
                _context.Reservacions.Add(reservacion);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(new Response() { Status = "Error", Message = "This car could not be reserved successfully" });
            }
            return Ok(new Response() { Status = "Success", Message = "This car was successfully reserved" });
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservacion>> DeleteReservacion(int? id)
        {
            var reservacion = await _context.Reservacions.FindAsync(id);
            if (reservacion == null)
            {
                return NotFound();
            }

            _context.Reservacions.Remove(reservacion);
            await _context.SaveChangesAsync();

            return reservacion;
        }


        private bool ReservacionExists(int? id)
        {
            return _context.Reservacions.Any(e => e.Idreservacion == id);
        }

        public bool CarExists(int? id)
        {
            return _context.Carros.Any(x => x.Idcarro == id);
        }

        public int? GetUserId()
        {
            return _context.Users.Include(x => x.Usuario).Single(u => u.NormalizedUserName == User.Identity.Name.ToUpper()).Usuario.Idusuario;
        }

        public IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
        }
    }
}
