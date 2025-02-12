using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class CookieUsuarioViewModel
    {            
        public string ckUsuario { get; set; }
       
        public List<MenuSideBarModel> ckMenu { get; set; }

    }
}