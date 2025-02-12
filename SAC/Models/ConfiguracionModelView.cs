using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class ConfiguracionModelView
    {

        public ICollection<RolModelView> IErol { get; set; }
        public ICollection<AccionModelView> IEaccion { get; set; }
        public ICollection<MenuSideBarModelView> IEmenusidebar { get; set; }

        public RolModelView rol { get; set; }
        public AccionModelView accion { get; set; }
        public MenuSideBarModelView menuSideBar { get; set; }
    }

}
