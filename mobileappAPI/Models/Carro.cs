using System;
using System.Collections.Generic;

#nullable disable

namespace mobileappAPI.Models
{
    public partial class Carro
    {
        public Carro()
        {
            Reservacions = new HashSet<Reservacion>();
        }

        public int Idcarro { get; set; }
        public int Idcategoria { get; set; }
        public int Idmarca { get; set; }
        public int Idpropietario { get; set; }
        public int Idcombustible { get; set; }
        public string Modelo { get; set; }
        public string Año { get; set; }
        public string Color { get; set; }
        public string Placa { get; set; }
        public bool Transmision { get; set; }
        public bool TienePost { get; set; }

        public virtual Categorium IdcategoriaNavigation { get; set; }
        public virtual Combustible IdcombustibleNavigation { get; set; }
        public virtual Marca IdmarcaNavigation { get; set; }
        public virtual Usuario IdpropietarioNavigation { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Reservacion> Reservacions { get; set; }
    }
}
