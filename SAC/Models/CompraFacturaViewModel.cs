using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class CompraFacturaViewModel
    {
        public CompraFacturaViewModel()
        {
            Cotizacion = 1;
            NumeroFactura = 1;
        }

        public int Id { get; set; }
        [Display(Name = "Tipo Comprobante")]
        public int IdTipoComprobante { get; set; }
        [Display(Name = "Punto Venta ")]
        [DisplayFormat(DataFormatString = "{0:0000}", ApplyFormatInEditMode = true)]
        public int PuntoVenta { get; set; }
        [Display(Name = "Número")]
        [DisplayFormat(DataFormatString = "{0:00000000}", ApplyFormatInEditMode = true)]
        public int NumeroFactura { get; set; }       
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }   
        public string IdTipoIva { get; set; }
        public string CAE { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }

        //[DataType(DataType.Date)]
        //[Display(Name = "Fecha"), Required(ErrorMessage = "Debe ingresar un fecha.")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }

        //[Display(Name = "Fecha"), Required(ErrorMessage = "Debe ingresar un fecha.")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Vencimiento { get; set; }

        public decimal TotalDolares { get; set; }
        public decimal Cotizacion { get; set; }
        public DateTime FechaPago { get; set; }
        public int Periodo { get; set; }
        public decimal Grupo { get; set; }
        public string Marca { get; set; }
        public string Pase { get; set; }
        public decimal CotizacionDePago { get; set; }
        public string Concepto { get; set; }
        public int IdImputacion { get; set; }
        public int IdMoneda { get; set; }
        public int IdCompraIva { get; set; }
        public decimal Parcial { get; set; }
        public int Recibo { get; set; }
        public int NumeroPago { get; set; }
        [Display(Name = "Aplica Factura Nº ")]
        public int? IdCompraFacturaAplica { get; set; }
        public int IdDiario { get; set; }

        public Nullable<int> CodigoDiario { get; set; }
        public int Auxiliar { get; set; }
        public string AxiliarNumero { get; set; }
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime UltimaModificacion { get; set; }

        //agrego esto para las facutas a pagar MODULO PAGOS
        public decimal pago { get; set; }
        public decimal aplicacion { get; set; }
        public decimal saldoPagos { get; set; }
        //------------------------------------------------

        public TipoComprobanteModelView TipoComprobante { get; set; }
        public CompraIvaModelView CompraIva { get; set; }
        public ProveedorModelView Proveedor { get; set; }
        public ImputacionModelView Imputacion { get; set; }
        public CompraFacturaViewModel CompraFacturaAplicada { get; set; }
        public TipoMonedaModelView TipoMoneda { get; set; }
        public List<TipoMonedaModelView> TipoMonedas { get; set; }
      
        public List<TipoComprobanteModelView> ListTipoComprobante { get; set; }

       //public List<TipoComprobanteModelView> TipoComprobantes { get; set; }

        public List<RetencionModelView> Retencion { get; set; }

    }
}