using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregado
using Datos.ModeloDeDatos;

namespace Negocio.Modelos
{
    public class TipoComprobanteModel
    {
        public int Id { get; set; }
        public string Denominacion { get; set; }
        public string Abreviatura { get; set; }
        public Nullable<int> IdAfipCategoria { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public System.DateTime UltimaModificacion { get; set; }


        public  ICollection<TipoComprobanteTipoIva> TipoComprobanteTipoIva { get; set; }
       
        public  ICollection<CompraFacturaModel> CompraFactura { get; set; }

    }
}
