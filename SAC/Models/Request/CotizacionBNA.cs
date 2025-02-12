using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models.Request
{
    public class CotizacionBNA
    {
        public string Fecha { get; set; }
        public string Moneda { get; set; }
        public string Compra { get; set; }
        public string Venta { get; set; }
    }
}