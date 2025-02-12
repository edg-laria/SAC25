using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using Datos.ModeloDeDatos;
using System.ComponentModel;

namespace SAC.Models
{
    public class DepartamentoModelView




    {



        public int Id { get; set; }

        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Total Mes")]
        public Nullable<decimal> TotalMes { get; set; }
        [Display(Name = "Total Año")]
        public Nullable<decimal> TotalAnio { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }





    }
}