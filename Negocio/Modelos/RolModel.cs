using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class RolModel
    {
        public int IdRol { get; set; }
        public string descripcion { get; set; }
        public bool EsAdministrador { get; set; }
        public int? IdHome { get; set; }
        public ICollection<AccionPorRol> AccionPorRol { get; set; }

        public Dictionary<string, List<string>> Acciones { get; set; }

        public string Controller { get; set; }

        public string Metodo { get; set; }
    }

}
