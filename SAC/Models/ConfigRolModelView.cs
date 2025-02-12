using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class ConfigRolModelView
    {
        public RolModelView Rol { get; set; }
        public ICollection<RolModelView> Roles { get; set; }    
        public ICollection<MenuSideBarModelView> Menusidebar { get; set; }

    }

}
