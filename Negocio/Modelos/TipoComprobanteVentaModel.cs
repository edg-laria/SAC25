using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class TipoComprobanteVentaModel
    {

        public int Id { get; set; }
        public int CodigoAfip { get; set; }
        public int PuntoVenta { get; set; }
        public string Denominacion { get; set; }
        public int Numero { get; set; }
        public string Abreviatura { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public ICollection<FactVenta> FactVenta { get; set; }

    }
}
