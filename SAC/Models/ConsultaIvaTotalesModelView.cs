using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;

namespace SAC.Models
{
    public class ConsultaIvaTotalesModelView
    {

        public decimal LocalPesos { get; set; }
        public decimal LocalDolares { get; set; }
        public decimal LocalEPesos { get; set; }
        public decimal LocalEDolares { get; set; }
        public decimal ExteriorPesos { get; set; }
        public decimal ExteriorDolares { get; set; }

        public decimal TotalPesos { get; set; }
        public decimal TotalDolares { get; set; }



        public decimal TotalGastosPesos { get; set; }

        public decimal TotalIBPagados { get; set; }
        public decimal TotalIBaPagar { get; set; }

        public string Periodo { get; set; }
    }

  
}