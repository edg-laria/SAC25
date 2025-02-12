using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class ConfigAccionModelView
    {

       public AccionModelView Accion { get; set; }
        public ICollection<AccionModelView> Acciones { get; set; }
       
             
    }

}
