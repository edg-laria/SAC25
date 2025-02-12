using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class ConfigMenuSidebarModelView
    {

        public IEnumerable<MenuSideBarModelView> IEmenuSideBar { get; set; }
        public ICollection<AccionModelView> ICaccion { get; set; }
        public MenuSideBarModelView menuSideBar { get; set; }
    }

}
