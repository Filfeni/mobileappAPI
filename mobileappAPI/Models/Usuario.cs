using System;
using System.Collections.Generic;

#nullable disable

namespace mobileappAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Carros = new HashSet<Carro>();
            Reservacions = new HashSet<Reservacion>();
        }

        public int Idusuario { get; set; }
        public int Idautenticacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Autenticacion IdautenticacionNavigation { get; set; }
        public virtual ICollection<Carro> Carros { get; set; }
        public virtual ICollection<Reservacion> Reservacions { get; set; }
    }
}
