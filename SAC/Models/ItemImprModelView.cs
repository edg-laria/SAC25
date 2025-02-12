using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SAC.Models
{
    public class ItemImprModelView
    {

        public int Id { get; set; }
        public Nullable<int> IdTipoComprobante { get; set; }
        public string PuntoVenta { get; set; }
        public Nullable<int> IdFactVenta { get; set; }
        public Nullable<int> Factura { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<double> Cantidad { get; set; }
        public string Unidad { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> AuxiliarNumero { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }


    }
}