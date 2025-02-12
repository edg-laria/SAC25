using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio.Modelos
{
   public class PresupuestoItemModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public Nullable<decimal> Pagado { get; set; }
        public int Periodo { get; set; }
        public string Ejecutado { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    }
}
