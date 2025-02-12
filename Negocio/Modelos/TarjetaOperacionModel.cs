using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public  class TarjetaOperacionModel
    {

        public int Id { get; set; }
        public int IdTarjeta { get; set; }
        public int IdGrupoCaja { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Conciliacion { get; set; }
        public string NumeroPago { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

      //  public ICollection<CompraFacturaPago> CompraFacturaPago { get; set; }
        public TarjetaModel Tarjetas { get; set; }
    }
}
