using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Datos.ModeloDeDatos
{
    public class CuentaCorrienteProveedorDetalles
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Cbte { get; set; }
        public int IdComprobante { get; set; }
        public int PuntoVenta { get; set; }
        public int NumeroFactura { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<decimal> Total { get; set; }        
        public Nullable<decimal> TotalDolares { get; set; }
        public Nullable<decimal> Parcial { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public decimal Cotizacion { get; set; }
        public Nullable<int> NumeroPago { get; set; }
        public Nullable<int> Recibo { get; set; }
        public ICollection<CompraFacturaPago> compraFacturaPagos { get; set; }
    }

}
