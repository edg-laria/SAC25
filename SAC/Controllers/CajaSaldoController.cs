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
    public class CajaSaldoController : BaseController
    {

        private ServicioCajaSaldo serviciocajasaldo = new ServicioCajaSaldo();
        public CajaSaldoController()
        {
            serviciocajasaldo._mensaje = (msg_, tipo_)  => AddMessage(tipo_, msg_);
        }


        // GET: Usuario
        public ActionResult Index()
        {
            List<CajaSaldoModelView> cajaGrupoModelViews = new List<CajaSaldoModelView>();
             cajaGrupoModelViews = Mapper.Map<List<CajaSaldoModel>, List<CajaSaldoModelView>>(serviciocajasaldo.GetAllCajaSaldo());
            return View(cajaGrupoModelViews);
        }





     
        public ActionResult AddOrEdit(int id = 0)
        {
            CajaSaldoModelView model;
            if (id == 0)
            {
                model = new CajaSaldoModelView();
            }
            else
            {
                model = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(serviciocajasaldo.GetCajaSaldoPorId(id));
                
            }

           // model.Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetAllRoles());
            return View(model);
        }

        public ActionResult Detalle(int id)
        {
            CajaSaldoModelView model;
           
                model = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(serviciocajasaldo.GetCajaSaldoPorId(id));         

           
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken] 
        
        public ActionResult AddOrEdit(CajaSaldoModelView model)
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
                   serviciocajasaldo.GuardarCajaSaldo(Mapper.Map<CajaSaldoModelView, CajaSaldoModel>(model));
                }
                else
                {
                        serviciocajasaldo.ActualizarCajaSaldo(Mapper.Map<CajaSaldoModelView, CajaSaldoModel>(model));
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
                serviciocajasaldo.Eliminar(id);
               
            }
            catch (Exception ex)
            {
                serviciocajasaldo._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }


    }
}



