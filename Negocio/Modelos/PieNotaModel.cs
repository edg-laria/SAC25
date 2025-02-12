using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class PieNotaModel
    {


        

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }
        public bool Visible { get; set; }
       public int IdBancoCuenta { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public virtual ICollection<ClienteModel> Cliente { get; set; }

        public virtual BancoCuentaModel BancoCuenta { get; set; }
    }
}
