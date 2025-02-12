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
    public class ArticuloController : BaseController
    {

        private ServicioArticulo servicioarticulo = new ServicioArticulo();
        public ArticuloController()
        {
            servicioarticulo._mensaje = (msg_, tipo_)  => AddMessage(tipo_, msg_);
        }


        // GET: Usuario
        public ActionResult Index()
        {
            List<ArticuloModelView> model = new List<ArticuloModelView>();
             model = Mapper.Map<List<ArticuloModel>, List<ArticuloModelView>>(servicioarticulo.GetAllArticulo());
            return View(model);
        }


        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                servicioarticulo.Eliminar(id);
                servicioarticulo._mensaje("El articulo se eliminó correctamente", "ok");

            }
            catch (Exception ex)
            {
                servicioarticulo._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }


        public ActionResult AddOrEdit(int id = 0)
        {
            ArticuloModelView model;
            if (id == 0)
            {
                model = new ArticuloModelView();
            }
            else
            {
                model = Mapper.Map<ArticuloModel, ArticuloModelView>(servicioarticulo.GetArticuloPorId(id));

            }

            CargarListaOpcion();


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(ArticuloModelView model)
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
                        servicioarticulo.GuardarArticulo(Mapper.Map<ArticuloModelView, ArticuloModel>(model));
                        servicioarticulo._mensaje("El articulo se agregó correctamente", "ok");

                    }
                    else
                    {
                        servicioarticulo.ActualizarArticulo(Mapper.Map<ArticuloModelView, ArticuloModel>(model));
                        servicioarticulo._mensaje("El articulo se modificó correctamente", "ok");
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





        public void CargarListaOpcion()
        {

            ViewBag.Opcion1 = CargarHonorarios();
            ViewBag.Opcion2 = CargarTipos();

        }

        private List<SelectListItem> CargarHonorarios()
        {
            return Opcion1;
        }

        private static readonly List<SelectListItem> Opcion1 = new List<SelectListItem>
        {
            new SelectListItem() {Value = "SI",Text = "SI"},
            new SelectListItem() {Value = "NO",Text = "NO"},
           
        };


        private List<SelectListItem> CargarTipos()
        {
            return Opcion2;
        }


        private static readonly List<SelectListItem> Opcion2 = new List<SelectListItem>
        {
            new SelectListItem() {Value = "Venta",Text = "Venta"},
            new SelectListItem() {Value = "Compra",Text = "Compra"},
            new SelectListItem() {Value = "Gastos",Text = "Gastos"},
           
        };





    }
}



