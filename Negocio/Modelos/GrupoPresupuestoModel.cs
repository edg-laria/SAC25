using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class GrupoPresupuestoModel

    {

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

          public virtual ICollection<ClienteModel> Cliente { get; set; }
      

    }

}
