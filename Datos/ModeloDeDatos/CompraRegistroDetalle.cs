using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.ModeloDeDatos
{
   public class CompraRegistroDetalle
    {

        //public CompraRegistroDetalle()
        //{
        //    //seteo valores para iniciarlos ya que puede que en la base no esten cargados - bre 09/02/2021
        //    Neto = 0;
        //    ISIB = 0;
        //    Total = 0;
        //}

        public int IdComprobante { get; set; }
        public string Cbte { get; set; }
        public int PuntoVenta { get; set; }
        public int NumeroFactura { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Cuit { get; set; }
        public string Empresa { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> Neto { get; set; }
        public Nullable<decimal> NetoNoGravado { get; set; }
        public Nullable<decimal> Gasto { get; set; }
        public Nullable<decimal> PercepcionIva { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public Nullable<decimal> PercepcionImporteIva { get; set; }
        public Nullable<decimal> ISIB { get; set; }
        public Nullable<decimal> Saldo { get; set; }

    }
}
