using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;

namespace SAC.Models
{
    public class CtaCteClienteModelView
    {

        public List<CobroFacturaModelView> CtaCte { get; set; }

        public ClienteModelView cliente { get; set; }

        public decimal TotalPesos { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal TotalParcialPesos { get; set; }
        public decimal TotalParcialDolares { get; set; }
        public decimal TotalSaldoPesos { get; set; }
        public decimal TotalSaldoDolares { get; set; }
        public decimal TotalPesosCobros { get;  set; }
        public decimal TotalDolaresCobros { get; set; }
        public DateTime FechaHasta { get; set; }
    }

  
}