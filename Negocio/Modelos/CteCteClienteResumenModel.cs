using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{

    public class CteCteClienteResumenModel
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
