using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class ValorCotizacionModel   
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public bool Activo { get; set; }
        public int IdTipoMoneda { get; set; }
        public int IdUsuario { get; set; }
        public DateTime UltimaModificacion { get; set; }
    }
}
