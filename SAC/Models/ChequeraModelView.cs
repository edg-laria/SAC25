using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SAC.Models
{
    public class ChequeraModelView
    {

        public int Id { get; set; }
        
        [Display(Name = "Número Cheque"), Required(ErrorMessage = "Debe ingresar un número.")]
        public int NumeroCheque { get; set; }

        public Nullable<int> IdBancoCuenta { get; set; }

        public Nullable<System.DateTime> Fecha { get; set; }

        [Display(Name = "Importe"), Required(ErrorMessage = "Debe ingresar un valor.")]
        public decimal Importes { get; set; }

        public Nullable<int> IdProveedor { get; set; }

        public string NumeroRecibo { get; set; }

        [Display(Name = "Fecha Ingreso")]

        public DateTime FechaIngreso { get; set; }
   
        public DateTime FechaEgreso { get; set; }

        public string Destino { get; set; }

        public Nullable<int> NumeroOperacion { get; set; }

        public Nullable<int> IdMoneda { get; set; }

        public string Registro { get; set; }

        public Nullable<bool> Usado { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public BancoCuentaModelView BancoCuenta { get; set; }

        public TipoMonedaModelView TipoMoneda { get; set; }



        //propiedad agregada

        public int idCuentaBancariaSeleccionada { get; set; }
        public bool seleccionado { get; set; }

    }
}