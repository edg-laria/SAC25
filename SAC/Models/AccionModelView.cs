using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class AccionModelView
    {
        public int IdAccion { get; set; }
        public string Controlador { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdModulo { get; set; }
        
    }

}
