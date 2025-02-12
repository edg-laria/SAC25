using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;
using Z.EntityFramework.Plus;

namespace Datos.Repositorios
{
    public class MenuSidebarRepositorio : RepositorioBase<MenuSidebar>
    {
       private SAC_Entities context;
    
        public MenuSidebarRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

       
        /// <summary>
        /// MenuSidebar
        /// </summary>
        public List<MenuSidebar> GetMenuSidebar()
        {
         
            var side = context.MenuSidebar.
                           IncludeFilter(m => m.MenuSidebar1.Where(p => p.Activo == true))
                           .Where(menu => menu.Activo == true && menu.IdParent == null)
                           .OrderBy(acc => acc.Orden).ToList();
            return side;
        }
      
      

        /// <summary>
        /// MenuSidebar
        /// </summary>
        public List<MenuSidebar> GetMenuSidebar(int IdUsuario)
        {
            var side = context.MenuSidebar.
                            IncludeFilter(m => m.MenuSidebar1.Where(p => p.Activo == true))
                            .Where(menu => menu.Activo == true && menu.IdParent == null)
                            .OrderBy(acc => acc.Titulo).ToList();
            return side;
        }


        public MenuSidebar CrearMenusidebar(MenuSidebar menusidebar)
        {
          return Insertar(menusidebar);         
        }

        public void DeleteMenusidebar(int id)
        {
            MenuSidebar menusidebar = GetMenuSidebarPorId(id);
            if (menusidebar !=  null) { 
                menusidebar.Activo = false;
                menusidebar.FechaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                context.SaveChanges();
            }
        }

        public MenuSidebar GetMenuSidebarPorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = true;
                return context.MenuSidebar
                        .Where(menu => menu.IdMenuSidebar == id && menu.Activo == true).FirstOrDefault();
        }
        public MenuSidebar GetMenuSidebarPorIdFull(int id)
        {
            return context.MenuSidebar
                    .Include(m => m.Accion)
                    .Include(m => m.MenuSidebar1)
                    .Where(menu => menu.IdMenuSidebar == id
                    && menu.Activo == true).FirstOrDefault();
        }

        public List<Accion> GetAccionEnMenuSide(int idmenusider)
        {
            var acciones= ( from a in context.Accion
                            join m in context.MenuSidebar
                                    on new { a.IdAccion, Activo = true }
                                equals new { IdAccion = (int)(m.IdAccion), m.Activo }
                            where  m.IdMenuSidebar == idmenusider
                            select a ).ToList();
            return acciones;
        }

        public void ActualizarMenusidebar(MenuSidebar menuSideBarModel)
        {

         //1 update completo
            //context.Entry(menuSideBarModel).State = EntityState.Modified;
            //context.SaveChanges();

         //2 casero jaja
            //MenuSidebar menu = GetMenuSidebarPorId(menuSideBarModel.IdMenuSidebar);
            // menu.Controlador = AccionParaActualizar.Controlador ?? menu.Controlador;          
            //context.SaveChanges();

            // modifica pero no retornaria datos //si mencionas al diablo se te puede aparecer
            context.MenuSidebar.Attach(menuSideBarModel);
            context.Entry(menuSideBarModel).Property(x => x.Icono).IsModified = true;
            context.Entry(menuSideBarModel).Property(x => x.Titulo).IsModified = true;
            context.Entry(menuSideBarModel).Property(x => x.Url).IsModified = true;
            context.Entry(menuSideBarModel).Property(x => x.Orden).IsModified = true;
            context.Entry(menuSideBarModel).Property(x => x.IdParent).IsModified = true;
            context.Entry(menuSideBarModel).Property(x => x.IdAccion).IsModified = true;
            context.SaveChanges();

            //MenuSidebar menu = GetMenuSidebarPorId(menuSideBarModel.IdMenuSidebar);
            //return menu;
        }

        public List<MenuSidebar> GetMenuSidebarPorRol(int idRol)
        {
          
            List<MenuSidebar> menuSidebar = (from m in context.MenuSidebar
                                             join a in context.Accion on m.IdAccion equals a.IdAccion
                                             join apr in context.AccionPorRol on a.IdAccion equals apr.idAccion
                                             where apr.idRol == idRol && m.Activo == true
                                             orderby m.Orden
                                             select m).ToList();


            return menuSidebar;
        }


        public List<Accion> GetAccionesEnMenuSidebarPorRol(int idRol)
        {

            List<Accion> AccionMenuSidebar = (from m in context.MenuSidebar
                                             join a in context.Accion on m.IdAccion equals a.IdAccion
                                             join apr in context.AccionPorRol on a.IdAccion equals apr.idAccion
                                             where apr.idRol == idRol && m.Activo == true
                                             orderby m.Orden
                                             select a).ToList();
            return AccionMenuSidebar;
        }

    }
}