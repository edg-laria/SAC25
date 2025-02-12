using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class TipoIvaModel
    {

        public int Id { get; set; }

        public string Descripcion { get; set; }

        public decimal Porcentaje { get; set; }

        public string Tipo { get; set; }

        public virtual ICollection<Proveedor> Proveedor { get; set; }

    }
}
