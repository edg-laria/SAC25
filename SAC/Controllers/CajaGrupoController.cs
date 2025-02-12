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
    public class CajaGrupoController : BaseController
    {

        private ServicioCajaGrupo serviciocajagrupo = new ServicioCajaGrupo();
        public CajaGrupoController()
        {            
            serviciocajagrupo._mensaje = (msg_, tipo_)  => AddMessage(tipo_, msg_);
        }


        // GET: Usuario
        public ActionResult Index()
        {
            List<CajaGrupoModelView> cajaGrupoModelViews = new List<CajaGrupoModelView>();
             cajaGrupoModelViews = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(serviciocajagrupo.GetAllCajaGrupo());
            return View(cajaGrupoModelViews);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            CajaGrupoModelView model;
            if (id == 0)
            {
                model = new CajaGrupoModelView();
            }
            else
            {
                model = Mapper.Map<CajaGrupoModel, CajaGrupoModelView>(serviciocajagrupo.GetGrupoCajaPorId(id));
                
            }

           // model.Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetAllRoles());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]         
        public ActionResult AddOrEdit(CajaGrupoModelView model)
        {
            try
            {        
            if (ModelState.IsValid)
            {

                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.IdUsuario= OUsuario.IdUsuario;
                    //serviciocajagrupo._mensaje("","ok");
               if (model.Id <= 0)
                {
                   serviciocajagrupo.GuardarGrupoCaja(Mapper.Map<CajaGrupoModelView, CajaGrupoModel>(model));
                }
                else
                {
                    serviciocajagrupo.ActualizarGrupoCaja(Mapper.Map<CajaGrupoModelView, CajaGrupoModel>(model));
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
                serviciocajagrupo.Eliminar(id);
               
            }
            catch (Exception ex)
            {
                serviciocajagrupo._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }
   
        public ActionResult ConsultaGrupo()
        {


            List<CajaGrupoModelView> cajaGrupoModelViews = new List<CajaGrupoModelView>();
            cajaGrupoModelViews = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(serviciocajagrupo.GetAllCajaGrupo());
            return View(cajaGrupoModelViews);



        }

        [HttpPost]
        public ActionResult CajaGrupoCerrar()
        {
            try
            {
               serviciocajagrupo.CerrarGrupoCaja();
            }
            catch (Exception ex)
            {
                serviciocajagrupo._mensaje(ex.Message, "error");
            }
            return RedirectToAction("ConsultaGrupo");
        }


    }
}



