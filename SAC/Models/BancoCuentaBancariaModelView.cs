using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;

namespace SAC.Models
{
    public class BancoCuentaBancariaModelView
    {
        public int Id { get; set; }
        public int NumeroOperacion { get; set; }
        public int IdBancoCuenta { get; set; }       
        public int IdGrupoCaja { get; set; }

        public string CuentaDescripcion { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.DateTime> FechaEfectiva { get; set; }
        public string DiaClearing { get; set; }
        public decimal Importe { get; set; }
        public string IdCliente { get; set; }
        public bool Conciliacion { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public string IdImputacion { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
      
        [Display(Name = "Tipo de Movimiento")]        
        public string TipoMovimiento { get; set; }
        public List<SelectListItem> ListItemsGrupoCaja { get; set; }
 
        public List<SelectListItem> ListItemsBancoCuenta { get; set; }
        public int IdBancoCuentaDestino { get; set; }
        public decimal ImporteDolar { get; set; }
        public decimal Cotizacion { get; set; }
        public int IdTipoMoneda { get; set; }

        public List<ChequeModelView> ListaChequesTerceros { get; set; }
        public ChequeraModelView oChequera { get; set; }
        public decimal montoChequesSeleccionados { get; set; }
        public string idChequesPropios { get; set; }
        public string idChequesTerceros { get; set; }

        public CajaGrupoModelView GrupoCaja { get; set; }
        public BancoCuentaModelView BancoCuenta { get; set; }

        public List<BancoCuentaBancariaModelView> ImprimeCtaBria { get; set; }

    }
}