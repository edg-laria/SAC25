using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class ProvinciaModelView
    {

        public int Id { get; set; }

        [Display(Name = "Nombre provincia")]
        [Required]
        [StringLength(50, ErrorMessage = "La longitud máxima es 50")]
        public string Nombre { get; set; }

        [Display(Name = "Codigo")]
        [Required]
        [StringLength(20, ErrorMessage = "La longitud máxima es 20")]
        public string Codigo { get; set; }

        [Display(Name = "Código número")]
        [Required]
        
        public Nullable<int> CodigoNumero { get; set; }

        [Display(Name = "Pais")]
        [Required]
        [Range(1, 1000, ErrorMessage = "El valor del pais no corresponde")]
        public Nullable<int> IdPais { get; set; }

        [Display(Name = "Código Afip")]
        public Nullable<int> CodigoAfip { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public PaisModelView Pais { get; set; }
        

        //agregados
        public string nombrePais { get; set; }

        //para tomar valor combo pais
        public int idCmbPais { get; set; }
    }
}