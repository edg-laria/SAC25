using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class CuentaCteProveedorModel
    {


        public int ProveedorCodigo { get; set; }

        public string ProveedorNombre { get; set; }

        public Nullable<decimal> MontoPesos { get; set; }

        public Nullable<decimal> MontoDolares { get; set; }

        public Nullable<decimal> TotalDeuda { get; set; }

        public Nullable<System.DateTime> UltimoMovimiento { get; set; }
    }
}
