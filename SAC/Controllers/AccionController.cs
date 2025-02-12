using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Models;
using Negocio.Servicios;
using Negocio.Modelos;
using AutoMapper;
namespace SAC.Controllers
{
    public class AccionController : BaseController
    {

        private ServicioConfiguracion servicioConfiguracion = new ServicioConfiguracion();

        public AccionController()
        {
            servicioConfiguracion._mensaje = (msg_, tipo_)  => AddMessage(tipo_, msg_);
        }
   
        // GET: Accion
        public ActionResult Index()
        {
            ConfigAccionModelView configAccionModelView = new ConfigAccionModelView
            {
                Acciones = Mapper.Map<List<AccionModel>, List<AccionModelView>>(servicioConfiguracion.GetAccion())
            };


            return View(configAccionModelView);
        }

        // GET: Accion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Accion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConfigAccionModelView configAccionModelView)
        {
            try
            {
                var evento = servicioConfiguracion.CreateAccion(Mapper.Map<AccionModelView, AccionModel>(configAccionModelView.Accion));
               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }

       
        // GET: Accion/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                AccionModel accionModel = servicioConfiguracion.GetAccionPorId(id);
                var accion = Mapper.Map<AccionModel, AccionModelView>(accionModel);
                return View(accion);
            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }

        // POST: Accion/Edit/5
        [HttpPost]
        public ActionResult Edit(AccionModelView accionModelView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var accionModel = servicioConfiguracion.ActualizarAccion(Mapper.Map<AccionModelView, AccionModel>(accionModelView));
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }


        // POST: Accion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                servicioConfiguracion.DeleteAccion(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }





    }
}
