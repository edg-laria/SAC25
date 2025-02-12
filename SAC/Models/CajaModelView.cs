using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using Datos.ModeloDeDatos;
using System.ComponentModel;
using System.Web.Mvc;

namespace SAC.Models
{
    public class CajaModelView
    {

        public int Id { get; set; }

        public int IdTipoMovimiento { get; set; }

        [Required(ErrorMessage = "El campo no puede estar vacio")]
        public string Concepto { get; set; }

        
        //[DisplayName("Fecha"), Required(ErrorMessage = "Debe ingresar una fecha.")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }

        public string Tipo { get; set; }
        public string Saldo { get; set; }
        //[Required(ErrorMessage = "Seleccione un items")]
        public int IdGrupoCaja { get; set; }

        public string Recibo { get; set; }


        [Display(Name = "Importe Pesos")]
        public decimal ImportePesos { get; set; }

        [Display(Name = "Importe Depósito")]
        public decimal ImporteDeposito { get; set; }

        //[Required(ErrorMessage = "Seleccione un items")]
        public Nullable<int> IdCuentaBanco { get; set; }

        [Display(Name = "Importe Dolar")]
        public decimal ImporteDolar { get; set; }

        [Display(Name = "Importe Tarjeta")]
        public decimal ImporteTarjeta { get; set; }

        [Display(Name = "Importe Cheque")]
        public decimal ImporteCheque { get; set; }


        public int IdTarjeta { get; set; }
        public int IdCheque { get; set; }

        public int IdCajaSaldo { get; set; }
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime UltimaModificacion { get; set; }

        public CajaTipoMovimiento CajaTipoMovimiento { get; set; }

        public CajaGrupoModelView GrupoCaja { get; set; }

        public List<CajaModelView> ListaCaja { get; set; }

        public CajaSaldoModelView CajaSaldoInicial { get; set; }

        //[DisplayName("Fecha Cierre"), Required(ErrorMessage = "Debe ingresar una Fecha.")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaCierre { get; set; }


        /* parametros para la consulta  */

        [Display(Name = "Grupo Caja")]
        public int cIdGrupoCaja { get; set; }

        //[Display(Name = "Fecha Desde")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
         public System.DateTime cFechaDesde { get; set; }

        
        //[Display(Name = "Fecha Hasta")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime cFechaHasta { get; set; }

        

        public CajaModelView CajaDesde { get; set; }
        public CajaModelView CajaHasta { get; set; }
        public CajaModelView CajaSaldoUltimoMovimiento { get; set; }

        //----dev-a
        public List<SelectListItem> listaTarjetasDrop { get; set; }
        public List<SelectListItem> listaCuentaBancariaDrop { get; set; }
        public List<SelectListItem> ListaTipoMonedaDrop { get; set; }

        public List<ChequeModelView> ListaChequesTerceros { get; set; }
        public ChequeraModelView oChequera { get; set; }
        public List<ChequeraModelView> ListaChequesPropios { get; set; }
        public decimal montoChequesSeleccionados { get; set; }
        public string idChequesPropios { get; set; }
        public string idChequesTerceros { get; set; }




    }
}