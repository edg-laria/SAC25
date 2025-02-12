using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class AfipRegimenModel
    {

        public int Id { get; set; }

        public string Descripcion { get; set; }

        public string Concepto { get; set; }

        public Nullable<decimal> Aliri { get; set; }

        public Nullable<decimal> Alirni { get; set; }

        public Nullable<decimal> Minimo { get; set; }

        public Nullable<byte> Imputacion { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }


        public virtual ICollection<Proveedor> Proveedor { get; set; }

    }
}
