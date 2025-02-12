using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.ModeloDeDatos
{
    public class ConsultaFacturaVentaIva
    {
        public string Clase { get; set; }
        public int Tipo { get; set; }
        public string Punto { get; set; }
        public int NumeroFactura { get; set; }
        public string Codigo { get; set; }
        public string Empresa { get; set; }
        public string Cuit { get; set; }
        public decimal Neto { get; set; }
        public decimal TotalIva { get; set; }
        public decimal Gasto { get; set; }
        public int Isib { get; set; }
        public decimal Total { get; set; }
        public decimal Dolar { get; set; }
        public string Moneda { get; set; }
        public string Periodo { get; set; }
        public int Asiento { get; set; }




    }

}
