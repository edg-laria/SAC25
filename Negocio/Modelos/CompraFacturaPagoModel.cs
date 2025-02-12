using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class CompraFacturaPagoModel
    {


        public int Id { get; set; }

        public Nullable<int> IdFacturaCompra { get; set; }

        public Nullable<int> IdTipoPago { get; set; }

        public Nullable<int> IdCheque { get; set; }

        public Nullable<int> IdChequera { get; set; }

        public Nullable<int> IdTarjeta { get; set; }

        public Nullable<int> IdBancoCuenta { get; set; }

        public Nullable<decimal> Monto { get; set; }

        public string Observaciones { get; set; }

        public bool Usado { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }



        public virtual BancoCuenta BancoCuenta { get; set; }

        public virtual Cheque Cheque { get; set; }

        public virtual Chequera Chequera { get; set; }

        public virtual CompraFactura CompraFactura { get; set; }

        public virtual Tarjetas Tarjetas { get; set; }

        public virtual TipoPago TipoPago { get; set; }

    }
}
