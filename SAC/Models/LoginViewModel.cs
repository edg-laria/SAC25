using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class LoginViewModel
    {
     
        [Required]
        [EmailAddress]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordame")]
        public bool Recordarme { get; set; }

        public string Email { get; set; }

        public string TelefonoAdministrador { get; set; }
    }
}