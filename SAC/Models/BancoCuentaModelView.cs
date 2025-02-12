using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class BancoCuentaModelView
    {

        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Display(Name = "Banco")]
        public int IdBanco { get; set; }

        [Display(Name = "Descipción de Cuenta")]
        [Required]
        public string BancoDescripcion { get; set; }

        [Display(Name = "Imputación")]
        [Required]
        public int IdImputacion { get; set; }

        public string CNombre { get; set; }

        public decimal Saldo { get; set; }
        public int NumeroCierre { get; set; }
        public System.DateTime Fecha { get; set; }

        [Display(Name = "Moneda")]
        public int IdMoneda { get; set; }

        public bool Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public BancoModelView Banco { get; set; }


    }
}