using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class CompraIvaModel
    {
        public int Id { get; set; }               
        public decimal NetoGravado { get; set; }
        public decimal NetoNoGravado { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalIva { get; set; }
        public decimal TotalPercepciones { get; set; }
        public decimal Total { get; set; }
        public decimal Importe25 { get; set; }
        public decimal Importe5 { get; set; }
        public decimal Importe105 { get; set; }
        public decimal Importe21 { get; set; }
        public decimal Importe27 { get; set; }
        public decimal Iva25 { get; set; }
        public decimal Ivan5 { get; set; }
        public decimal Iva105 { get; set; }
        public decimal Iva21 { get; set; }
        public decimal Iva27 { get; set; }
        public decimal PercepcionIva { get; set; }
        public decimal PercepcionIB { get; set; }
        public decimal PercepcionProvincia { get; set; }
        public decimal PercepcionImporteIva { get; set; }
        public decimal PercepcionImporteIB { get; set; }
        public decimal PercepcionImporteProvincia { get; set; }
        public decimal OtrosImpuestos { get; set; }       
        public bool Activo { get; set; }
        public int Idusuario { get; set; }
        public System.DateTime UltimaModificacion { get; set; }

     }
}
