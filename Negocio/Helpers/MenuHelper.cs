using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad.Modelos;
using Negocio.Modelos;
using Negocio.Servicios;
namespace Negocio.Helpers
{
 public  class MenuHelper
    {
        public List<MenuItemModel> ObtenerMenu(int IdUsuario)
        {
            var menuItems = new MenuItemModel[4] {
                                new MenuItemModel() { Icono = "fa-th-list", Url = "Evento", Metodo = "Index" , Controller = "Evento", Titulo = "Eventos" },
                                new MenuItemModel() { Icono = "fa-calendar", Url = "Algo", Metodo = "Calendario", Controller = "Evento", Titulo = "Calendario" },
                                 new MenuItemModel() { Icono = "fa-cogs", Url = "Algo", Metodo = "Index", Controller = "Usuario", Titulo = "Usuarios " },
                                new MenuItemModel() { Icono = "fa-cogs", Url = "Algo", Metodo = "Index", Controller = "Configuracion", Titulo = "Configuración " }
                                };
           
            ServicioUsuarios servicio = new ServicioUsuarios();
            return servicio.ObtenerMenuUsuario(IdUsuario, menuItems);
        }

        public List<MenuSideBarModel> ObtenerMenuSidebar(int IdUsuario)
        {
            //ServicioConfiguracion servicio = new ServicioConfiguracion();
            //return servicio.GetMenuSidebar(IdUsuario);
            ServicioUsuarios servicio = new ServicioUsuarios();
            return servicio.ObtenerMenuUsuario(IdUsuario);
        }
    }

}
