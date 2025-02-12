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
using System.Globalization;
using System.IO;

namespace SAC.Controllers
{
    public class CajaController : BaseController
    {

        private ServicioCaja servicioCaja = new ServicioCaja();

        private ServicioCajaGrupo servicioCajaGrupo = new ServicioCajaGrupo();
        private ServicioCajaSaldo servicioCajaSaldo = new ServicioCajaSaldo();

        private ServicioTarjeta oservicioTarjeta = new ServicioTarjeta();

        private ServicioTarjetaOperacion oservicioTarjetaOperacion = new ServicioTarjetaOperacion();

        private ServicioChequera oServicioChequera = new ServicioChequera();
        private ServicioCheque oServicioCheque = new ServicioCheque();
        private ServicioBancoCuenta oServicioCuentaBancaria = new ServicioBancoCuenta();
        private ServicioTipoMoneda oServicioTipoMoneda = new ServicioTipoMoneda();

        public CajaController()
        {
            servicioCaja._mensaje = (msg_, tipo_)  => AddMessage(tipo_, msg_);
        }
        public ActionResult Index()
        {
            CajaModelView model = new CajaModelView();
            model.ListaCaja = Mapper.Map<List<CajaModel>, List<CajaModelView>>(servicioCaja.GetAllCaja());
            model.CajaSaldoInicial = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(servicioCajaSaldo.GetUltimoCierre());
            CargarCajaGrupo();
            CargarTarjetas();


            //---------para el PartialView cheques terceros
            List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());
            model.ListaChequesTerceros = ListaChequesTerceros;
            //--------PartialView cheques propios  
            model.oChequera = new ChequeraModelView();
            List<ChequeraModelView> ListaChequesPropios = Mapper.Map<List<ChequeraModel>, List<ChequeraModelView>>(oServicioChequera.GetAllChequera());

            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(oServicioTipoMoneda.GetAllTipoMonedas());




            List<SelectListItem> retornoListaTipoMoneda = (ListaTipoMoneda.Select(x =>
                                         new SelectListItem()
                                         {
                                             Value = x.Id.ToString(),
                                             Text = x.Descripcion
                                         })).ToList();

            model.ListaChequesPropios = new List<ChequeraModelView>(); //ListaChequesPropios; 

            // segun la cuenta bancaria selecccionada se obtiene el numero de cheque  hacerlo via json
            //List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(oServicioCuentaBancaria.GetAllCuenta());           
            //List<SelectListItem> retornoListaCuentaBancaria = (ListaCuentaBancaria.Select(x =>
            //                            new SelectListItem()
            //                            {
            //                                Value = x.Id.ToString(),
            //                                Text = x.Banco.Nombre + " " + x.BancoDescripcion
            //                            })).ToList();
            //model.listaCuentaBancariaDrop = retornoListaCuentaBancaria;

            //drop cuenta bancaria

            ListaCuentaBancaria();

            model.ListaTipoMonedaDrop = retornoListaTipoMoneda;

            return View(model);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CajaModelView model)
        {
            {
                try
                {


                    if (model.ImporteCheque <= 0 && model.ImportePesos <= 0 && model.ImporteDolar <= 0 && model.ImporteTarjeta <= 0)
                    {
                        servicioCaja._mensaje?.Invoke("debe ingresar un Importe", "error");
                        //throw new Exception("Debe ingresar un Importe");
                        throw new NotImplementedException();
                    }

                 
                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];


                    ///------------Registro Manual Caja----------
                    CajaModel ingresoManuaCaja = new CajaModel()
                    {
                        IdTipoMovimiento = 2,
                        Concepto = model.Concepto,
                        IdCajaSaldo = 0,
                        Fecha = model.Fecha,
                        Saldo = "",
                        Tipo = "",
                        Recibo = "",
                        ImportePesos = model.ImportePesos,
                        ImporteDolar = model.ImporteDolar,
                        ImporteTarjeta = model.ImporteTarjeta,
                        ImporteCheque = model.montoChequesSeleccionados,
                        IdGrupoCaja = model.IdGrupoCaja,                      
                        Activo = true,
                        IdUsuario = OUsuario.IdUsuario,
                        UltimaModificacion = DateTime.Now
                    };

                    if (model.ImporteTarjeta > 0)
                    {
                        ingresoManuaCaja.IdTarjeta = model.IdTarjeta;
                    }

                    CajaModel ingresoRegistrado = servicioCaja.RegistroManualDeCaja(ingresoManuaCaja);
                 
                    if (ingresoRegistrado != null &&  model.IdTarjeta > 0 && model.ImporteTarjeta > 0)
                    {
                        TarjetaOperacionModel oTarjetaOperacionModel = new TarjetaOperacionModel();
                        oTarjetaOperacionModel.IdTarjeta = Convert.ToInt32(model.IdTarjeta);
                        oTarjetaOperacionModel.IdGrupoCaja = Convert.ToInt32(model.IdGrupoCaja) ;
                        oTarjetaOperacionModel.Descripcion = Convert.ToString(model.Concepto) + ", Ingreso Manual de Caja";
                        oTarjetaOperacionModel.Importe = Convert.ToDecimal(model.ImporteTarjeta);
                        oTarjetaOperacionModel.UltimaModificacion = DateTime.Now;
                        oTarjetaOperacionModel.Activo = true;
                        oTarjetaOperacionModel.IdUsuario = Convert.ToInt32(model.IdUsuario);
                        servicioCaja.InsertarOperacionTarjeta(oTarjetaOperacionModel);
                    }

                   

                    if ((model.idChequesTerceros != null) && (model.idChequesTerceros != ""))
                    {
                        ChequeModel oCheque = new ChequeModel();
                        string[] chequesSeleccionados = model.idChequesTerceros.Split(';');
                        foreach (var itemCheque in chequesSeleccionados)
                        {                            
                            oCheque = oServicioCheque.obtenerCheque(int.Parse(itemCheque));


                            //registrar tabla cheques
                            oCheque.FechaEgreso = DateTime.Now;
                            oCheque.Destino = model.Concepto;
                            oCheque.IdMoneda = oCheque.IdMoneda;
                            oCheque.NumeroPago = null;
                            oCheque.Proveedor = model.IdGrupoCaja.ToString();
                            oCheque.Activo = false;
                            oCheque.Endosado = true;
                            oServicioCheque.Actualizar(oCheque);

                        }

                    }
                        return RedirectToAction(nameof(Index));


                }
                catch (Exception)
                {

                    
                    return RedirectToAction(nameof(Index));
                }
            }


        }



        public ActionResult AddOrEdit(int id = 0)
        {

            CargarCajaGrupo();

            CajaModelView model;
            if (id == 0)
            {
                model = new CajaModelView();
            }
            else
            {
                model = Mapper.Map<CajaModel, CajaModelView>(servicioCaja.GetCajaPorId(id));

            }

            // model.Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetAllRoles());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(CajaModelView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.IdUsuario = OUsuario.IdUsuario;
                    if (model.Id <= 0)
                    {
                        servicioCaja.GuardarCaja(Mapper.Map<CajaModelView, CajaModel>(model));
                    }
                    else
                    {
                        servicioCaja.ActualizarCaja(Mapper.Map<CajaModelView, CajaModel>(model));
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
                servicioCaja.Eliminar(id);

            }
            catch (Exception ex)
            {
                servicioCaja._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }

        //combo cajagrupo
        public void CargarCajaGrupo()
        {
            List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Codigo
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Seleccionar Grupo", Value = "" });
            ViewBag.Listapagina = retornoListaCajaGrupo;
        }

        private void ListaCuentaBancaria()
        {
            List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(oServicioCuentaBancaria.GetAllCuenta());

            ViewBag.listaCuentaBancariaDrop = (ListaCuentaBancaria.Select(x =>
                                                 new SelectListItem()
                                                 {
                                                     Value = x.Id.ToString(),
                                                     Text = x.Banco.Nombre + ' ' + x.BancoDescripcion
                                                 })).ToList();

        }

        public ActionResult ConsultaCaja(int CIdGrupoCaja = 0, String searchFechaDesde = null, String searchFechaHasta = null)
        {
            CajaModelView model = new CajaModelView();
            DateTime fechaDesde = DateTime.Now;
            DateTime fechaHasta = DateTime.Now;
            if (CIdGrupoCaja != 0)
            {

                if (!string.IsNullOrEmpty(searchFechaDesde))
                {
                    fechaDesde = DateTime.ParseExact(searchFechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                if (!string.IsNullOrEmpty(searchFechaHasta))
                {
                    fechaHasta = DateTime.ParseExact(searchFechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
              
                model.GrupoCaja = Mapper.Map<CajaGrupoModel, CajaGrupoModelView>(servicioCajaGrupo.GetGrupoCajaPorId(CIdGrupoCaja));
             
                model.ListaCaja = Mapper.Map<List<CajaModel>, List<CajaModelView>>(servicioCaja.getGrupoCajaFecha(CIdGrupoCaja, fechaDesde, fechaHasta));

                model.CajaDesde = Mapper.Map<CajaModel, CajaModelView>(servicioCaja.GetCajaPorFecha(CIdGrupoCaja, fechaDesde)) ?? new CajaModelView();

                DateTime fecha = DateTime.Now;
                model.Fecha = fecha;
                model.CajaHasta = Mapper.Map<CajaModel, CajaModelView>(servicioCaja.GetCajaMenorIgualAFecha(CIdGrupoCaja, fecha)) ?? new CajaModelView();
             
                model.CajaSaldoInicial = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(servicioCajaSaldo.GetUltimoCierre());

            }

            model.cFechaDesde = fechaDesde;
            model.cFechaHasta = fechaHasta;


            CargarCajaGrupo();

            return View(model);
        }

        public ActionResult ConsultaPorGrupo()
        {


            CajaModelView model = new CajaModelView();
            model.ListaCaja = Mapper.Map<List<CajaModel>, List<CajaModelView>>(servicioCaja.GetAllCaja());
            model.CajaSaldoInicial = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(servicioCajaSaldo.GetUltimoCierre());
            model.cFechaDesde = DateTime.Now;
            model.cFechaHasta = DateTime.Now;
            CargarCajaGrupo();

            // return View();
            return View(model);



        }

        private void CargarTarjetas()
        {

            List<TarjetaModelView> ListaCajaGrupo = Mapper.Map<List<TarjetaModel>, List<TarjetaModelView>>(oservicioTarjeta.GetAllTarjetas());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descripcion
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Tarjeta", Value = "0" });
            ViewBag.ListaTarjeta = retornoListaCajaGrupo;



        }


        //--------------cheques
        [HttpPost]
        public ActionResult CargarChequesPropios()
        {
            //cheques propios          
            List<ChequeraModelView> ListaChequesPropios = Mapper.Map<List<ChequeraModel>, List<ChequeraModelView>>(oServicioChequera.GetAllChequera());
            //Listo cuentas bancarias           
            List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(oServicioCuentaBancaria.GetAllCuenta());

            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(oServicioTipoMoneda.GetAllTipoMonedas());

            List<SelectListItem> retornoListaCuentaBancaria = (ListaCuentaBancaria.Select(x =>
                                        new SelectListItem()
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Banco.Nombre + " " + x.BancoDescripcion
                                        })).ToList();

            List<SelectListItem> retornoListaTipoMoneda = (ListaTipoMoneda.Select(x =>
                                         new SelectListItem()
                                         {
                                             Value = x.Id.ToString(),
                                             Text = x.Descripcion
                                         })).ToList();

            FacturaPagoViewModel oChequesModel = new FacturaPagoViewModel
            {
                ListaChequesPropios = ListaChequesPropios,
                listaCuentaBancariaDrop = retornoListaCuentaBancaria,
                ListaTipoMonedaDrop = retornoListaTipoMoneda
            };

            return PartialView("CuentaCteProveedor/_TablaChequesPropios", oChequesModel);

        }

        [HttpPost]
        public ActionResult CargarChequesTerceros()

        {
            //cheques de terceros

            List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());

            FacturaPagoViewModel oChequesModel = new FacturaPagoViewModel();
            oChequesModel.ListaChequesTerceros = ListaChequesTerceros;

            return PartialView("CuentaCteProveedor/_tablaChequesTerceros", oChequesModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IngresarChequeAjax(ChequeraModelView oFacturaPago)

        {
            List<ChequeraModelView> listChequeraModelView = new List<ChequeraModelView>();
            try
            {
                //buscar el tipo de moneda de la cta
                BancoCuentaModelView bancoCuentaModelView = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(oServicioCuentaBancaria.GetCuentaPorId(oFacturaPago.idCuentaBancariaSeleccionada));

                oFacturaPago.IdBancoCuenta = oFacturaPago.idCuentaBancariaSeleccionada;
                oFacturaPago.Fecha = DateTime.Now;
                oFacturaPago.IdMoneda = bancoCuentaModelView.IdMoneda;
                oFacturaPago.Usado = false;
                oFacturaPago.IdProveedor = null;
                oFacturaPago.NumeroRecibo = null;
                oFacturaPago.Activo = true;
                //oFacturaPago.oChequera.IdUsuario = oFacturaPago.idUsuario;
                oFacturaPago.UltimaModificacion = DateTime.Now;

                ChequeraModel chequePropioGuardado = oServicioChequera.InsertarAjax(Mapper.Map<ChequeraModelView, ChequeraModel>(oFacturaPago));

                if (chequePropioGuardado != null)
                {
                    oServicioChequera.ActualizarNumeroCheque(chequePropioGuardado);
                    ChequeraModelView chequeraModelView = Mapper.Map<ChequeraModel, ChequeraModelView>(oServicioChequera.GetChequePropioPorId(chequePropioGuardado.Id));
                    listChequeraModelView = new List<ChequeraModelView> { chequeraModelView };
                }

                return PartialView("~/Views/CuentaCteProveedor/_RDChequesPropios.cshtml", listChequeraModelView);
            }
            catch (Exception ex)
            {
                ListaCuentaBancaria();

                return JsonView(false, ex.Message.ToString(), "~/Views/CuentaCteProveedor/_tablaChequesTerceros.cshtml", oFacturaPago);
            }
        }

        private JsonResult JsonView(bool success, string message, string viewName, object model)
        {
            return Json(new { Success = success, Message = message, View = RenderPartialView(viewName, model) });
        }

        private string RenderPartialView(string partialViewName, object model)
        {
            if (ControllerContext == null)
                return string.Empty;

            if (model == null)
                throw new ArgumentNullException("model");

            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentNullException("partialViewName");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, partialViewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuitarCheque(int IdCheque = 0)  //PartialViewResult
        {
            List<ChequeraModelView> listChequeraModelView = new List<ChequeraModelView>();
            try
            {
                oServicioChequera.DeleteChequePropio(IdCheque);
                return PartialView("~/Views/CuentaCteProveedor/_RDChequesPropios.cshtml", listChequeraModelView);
            }
            catch (Exception ex)
            {
                return JsonView(false, ex.Message.ToString(), "~/Views/CuentaCteProveedor/_RDChequesPropios.cshtml", listChequeraModelView);
            }
        }


    }

}



