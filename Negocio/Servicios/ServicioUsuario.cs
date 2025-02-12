using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;

namespace Negocio.Servicios
{

    public class ServicioUsuarios : ServicioBase, IServicioUsuarios
    {
        private IUsuarioRepositorio repositorio { get; set; }      
        public ServicioUsuarios()
        {
            repositorio = kernel.Get<IUsuarioRepositorio>();
        }
     
        
        public UsuarioModel Agregar(UsuarioModel usuario)
        {
            try
            {
               var usuarioDTO = Mapper.Map<UsuarioModel, Usuario>(usuario);
            usuarioDTO = repositorio.Insertar(usuarioDTO);
            usuario.IdUsuario = usuarioDTO.IdUsuario;
            return usuario;
            }
            catch (Exception)
            {
               _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
           
        }
        public IEnumerable<UsuarioModel> Obtener()
        {
            try
            {
          
                var usuarios = from usuario in repositorio.Obtener()
                               select new UsuarioModel();
                return usuarios;
            }
            catch (Exception)
            {
               _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
         
        }

        public List<UsuarioModel> GetAllUsuario()
        {
            try
            {
                var usuarios = Mapper.Map< List<Usuario>, List<UsuarioModel> >(repositorio.GetAllUsuario());
                return usuarios;
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }

        }


        public UsuarioModel ObtenerPorID(int idUsuario)
        {
            try
            {
                var usuario = Mapper.Map<Usuario, UsuarioModel>(repositorio.ObtenerPorID(idUsuario));
                //UsuarioModel usuarioModel = new UsuarioModel() { IdUsuario = usuario.IdUsuario };

                return usuario;
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
            
        }
        public UsuarioModel ObtenerUsuario(string usuarioLogin, int IdModulo)
        {

            try
            {
            var usuario = repositorio.ObtenerUsuarioPorUserNameEmail(usuarioLogin);

            UsuarioModel usuarioModel = new UsuarioModel() { IdUsuario = usuario.IdUsuario
                                                            , UserName =  usuario.Persona.Email                                                          
                                                            };

            var rolModel = Mapper.Map<Rol, RolModel>(usuario.Rol);
            rolModel.Acciones = new Dictionary<string, List<string>>();
            foreach (var accion in usuario.Rol.AccionPorRol)
            {
                //if (accion.Accion.IdModulo == IdModulo)
                //    continue;

                if (!rolModel.Acciones.ContainsKey(accion.Accion.Controlador))
                    rolModel.Acciones.Add(accion.Accion.Controlador, new List<string>());

                rolModel.Acciones[accion.Accion.Controlador].Add(accion.Accion.Nombre);
            }

            usuarioModel.EsAdministrador = rolModel.EsAdministrador;
            usuarioModel.CargarRoles(rolModel);
            return usuarioModel;
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }         
        }

      

        public bool Obtener(string usuario, string password, int idRolInvitado)
        {
            try
            {
                var resp = this.repositorio.Obtener(usuario, StringHelper.ObtenerMD5(password), idRolInvitado);
                if (resp)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
               _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return false;
            }
           
        }

        public void UpdateUsuario(UsuarioModel usuarioModel)
        {
            try
            {               
                usuarioModel.Actualizado = Convert.ToDateTime(DateTime.Now.ToString());
                usuarioModel.Persona.FechaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                usuarioModel.Persona.Activo = usuarioModel.Activo;
                repositorio.UpdateUsuario(Mapper.Map<UsuarioModel, Usuario>(usuarioModel));
                _mensaje?.Invoke("Se registro correctamente", "ok");
               
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador " + ex.Message, "error");
               
            }
        }

        public void CreateUsuario(UsuarioModel usuarioModel)
        {
            try
            {
                usuarioModel.Actualizado = Convert.ToDateTime(DateTime.Now.ToString());
                usuarioModel.Creado = Convert.ToDateTime(DateTime.Now.ToString());                           
                usuarioModel.Password = StringHelper.ObtenerMD5("12345678");
                usuarioModel.Persona.FechaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                usuarioModel.Persona.Activo = usuarioModel.Activo;
                repositorio.CreateUsuario(Mapper.Map < UsuarioModel, Usuario>( usuarioModel));
                _mensaje?.Invoke("Se registro correctamente", "ok");
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
             
            }


        }

        public void ActualizarRolDeUsaurio(int idUsuario, int idRol, int idUsuarioLogueado)
        {
          try
            {
                repositorio.ActualizarRolDeUsaurio(idUsuario, idRol, idUsuarioLogueado);         
                _mensaje?.Invoke("Se Actualizo correctamente", "ok");
            }
            catch (Exception ex)
            {
               _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
               
            }
            
        }
        public List<MenuItemModel> ObtenerMenuUsuario(int idUsuario, MenuItemModel[] menuItems)
        {
            try
            {
                return repositorio.ObtenerMenuUsuario(idUsuario, menuItems);               
            }
            catch (Exception ex)
            {
               _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
            
        }
        public List<MenuSideBarModel> ObtenerMenuUsuario(int idUsuario)
        {
            try
            {
                var lista = Mapper.Map<List<MenuSidebar>, List<MenuSideBarModel>>(repositorio.ObtenerMenuUsuario(idUsuario));
                return lista;
            }
            catch (Exception ex)
            { 
               _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }

        }

        public void RestablecerCuenta(int id, int idUsuario)
        {
            try
            {
                var passwordHasheado = StringHelper.ObtenerMD5("12345678");
                repositorio.CambiarPassword(id, passwordHasheado);
                _mensaje?.Invoke("Se Actualizo correctamente", "ok");
            }
            catch (Exception ex)
            {
               _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");

            }
        }
        /// <summary>
        /// all 2120225
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CambiarPassword(int idUsuario, string password)
        {
            try
            {
                var passwordHasheado = StringHelper.ObtenerMD5(password);
                repositorio.CambiarPassword(idUsuario, passwordHasheado);

                _mensaje?.Invoke("Se Actualizo correctamente", "ok");
                return true;
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return false;
            }
        }
       
        public RolModel ObtenerRol(int idUsuario)
        {
            try
            {
                return Mapper.Map<Rol, RolModel>(repositorio.ObtenerRol(idUsuario));
            }
            catch (Exception)
            {
               _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }


        }
        public void LogLogin(int idUsuario, string ip)
        {
            try
            {
                 repositorio.logLogin(idUsuario, ip);
               
            }
            catch (Exception)
            {
               _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");

            }
           
        }

      

    }

}
