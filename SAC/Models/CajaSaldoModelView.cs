using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class CajaSaldoModelView
    {



        public int Id { get; set; }
        public int NumeroCierrre { get; set; }
        [Display(Name = "Importe Inicial Pesos")]
        public decimal ImporteInicialPesos { get; set; }
        [Display(Name = "Importe Inicial Dolares")]
        public Nullable<decimal> ImporteInicialDolares { get; set; }
        [Display(Name = "Importe Inicial Cheques")]
        public decimal ImporteInicialCheques { get; set; }
        [Display(Name = "Importe Inicial Tarjetas")]
        public decimal ImporteInicialTarjetas { get; set; }
        [Display(Name = "Importe Inicial Depositos")]
        public decimal ImporteInicialDepositos { get; set; }


        [Display(Name = "Importe Final Pesos")]
        public decimal ImporteFinalPesos { get; set; }
        [Display(Name = "Importe Final Dolares")]
        public decimal ImporteFinalDolares { get; set; }

        [Display(Name = "Importe Final Cheques")]
        public decimal ImporteFinalCheques { get; set; }

        [Display(Name = "Importe Final Tarjetas")]
        public decimal ImporteFinalTarjetas { get; set; }

        [Display(Name = "Importe Final Depositos")]
        public decimal ImporteFinalDepositos { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

      
        public virtual ICollection<Caja> Caja { get; set; }










    }
}