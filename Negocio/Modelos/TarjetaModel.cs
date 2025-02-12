using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class TarjetaModel
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public int IdImputacion { get; set; }

        public string Numero { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public  ICollection<CajaModel> Caja { get; set; }

        public ICollection<CompraFacturaPago> CompraFacturaPago { get; set; }

        public ICollection<TarjetaOperacion> TarjetaOperacion { get; set; }

    }
}
