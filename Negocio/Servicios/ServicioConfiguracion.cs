using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using AutoMapper;
using Negocio.Modelos;

namespace Negocio.Servicios
{   
    public class ServicioConfiguracion : ServicioBase
    {
        private RolRepositorio rolRepositorio;
        private AccionRepositorio accionRepositorio;
        private MenuSidebarRepositorio menuSidebarRepositorio;
       
        public ServicioConfiguracion()
        {
            rolRepositorio = kernel.Get<RolRepositorio>();
            accionRepositorio = kernel.Get<AccionRepositorio>();
            menuSidebarRepositorio = kernel.Get<MenuSidebarRepositorio>();

        }
        /// <summary>
        ///  meodos para gestion de acciones
        /// </summary>
        /// <returns></returns>
        public List<Modelos.AccionModel> GetAccion()
        {           
            return Mapper.Map < List<Accion>,  List<Modelos.AccionModel>>(accionRepositorio.GetAccion());    
        }    
        public AccionModel CreateAccion(Modelos.AccionModel Accion)
        {          
            try
            {
                Accion acc = Mapper.Map<Modelos.AccionModel, Accion>(Accion);
                acc.Nombre = Accion.Nombre.ToLower();
                acc.Activo = true;
                acc.fechaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                _mensaje?.Invoke("Se guardo Correctamente", "ok");
                return Mapper.Map<Accion, AccionModel>(accionRepositorio.CreateAccion(acc));         
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }
        public AccionModel GetAccionPorId(int idAccion)
        {
            return Mapper.Map < Accion, Modelos.AccionModel > (accionRepositorio.GetAccionPorId(idAccion));
        }
        public AccionModel ActualizarAccion(AccionModel Accion)
        {

            try
            {   
                Accion AccionParaActualizar = Mapper.Map<Modelos.AccionModel, Accion>(Accion);
                _mensaje?.Invoke("Se Actualizo Correctamente", "ok");
            return Mapper.Map<Accion, Modelos.AccionModel>(accionRepositorio.ActualizarAccion(AccionParaActualizar));
           
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
         
        }      
        public void DeleteAccion(int idAccion)
        {
            try
            {
                accionRepositorio.DeleteAccion(idAccion);

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");               
            }
            
        }

       
       
        /// <summary>
        ///  meodos para gestion de roles
        /// </summary>
        /// <returns></returns>
        public List<Modelos.RolModel> GetAllRoles()
        {
            try
            {
                return Mapper.Map<List<Rol>, List<Modelos.RolModel>>(rolRepositorio.GetAllRol());
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
                   
        }    
        public Modelos.RolModel CrearRol(Modelos.RolModel rol)
        {
            try
            { 
                Rol p = Mapper.Map<Modelos.RolModel, Rol>(rol);
                _mensaje?.Invoke("Se Guardo Correctamente", "ok");
                return Mapper.Map<Rol, Modelos.RolModel>(rolRepositorio.Insertar(p));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
           
        }
        public RolModel GetRolPorId(int idRol)
        {
            return Mapper.Map<Rol, Modelos.RolModel>(rolRepositorio.GetRolPorId(idRol));
        }

      
        public void ActualizarRol(RolModel rol)
        {
            try
            {  
                rolRepositorio.ActualizarRol(Mapper.Map<Modelos.RolModel, Rol>(rol));
                _mensaje?.Invoke("Se Actualizo Correctamente", "ok");
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
               
            }
         
        }
        public void InsertarAccionPorRol(AccionPorRolModel accionPorRol)
        {
            rolRepositorio.InsertarAccionPorRol(Mapper.Map< AccionPorRolModel, AccionPorRol>(accionPorRol));
        }

        public List<AccionModel> GetAccionNoMenu(int idRol)
        {
            var a = accionRepositorio.GetAccionNoMenu(idRol);
            return Mapper.Map<List<Accion>, List<AccionModel>>(a);
        }

        public List<AccionPorRolModel> GetAllAccionPorRol(int idRol)
        {
            return Mapper.Map<List<AccionPorRol>, List<AccionPorRolModel>>(accionRepositorio.GetAllAccionPorRol(idRol));
        }

        public AccionPorRolModel GetAccionPorRol(int idRol,int idAccion)
        {
            return Mapper.Map<AccionPorRol, AccionPorRolModel>(accionRepositorio.GetAccionPorRol(idRol, idAccion));
        }

        public List<MenuSideBarModel> GetMenuSidebarPorRol(int idRol)
        {
            return Mapper.Map<List<MenuSidebar>, List<MenuSideBarModel>>(menuSidebarRepositorio.GetMenuSidebarPorRol(idRol));
        }

        
        public List<AccionModel> GetAccionesEnMenuSidebarPorRol(int idRol)
        {
            return Mapper.Map<List<Accion>, List<AccionModel>>(menuSidebarRepositorio.GetAccionesEnMenuSidebarPorRol(idRol));
        }

        public RolModel DeleteAccionPorRol(int idRolPorAccion)
        {
            return Mapper.Map<Rol, Modelos.RolModel>(rolRepositorio.DeleteAccionPorRol(idRolPorAccion));           
        }
       
        /// <summary>
        ///  meodos para gestion del MenuSideBar
        ///  treeview
        /// </summary>
        /// <returns></returns>
        public List<MenuSideBarModel> GetMenuSidebar()
        {
          return  Mapper.Map<List<MenuSidebar>, List<MenuSideBarModel>>(menuSidebarRepositorio.GetMenuSidebar());          
        }


        public MenuSideBarModel GetMenuSidebarPorId(int id)
        {
            
            try
            {
                var menuSideBarModel = Mapper.Map<MenuSidebar, MenuSideBarModel>(menuSidebarRepositorio.GetMenuSidebarPorId(id));
                return menuSideBarModel;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }
        public MenuSideBarModel GetMenuSidebarPorIdFull(int id)
        {
            try
            {
                var menuSideBarModel = Mapper.Map<MenuSidebar, MenuSideBarModel>(menuSidebarRepositorio.GetMenuSidebarPorIdFull(id));
                return menuSideBarModel;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }
      
        public List<AccionModel> GetAccionEnMenuSide(int idmenusider)
        {
            try
            {               
                var AccionesEnMenu = Mapper.Map<List<Accion>, List<AccionModel>>(menuSidebarRepositorio.GetAccionEnMenuSide(idmenusider));
                return AccionesEnMenu;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }

        public List<MenuSideBarModel> GetMenuSidebar(int IdUsuario)
        {         
            try
            {
                var lista = Mapper.Map<List<MenuSidebar>, List<MenuSideBarModel>>(menuSidebarRepositorio.GetMenuSidebar());
                return lista;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }

        }

        public MenuSideBarModel CreateMenusidebar(MenuSideBarModel sidebar)
        {          
            try
            {
                MenuSidebar menu = Mapper.Map<MenuSideBarModel, MenuSidebar>(sidebar);
        _mensaje?.Invoke("Se Guardo Correctamente", "ok");
                return Mapper.Map<MenuSidebar, MenuSideBarModel>(menuSidebarRepositorio.Insertar(menu));        
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }

        }
        public void ActualizarMenusidebar(MenuSideBarModel menuSideBarModel)
        {           
            try
            {
                MenuSidebar menu = Mapper.Map<MenuSideBarModel, MenuSidebar>(menuSideBarModel);               
                menuSidebarRepositorio.ActualizarMenusidebar(menu);   
                 _mensaje?.Invoke("Se Actualizo Correctamente", "ok");
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");              
            }
        }

        public void DeleteMenusidebar(int id)
        {
                menuSidebarRepositorio.DeleteMenusidebar(id);          
        }
      
       
    }

}

