using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace mobileappAPI.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        public string Celular { get; set; }
    }
}
