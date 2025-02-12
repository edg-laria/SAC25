using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class AccionPorRolModelView
    {
        public int idRolPorAccion { get; set; }
        public int idRol { get; set; }
        public int idAccion { get; set; }
        public  AccionModelView Accion { get; set; }
        public  RolModelView Rol { get; set; }
    }
}

