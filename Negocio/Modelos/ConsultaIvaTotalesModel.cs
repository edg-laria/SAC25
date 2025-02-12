using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class ConsultaIvaTotalesModel
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


    }
}
