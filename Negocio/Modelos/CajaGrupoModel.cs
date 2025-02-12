using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class CajaGrupoModel

    {

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public Nullable<decimal> TotalPesos { get; set; }
        public Nullable<decimal> TotalDolares { get; set; }
        public Nullable<decimal> TotalCheques { get; set; }
        public Nullable<decimal> TotalTarjetas { get; set; }
        public Nullable<decimal> TotalDepositos { get; set; }
        public Nullable<decimal> ParcialPesos { get; set; }
        public Nullable<decimal> ParcialDolares { get; set; }
        public Nullable<decimal> ParcialCheques { get; set; }
        public Nullable<decimal> ParcialTarjetas { get; set; }
        public Nullable<decimal> ParcialDepositos { get; set; }
        public Nullable<int> IdImputacion { get; set; }
        public Nullable<bool> NoBorrar { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

          public  ICollection<Caja> Caja { get; set; }
    }

}
