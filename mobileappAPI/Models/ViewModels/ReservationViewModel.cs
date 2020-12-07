using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileappAPI.Models.ViewModels
{
    public class ReservationViewModel
    {
        public int Idreservacion { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Modelo { get; set; }
        public string Alcance { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
