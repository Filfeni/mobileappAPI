using System;
using System.Collections.Generic;

#nullable disable

namespace mobileappAPI.Models
{
    public partial class TipoReservacion
    {
        public TipoReservacion()
        {
            Reservacions = new HashSet<Reservacion>();
        }

        public int IdtipoReservacion { get; set; }
        public string Alcance { get; set; }

        public virtual ICollection<Reservacion> Reservacions { get; set; }
    }
}
