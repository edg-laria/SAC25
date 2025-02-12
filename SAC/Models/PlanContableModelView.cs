using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class PlanContableModelView
    {

        public IEnumerable<MenuSideBarModelView> IEmenuSideBar { get; set; }
       
        public CuentaPlanContableModelView cuentaContable { get; set; }
    }

}
