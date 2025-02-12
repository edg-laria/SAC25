using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Entidad.Modelos;
using Z.EntityFramework.Plus;
namespace Datos.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>
        , IUsuarioRepositorio
        , IRepositorio<Usuario>
    {
        private SAC_Entities contexto;

        public UsuarioRepositorio(SAC_Entities contextoDeDatos) : base(contextoDeDatos)
        {
            this.contexto = contextoDeDatos;
        }

        public void ActualizarRolDeUsaurio(int idUsuario, int idRol, int idUsuarioLogueado)
        {
            Usuario usuario = ObtenerPorID(idUsuario);
            usuario.IdRol = idRol;            
            contexto.Entry(usuario).State = EntityState.Modified;
            contexto.SaveChanges();
        }

     
        public void CambiarPassword(int idUsuario, string password)
        {
            Usuario usuario = ObtenerPorID(idUsuario);
            usuario.Password = password;
            contexto.Entry(usuario).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object logLogin(int idUsuario, string IP)
        {
            throw new NotImplementedException();
        }

        public bool Obtener(string usuario, string password, int idRolInvitado)
        {
            return contexto.Usuario.Any(x =>
                                           (x.UserName == usuario || x.Persona.Email == usuario) &&
                                           x.Password == password &&
                                           x.IdRol != idRolInvitado
                                       );

            //return !( contexto.Usuario
            //                  .Where(x => x.UserName == usuario || x.Persona.Email == usuario
            //                  &&  x.Password  == password 
            //                  && x.IdRol != idRolInvitado).Count() == 0);           
        }
        public Usuario ObtenerUsuarioPorUserNameEmail(String usuariologin)
        {
            Usuario usuario = contexto.Usuario
                             .Include(x => x.Persona)
                             .Include(x => x.Rol)
                             .Where(x => x.UserName.Equals(usuariologin) || x.Persona.Email.Equals(usuariologin)).FirstOrDefault();
            return usuario;
        }
        public List<Usuario> GetAllUsuario()
        {
            return contexto.Usuario
                   .Include(x => x.Rol)
                   .Include(x => x.Persona)
                   .Where(x => x.Activo == true).ToList();
        }


        List<MenuItemModel> IUsuarioRepositorio.ObtenerMenuUsuario(int idUsuario, MenuItemModel[] menuItems)
        {
            var items = contexto.Usuario
                          .Where(x => x.IdUsuario == idUsuario)                         
                          .Include(x => x.Rol)
                          .Include(x => x.Rol.AccionPorRol)
                          .Include(x => x.Rol.AccionPorRol.Select(AccPorRol => AccPorRol.Accion))
                          .Select(x => x.Rol.AccionPorRol)
                          .Select(accionPorRol => accionPorRol.Select(acc => acc.Accion.Controlador.ToLower() + acc.Accion.Nombre.ToLower())).FirstOrDefault().ToArray();
             //.Include(x => x.Persona)
            return menuItems.Where(item => items.Contains(item.Controller.ToLower() + item.Metodo.ToLower())).ToList();
        }
     
        List<MenuSidebar> IUsuarioRepositorio.ObtenerMenuUsuario(int idUsuario)
        {
            var items = contexto.Usuario
                          .Where(x => x.IdUsuario == idUsuario)
                          .Include(x => x.Rol)
                          .Include(x => x.Rol.AccionPorRol)
                          .Include(x => x.Rol.AccionPorRol.Select(AccPorRol => AccPorRol.Accion))
                          .Select(x => x.Rol.AccionPorRol)
                          .Select(accionPorRol => accionPorRol.Select(acc => acc.Accion.Controlador.ToLower() + acc.Accion.Nombre.ToLower())).FirstOrDefault().ToArray();
            
            var side = contexto.MenuSidebar
                                 .Include(m => m.Accion)
                                 .IncludeFilter(m => m.MenuSidebar1.Where(p => p.Activo == true).OrderBy(i=>i.Orden))
                                 .Where(menu => menu.Activo == true 
                                            && menu.IdParent == null
                                            && items.Contains(menu.Accion.Controlador.ToLower() + menu.Accion.Nombre.ToLower()))
                                 .OrderBy(m => m.Orden).ToList();
            return side;
        }

        public Usuario ObtenerPorDocumento(String documento)
        {
           Usuario usuario = contexto.Usuario
                            .Include(x => x.Persona)
                            .Include(x => x.Rol)
                             .Where(x => x.Persona.Documento == documento).FirstOrDefault();
            return usuario;
        }

        public Usuario ObtenerPorID(int idUsuario)
        {
            Usuario usuario = contexto.Usuario
                            .Include(x => x.Persona)
                            .Include(x => x.Rol)
                             .Where(x => x.IdUsuario == idUsuario).FirstOrDefault();

            return usuario;
        }

        public Rol ObtenerRol(int idUsuario)
        {
            Usuario usuario = contexto.Usuario
                            .Include(x => x.Persona)
                            .Include(x => x.Rol)
                            .Where(x => x.IdUsuario == idUsuario).FirstOrDefault();

            return usuario.Rol;
        }

        public void CreateUsuario(Usuario usuario)
        {
             Insertar(usuario);
        }
       
        public void UpdateUsuario(Usuario model)
        {
          
            contexto.Usuario.Attach(model);

            contexto.Entry(model).Property(x => x.UserName).IsModified = true;
            contexto.Entry(model).Property(x => x.IdRol).IsModified = true;
            contexto.Entry(model).Property(x => x.Activo).IsModified = true;

            contexto.Persona.Attach(model.Persona);
            contexto.Entry(model.Persona).Property(x => x.Email).IsModified = true;
            contexto.Entry(model.Persona).Property(x => x.Documento).IsModified = true;
            contexto.Entry(model.Persona).Property(x => x.Nombre).IsModified = true;
            contexto.Entry(model.Persona).Property(x => x.Apellido).IsModified = true;
            contexto.Entry(model.Persona).Property(x => x.Cuil).IsModified = true;
            contexto.Entry(model.Persona).Property(x => x.TelefonoMovil).IsModified = true;
            contexto.Entry(model.Persona).Property(x => x.Activo).IsModified = true;

            contexto.SaveChanges();

        }



    }
}