using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Models
{
    public class RolModelView
    {
        public int idRol { get; set; }
        public string descripcion { get; set; }
        public bool esAdministrador { get; set; }
        public int? idHome { get; set; }
        public ICollection<AccionPorRolModelView> AccionPorRol { get; set; }
        public ICollection<AccionModelView> Acciones { get; set; }      
        public int idAccionPorRol { get; set; }

        public string MenuSidePorRol { get; set; }
    }

}
