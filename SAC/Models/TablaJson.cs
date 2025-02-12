using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class TablaJson
    {
        public string[] headers { get; set; }
        public object[][] data { get; set; }
    }
}