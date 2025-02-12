using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregado
using Datos.ModeloDeDatos;

namespace Negocio.Modelos
{
    public class TipoMonedaModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public ICollection<Cheque> Cheque { get; set; }
        public ICollection<Chequera> Chequera { get; set; }
        public ICollection<Cliente> Cliente { get; set; }
        public ICollection<ClienteDireccion> ClienteDireccion { get; set; }
        public ICollection<CompraFactura> CompraFactura { get; set; }
        public ICollection<FactVenta> FactVenta { get; set; }
        public ICollection<Proveedor> Proveedor { get; set; }
    }
}
