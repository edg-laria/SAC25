using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;

namespace SAC.Models
{
    public class PaisModelView
    {
        //necesito las longitudes de los campos para validar
        public int Id { get; set; }

        [Display(Name ="Nombre pais")]
        [Required]
        [StringLength(50, ErrorMessage = "La longitud maxima es 50")]
        public string Nombre { get; set; }

        [Display(Name = "Codigo Afip")]
        [Required]
        [StringLength(50, ErrorMessage = "La longitud maxima es 50")]

        public string CodigoAfip { get; set; }

        [Display(Name = "Cuit")]
        [Required]
        //[StringLength(50, ErrorMessage = "La longitud maxima es 50")]
        [MaxLength(50, ErrorMessage = "La longitud maxima es 50")]
        public string Cuit { get; set; }

        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }


        //agregados
        public string MensajeError { get; set; }

    }
}