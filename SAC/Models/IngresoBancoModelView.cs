using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class IngresoBancoModelView
    {
       
        public int IdBancoCuenta { get; set; }
        public int IdNroCierre { get; set; }

        public String Fecha { get; set; }

        public string IdConciliacionMovimiento { get; set; }

        public decimal Cotizacion { get; set; }
        public decimal SaldoConciliado { get; set; }
        public decimal Saldo { get; set; }

        public BancoCuentaModelView BancoCuenta { get; set; }
        public BancoCuentaBancariaModelView Ingresos { get; set; }

        public List<BancoCuentaBancariaModelView> ListaBancoCuenta { get; set; }
        public List<BancoCuentaBancariaModelView> ListaBancoCuentaDestino { get; set; }

    }
}