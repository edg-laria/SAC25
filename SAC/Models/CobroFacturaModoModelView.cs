using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;
using Negocio.Modelos;

namespace SAC.Models
{
    public class CobroFacturaModoModelView
    {

        public CobroFacturaModoModelView()
        {
            Retencion = new RetencionModelView();
          
            ListaChequesTerceros = new List<ChequeModelView>();
            ListaChequesPropios = new List<ChequeraModelView>();
            Cheque = new ChequeModelView();
            ListadoRetenciones = new List<RetencionModelView>();
            SelectTarjetas = new List<SelectListItem>();
            SelectCuentasBancarias = new List<SelectListItem>();            
            SelectPresupuestoActual = new List<SelectListItem>();
            SelectTipoMoneda = new List<SelectListItem>();

            //inicializo para probar

            IdTipoMoneda= 0;
            NumeroRecibo = 0;           
            IdCuentasBancarias= 0;
            IdTarjeta = 0;        
            idChequesPropios = "";
            idChequesTerceros = "";
            idChequesCliente = "";
            idRetencionesCliente = "";
            IdPresupuesto = 0;
            montoEfectivo= 0;
            montoChequesSeleccionados = 0;
            montoTarjeta = 0;


        }

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
   
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        public ClienteModelView Cliente { get; set; }

        [Display(Name = "Cotización")]     
        public ValorCotizacionModel Cotizacion { get; set; }

        [Display(Name = "Moneda de Operación")]
        [Required]
        public Nullable<int> IdTipoMoneda { get; set; }

        public int Periodo { get; set; }
        public String Fecha { get; set; }

        public List<CobroFacturaModelView> CuentaCorriente { get; set; }
        public List<CobroFacturaModelView> ResumenPago { get; set; }         
        public List<SelectListItem> SelectTipoMoneda { get; set; }
        public List<SelectListItem> SelectCuentasBancarias { get; set; }     
        public List<SelectListItem> SelectTarjetas { get; set; }
        public List<SelectListItem> SelectPresupuestoActual { get; set; }
        public List<ChequeModelView> ListaChequesTerceros { get; set; }
        public List<ChequeraModelView> ListaChequesPropios { get; set; }
        public ChequeraModelView Chequera { get; set; }
        public ChequeModelView Cheque { get; set; }
        public string ConceptoCobro { get; set; }
        public int NumeroRecibo { get; set; }     
        public Nullable<int> IdCuentasBancarias { get; set; }
       
        public int idCuentaBancariaSeleccionada_ { get; set; }
        public int idCuentaBancariaSelec_ { get; set; }

   
        public string idChequesPropios { get; set; }
        public string idChequesTerceros { get; set; }
        public string idChequesCliente { get; set; }
        public string idRetencionesCliente { get; set; }
        

        public Nullable<int> IdPresupuesto { get; set; }
        public Nullable<int> IdRetencion { get; set; }

        public decimal montoEfectivo { get; set; }
        [Display(Name = " Monto cheques")]
        public decimal montoChequesSeleccionados { get; set; }
        public decimal montoTarjeta { get; set; }
        public decimal montoCuentaBancaria { get; set; }
        public decimal montoRetencion { get; set; }
        public decimal montoComision { get; set; }
        public decimal montoTotal { get; set; }

        public int IdMonedaDeOperacion { get; set; }

        public int idUsuario_ { get; set; }

        //retenciones 
        public RetencionModelView Retencion { get; set; }
        public List<RetencionModelView> ListadoRetenciones { get; set; }
        public decimal TotalRetenciones { get; set; }
      
    }

}