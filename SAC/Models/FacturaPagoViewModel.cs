using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Models
{
    public class FacturaPagoViewModel
    {
        public FacturaPagoViewModel()
        {
            efectivo = 0;
            TotalAPagar = 0;
            montoTarjetaSeleccionados = 0;
            montoChequesSeleccionados = 0;
            oChequera = new ChequeraModelView();
        }

     //listado de facturas obtenidas para el proveedor
       public List<CompraFacturaViewModel> ListaFacturas { get; set; }

        //proveedor consultado
        public ProveedorModelView Proveedor { get; set; }

        //listado de facturas seleccionadas para pagar, estas se van a cargar del hidden idfacturas
        public List<CompraFacturaViewModel> FacturasAPagar { get; set; }

        //monto total a pagas
        [Display(Name = "Total Seleccionado")]
     
        public decimal TotalAPagar { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaPago { get; set; }

        //resto
        public Nullable<decimal> Diferencia { get; set; }

        //medios de pago
        [Display(Name = "Monto en Efectivo")]          
        public decimal efectivo { get; set; }

        //estas propiedades son para listar los tipos
        //public List<CuentaModelView> listaCuentaBancaria { get; set; }
        public List<SelectListItem> listaTarjetasDrop { get; set; }
        public List<SelectListItem> listaCuentaBancariaDrop { get; set; }
        public List<ChequeModelView> ListaChequesTerceros { get; set; }
        public List<ChequeraModelView> ListaChequesPropios { get; set; }
        public List<SelectListItem> ListaPresupuestoActual { get; set; }

        public List<SelectListItem> ListaTipoMonedaDrop { get; set; }

        //id banco insertar cheque propio
        public int idCuentaBancariaSeleccionada { get; set; }
        public int idTipoMonedaSeleccionada { get; set; }

        public int idTarjeta { get; set; }

        [Display(Name = " Monto Tarjeta")]
        public decimal montoTarjetaSeleccionados { get; set; }

        //estas propiedades son de los hidden para pasar al controler
        public string idFacturas { get; set; }
        public string idChequesPropios { get; set; }
        public string idChequesTerceros { get; set; }

        public int idPresupuesto { get; set; }

        [Display(Name = " Monto cheques")]
        public decimal montoChequesSeleccionados { get; set; }

        public int idCuentasBancarias { get; set; }

        [Display(Name = " Monto Tranferencia")]
        public decimal montoTranferencia { get; set; }

        public int idProveedor { get; set; }

        public int idUsuario { get; set; }

        //agrego para insertar un cheque propio
        public ChequeraModelView oChequera { get; set; }


       

    }
}