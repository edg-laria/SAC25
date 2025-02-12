
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ChequeraModel
    {
        public int Id { get; set; }

        public int NumeroCheque { get; set; }

        public Nullable<int> IdBancoCuenta { get; set; }

        public Nullable<System.DateTime> Fecha { get; set; }

        public decimal Importes { get; set; }

        public Nullable<int> IdProveedor { get; set; }

        public string NumeroRecibo { get; set; }

        public Nullable<System.DateTime> FechaIngreso { get; set; }

        public System.DateTime FechaEgreso { get; set; }

        public string Destino { get; set; }

        public Nullable<int> NumeroOperacion { get; set; }

        public Nullable<int> IdMoneda { get; set; }

        public string Registro { get; set; }

        public Nullable<bool> Usado { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public TipoMonedaModel TipoMoneda { get; set; }
        public BancoCuentaModel BancoCuenta { get; set; }
        public List<CompraFacturaPagoModel> CompraFacturaPago { get; set; }



        //prop agregada
        // public string tipoMonedaDescripcion { get; set; }

    }
}
