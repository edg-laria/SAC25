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
using System.Globalization;

namespace SAC.Controllers
{
    public class CuentaCteProveedorController : BaseController
    {
        private ServicioCompra oServicioCompra = new ServicioCompra();
        private ServicioProveedor servicioProveedor = new ServicioProveedor();
        private ServicioCuentaCteProveedor servicioCuentaCteProveedor = new ServicioCuentaCteProveedor();
        private ServicioPresupuestoActual servicioPresupuestoActual = new ServicioPresupuestoActual();
        private ServicioChequera oServicioChequera = new ServicioChequera();
        private ServicioCheque oServicioCheque = new ServicioCheque();
        private ServicioBancoCuenta oServicioCuentaBancaria = new ServicioBancoCuenta();
        private ServicioTipoMoneda oServicioTipoMoneda = new ServicioTipoMoneda();

        public CuentaCteProveedorController()
        {
            servicioCuentaCteProveedor._mensaje = (msg_, tipo_)  => AddMessage(tipo_, msg_);
            oServicioCompra._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioProveedor._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioPresupuestoActual._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            oServicioChequera._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            oServicioCuentaBancaria._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            oServicioTipoMoneda._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
        }

        // GET: CuentaCteProveedor
        public ActionResult Index()
        {

            //DateTime fechaDesde = DateTime.Now;
            DateTime fechaDesde = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            List<CuentaCteProveedorModelView> model = Mapper.Map<List<CuentaCteProveedorModel>, List<CuentaCteProveedorModelView>>(servicioCuentaCteProveedor.GetCtaCteProveedorAl(fechaDesde));
            var Lista = model.GroupBy(c => new { c.ProveedorCodigo })
                                             .Select(c => new CuentaCteProveedorModelView()
                                             {
                                                 ProveedorCodigo = c.FirstOrDefault().ProveedorCodigo,
                                                 ProveedorNombre = c.FirstOrDefault().ProveedorNombre,
                                                 MontoPesos = c.Sum(x => x.MontoPesos),
                                                 MontoDolares = c.Sum(x => x.MontoDolares),
                                                 TotalDeuda = c.Sum(x => x.TotalDeuda),
                                                 UltimoMovimiento = c.FirstOrDefault().UltimoMovimiento,
                                             }).ToList();


            return View(Lista);
        }

        public ActionResult CtaCteDetalle(string searchIdProveedor,  string searchFecha)
        {

            CuentaCorrienteProveedorModelView model = new CuentaCorrienteProveedorModelView();

            if (!string.IsNullOrEmpty(searchIdProveedor))
            {
                DateTime fechaDesde = DateTime.Now;
                if (!string.IsNullOrEmpty(searchFecha))
                {
                    fechaDesde = DateTime.ParseExact(searchFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture); 
                }
                int idProveedor = int.Parse(searchIdProveedor);
                ViewBag.idProveedor = idProveedor;

                model.Proveedor = Mapper.Map<ProveedorModel, ProveedorModelView>(servicioProveedor.GetProveedorCompleto(idProveedor));                
                model.Detalles = Mapper.Map<List<CuentaCorrienteProveedorDetallesModel>, List<CuentaCorrienteProveedorDetallesModelView>>(servicioCuentaCteProveedor.CtaCteDetalle(idProveedor, fechaDesde));


            }



            return View(model);
        }



        public ActionResult PagarFactura(int idProveedor)
        {
            FacturaPagoViewModel model = new FacturaPagoViewModel();
            model.ListaFacturas = Mapper.Map<List<CompraFacturaModel>, List<CompraFacturaViewModel>>(oServicioCompra.ObtenerPorIDProveedor(idProveedor));
            model.Proveedor = Mapper.Map<ProveedorModel, ProveedorModelView>(servicioProveedor.GetProveedor(idProveedor));
            List<PresupuestoActualModelView> ListaPresupuesto = Mapper.Map<List<PresupuestoActualModel>, List<PresupuestoActualModelView>>(servicioPresupuestoActual.GetAllPresupuestos());
            List<SelectListItem> ListaPresupuestoActualDrop = (ListaPresupuesto.Select(x =>
                                                                new SelectListItem()
                                                                {
                                                                    Value = x.Id.ToString(),
                                                                    Text = x.Concepto
                                                                })).ToList();
            model.ListaPresupuestoActual = ListaPresupuestoActualDrop;

            //---------para el PartialView cheques terceros
            List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());
            model.ListaChequesTerceros = ListaChequesTerceros;
            //--------PartialView cheques propios          

            List<ChequeraModelView> ListaChequesPropios = Mapper.Map<List<ChequeraModel>, List<ChequeraModelView>>(oServicioChequera.GetAllChequera());
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

            model.ListaChequesPropios = new List<ChequeraModelView>(); //ListaChequesPropios; 

            // segun la cuenta bancaria selecccionada se obtiene el numero de cheque  hacerlo via json
            model.listaCuentaBancariaDrop = retornoListaCuentaBancaria;
            ViewBag.listaCuentaBancariaDrop = retornoListaCuentaBancaria;
            model.ListaTipoMonedaDrop = retornoListaTipoMoneda;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacturaPago(FacturaPagoViewModel model)
        {
            try
            {
                //agrego el usuario
                var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                model.idUsuario = datosUsuario.IdUsuario;
                //registramos
                oServicioCompra.RegistrarPago(Mapper.Map<FacturaPagoViewModel, FacturaPagoModel>(model));

                return RedirectToAction("Index");

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                servicioProveedor._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return View(model);
            }

        }

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

            return PartialView("_TablaChequesPropios", oChequesModel);

        }

        [HttpPost]
        public ActionResult CargarChequesTerceros()

        {
            //cheques de terceros

            List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());

            FacturaPagoViewModel oChequesModel = new FacturaPagoViewModel();
            oChequesModel.ListaChequesTerceros = ListaChequesTerceros;

            return PartialView("_tablaChequesTerceros", oChequesModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IngresarChequeAjax(ChequeraModelView oFacturaPago)

        {
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
                ChequeraModel chequePropioGuardado = oServicioChequera.Insertar(Mapper.Map<ChequeraModelView, ChequeraModel>(oFacturaPago));
                if (chequePropioGuardado != null)
                {
                    oServicioChequera.ActualizarNumeroCheque(chequePropioGuardado);
                }

                
                ChequeraModelView chequeraModelView = Mapper.Map<ChequeraModel, ChequeraModelView>(oServicioChequera.GetChequePropioPorId(chequePropioGuardado.Id));
                List<ChequeraModelView> listChequeraModelView = new List<ChequeraModelView>{ chequeraModelView };
             
                return PartialView("_RDChequesPropios", listChequeraModelView);

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                //throw;
                oServicioChequera._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuitarCheque(int IdCheque = 0)  //PartialViewResult
        {
            try
            {
                oServicioChequera.DeleteChequePropio(IdCheque);
                List<ChequeraModelView> listChequeraModelView = new List<ChequeraModelView>();                
                return PartialView("_RDChequesPropios", listChequeraModelView);

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                oServicioChequera._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }

        }
      
        
        

        [HttpPost]
        public ActionResult CargarCuentas()
        {
            //cheques de terceros
            ServicioBancoCuenta oServicioCuentaBancaria = new ServicioBancoCuenta();
            List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(oServicioCuentaBancaria.GetAllCuenta());

            FacturaPagoViewModel oCompraFacturaModel = new FacturaPagoViewModel();

            List<SelectListItem> retornoListaCuentaBancaria = new List<SelectListItem>();
            retornoListaCuentaBancaria = (ListaCuentaBancaria.Select(x =>
                                         new SelectListItem()
                                         {
                                             Value = x.Id.ToString(),
                                             Text = x.BancoDescripcion
                                         })).ToList();

            oCompraFacturaModel.listaCuentaBancariaDrop = retornoListaCuentaBancaria;
            return PartialView("_TablaCuentasBancaria", oCompraFacturaModel);
        }

        [HttpPost]
        public ActionResult CargarTarjetas()
        {

            //cheques de terceros
            ServicioTarjeta oServicioTarjeta = new ServicioTarjeta();
            List<TarjetaModelView> ListaCuentaBancaria = Mapper.Map<List<TarjetaModel>, List<TarjetaModelView>>(oServicioTarjeta.GetAllTarjetas());

            FacturaPagoViewModel oCompraFacturaModel = new FacturaPagoViewModel();

            List<SelectListItem> retornoListaTarjetas = new List<SelectListItem>();
            retornoListaTarjetas = (ListaCuentaBancaria.Select(x =>
                                         new SelectListItem()
                                         {
                                             Value = x.Id.ToString(),
                                             Text = x.Descripcion
                                         })).ToList();

            oCompraFacturaModel.listaTarjetasDrop = retornoListaTarjetas;
            return PartialView("_TablaTarjetas", oCompraFacturaModel);

        }


        [HttpGet()]
        public ActionResult GetNroChequePorCta(int id)
        {
            try
            {
                ///ak realizar la busqueda por idcta del numero de cheque              
                return Json(new { result = true, data = oServicioChequera.GetNroChequePorCta(id) }, JsonRequestBehavior.AllowGet);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return Json(new { result = false, data = "Ops!, A ocurriodo un error. Contacte al Administrador" }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult Filtrado(string inicio)
        {
            var dInicio = DateTime.Now;
            List<CuentaCteProveedorModelView> model = new List<CuentaCteProveedorModelView>();
            if (inicio != "")
            {
                dInicio = DateTime.ParseExact(inicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            model = Mapper.Map<List<CuentaCteProveedorModel>, List<CuentaCteProveedorModelView>>(servicioCuentaCteProveedor.GetCtaCteProveedorAl(dInicio));

            /*
            if (inicio == "" )
            {


                DateTime fechaDesde = DateTime.Now;
                model = Mapper.Map<List<CuentaCteProveedorModel>, List<CuentaCteProveedorModelView>>(servicioCuentaCteProveedor.GetCtaCteProveedorAl(fechaDesde));

            }
            else
            {
                var dInicio = DateTime.ParseExact(inicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                model = Mapper.Map<List<CuentaCteProveedorModel>, List<CuentaCteProveedorModelView>>(servicioCuentaCteProveedor.GetCtaCteProveedorAl(dInicio));
            }
            */

            var Lista = model.GroupBy(c => new { c.ProveedorCodigo })
                                           .Select(c => new CuentaCteProveedorModelView()
                                           {
                                               ProveedorCodigo = c.FirstOrDefault().ProveedorCodigo,
                                               ProveedorNombre = c.FirstOrDefault().ProveedorNombre,
                                               MontoPesos = c.Sum(x => x.MontoPesos),
                                               MontoDolares = c.Sum(x => x.MontoDolares),
                                               TotalDeuda = c.Sum(x => x.TotalDeuda),
                                               UltimoMovimiento = c.FirstOrDefault().UltimoMovimiento,
                                           }).ToList();

            return PartialView("_Tabla", Lista);

        }

        //public int AgregarFacturaAPagar(string idFactura)
        //{
        //    try
        //    {
        //        CompraFacturaPagoViewModel model = new CompraFacturaPagoViewModel();

        //        model.FacturasAPagar = Mapper.Map<List<CompraFacturaModel>, List<CompraFacturaViewModel>>(oServicioCompra.ObtenerListaPorID(int.Parse(idFactura)));
        //        return 1;
        //    }
        //    catch(Exception ex)
        //    {
        //        return 0;
        //    }
        //}

        //public int QuitarFacturaAPagar(string idFactura)
        //{
        //    try
        //    {
        //        CompraFacturaPagoViewModel model = new CompraFacturaPagoViewModel();
        //        model.FacturasAPagar.RemoveAll(p => p.Id.Equals(int.Parse(idFactura)));
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}
        //public ActionResult Filtrado(string inicio, string fin)
        //{
        //    List<CuentaCteProveedorModelView> model = new List<CuentaCteProveedorModelView>();
        //    if (inicio == "" || fin == "")
        //    {
        //        model = Mapper.Map<List<CuentaCteProveedorModel>, List<CuentaCteProveedorModelView>>(servicioCuentaCteProveedor.GetAllCuentasCteProveedor());
        //    }
        //    else
        //    {
        //        var dInicio = DateTime.ParseExact(inicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        var dFin = DateTime.ParseExact(fin, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        //        model = Mapper.Map<List<CuentaCteProveedorModel>, List<CuentaCteProveedorModelView>>(servicioCuentaCteProveedor.GetAllCuentasCteProveedor(dInicio, dFin));
        //    }
        //    return PartialView("_Tabla", model);

        //}

        //[HttpPost, ActionName("QuitarCheque")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteChequePropio(int id)
        //{
        //    try
        //    {
        //        oServicioChequera.DeleteChequePropio(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { result = false, data = "Ops!, A ocurriodo un error. Contacte al Administrador" }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { result = true, data = "Operacion realizada correctamente... " }, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult AjaxMethodIngresarCheque(FacturaPagoViewModel oFacturaPago)
        //{
        //    string strJson;
        //    try
        //    {


        //        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(proveedor);
        //        if ((strJson != null))
        //        {
        //            var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
        //            return rJson;
        //        }            
        //        ///ak realizar la busqueda por idcta del numero de cheque              
        //        return Json(new { result = true, data = oServicioChequera.GetNroChequePorCta(id) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { result = false, data = "Ops!, A ocurriodo un error. Contacte al Administrador" }, JsonRequestBehavior.AllowGet);
        //    }

        //}

   
    }
}