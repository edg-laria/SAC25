using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.ModeloDeDatos
{
   public class CteCteClienteResumen
    {

       

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal TotalPesos { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal TotalPesosLocal { get; set; }
        public decimal TotalDolaresLocal { get; set; }
        public DateTime FechaUltimoMov { get; set; }


    }
}
