using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class AccionModel
    {

        public int IdAccion { get; set; }
        public string Controlador { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdModulo { get; set; }
        public  ICollection<AccionPorRol> AccionPorRol { get; set; }
    }

}
