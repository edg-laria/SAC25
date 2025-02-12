using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;

namespace SAC.Models
{
    public class PagosFacturasModelView
    {
        public PagosFacturasModelView()
        {
            Retencion_ = new RetencionModelView();
            ListaFacturasAPagar_ = new List<CompraFacturaViewModel>();
            LsitaFacturasSeleccionadasPagar_ = new List<CompraFacturaViewModel>();
            ListaChequesTerceros = new List<ChequeModelView>();
            ListaChequesPropios = new List<ChequeraModelView>();
            oChequera = new ChequeraModelView();
            ListadoRetenciones_ = new List<RetencionModelView>();
            ListaTarjetas_ = new List<SelectListItem>();
            ListaCuentasBancarias_ = new List<SelectListItem>();
            ListaProveedores_ = new List<SelectListItem>();
            ListaPresupuestoActual_ = new List<SelectListItem>();
            ListaTipoMonedas_ = new List<SelectListItem>();
            //FechaOperacion_ = new DateTime();
            //FechaOperacion_ = DateTime.Now;

            //inicializo para probar
            nroRecibo_ = 0;
            idCuentaBancariaSeleccionada_ = 0;
            idCuentaBancariaSelec_ = 0;
            idTipoMonedaSelec_ = 0;
            idTarjetaSelec_ = 0;
            idFacturas = "";
            idChequesPropios = "";
            idChequesTerceros = "";
            idPresupuesto_ = 0;
            montoEfectivo_ = 0;
            montoChequesSeleccionados = 0;
            montoTarjetaSelec_ = 0;

            oChequera = new ChequeraModelView();

        }

        public List<SelectListItem> ListaProveedores_ { get; set; }

        public int ProveedorSelec_ { get; set; }
        public decimal cotizacion_ { get; set; }
        public List<SelectListItem> ListaTipoMonedas_ { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaOperacion_ { get; set; }
        public int Periodo_ { get; set; }

        //Cuenta Corriente segun el Pagos.doc
        public List<CompraFacturaViewModel> ListaFacturasAPagar_ { get; set; }

        //Resumen del pago segun el Pagos.doc - ver si se usa el modelo puesto o creo uno nuevo
        public List<CompraFacturaViewModel> LsitaFacturasSeleccionadasPagar_ { get; set; }

        public string ConceptoPago_ { get; set; }

        //medios de pago mostrar
        public List<SelectListItem> ListaTarjetas_ { get; set; }
        public List<SelectListItem> ListaCuentasBancarias_ { get; set; }
        public List<SelectListItem> ListaPresupuestoActual_ { get; set; }
        public List<ChequeModelView> ListaChequesTerceros { get; set; }
        public List<ChequeraModelView> ListaChequesPropios { get; set; }

        public ChequeraModelView oChequera { get; set; }

        //seleccion tipo de pago
    
       
        public int nroRecibo_ { get; set; }
        public int idCuentaBancariaSeleccionada_ { get; set; }
        public int idCuentaBancariaSelec_ { get; set; }
        public int idTipoMonedaSelec_ { get; set; }
        public int idTarjetaSelec_ { get; set; }

        public string idFacturas { get; set; }
        public string idChequesPropios { get; set; }
        public string idChequesTerceros { get; set; }
        public int idPresupuesto_ { get; set; }
        public int idRetencion { get; set; }

        public decimal montoEfectivo_ { get; set; }
        [Display(Name = " Monto cheques")]
        public decimal montoChequesSeleccionados { get; set; }
        public decimal montoTarjetaSelec_ { get; set; }
        public decimal montoCuentaBancaria_ { get; set; }
        public decimal montoRetencion_ { get; set; }
        public decimal montoTotal_ { get; set; }

        public int idProveedor_ { get; set; }
        public int idUsuario_ { get; set; }

        //retenciones 
        public RetencionModelView Retencion_ { get; set; }
        public List<RetencionModelView> ListadoRetenciones_ { get; set; }
        public decimal totalRetenciones_ { get; set; }

        //resumen de pago
        //public List<PagosFacturasAPagarModelView>  pagosFacturasAPagarModelViews { get; set; }
    }
}