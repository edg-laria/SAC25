using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class TrackingFacturaPagoCompraModel
    {

        public int Id { get; set; }
        public int IdFactura { get; set; }
        public int IdPago { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public CompraFactura CompraFactura { get; set; }
        public CompraFactura CompraFactura1 { get; set; }
    }

}
