using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;

namespace SAC.Models
{
    public class CteCteClienteResumenModelView
    {



        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal TotalPesos { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal TotalPesosLocal { get; set; }
        public decimal TotalDolaresLocal { get; set; }
        public decimal TotalDolaresLE { get; set; }
        public DateTime FechaUltimoMov { get; set; }


    }


}