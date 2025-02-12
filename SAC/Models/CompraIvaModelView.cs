using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class CompraIvaModelView
    {
        public int Id { get; set; }

     

        public decimal NetoGravado { get; set; }

        [Display(Name = "Neto No Gravado")]
       
        public decimal? NetoNoGravado { get; set; }

        [Display(Name = "Sub Total ")]
       
        public decimal SubTotal { get; set; }

        [Display(Name = "Total IVA ")]
       
        public decimal? TotalIva { get; set; }

        [Display(Name = " Total Percepciones")]
       
        public decimal? TotalPercepciones { get; set; }

        [Display(Name = "Total ")]
       
        public decimal Total { get; set; }

        [Display(Name = "Importe 2.5 ")]
       
        public decimal? Importe25 { get; set; }
        
        [Display(Name = "Importe 5 ")]
       
        public decimal? Importe5 { get; set; }
       
        [Display(Name = "Importe 10.5 ")]
       
        public decimal? Importe105 { get; set; }

        [Display(Name = "Importe 21 ")]
       
        public decimal? Importe21 { get; set; }

        [Display(Name = "Importe 27 ")]
       
        public decimal? Importe27 { get; set; }

        [Display(Name = "IVA 2.5 ")]
       
        public decimal? Iva25 { get; set; }

        [Display(Name = "IVA 5 ")]
       
        public decimal? Iva5 { get; set; }

        [Display(Name = "IVA 10.5 ")]
       
        public decimal? Iva105 { get; set; }

        [Display(Name = "IVA 21 ")]
       
        public decimal? Iva21 { get; set; }

        [Display(Name = "IVA 27 ")]
       
        public decimal? Iva27 { get; set; }

        [Display(Name = " Percepcion Iva ")]
       
        public decimal? PercepcionIva { get; set; }

        [Display(Name = " Percepcion IB ")]
       
        public decimal? PercepcionIB { get; set; }

        [Display(Name = " Percepcion Provincia ")]
       
        public decimal? PercepcionProvincia { get; set; }

        [Display(Name = " Percepcion Importe Iva")]
       
        public decimal? PercepcionImporteIva { get; set; }

        [Display(Name = " Percepcion Importe IB")]
       
        public decimal? PercepcionImporteIB { get; set; }

        [Display(Name = " Percepcion Importe Provincia")]
       
        public decimal? PercepcionImporteProvincia { get; set; }

        [Display(Name = " Importe Otros Impuestos")]
       
        public decimal? OtrosImpuestos { get; set; }
        
        [Display(Name = " Percepcion ISIB")]
              
        public bool Activo { get; set; }
        public int Idusuario { get; set; }
        public DateTime UltimaModificacion { get; set; }



    }
}