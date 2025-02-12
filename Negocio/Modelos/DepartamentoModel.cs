using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class DepartamentoModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public Nullable<decimal> TotalMes { get; set; }
        public Nullable<decimal> TotalAnio { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    }

}

