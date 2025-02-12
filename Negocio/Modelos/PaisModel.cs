using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregado
using Datos.ModeloDeDatos;


namespace Negocio.Modelos
{
   public class PaisModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoAfip { get; set; }
        public string Cuit { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

    }
}
