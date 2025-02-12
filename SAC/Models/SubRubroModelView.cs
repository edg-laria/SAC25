using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class SubRubroModelView
    {
        [Display(Name = "Codigo imputación")]
        public int Id { get; set; }

        public string codigo { get; set; }

        public string Descripcion { get; set; }

        public Nullable<int> IdRubro { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }


        public  ICollection<Imputacion> Imputacion { get; set; }

        public  Rubro Rubros { get; set; }

    }
}