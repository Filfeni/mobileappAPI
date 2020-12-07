using System;
using System.Collections.Generic;

#nullable disable

namespace mobileappAPI.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Carros = new HashSet<Carro>();
        }

        public int Idcategoria { get; set; }
        public string NombreCategoria { get; set; }

        public virtual ICollection<Carro> Carros { get; set; }
    }
}
