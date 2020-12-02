using System;
using System.Collections.Generic;

#nullable disable

namespace mobileappAPI.Models
{
    public partial class Combustible
    {
        public Combustible()
        {
            Carros = new HashSet<Carro>();
        }

        public int Idcombustible { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Carro> Carros { get; set; }
    }
}
