using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mobileappAPI.Models;
using mobileappAPI.Services;

namespace mobileappAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservacionService _reservacionService;

        public ReservationController(IReservacionService reservacionService)
        {
            _reservacionService = reservacionService;
        }

        [HttpGet("[action]/IDcarro")]
        public IActionResult GetReservations(int IDcarro)
        {
            try
            {
                var reservations = _reservacionService.GetReservationByCarID(IDcarro);
                return Ok(reservations);
            }
            catch(ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("[action]")]
        public IActionResult CreateReservation([FromBody] Reservacion reservacion)
        {
            try
            {
                _reservacionService.CreateReservation(reservacion);
                return Ok();
            }catch(ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{modelo}")]
        public IActionResult SearchReservations(string modelo)
        {
            try
            {
                var reservations = _reservacionService.SearchReservations(modelo);
                return Ok(reservations);
            }catch(ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
