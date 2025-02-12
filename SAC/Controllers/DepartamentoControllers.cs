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
    public class DepartamentoController : BaseController
    {

        private ServicioDepartamento OservicioDepartamento = new ServicioDepartamento();

      

        public DepartamentoController()
        {
            OservicioDepartamento._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
        }


        public ActionResult Index()
        {
            List<DepartamentoModelView> departamentoModelView = Mapper.Map<List<DepartamentoModel>, List<DepartamentoModelView>>(OservicioDepartamento.GetAllDepartamento());
            return View(departamentoModelView);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DepartamentoModelView model)
        {

            {
                try
                {

                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.IdUsuario = OUsuario.IdUsuario;
                   

                    OservicioDepartamento.Agregar(Mapper.Map<DepartamentoModelView, DepartamentoModel>(model));

                    return RedirectToAction(nameof(Index));


                }
                catch (Exception)
                {
                    return View(model);
                }
            }


        }





        public ActionResult AddOrEdit(int id = 0)
        {


            DepartamentoModelView model;

            

            if (id == 0)
            {
                model = new DepartamentoModelView();
            }
            else
            {
                model = Mapper.Map<DepartamentoModel, DepartamentoModelView>(OservicioDepartamento.GetDepartamentoPorId(id));

            }

            // model.Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetAllRoles());
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(DepartamentoModelView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.IdUsuario = OUsuario.IdUsuario;
                    if (model.Id <= 0)
                    {
                        OservicioDepartamento.Agregar(Mapper.Map<DepartamentoModelView, DepartamentoModel>(model));
                    }
                    else
                    {
                        OservicioDepartamento.Actualizar(Mapper.Map<DepartamentoModelView, DepartamentoModel>(model));
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

                var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                 
                OservicioDepartamento.Eliminar(id, OUsuario.IdUsuario);

            }
            catch (Exception ex)
            {
                OservicioDepartamento._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }





    

     

    }

  




}



