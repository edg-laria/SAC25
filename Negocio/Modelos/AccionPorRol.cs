using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class AccionPorRolModel
    {

        public int idRolPorAccion { get; set; }
        public int idRol { get; set; }
        public int idAccion { get; set; }

        public virtual Accion Accion { get; set; }
        public virtual Rol Rol { get; set; }
    }

}
