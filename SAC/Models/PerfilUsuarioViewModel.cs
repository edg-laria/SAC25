using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class PerfilUsuarioViewModel
    {
        public UsuarioModelView Usuario { get; set; }
        public ResetPasswordViewModel ResetPassword { get; set; }
    }

}