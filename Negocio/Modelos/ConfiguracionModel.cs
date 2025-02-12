using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class ConfiguracionModel
    {
        public ICollection<RolModel> IErol { get; set; }
        public ICollection<AccionModel> IEaccion { get; set; }
        public ICollection<MenuSideBarModel> IEmenusidebar { get; set; }

        public RolModel rol { get; set; }
        public AccionModel accion { get; set; }
        public MenuSideBarModel menuSideBar { get; set; }
    }

}
