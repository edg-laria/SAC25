using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class CompraFacturaPagoModelView
    {


        public int Id { get; set; }

       
        public Nullable<int> IdFacturaCompra { get; set; }


        [Display(Name = "Tipo Pago")]
        public Nullable<int> IdTipoPago { get; set; }

        [Display(Name = "Nro Cheque")]
        public Nullable<int> IdCheque { get; set; }

        [Display(Name = "Nro Chequera")]
        public Nullable<int> IdChequera { get; set; }

        [Display(Name = "Nro Tarjeta")]
        public Nullable<int> IdTarjeta { get; set; }

        [Display(Name = "Nro Banco Cuenta")]
        public Nullable<int> IdBancoCuenta { get; set; }


        public Nullable<decimal> Monto { get; set; }

        public string Observaciones { get; set; }

        public bool Usado { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }



        public  BancoCuentaModelView BancoCuenta { get; set; }

        public  ChequeModelView Cheque { get; set; }

        public  ChequeraModelView Chequera { get; set; }

        public  CompraFacturaViewModel CompraFactura { get; set; }

        public  TarjetaModelView Tarjetas { get; set; }

        public  TipoPagoModelView TipoPago { get; set; }

        public List<CompraFacturaPagoModelView> ListaCompraFacturaPaga { get; set; }


    }
}