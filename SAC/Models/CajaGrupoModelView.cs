using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class CajaGrupoModelView
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Concepto { get; set; }

        [Display(Name = "Neto Total Pesos")]
        public decimal? TotalPesos { get; set; }
        [Display(Name = "Neto Total Dólares")]
        public decimal? TotalDolares { get; set; }

        [Display(Name = "Neto Total Cheques")]
        public decimal TotalCheques { get; set; }
        [Display(Name = "Neto Total Tarjetas")]
        public decimal? TotalTarjetas { get; set; }
        [Display(Name = "Neto Total Depósitos")]
        public decimal? TotalDepositos { get; set; }

        [Display(Name = "Neto Parcial Pesos")]
        public decimal? ParcialPesos { get; set; }
        [Display(Name = "Neto Parcial Dólares")]
        public decimal? ParcialDolares { get; set; }
        [Display(Name = "Neto Parcial Cheques")]
        public decimal? ParcialCheques { get; set; }
        [Display(Name = "Neto Parcial Tajetas")]
        public decimal? ParcialTarjetas { get; set; }
        [Display(Name = "Neto Parcial Depósitos")]
        public decimal? ParcialDepositos { get; set; }

        [Required]
        [Display(Name = "Imputacion")]
        public int IdImputacion { get; set; }
        public bool NoBorrar { get; set; }
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime UltimaModificacion { get; set; }

        public ICollection<Caja> Caja { get; set; }




    }
}