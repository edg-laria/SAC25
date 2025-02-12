using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;


namespace SAC.Models
{
    public class CuentaCteProveedorModelView
    {

        
        [Display(Name ="Codigo")]
        public int ProveedorCodigo { get; set; }

        [Display(Name = "Nombre")]
        public string ProveedorNombre { get; set; }

        [Display(Name = "Total Pesos")]
        public decimal MontoPesos { get; set; }

        [Display(Name = "Total Dólares  ")]
        public decimal MontoDolares { get; set; }

        [Display(Name = "Total")]
        public Nullable<decimal> TotalDeuda { get; set; }

        [Display(Name = "Ultimo Movimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime UltimoMovimiento { get; set; }
















    }
}