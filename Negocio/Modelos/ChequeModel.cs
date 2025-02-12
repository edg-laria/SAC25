using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
 public class ChequeModel
    {
        public int Id { get; set; }

        public int NumeroCheque { get; set; }
        public int IdBanco { get; set; }
        public DateTime Fecha { get; set; }
        public string DiaClearing { get; set; }

        public decimal Importe { get; set; }

        public int IdCliente { get; set; }

        public string Descripcion { get; set; }

        public string NumeroRecibo { get; set; }
        public Nullable<System.DateTime> FechaIngreso { get; set; }
        public System.DateTime FechaEgreso { get; set; }
        public string Destino { get; set; }
        public int IdMoneda { get; set; }
        public int IdGrupoCaja { get; set; }
        public string IdFactura { get; set; }

        public string NumeroPago { get; set; }

        public string Registro { get; set; }

        public string Proveedor { get; set; }

        public Nullable<bool> Endosado { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

       
        public TipoMonedaModel TipoMoneda { get; set; }
      
        //propiedad agregada
        public string tipoMonedaDescripcion { get; set; }

        public ICollection<CajaModel> Caja { get; set; }
        public BancoCuentaModel BancoCuenta { get; set; }
        public BancoModel BancoCheque { get; set; }

    }
}
