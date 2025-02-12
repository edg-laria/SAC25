using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class CajaSaldoModel

    {

        public int Id { get; set; }
        public int NumeroCierrre { get; set; }
        public decimal ImporteInicialPesos { get; set; }
        public Nullable<decimal> ImporteInicialDolares { get; set; }
        public decimal ImporteInicialCheques { get; set; }
        public decimal ImporteInicialTarjetas { get; set; }
        public decimal ImporteInicialDepositos { get; set; }
        public decimal ImporteFinalPesos { get; set; }
        public decimal ImporteFinalDolares { get; set; }
        public decimal ImporteFinalCheques { get; set; }
        public decimal ImporteFinalTarjetas { get; set; }
        public decimal ImporteFinalDepositos { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

         public virtual ICollection<Caja> Caja { get; set; }





    }

}
