using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Models
{
    public class PieNotaModelView
    {

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }
        public bool Visible { get; set; }
        public int IdBancoCuenta { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public virtual ICollection<ClienteModelView> Cliente { get; set; }

        public virtual BancoCuentaModelView BancoCuenta { get; set; }



    }

}
