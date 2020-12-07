using Microsoft.AspNetCore.Identity;
using mobileappAPI.Authentication;
using mobileappAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileappAPI.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public Usuario Usuario { get; set; }
    }
}
