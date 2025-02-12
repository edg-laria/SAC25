using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class Afip_TicketAccesoModel
    {
        public int id { get; set; }
        public string uniqueId { get; set; }
        public Nullable<System.DateTime> fecha_solicitud { get; set; }
        public Nullable<System.DateTime> fecha_expiracion { get; set; }
        public string servicio { get; set; }
        public string token { get; set; }
        public string sing { get; set; }
        public string url { get; set; }
        public Nullable<int> usuario { get; set; }
    }
}
