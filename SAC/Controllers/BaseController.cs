using System;
using System.Web.Mvc;
using Negocio.Modelos;
using Entidad.Modelos;
using System.Collections.Generic;
using System.Globalization;
using Negocio.Servicios;
using System.Threading;
using SAC.Models;
using Negocio.Helpers;
using System.Linq;

namespace SAC.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public BaseController()
        {
            UsuarioModel datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            if (datosUsuario != null)
            {
                ViewBag.UserCompleteName = datosUsuario.UserName;
                ViewBag.Metodo = System.Web.HttpContext.Current.Session["metodo"] ?? "Index";
                ViewBag.Controller = System.Web.HttpContext.Current.Session["controller"] ?? "Home";
                ViewBag.Menu = (ICollection<MenuSideBarModel>)System.Web.HttpContext.Current.Session["menu"];
            
                // PARA RESTAURAR
                CookieUsuarioViewModel cookieModel = new CookieUsuarioViewModel { 
                    ckUsuario = datosUsuario.UserName, 
                    ckMenu = (List<MenuSideBarModel>)System.Web.HttpContext.Current.Session["menu"]
                };

            }
            else 
                {
                    RedirectToAction("Acceder", "Cuenta");                               
               }
        }
        [NonAction]
        public void CrearTempData(string msg_, string tipo_)
        {
            TempData[tipo_] = msg_;
        }
        /// <summary>
        /// mensajes toastr Ejemplo de implementacion 
        ///  AddMessage("success", "Excelente...")
        ///  AddMessage("error", "Un mensaje de prueba")
        /// </summary>
        /// <param name="tipo">success | info | warning | error </param>
        /// <param name="message">Texto del mensaje </param>
        public void AddMessage(string tipo, string message)
        {
            try
            {
                if (TempData.ContainsKey("messages"))
                {                  
                        Dictionary<string, string> d = (Dictionary<string, string>)TempData[TempData.Keys.ElementAt(0)];
                        (TempData["messages"] as Dictionary<string, string>).Add(tipo + "_" + d.Count, message);
                                              
                }
                else
                {
                    TempData["messages"] = new Dictionary<string, string>
                    {
                        [tipo.ToLower()] = message
                    };
                }
            }
            catch (Exception ex)
            {
                NLogHelper.Instance.ErrorLog("BaseController", "AddMessage", ex.Message.ToString());


            }

        }
     

        //  CultureInfo cultureInfo = CultureInfo.GetCultureInfo("ar");
        //set CurrentUICulture
        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
        //CultureInfo newUICulture = new CultureInfo("en-US");
        //Thread.CurrentThread.CurrentUICulture = newUICulture;
        //myCIclone.DateTimeFormat.AMDesignator = "a.m.";
        //myCIclone.DateTimeFormat.DateSeparator = "-";
        //myCIclone.NumberFormat.CurrencySymbol = "USD";
        //myCIclone.NumberFormat.NumberDecimalDigits = 4;
    }
}