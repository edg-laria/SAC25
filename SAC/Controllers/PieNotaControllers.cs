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
    public class PieNotaController : BaseController
    {

         private ServicioPieNota oServicioPieNota = new ServicioPieNota();

          


        public PieNotaController()
        {
            oServicioPieNota._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
          
        }


        // GET: Usuario
        public ActionResult Index()
        {
            List<PieNotaModelView> ModelViews = new List<PieNotaModelView>();
            ModelViews = Mapper.Map<List<PieNotaModel>, List<PieNotaModelView>>(oServicioPieNota.GetAllPieNota());
            return View(ModelViews);
        }
        public ActionResult AddOrEdit(int id = 0)
        {                   
            PieNotaModelView model;                      
            if (id == 0)
            {
                model = new PieNotaModelView();
            }
            else
            {
                model = Mapper.Map<PieNotaModel, PieNotaModelView>(oServicioPieNota.GetPieNotaPorId(id));
            }
            ViewBag.SelectBancoCuenta = SelectBancoCuenta();
            return View(model);
        }

        public List<SelectListItem> SelectBancoCuenta()
        {
            ServicioBancoCuenta _servicio = new ServicioBancoCuenta();
            List<BancoCuentaModel> ListCbt = _servicio.GetAllCuenta();
            List<SelectListItem> model = (ListCbt.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.BancoDescripcion
                                  })).ToList();
        
            return model;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddOrEdit(PieNotaModelView model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.IdUsuario = OUsuario.IdUsuario;
                    //serviciocajagrupo._mensaje("","ok");
                    if (model.Id <= 0)
                    {
                       oServicioPieNota.GuardarPieNota(Mapper.Map<PieNotaModelView, PieNotaModel>(model));
                    }
                    else
                    {
                        oServicioPieNota.ActualizarPienota(Mapper.Map<PieNotaModelView, PieNotaModel>(model));
                    }

                    return RedirectToAction(nameof(Index));
                }
                return View(model);

            }

            catch (Exception)
            {
                return View(model);
            }
        }



        



        [HttpPost]
        public ActionResult Eliminar(int id)
            
        {
            try
            {
                oServicioPieNota.Eliminar(id);

            }
            catch (Exception ex)
            {
                oServicioPieNota._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult BloquearPieNota(int id)
        {
            try
            {

          
                oServicioPieNota.BloquearPieNota(id);

            }
            catch (Exception ex)
            {
                oServicioPieNota._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult HabilitarPieNota(int id)
        {
            try
            {
               

                oServicioPieNota.HabilitarPieNota(id);

            }
            catch (Exception ex)
            {
                oServicioPieNota._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }










    }






}



