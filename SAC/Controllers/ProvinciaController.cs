using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//agregadas
using AutoMapper;
using Negocio.Modelos;
using Negocio.Servicios;
using SAC.Atributos;
using SAC.Models;
using System.Web.Routing;

namespace SAC.Controllers
{
    public class ProvinciaController : BaseController
    {
        private ServicioProvincia servicioProvincia = new ServicioProvincia();


        public ProvinciaController()
        {
            servicioProvincia._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
        }

        // GET: Provincia
        public ActionResult Index(ProvinciaModelView oProvinciaModel)
        {
            CargarPais();

            int idPais = oProvinciaModel.idCmbPais;

            if (idPais == 0) {
                List<ProvinciaModelView> provinciaModelView = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvincias());
                return View(provinciaModelView);
            }
            else
            {
                List<ProvinciaModelView> provinciaModelView = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvincias(idPais));
                return View(provinciaModelView);
            }
        
          
        }

        public ActionResult Agregar()
        {
            CargarPais();
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ProvinciaModelView oProvinciaModelView)
        {
            CargarPais();
            int respuesta = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oProvinciaModelView);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    ProvinciaModel oProvincia = new ProvinciaModel();
                    var oProvinciaModel = Mapper.Map<ProvinciaModelView, ProvinciaModel>(oProvinciaModelView);

                    oProvincia.Nombre = oProvinciaModel.Nombre;
                    oProvincia.Codigo = oProvinciaModel.Codigo;
                    oProvincia.CodigoNumero = oProvinciaModel.CodigoNumero;
                    oProvincia.IdPais = oProvinciaModel.IdPais;
                    oProvincia.CodigoAfip = oProvinciaModel.CodigoAfip;
                    
                    oProvincia.Activo = true;
                    oProvincia.IdUsuario = datosUsuario.IdUsuario;//hay que poner el id del usuario logueado
                    oProvincia.UltimaModificacion = DateTime.Now.Date;

                    respuesta = servicioProvincia.GuardarProvincia(oProvincia);

                    if (respuesta == 0) //grabo
                    {
                        servicioProvincia._mensaje("Se registró correctamente", "ok");
                    }
                    else if (respuesta == -1) // paso algo
                    {
                        servicioProvincia._mensaje("No se pudo registrar", "error");
                    }
                    else //ya existe
                    {
                        servicioProvincia._mensaje("Ya se encuentra registrado", "error");
                    }
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                servicioProvincia._mensaje(ex.Message, "error");
                return View(oProvinciaModelView);
            }


          
        }

        public ActionResult Editar(int _id)
        {
            CargarPais();
            ProvinciaModelView oPovinciaModel = new ProvinciaModelView();
            oPovinciaModel = Mapper.Map<ProvinciaModel, ProvinciaModelView>(servicioProvincia.GetProvincia(_id));
            return View(oPovinciaModel);
           
        }

        [HttpPost]
        public ActionResult Editar(ProvinciaModelView oProvinciaModelView)
        {
            int respuesta = -1;
            CargarPais();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oProvinciaModelView);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

                    ProvinciaModel oProvinciaModel = new ProvinciaModel();
                    var oProvincia = Mapper.Map<ProvinciaModelView, ProvinciaModel>(oProvinciaModelView);

                    oProvinciaModel.Id = oProvincia.Id;
                    oProvinciaModel.Nombre = oProvincia.Nombre;
                    oProvinciaModel.Codigo = oProvincia.Codigo;
                    oProvinciaModel.CodigoAfip = oProvincia.CodigoAfip;
                    oProvinciaModel.CodigoNumero = oProvincia.CodigoNumero;
                    oProvinciaModel.IdPais = oProvincia.IdPais;
                    oProvinciaModel.Activo = true;
                    oProvinciaModel.IdUsuario = datosUsuario.IdUsuario;//hay que poner el id del usuario logueado
                    oProvinciaModel.UltimaModificacion = DateTime.Now.Date;


                    respuesta = servicioProvincia.ActualizarPais(oProvinciaModel);

                    if (respuesta == 0) //grabo
                    {
                        servicioProvincia._mensaje("El país se registró correctamente", "ok");
                    }
                    else if (respuesta == -1) // paso algo
                    {
                        servicioProvincia._mensaje("El país ingresado No se pudo registrar", "error");
                    }
                    else //-2 existe
                    {
                        servicioProvincia._mensaje("El país que intenta resitrar ya se encuentra cargado", "error");
                    }

                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                servicioProvincia._mensaje(ex.Message, "error");
                return View(oProvinciaModelView);
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int idProvincia)
        {
            try
            {
                servicioProvincia.Eliminar(idProvincia);
                servicioProvincia._mensaje("El país se eliminó correctamente", "ok");
            }
            catch (Exception ex)
            {
                servicioProvincia._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }

        //combo pais
        public void CargarPais()
        {

            ServicioPais servicioPais = new ServicioPais();
            List<PaisModelView> ListaPais = Mapper.Map<List<PaisModel>, List<PaisModelView>>(servicioPais.GetAllPais());
            //ListaPais = ListaPais.OrderBy(p => p.Nombre).ToList();
            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaPais = null;
            retornoListaPais = (ListaPais.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Nombre
                                  })).ToList();
            retornoListaPais.Insert(0, new SelectListItem { Text = "--Seleccione País--", Value = "" });


            ViewBag.Listapagina = retornoListaPais;
        }

       

    }
}