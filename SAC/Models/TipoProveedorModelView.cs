using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class TipoProveedorModelView
    {
      
        public int Id { get; set; }

        [Display(Name = "Tipo de proveedor")]
        [Required]
        [StringLength(50, ErrorMessage = "La longitud máxima es 50")]
        public string Descripcion { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        [Display(Name = "Codigo")]
        [Required]
        [StringLength(50, ErrorMessage = "La longitud máxima es 50")]
        public string Codigo { get; set; }
    }
}