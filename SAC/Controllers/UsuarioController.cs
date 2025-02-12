using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Atributos;
using SAC.Models;
using AutoMapper;
using Negocio.Modelos;

namespace SAC.Controllers
{
    public class UsuarioController : BaseController
    {

        private readonly ServicioUsuarios servicioUsuario;

        private ServicioConfiguracion servicioConfiguracion = new ServicioConfiguracion();

        public UsuarioModelView usuario = new UsuarioModelView();

        public UsuarioController()
        {
            servicioUsuario = new ServicioUsuarios();
            servicioUsuario._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
        }


        // GET: Usuario
        [AutorizacionDeSistema]
        public ActionResult Index()
        {
            List<UsuarioModelView> usuarioModelView = Mapper.Map<List<UsuarioModel>, List<UsuarioModelView>>(servicioUsuario.GetAllUsuario());
            return View(usuarioModelView);
        }


        private SelectList selectListRoles()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var roles = servicioConfiguracion.GetAllRoles();
            foreach (var item in roles)
            {
                dic.Add(item.IdRol, item.descripcion);
            }
            return new SelectList(dic, "Key", "Value");
        }



        public ActionResult PerfilUsuario()
        {
            UsuarioModel usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
          
            UsuarioModelView _usuarioModelView;
            if (usuario != null)
            {
            
                _usuarioModelView = Mapper.Map<UsuarioModel, UsuarioModelView>(servicioUsuario.ObtenerPorID(usuario.IdUsuario));
                        
            _usuarioModelView.Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetAllRoles());

            PerfilUsuarioViewModel perfilUsuarioViewModel = new PerfilUsuarioViewModel();
            perfilUsuarioViewModel.Usuario = _usuarioModelView;

            perfilUsuarioViewModel.ResetPassword = new ResetPasswordViewModel();   
            perfilUsuarioViewModel.ResetPassword.idUsuario = usuario.IdUsuario;
                return View(perfilUsuarioViewModel);

            }
            return RedirectToAction("Logout", "Cuenta");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Obtener el usuario de la base de datos (simulado aquí)
            var usuario = servicioUsuario.ObtenerPorID(model.idUsuario);

            if (usuario == null)
            {
                ModelState.AddModelError("", "El usuario no existe.");
                return View(model);
            }

            bool actualizado = servicioUsuario.CambiarPassword(usuario.IdUsuario,model.NewPassword);

            if (actualizado)
            {
                return RedirectToAction("Logout", "Cuenta");
            }

            ModelState.AddModelError("", "Hubo un problema al restablecer la contraseña.");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarPerfilUsuario(UsuarioModelView model)
        {
            // Lógica para actualizar los datos del usuario y la persona
            if (ModelState.IsValid)
            {
                UsuarioModel usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];               
                if (usuario != null)
                {   
                    model.idUsuarioLogin = usuario.IdUsuario;              
                    servicioUsuario.UpdateUsuario(Mapper.Map<UsuarioModelView, UsuarioModel>(model));
                }
               
            }
            return RedirectToAction(nameof(PerfilUsuario));
                   
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            UsuarioModelView model;
            if (id == 0)
            {
                model = new UsuarioModelView();               
            }
            else
            {
                model = Mapper.Map<UsuarioModel, UsuarioModelView>(servicioUsuario.ObtenerPorID(id));
                            
            }      
            model.Roles = Mapper.Map<List<RolModel>,List<RolModelView>>(servicioConfiguracion.GetAllRoles());
            return View(model);                 
        }
       
        [AutorizacionDeSistema]
        [HttpPost]       
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(UsuarioModelView model)
        {
            //bool hasErrors = ViewData.ModelState.Values.Any(x => x.Errors.Count > 1);
            //foreach (ModelState state in ViewData.ModelState.Values.Where(x => x.Errors.Count > 0))
            //{
            //    servicioConfiguracion._mensaje(state.Value.ToString(), "ok");
            //}
            if (ModelState.IsValid)
            {
                UsuarioModel usuario = (UsuarioModel) System.Web.HttpContext.Current.Session["currentUser"];
                model.idUsuarioLogin = usuario.IdUsuario;
                if (model.idUsuario == null) {                   
                    servicioUsuario.CreateUsuario(Mapper.Map< UsuarioModelView, UsuarioModel>(model));
                }
                else {
                    servicioUsuario.UpdateUsuario(Mapper.Map<UsuarioModelView, UsuarioModel>(model));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpPost]
        [AutorizacionDeSistema]
        [ValidateAntiForgeryToken]
        public ActionResult RestablecerCuenta(int id )
        {           
                UsuarioModel usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];               
                if (id > 0)
                {
                    servicioUsuario.RestablecerCuenta(id , usuario.IdUsuario);
                }                
                return RedirectToAction(nameof(Index));           
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult RestablecerCuenta(int id)
        //{
        //    UsuarioModel usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

        //    if (id > 0)
        //    {
        //        servicioUsuario.RestablecerCuenta(id, usuario.IdUsuario);
        //    }
        //    return RedirectToAction(nameof(Index));
        //}



        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [AutorizacionDeSistema]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    
    
    }
}
