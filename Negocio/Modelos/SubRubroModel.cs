using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public  class SubRubroModel
    {

        public int Id { get; set; }

        public string codigo { get; set; }

        public string Descripcion { get; set; }

        public Nullable<int> IdRubro { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }


        public virtual ICollection<Imputacion> Imputacion { get; set; }

        public virtual Rubro Rubros { get; set; }

    }
}
