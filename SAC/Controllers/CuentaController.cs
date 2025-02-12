using SAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio.Modelos;
using Negocio.Interfaces;
using Negocio.Servicios;
using Negocio.Helpers;
using System.Configuration;
using AutoMapper;
using Entidad.Modelos;
using SAC.Helpers;
using SAC.Atributos;

namespace SAC.Controllers
{
    public class CuentaController : BaseController
    {
        private readonly ServicioUsuarios servicioUsuario;
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private AfipHelper afipHelper = new AfipHelper();
        public CuentaController()
        {
            servicioUsuario = new ServicioUsuarios();
            servicioUsuario._mensaje = (msg_, tipo_)  => AddMessage(tipo_, msg_);
        }

        // GET: Cuenta
        public ActionResult Acceder()
        {

            if ((System.Web.HttpContext.Current.Session["currentUser"] != null))
            {
                if ((System.Web.HttpContext.Current.Session["controller"] != null & System.Web.HttpContext.Current.Session["metodo"] != null))
                    return RedirectToAction(System.Web.HttpContext.Current.Session["metodo"].ToString(), System.Web.HttpContext.Current.Session["controller"].ToString());
                else
                    return RedirectToAction("Acceder", "Cuenta");
            }

            var viewModel = new LoginViewModel();
            return View(viewModel);
        }


        [HttpPost, ActionName("Acceder")]
        [ValidateAntiForgeryToken]
        public ActionResult Acceder(LoginViewModel loginViewModel)
        {

            NLogHelper.Instance.Info("CuentaController", "Acceder", loginViewModel.Usuario);

            if (servicioUsuario.Obtener(loginViewModel.Usuario, loginViewModel.Password, Convert.ToInt32(ConfigurationManager.AppSettings["rolInvitado"])))
            {
                UsuarioModel usuario = servicioUsuario.ObtenerUsuario(loginViewModel.Usuario, 1);
                System.Web.HttpContext.Current.Session["currentUser"] = usuario;
                //guardo una cookie para reiniciar session
                HttpCookie ckUsuario = new HttpCookie("ckUsuario")
                {
                    Expires = DateTime.Now.AddSeconds(36000),
                    Value = usuario.UserName.ToString()
                };
                Response.Cookies.Add(ckUsuario);                
                if (Convert.ToString(loginViewModel.Usuario) == loginViewModel.Password)
                {
                    System.Web.HttpContext.Current.Session["controller"] = null;
                    return RedirectToAction("CambiarPassword", "Cuenta");
                }
                else
                {
                    MenuHelper menu = new MenuHelper();
                    System.Web.HttpContext.Current.Session["menu"] = menu.ObtenerMenuSidebar(usuario.IdUsuario);
                    RolModel rol = servicioUsuario.ObtenerRol(usuario.IdUsuario);
                    if (rol != null)
                    {
                        System.Web.HttpContext.Current.Session["controller"] = rol.Controller;
                        System.Web.HttpContext.Current.Session["metodo"] = rol.Metodo;
                        if (rol.Controller != null && rol.Metodo != null)
                        {
                            return RedirectToAction(rol.Metodo, rol.Controller);
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewData.ModelState.AddModelError("Usuario", "Acceso invalido");
                return View("Acceder");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {

            Session.Remove("username");
            System.Web.HttpContext.Current.Session["currentUser"] = null;

            if (Response.Cookies["ckUsuario"] != null)
            {
                HttpCookie ckUsuario = new HttpCookie("ckUsuario");
                ckUsuario.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckUsuario);
            }
            if (Response.Cookies["ckIdUsuario"] != null)
            {
                HttpCookie ckIdUsuario = new HttpCookie("ckIdUsuario");
                ckIdUsuario.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckIdUsuario);
            }

            return RedirectToAction("Acceder");
        }

        [HttpPost]       
        [ValidateAntiForgeryToken]
        public ActionResult RestablecerSesion(string Usuario, string Password)
        {
            try
            {
                LoginViewModel loginViewModel = new LoginViewModel
                {
                    Usuario = Usuario,
                    Password = Password
                };

                if (servicioUsuario.Obtener(loginViewModel.Usuario, loginViewModel.Password, Convert.ToInt32(ConfigurationManager.AppSettings["rolInvitado"])))
                {
                    UsuarioModel usuario = servicioUsuario.ObtenerUsuario(loginViewModel.Usuario, 1);
                    System.Web.HttpContext.Current.Session["currentUser"] = usuario;

                    //guardo una cookie para reiniciar session
                    HttpCookie ckUsuario = new HttpCookie("ckUsuario")
                    {
                        Expires = DateTime.Now.AddSeconds(36000),
                        Value = usuario.UserName.ToString()
                    };
                    Response.Cookies.Add(ckUsuario);
                   
                   
                    MenuHelper menu = new MenuHelper();
                    System.Web.HttpContext.Current.Session["menu"] = menu.ObtenerMenuSidebar(usuario.IdUsuario);


                    //ViewBag.UserCompleteName = usuario.UserName;        
                    //ViewBag.Menu = (ICollection<MenuSideBarModel>)System.Web.HttpContext.Current.Session["menu"];


                    return Json(new { Success = true, Message = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {                  
                    return Json(new { Success = false, Message = "Acceso invalido" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json(new { Success = false, Message = "Oops! No podemos restaurar su sesion... " }, JsonRequestBehavior.AllowGet);
            }
        }
     
    }
}