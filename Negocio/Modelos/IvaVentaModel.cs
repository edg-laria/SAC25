using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class IvaVentaModel
    {

        public int Id { get; set; }
        public int IdTipoComprobantes { get; set; }
        public string PuntoVenta { get; set; }
        public int NumeroFactura { get; set; }
        public string AuxiliarNumero { get; set; }
        public int NroEmp { get; set; }
        public string Empre { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal Iva { get; set; }
        public decimal IvaRi { get; set; }
        public decimal IvaRes { get; set; }
        public decimal TotIva { get; set; }
        public decimal Gasto { get; set; }
        public decimal Neto { get; set; }
        public string Espe { get; set; }
        public decimal Res { get; set; }
        public string TipoIva { get; set; }
        public string TipoFac { get; set; }
        public decimal ResDGR { get; set; }
        public string Cuit { get; set; }
        public decimal Dolar { get; set; }
        public string Difer { get; set; }
        public string Clase { get; set; }
        public string NomEmp { get; set; }
        public string Periodo { get; set; }
        public string ClaseFac { get; set; }
        public string Moneda { get; set; }
        public double Isib { get; set; }
        public string Diario { get; set; }
        public Nullable<double> Cae { get; set; }
        public double Impint { get; set; }
        public string IdImputacion { get; set; }
        public string Tnogra { get; set; }
        public string Texento { get; set; }
        public string Pergral { get; set; }
        public string Peib { get; set; }
        public string Permun { get; set; }
        public string OtrosImp { get; set; }
        public string Tnocat { get; set; }
        public string Pernac { get; set; }
        public string Iva27 { get; set; }
        public string Iva21 { get; set; }
        public string Iva105 { get; set; }
        public string Iva5 { get; set; }
        public string Iva25 { get; set; }
        public string Total21 { get; set; }
        public string Total27 { get; set; }
        public string Total105 { get; set; }
        public string Total5 { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

    }
}
