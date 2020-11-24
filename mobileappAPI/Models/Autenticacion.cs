using System;
using System.Collections.Generic;

#nullable disable

namespace mobileappAPI.Models
{
    public partial class Autenticacion
    {
        public int Idautenticacion { get; set; }
        public string NombreUsuario { get; set; }
        public string HashedPassword { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
