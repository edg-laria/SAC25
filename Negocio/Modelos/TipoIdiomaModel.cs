using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregado
using Datos.ModeloDeDatos;

namespace Negocio.Modelos
{
    public class TipoIdiomaModel
    {

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsaurio { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public virtual ICollection<ClienteModel> Cliente { get; set; }

    }
}
