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
    public class ProveedorController : BaseController
    {

        private ServicioProveedor servicioProveedor = new ServicioProveedor();
        private ServicioLocalidad servicioLocalidad = new ServicioLocalidad();
        private ServicioPais servicioPais = new ServicioPais();
        private ServicioCuentaCteProveedor servicioCuentaCteProveedor = new ServicioCuentaCteProveedor();
        public ProveedorController()
        {
            servicioProveedor._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioLocalidad._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioPais._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
        }

        // GET: Accion
        public ActionResult Index()
        {
            List<ProveedorModelView> model = Mapper.Map<List<ProveedorModel>, List<ProveedorModelView>>(servicioProveedor.GetAllProveedor());
            return View(model);
        }

        // GET: Accion/Create
        public ActionResult Agregar()
        {
            CargarCombos();
            return View();
        }
        // GET: Accion/Create
        public ActionResult Editar(int _id)
        {
            CargarCombos();

            ProveedorModelView oProveedorModel = new ProveedorModelView();
           // oProveedorModel = Mapper.Map<ProveedorModel, ProveedorModelView>(servicioProveedor.GetProveedorEdit(_id));
            oProveedorModel = Mapper.Map<ProveedorModel, ProveedorModelView>(servicioProveedor.GetProveedor(_id));

            //estod deberia ir en el negocio pero el automapper no me deja 01/12/2020
            //ServicioLocalidad oServicioLocalidad = new ServicioLocalidad();
            CargarProvincia(oProveedorModel.IdPais ?? 0);
            CargarLocalidad(oProveedorModel.IdProvincia ?? 0 );
            oProveedorModel.IdCodigoPostal = servicioLocalidad.GetCodigoPostalDeLocalidad(oProveedorModel.IdLocalidad ?? 0);
            return View(oProveedorModel);
        }
        

        [HttpPost]
        public ActionResult Agregar(ProveedorModelView oProveedorModelView)
        {
            CargarCombos();
            int respuesta = -1;
            try
            {
                if (!ModelState.IsValid)
                {

                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();

                    return View(oProveedorModelView);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    ProveedorModel oProveedor = new ProveedorModel();
                    var oProveedorModel = Mapper.Map<ProveedorModelView, ProveedorModel>(oProveedorModelView);

                    oProveedor.Nombre = oProveedorModel.Nombre;
                    oProveedor.Direccion = oProveedorModel.Direccion;
                    oProveedor.Telefono = oProveedorModel.Telefono;
                    oProveedor.IdPais = oProveedorModel.IdPais;
                    oProveedor.IdProvincia= oProveedorModel.IdProvincia;
                    oProveedor.IdLocalidad = oProveedorModel.IdLocalidad;
                    //oProveedor.IdCodigoPostal = oProveedorModel.IdCodigoPostal;
                    oProveedor.Email = oProveedorModel.Email;
                    oProveedor.IdImputacionProveedor = oProveedorModel.IdImputacionProveedor;
                   
                    oProveedor.IdTipoIva = oProveedorModel.IdTipoIva;
                    oProveedor.Cuit = oProveedorModel.Cuit;
                    oProveedor.IdImputacionFactura = oProveedorModel.IdImputacionFactura;
                    oProveedor.IdTipoProveedor = oProveedorModel.IdTipoProveedor;
                    oProveedor.IdTipoMoneda = oProveedorModel.IdTipoMoneda;
                    oProveedor.Observaciones = oProveedorModel.Observaciones;
                    oProveedor.Activo = true;
                    oProveedor.IdUsuario = datosUsuario.IdUsuario;//hay que poner el id del usuario logueado
                    oProveedor.UltimaModificacion = DateTime.Now.Date;

                    respuesta = servicioProveedor.GuardarProveedor(oProveedor);

                    if (respuesta == 0) //grabo
                    {
                        servicioProveedor._mensaje("El país se registró correctamente", "ok");
                    }
                    else if (respuesta == -1) // paso algo
                    {
                        servicioProveedor._mensaje("El país ingresado No se pudo registrar", "error");
                    }
                    else //ya existe
                    {
                        servicioProveedor._mensaje("El país que intenta resitrar ya se encuentra cargado", "error");
                    }
                    return RedirectToAction("Index");
                }

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                servicioProveedor._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return View(oProveedorModelView);
            }
        }


        [HttpPost]
        public ActionResult Editar(ProveedorModelView oProveedorModelView)
        {
            //CargarCombos();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oProveedorModelView);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

                    ProveedorModel oProveedorModel = new ProveedorModel();
                    var oProveedor = Mapper.Map<ProveedorModelView, ProveedorModel>(oProveedorModelView);
                    oProveedorModel.Id = oProveedor.Id;
                    oProveedorModel.Nombre = oProveedor.Nombre;
                    oProveedorModel.Direccion = oProveedor.Direccion;
                    oProveedorModel.Telefono = oProveedor.Telefono;
                    oProveedorModel.IdPais = oProveedor.IdPais;
                    oProveedorModel.IdProvincia = oProveedor.IdProvincia;
                    oProveedorModel.IdLocalidad = oProveedor.IdLocalidad;
                   // oProveedorModel.IdCodigoPostal = oProveedor.IdCodigoPostal;
                    oProveedorModel.Email = oProveedor.Email;
                    oProveedorModel.IdImputacionProveedor = oProveedor.IdImputacionProveedor;

                    oProveedorModel.IdTipoIva = oProveedor.IdTipoIva;
                    oProveedorModel.Cuit = oProveedor.Cuit;
                    oProveedorModel.IdImputacionFactura = oProveedor.IdImputacionFactura;
                    oProveedorModel.IdTipoProveedor = oProveedor.IdTipoProveedor;
                    oProveedorModel.IdTipoMoneda = oProveedor.IdTipoMoneda;
                    oProveedorModel.Observaciones = oProveedor.Observaciones;
                    oProveedorModel.Activo = true;
                    oProveedorModel.IdUsuario = datosUsuario.IdUsuario;//hay que poner el id del usuario logueado
                    oProveedorModel.UltimaModificacion = DateTime.Now.Date;

                    servicioProveedor.ActualizarProveedor(oProveedorModel);

                    return RedirectToAction("Index");
                }
            }

#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                servicioProveedor._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return View(oProveedorModelView);
            }
        }



        //busco las provincias
        public JsonResult GetlistaProvincias(int idPais)
        {
            //string a = "dsaasdasdad";
            ServicioProvincia servicioProvincia = new ServicioProvincia();
            List<ProvinciaModelView> ListaProvincia = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvinciasNombreId(idPais));
            return Json(ListaProvincia, JsonRequestBehavior.AllowGet);
        }

        //busco las provincias
        public JsonResult GetlistaLocalidades(int idProvincia)
        {
            //string a = "dsaasdasdad";
            ServicioLocalidad servicioLocalidad = new ServicioLocalidad();
            List<LocalidadModelView> ListaLocalidad = Mapper.Map<List<LocalidadModel>, List<LocalidadModelView>>(servicioLocalidad.GetAllLocalidads(idProvincia));
            return Json(ListaLocalidad, JsonRequestBehavior.AllowGet);
        }

        //busco el codigo postal de la localidad
        public JsonResult GetCodigoPostal(int idLocalidad)
        {
            
            var CpLocalidad = servicioLocalidad.GetCodigoPostalDeLocalidad(idLocalidad);
            return Json(CpLocalidad, JsonRequestBehavior.AllowGet);
        }

        //busco datos del pais seleccionado
        public JsonResult GetDatosPais(int idPais)
        {
            PaisModel oPaisModel = new PaisModel();
            oPaisModel = servicioPais.GetPais(idPais);
            return Json(oPaisModel, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetCuentaCorriente(int oIdProveedor)
        {
          bool enCero = servicioCuentaCteProveedor.CtaCteEnCero(oIdProveedor);
            return Json(enCero, JsonRequestBehavior.AllowGet);
        }


        public void CargarCombos()
        {
            CargarPais();
            CargarTipoIva();
           // CargarSubRubro();
            CargarTipoMoneda();
            CargarTipoProveedor();
        }
       

        [HttpPost]
        public ActionResult CargarImputacion()
        {
            ServicioImputacion oServicioImputacion = new ServicioImputacion();
            List<ImputacionModelView> ListaImputacion = Mapper.Map<List<ImputacionModel>, List<ImputacionModelView>>(oServicioImputacion.GetAllImputacions());
            return PartialView("_TablaImputacion", ListaImputacion);
        }

        [HttpPost]
        public ActionResult Eliminar(int idProveedor)
        {
            try
            {
                servicioProveedor.Eliminar(idProveedor);
                servicioProveedor._mensaje("El proveedor se eliminó correctamente", "ok");
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
               servicioProveedor._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
            }

            return RedirectToAction("Index");
        }


        public void CargarPais()
        {
            ServicioPais servicioPais = new ServicioPais();
            List<PaisModelView> ListaPais = Mapper.Map<List<PaisModel>, List<PaisModelView>>(servicioPais.GetAllPais());
            ListaPais = ListaPais.OrderBy(p => p.Nombre).ToList();

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaPais = null;
            retornoListaPais = (ListaPais.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Nombre
                                  })).ToList();
            retornoListaPais.Insert(0, new SelectListItem { Text = "-- Seleccione País --", Value = "" });
            ViewBag.ListaPais = retornoListaPais;
        }

        public void CargarProvincia(int oPais)
        {
            ServicioProvincia servicioProvincia = new ServicioProvincia();
            List<ProvinciaModelView> ListaProvincia = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvincias(oPais));
            ListaProvincia = ListaProvincia.OrderBy(p => p.Nombre).ToList();

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaProvincia = null;
            retornoListaProvincia = (ListaProvincia.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Nombre
                                  })).ToList();
            retornoListaProvincia.Insert(0, new SelectListItem { Text = "-- Seleccione Provincia --", Value = "" });
            ViewBag.ListaProvincia = retornoListaProvincia;
        }

        public void CargarLocalidad(int oProvincia)
        {
            ServicioLocalidad servicioLocalidad = new ServicioLocalidad();
            List<LocalidadModelView> ListaLocalidad = Mapper.Map<List<LocalidadModel>, List<LocalidadModelView>>(servicioLocalidad.GetAllLocalidads(oProvincia));
            ListaLocalidad = ListaLocalidad.OrderBy(p => p.Nombre).ToList();

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaLocalidad = null;
            retornoListaLocalidad = (ListaLocalidad.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Nombre
                                  })).ToList();
            retornoListaLocalidad.Insert(0, new SelectListItem { Text = "-- Seleccione Localidad --", Value = "" });
            ViewBag.ListaLocalidad = retornoListaLocalidad;
        }

        public void CargarTipoIva()
        {
            ServicioTipoIva servicioTipoIva = new ServicioTipoIva();
            List<TipoIvaViewModel> ListaPais = Mapper.Map<List<TipoIvaModel>, List<TipoIvaViewModel>>(servicioTipoIva.GetAllTipoIva());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaTipoIva = null;
            retornoListaTipoIva = (ListaPais.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoListaTipoIva.Insert(0, new SelectListItem { Text = "-- Seleccione Tipo Iva --", Value = "" });
            ViewBag.ListaTipoIva = retornoListaTipoIva;
        }

        //public void CargarSubRubro()
        //{
        //    ServicioSubRubro servicioSubRubro = new ServicioSubRubro();
        //    List<SubRubroModelView> ListaSubRubro = Mapper.Map<List<SubRubroModel>, List<SubRubroModelView>>(servicioSubRubro.GetAllSubRubro());

        //    //esto es para pasarlo a select list (drop down list)
        //    List<SelectListItem> retornoListaSubRubro = null;
        //    retornoListaSubRubro = (ListaSubRubro.Select(x =>
        //                          new SelectListItem()
        //                          {
        //                              Value = x.Id.ToString(),
        //                              Text = x.Descripcion
        //                          })).ToList();
        //    retornoListaSubRubro.Insert(0, new SelectListItem { Text = "-- Seleccione Imputación --", Value = "" });
        //    ViewBag.ListaSubRubro = retornoListaSubRubro;
        //}

        //public void CargarAfipRegimen()
        //{
        //    ServicioAfipRegimen servicioSubRubro = new ServicioAfipRegimen();
        //    List<AfipRegimenModelView> ListaSubRubro = Mapper.Map<List<AfipRegimenModel>, List<AfipRegimenModelView>>(servicioSubRubro.GetAllAfipRegimen());

        //    //esto es para pasarlo a select list (drop down list)
        //    List<SelectListItem> retornoListaAfipRegimen = null;
        //    retornoListaAfipRegimen = (ListaSubRubro.Select(x =>
        //                          new SelectListItem()
        //                          {
        //                              Value = x.Id.ToString(),
        //                              Text = x.Descripcion
        //                          })).ToList();
        //    retornoListaAfipRegimen.Insert(0, new SelectListItem { Text = "--Seleccione País--", Value = "" });
        //    ViewBag.ListaAfipRegimen = retornoListaAfipRegimen;
        //}

        public void CargarTipoMoneda()
        {
            ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaAfipRegimen = null;
            retornoListaAfipRegimen = (ListaTipoMoneda.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoListaAfipRegimen.Insert(0, new SelectListItem { Text = "-- Seleccione Tipo moneda --", Value = "" });
            ViewBag.ListaTipoMoneda = retornoListaAfipRegimen;
        }

        public void CargarTipoProveedor()
        {
            ServicioTipoProveedor servicioTipoProveedor = new ServicioTipoProveedor();
            List<TipoProveedorModelView> ListaTipoProveedor = Mapper.Map<List<TipoProveedorModel>, List<TipoProveedorModelView>>(servicioTipoProveedor.GetAllTipoProveedors());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaTipoProveedor = null;
            retornoListaTipoProveedor = (ListaTipoProveedor.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoListaTipoProveedor.Insert(0, new SelectListItem { Text = "-- Seleccione Tipo Proveedor --", Value = "" });
            ViewBag.ListaTipoProveedor = retornoListaTipoProveedor;
        }
    }
}
