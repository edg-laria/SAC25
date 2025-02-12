using SAC.Infrastructure;
using SAC.App_Start;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;

namespace SAC
{
    public class MvcApplication : System.Web.HttpApplication
    {


        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperWebProfile.Run();
            //ignora referencia circular 
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };

            
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
           // HttpContext context = HttpContext.Current;
            //var languageSession = "en";
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageSession);
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(languageSession);

            //CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            //Thread.CurrentThread.CurrentCulture = cultureInfo;
            //Thread.CurrentThread.CurrentUICulture = cultureInfo;

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es");
            CultureInfo newUICulture = new CultureInfo("es-AR");
            //DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo
            //{
            //    TimeSeparator = "/"
            //};
            NumberFormatInfo formato = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberGroupSeparator = ","
            };
            newUICulture.NumberFormat = formato;
            Thread.CurrentThread.CurrentCulture = newUICulture;
            Thread.CurrentThread.CurrentUICulture = newUICulture;
        }


    }
}
