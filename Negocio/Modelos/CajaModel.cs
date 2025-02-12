using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class CajaModel

    {

        public int Id { get; set; }
        public Nullable<int> IdTipoMovimiento { get; set; }
        public string Concepto { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Tipo { get; set; }
        public string Saldo { get; set; }
        public Nullable<int> IdGrupoCaja { get; set; }
        public Nullable<decimal> ImportePesos { get; set; }
        public string Recibo { get; set; }
        public Nullable<decimal> ImporteDeposito { get; set; }
        public Nullable<int> IdCuentaBanco { get; set; }
        public Nullable<decimal> ImporteDolar { get; set; }
        public Nullable<decimal> ImporteTarjeta { get; set; }
        public Nullable<decimal> ImporteCheque { get; set; }

        public Nullable<int> IdTarjeta { get; set; }
        public Nullable<int> IdCheque { get; set; }

        public Nullable<int> IdCajaSaldo { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public virtual GrupoCaja GrupoCaja { get; set; }



        public virtual CajaTipoMovimiento  CajaTipoMovimiento { get; set; }


      


    }

}
