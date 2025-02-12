using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace SAC.Atributos
{
    public class AutorizacionDeSistema : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            UsuarioModel usuario = (UsuarioModel)HttpContext.Current.Session["currentUser"];

            if (usuario == null )
            {             
                if (HttpContext.Current.Request.Cookies["ckUsuario"] == null)
                {               
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Acceder", controller = "Cuenta" }));
                }
            }
            else if (!usuario.TienePermisos(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower(),
             filterContext.ActionDescriptor.ActionName.ToLower()))

            {
                //& !usuario.EsAdministrador
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Unauthorised" }));
            }
        }

       






    }
}