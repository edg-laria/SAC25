

using Negocio.Modelos;
using System.Collections.Generic;

namespace SAC.Models
{
    public class MenuSideBarModelView
    {
        public int IdMenuSidebar { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public string Titulo { get; set; }

        public int? IdParent { get; set; }
        public int? IdAccion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaModificacion { get; set; }
        public int? Orden { get; set; }
        public virtual AccionModel Accion { get; set; }
        
        public virtual ICollection<MenuSideBarModelView> Group { get; set; }

    }
}