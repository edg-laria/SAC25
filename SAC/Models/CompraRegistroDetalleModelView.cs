using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class CompraRegistroDetalleModelView
    {

        public int IdComprobante { get; set; }
        public string Cbte { get; set; }
        public int PuntoVenta { get; set; }
        public int NumeroFactura { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Cuit { get; set; }
        public string Empresa { get; set; }
        public Nullable<decimal> Total { get; set; }
        public decimal Neto { get; set; }
        public Nullable<decimal> NetoNoGravado { get; set; }
        public Nullable<decimal> Gasto { get; set; }
        public decimal PercepcionIva { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public decimal PercepcionImporteIva { get; set; }
        public Nullable<decimal> ISIB{ get; set; }
        public decimal Saldo { get; set; }
      

    }
}