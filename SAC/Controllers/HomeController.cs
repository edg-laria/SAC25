
using Negocio.Modelos;
using Negocio.Servicios;
using SAC.Helpers;
using SAC.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Controllers
{
    public class HomeController : BaseController
    {   
        
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        public HomeController()
        {
            servicioTipoMoneda._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
        }
        // GET: Home
        public ActionResult Index()
        {

          

        //   var cotizavionMoneda = servicioTipoMoneda.ObtenerCotizacion(DateTime.Now);

        //    if(cotizavionMoneda.Count == 0)
        //    {
        //     BcraHelper bcra = new BcraHelper();
        //    List<CotizacionBNA> cotizacionBNA = bcra.GetCotizacionBNAScraping();
        //        foreach (var item in cotizacionBNA)
        //        {
           
        //            TipoMonedaModel  moneda =  servicioTipoMoneda.ObtenerTipoMonedaPorNombre(item.Moneda);
        //            ValorCotizacionModel valorCotizacionModel = new ValorCotizacionModel() {
        //                IdTipoMoneda = moneda.Id
        //                , Fecha = Convert.ToDateTime(item.Fecha)
        //                , Monto = Convert.ToDecimal(item.Venta)
        //                , UltimaModificacion = DateTime.Now
        //                , Activo = true
        //            };

        //            servicioTipoMoneda.GuardarCotizacionMoneda(valorCotizacionModel);

        //         }


           
        //}


 return View();


    }
    }
}