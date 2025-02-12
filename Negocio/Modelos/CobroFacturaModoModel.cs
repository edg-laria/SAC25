using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class CobroFacturaModoModel
    {
        public int Id { get; set; }
        public Nullable<int> IdFactVenta { get; set; }
        public Nullable<int> IdTipoPago { get; set; }
        public Nullable<int> IdCheque { get; set; }
        public Nullable<int> IdChequera { get; set; }
        public Nullable<int> IdTarjeta { get; set; }
        public Nullable<int> IdBancoCuenta { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public string Observaciones { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public BancoCuentaModel BancoCuenta { get; set; }
        public ChequeModel Cheque { get; set; }
        public ChequeraModel Chequera { get; set; }
        public Tarjetas Tarjetas { get; set; }
        public TipoPago TipoPago { get; set; }
        public FacturaVentaModel FacturaVenta { get; set; }



        public string idChequesPropios { get; set; }
        public string idChequesTerceros { get; set; }
        public string idChequesCliente { get; set; }
        public string idRetencionesCliente { get; set; }
        public int IdPresupuesto { get; set; }
        public int IdRetencion { get; set; }

        public decimal montoEfectivo { get; set; }
        public decimal montoChequesSeleccionados { get; set; }
        public decimal montoTarjeta{ get; set; }
        public decimal montoCuentaBancaria { get; set; }
        public decimal montoRetencion { get; set; }
        public decimal montoComision { get; set; }
        public decimal montoTotal { get; set; }







    }
}
