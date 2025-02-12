using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;

namespace SAC.Models
{
    public class PagosFacturasAPagarModelView
    {
        public int tipo { get; set; }
        public int punto { get; set; }
        public int idFactura { get; set; }
        public int nroFatura { get; set; }
        public decimal montoFactura { get; set; }
        public decimal pago { get; set; }
        public decimal aplicacion { get; set; }
        public decimal saldo { get; set; }

    }
}