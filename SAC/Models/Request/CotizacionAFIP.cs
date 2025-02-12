using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models.Request
{
    public class CotizacionAFIP
    {
     
        public string IdMoneda { get; set; }
        public decimal Importe { get; set; } 
        public string Fecha { get; set; }
    }
}