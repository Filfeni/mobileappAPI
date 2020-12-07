using System;
using System.Collections.Generic;

#nullable disable

namespace mobileappAPI.Models
{
    public partial class Post
    {
        public int Idpost { get; set; }
        public int Idcarro { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Direccion { get; set; }

        public virtual Carro IdcarroNavigation { get; set; }
    }
}
