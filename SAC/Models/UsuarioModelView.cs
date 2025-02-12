using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entidad.Modelos;
using Negocio.Modelos;
namespace SAC.Models
{
    public class UsuarioModelView
    {
        public int? idUsuario { get; set; }

        [Display(Name = "Usuario: ")]
        [Required(ErrorMessage = "Ops!, complete el campo Usuario.")]      
        public string userName { get; set; }
       
        public string password { get; set; }
        public bool activo { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Actualizado { get; set; }
        public int idPersona { get; set; }
        [Display(Name = "Rol de Usuario: ")]
        [Required(ErrorMessage = "Ops!, complete el campo.")]
        public int idRol { get; set; }
        public int idUsuarioLogin { get; set; }      
        public PersonaModelView Persona { get; set; }
        public RolModelView Rol { get; set; }

        public List<RolModelView> Roles { get; set; }
    }
}