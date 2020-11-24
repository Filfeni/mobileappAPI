using System;
using System.Collections.Generic;

#nullable disable

namespace mobileappAPI.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Carros = new HashSet<Carro>();
        }

        public int Idmarca { get; set; }
        public string Marca1 { get; set; }

        public virtual ICollection<Carro> Carros { get; set; }
    }
}
