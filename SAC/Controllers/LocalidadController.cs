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


namespace SAC.Controllers
{
    public class LocalidadController : BaseController
    {

        private ServicioLocalidad servicioLocalidad = new ServicioLocalidad();

        public LocalidadController()
        {
            servicioLocalidad._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
        }


        public JsonResult GetlistaProvincias(int idPais)
        {
            //string a = "dsaasdasdad";
            ServicioProvincia servicioProvincia = new ServicioProvincia();
            List<ProvinciaModelView> ListaProvincia = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvinciasNombreId(idPais));
            return Json(ListaProvincia, JsonRequestBehavior.AllowGet);
        }

       public ActionResult ObtenerLocalidades (LocalidadModelView vm)
        {
            if (vm.idCmbPais != 0 && vm.idCmbProvincia != 0)
            {
                List<LocalidadModelView> oListaLocalidad = null;
                oListaLocalidad = Mapper.Map<List<LocalidadModel>, List<LocalidadModelView>>(servicioLocalidad.GetAllLocalidads(vm.idCmbPais, vm.idCmbProvincia));
                return PartialView("_Tabla", oListaLocalidad);
                //return View(oListaLocalidad);
            }
            else
            {
                return View();
            }
        }

        // GET: Localidad
        public ActionResult Index(LocalidadModelView oLocalidaViewModel)
        {
            CargarPais();
            return View();
        }

        public ActionResult Agregar()
        {
            CargarPais();
            return View();
        }


        [HttpPost]
        public ActionResult Agregar(LocalidadModelView oLocalidadModelView)
        {
            CargarPais();
            int respuesta = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oLocalidadModelView);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    LocalidadModel oProvincia = new LocalidadModel();
                    var oProvinciaModel = Mapper.Map<LocalidadModelView, LocalidadModel>(oLocalidadModelView);

                    oProvincia.Nombre = oProvinciaModel.Nombre;
                    oProvincia.CodigoPostal = oProvinciaModel.CodigoPostal;
                    oProvincia.CodigoProvincia = oProvinciaModel.CodigoProvincia;
                    oProvincia.IdPais = oLocalidadModelView.idCmbPais;
                    oProvincia.IdProvincia = oLocalidadModelView.idCmbProvincia;

                    oProvincia.Activo = true;
                    oProvincia.IdUsuario = datosUsuario.IdUsuario;//hay que poner el id del usuario logueado
                    oProvincia.UltimaModificacion = DateTime.Now.Date;

                    respuesta = servicioLocalidad.GuardarLocalidad(oProvincia);

                    if (respuesta == 0) //grabo
                    {
                        servicioLocalidad._mensaje("se registró correctamente", "ok");
                    }
                    else if (respuesta == -1) // paso algo
                    {
                        servicioLocalidad._mensaje("No se pudo registrar", "error");
                    }
                    else //ya existe
                    {
                        servicioLocalidad._mensaje("Ya se encuentra cargado", "error");
                    }
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                servicioLocalidad._mensaje(ex.Message, "error");
                return View(oLocalidadModelView);
            }



        }

        public ActionResult Editar(int _id)
        {
            CargarPais();
            CargarProvincia(0);
            LocalidadModelView oLocalidadModel = new LocalidadModelView();
            oLocalidadModel = Mapper.Map<LocalidadModel, LocalidadModelView>(servicioLocalidad.GetLocalidad(_id));
            return View(oLocalidadModel);
        }

        [HttpPost]
        public ActionResult Editar(LocalidadModelView oLocalidadModelView)
        {
            int respuesta = -1;
            CargarPais();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oLocalidadModelView);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

                    LocalidadModel oLocalidadModel = new LocalidadModel();
                    var oLocalidad = Mapper.Map<LocalidadModelView, LocalidadModel>(oLocalidadModelView);

                    oLocalidadModel.Id = oLocalidad.Id;
                    oLocalidadModel.Nombre = oLocalidad.Nombre;
                    oLocalidadModel.CodigoPostal = oLocalidad.CodigoPostal;
                    //tomo como codigo de provincia la primera letra
                    oLocalidadModel.CodigoProvincia = oLocalidadModelView.Nombre.Substring(oLocalidadModelView.Nombre.Length - 2, 1); ;
                    oLocalidadModel.IdPais = oLocalidad.IdPais;
                    oLocalidadModel.IdProvincia = oLocalidad.IdProvincia;
                    oLocalidadModel.Activo = true;
                    oLocalidadModel.IdUsuario = datosUsuario.IdUsuario;//hay que poner el id del usuario logueado
                    oLocalidadModel.UltimaModificacion = DateTime.Now.Date;

                    respuesta = servicioLocalidad.ActualizarPais(oLocalidadModel);

                    if (respuesta == 0) //grabo
                    {
                        servicioLocalidad._mensaje("Se registró correctamente", "ok");
                    }
                    else if (respuesta == -1) // paso algo
                    {
                        servicioLocalidad._mensaje("No se pudo registrar", "error");
                    }
                    else //-2 existe
                    {
                        servicioLocalidad._mensaje("Ya se encuentra cargado", "error");
                    }

                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                servicioLocalidad._mensaje(ex.Message, "error");
                return View(oLocalidadModelView);
            }
        }

        //combo pais
        public void CargarPais()
        {
            ServicioPais servicioPais = new ServicioPais();
            List<PaisModelView> ListaPais = Mapper.Map<List<PaisModel>, List<PaisModelView>>(servicioPais.GetAllPais());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaPais = null;
            retornoListaPais = (ListaPais.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Nombre
                                  })).ToList();
            retornoListaPais.Insert(0, new SelectListItem { Text = "--Seleccione País--", Value = "" });

            ViewBag.ListaPais = retornoListaPais;
        }

        public void CargarProvincia(int idPais)
        {
            List<SelectListItem> retornoListaProvincia = new List<SelectListItem>();
            if (idPais !=0)
            {
               
                    ServicioProvincia servicioProvincia = new ServicioProvincia();
                    List<ProvinciaModelView> ListaProvincia = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvincias(idPais));

                    //esto es para pasarlo a select list (drop down list)
                    retornoListaProvincia = (ListaProvincia.Select(x =>
                                          new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.Nombre
                                          })).ToList();
                    retornoListaProvincia.Insert(0, new SelectListItem { Text = "--Seleccione Provincia--", Value = "" });
               
            }
            else
            {
                ServicioProvincia servicioProvincia = new ServicioProvincia();
                List<ProvinciaModelView> ListaProvincia = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvincias());

                //esto es para pasarlo a select list (drop down list)
                retornoListaProvincia = (ListaProvincia.Select(x =>
                                      new SelectListItem()
                                      {
                                          Value = x.Id.ToString(),
                                          Text = x.Nombre
                                      })).ToList();
                retornoListaProvincia.Insert(0, new SelectListItem { Text = "--Seleccione Provincia--", Value = "" });

            }
           

            ViewBag.ListaProvincia = retornoListaProvincia;
        }

        //[HttpGet()]
        //public ActionResult getLocalidadesJson(int idPais, int idProvincia)
        //{
        //    string strJson;

        //    try
        //    {
        //        List<LocalidadModelView> oListaLocalidad = null;
        //        oListaLocalidad = Mapper.Map<List<LocalidadModel>, List<LocalidadModelView>>(servicioLocalidad.GetAllLocalidads(idPais, idProvincia));
        //       // return PartialView("_Tabla", oListaLocalidad);           
        //        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(oListaLocalidad);
        //      if ((strJson != null))
        //        {
        //            var rJson = Json(new { data = strJson, result = true }, JsonRequestBehavior.AllowGet);
        //            return rJson;
        //        }
        //        else
        //        {
        //            var rJson = Json(new { result = false, tipoError = "Error" }, JsonRequestBehavior.AllowGet);
        //            return rJson;
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        var rJson = Json(new { result = false, tipoError = "Error" }, JsonRequestBehavior.AllowGet);
        //        return rJson;
        //    }

          
        //}

        [HttpPost]
        public ActionResult Eliminar(int idLocalidad)
        {
            try
            {
                servicioLocalidad.Eliminar(idLocalidad);
                servicioLocalidad._mensaje("El país se eliminó correctamente", "ok");
            }
            catch (Exception ex)
            {
                servicioLocalidad._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }

    }
}