using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.ModeloDeDatos
{
    public class ConsultaCtaBria
    {
		public int IdCta { get; set; }
		public String Caja { get; set; }
		public int NumeroCierre { get; set; }
		public String Codigo { get; set; }
		public String Descripcion { get; set; }
		public String Fecha { get; set; }
		public decimal Importe { get; set; }
		public String Banco { get; set; }
		public int Cierre { get; set; }
	}
}
