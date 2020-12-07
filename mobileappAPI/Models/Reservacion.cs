using System;
using System.Collections.Generic;

#nullable disable

namespace mobileappAPI.Models
{
    public partial class Reservacion
    {
        public int? Idreservacion { get; set; }
        public int Idcarro { get; set; }
        public int Idcliente { get; set; }
        public int IdtipoReservacion { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaEntrega { get; set; }

        public virtual Carro IdcarroNavigation { get; set; }
        public virtual Usuario IdclienteNavigation { get; set; }
        public virtual TipoReservacion IdtipoReservacionNavigation { get; set; }
    }
}
