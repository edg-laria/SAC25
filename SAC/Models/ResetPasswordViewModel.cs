using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
    using System.ComponentModel.DataAnnotations;
namespace SAC.Models
{


    public class ResetPasswordViewModel
    {
        public int idUsuario { get; set; } // ID del usuario que restablecerá su contraseña

        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Debe confirmar la nueva contraseña.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

}