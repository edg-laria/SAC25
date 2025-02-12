using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Models;
using Negocio.Servicios;
using Negocio.Modelos;
using AutoMapper;
using SAC.Models.Cobro;
using System.IO;

namespace SAC.Controllers
{
    public class CobroController : BaseController
    {
        private ServicioCobro servicioCobro = new ServicioCobro();
        private ServicioCliente servicioCliente = new ServicioCliente();
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioBancoCuenta servicioBancoCuenta = new ServicioBancoCuenta();


        private ServicioPresupuestoActual servicioPresupuestoActual = new ServicioPresupuestoActual();
        private ServicioChequera servicioChequera = new ServicioChequera();
        private ServicioCheque servicioCheque = new ServicioCheque();
        private ServicioTarjeta servicioTarjeta = new ServicioTarjeta();
        private ServicioProvincia servicioProvincia = new ServicioProvincia();
        private ServicioTipoRetencion servicioTipoRetencion = new ServicioTipoRetencion();
        private ServicioRetencion servicioRetencion = new ServicioRetencion();

        private ServicioFacturaVenta servicioFacturaVenta = new ServicioFacturaVenta();

        public CobroController()
        {
            servicioCobro._mensaje = (msg_, tipo_)  => AddMessage(tipo_, msg_);
        }

        [HttpGet()]
        public ActionResult GetListClienteJson(string term)
        {
            try
            {
                List<ClienteModel> model = servicioCliente.GetClientePorNombre(term);
                var arrayModel = (from prov in model
                                  select new AutoCompletarViewModel()
                                  {
                                      id = prov.Id,
                                      label = prov.Nombre
                                  }).ToArray();
                return Json(arrayModel, JsonRequestBehavior.AllowGet);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                servicioCobro._mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult Index(int IdCliente = 0)
        {
            CobroFacturaModoModelView modelView = new CobroFacturaModoModelView();
            try
            {
                if (IdCliente > 0)
                {

                    Session["mediosCobro"] = null;

                    modelView.Cliente = Mapper.Map<ClienteModel, ClienteModelView>(servicioCliente.GetClientePorId(IdCliente));

                    modelView.Cotizacion = servicioTipoMoneda.GetCotizacionPorIdMoneda(2);
                    modelView.Periodo = Int32.Parse(DateTime.Now.ToString("yyMM"));

                    modelView.CuentaCorriente = Mapper.Map<List<CobroFacturaModel>, List<CobroFacturaModelView>>(servicioFacturaVenta.GetClienteCtaCteCbte(IdCliente));

                    modelView.ResumenPago = null; // Mapper.Map<List<CobroFacturaModel>, List<CobroFacturaModelView>>(servicioFacturaVenta.ObtenerPorIdCliente_Moneda(IdCliente, IdTipoMoneda));

                    Session["Facturas_Cobro"] = null;

                    List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioBancoCuenta.GetAllCuenta());

                    modelView.SelectCuentasBancarias = (ListaCuentaBancaria.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Banco.Nombre + ' ' + x.BancoDescripcion
                    })).ToList();
                    modelView.SelectCuentasBancarias.Insert(0, new SelectListItem() { Value = "0", Text = "Cuentas " });
                    ViewBag.listaCuentaBancariaDrop = modelView.SelectCuentasBancarias;

                    List<TipoMonedaModelView> tipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());
                    modelView.SelectTipoMoneda = (tipoMoneda.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Descripcion
                    })).ToList();
                    modelView.IdMonedaDeOperacion = 2;
                    ///continuar agregando los drop para el cbt de ingreso

                    //List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(servicioCheque.GetAllCheque());
                    //modelView.ListaChequesTerceros = ListaChequesTerceros;
                    ////--------PartialView cheques propios          
                    //List<ChequeraModelView> ListaChequesPropios = Mapper.Map<List<ChequeraModel>, List<ChequeraModelView>>(servicioChequera.GetAllChequera());
                    //modelView.ListaChequesPropios = ListaChequesPropios;


                    List<TarjetaModelView> ListaTarjetas = Mapper.Map<List<TarjetaModel>, List<TarjetaModelView>>(servicioTarjeta.GetAllTarjetas());
                    modelView.SelectTarjetas = (ListaTarjetas.Select(x =>
                                                 new SelectListItem()
                                                 {
                                                     Value = x.Id.ToString(),
                                                     Text = x.Descripcion
                                                 })).ToList();
                    modelView.SelectTarjetas.Insert(0, new SelectListItem() { Value = "0", Text = "Tarjetas " });



                    //drop presupuesto
                    List<PresupuestoActualModelView> ListaPresupuesto = Mapper.Map<List<PresupuestoActualModel>, List<PresupuestoActualModelView>>(servicioPresupuestoActual.GetPresupuestoCliente());
                    modelView.SelectPresupuestoActual = (ListaPresupuesto.Select(x =>
                                                                        new SelectListItem()
                                                                        {
                                                                            Value = x.Id.ToString(),
                                                                            Text = x.Concepto
                                                                        })).ToList();

                    //para la retencion
                    RetencionModelView retencionModelView = new RetencionModelView();


                    List<TipoRetencionModelView> tipoRetencionModelViews = Mapper.Map<List<TipoRetencionModel>, List<TipoRetencionModelView>>(servicioTipoRetencion.GetAllTipoRetencion());
                    retencionModelView.ListaTipoRetencion = (tipoRetencionModelViews.Select(x =>
                                                 new SelectListItem()
                                                 {
                                                     Value = x.Id.ToString(),
                                                     Text = x.Descripcion
                                                 })).ToList();

                    List<ProvinciaModelView> provinciaModelViews = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvincias());
                    retencionModelView.ListadoProvincias = (provinciaModelViews.Select(x =>
                                                 new SelectListItem()
                                                 {
                                                     Value = x.Id.ToString(),
                                                     Text = x.Nombre
                                                 })).ToList();

                    //List<CobroFacturaModel> compraFacturaViewModel = Mapper.Map<List<CobroFacturaModel>, List<CobroFacturaModel>>( servicioFacturaVenta.ObtenerPorIdCliente_Moneda(modelView.IdCliente, modelView.IdTipoMoneda));
                    //if (compraFacturaViewModel.Count > 0)
                    //{
                    //    retencionModelView.ListadoFacturas = (compraFacturaViewModel.Select(x =>
                    //                          new SelectListItem()
                    //                          {
                    //                              Value = x.Id.ToString(),
                    //                              Text = x.NumeroFactura.ToString()
                    //                          })).ToList();
                    //}

                    modelView.Retencion = retencionModelView;

                    modelView.Cheque.SelectBancos = SelectListBanco();
                }
                else
                {

                    modelView.Cotizacion = servicioTipoMoneda.GetCotizacionPorIdMoneda(2);
                    modelView.Periodo = Int32.Parse(DateTime.Now.ToString("yyMM"));
                    List<TipoMonedaModelView> tipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());
                    modelView.SelectTipoMoneda = (tipoMoneda.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Descripcion
                    })).ToList();

                    modelView.IdMonedaDeOperacion = 1;

                    List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioBancoCuenta.GetAllCuenta());
                    modelView.SelectCuentasBancarias = (ListaCuentaBancaria.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Banco.Nombre + ' ' + x.BancoDescripcion
                    })).ToList();
                    modelView.SelectCuentasBancarias.Insert(0, new SelectListItem() { Value = "0", Text = "Cuentas " });
                    ViewBag.listaCuentaBancariaDrop = modelView.SelectCuentasBancarias;

                    modelView.Cheque.SelectBancos = SelectListBanco();
                }


            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                throw;
            }
            return View(modelView);
        }

        private List<SelectListItem> SelectListBanco()
        {
            List<BancoModel> listaBanco = servicioBancoCuenta.GetAllBanco();
            var lista = (listaBanco.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nombre
            })).ToList();
            // lista.Insert(0, new SelectListItem() { Value = "0", Text = "Seleccionar " });
            return lista;
        }

        //1
        public int VerificarCobro()
        {
            int respuesta = 0;
            var IdTipoComprobante = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["IdTipoComprobanteVenta"].ToString());
            try
            {
                List<FacturaModelView> listaFacturasSeleccionadas = new List<FacturaModelView>();
                listaFacturasSeleccionadas = Session["Facturas_Cobro"] as List<FacturaModelView>;
                if (listaFacturasSeleccionadas != null)
                {
                    foreach (FacturaModelView item in listaFacturasSeleccionadas)
                    {
                        // El id 34 es el comprobante 99 - Cobro
                        if (item.IdTipoComprobante == IdTipoComprobante && item.NumeroFactura == 0)
                        {
                            respuesta = 1;
                            break;
                        }
                    }
                }
                else
                {
                    respuesta = 0;
                }
                return respuesta;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                respuesta = 0;
            }
            return respuesta;
        }


        //2  esto es para alimentar la grilla de facturas seleccionadas para pagar
        [HttpPost]
        public ActionResult AgregarFacturaCobro(int idFacturaSeleccionada, int idModoCobro, string MontoCobro, string CotizacionActual, int idMoneda)
        {
            CobroFacturaModelView model = Mapper.Map<CobroFacturaModel, CobroFacturaModelView>(servicioFacturaVenta.ObtenerFacturaPorID(idFacturaSeleccionada));
            List<CobroFacturaModelView> listaFacturasSeleccionadas = new List<CobroFacturaModelView>();

            if (idModoCobro == 1)
            {
                decimal ValorActualizado = 0;
                if (idMoneda != model.IdMoneda)
                {   // pesos to new money
                    if (model.IdMoneda == 1 && idMoneda != 1)
                    {
                        ValorActualizado = model.Saldo / Convert.ToDecimal(CotizacionActual);
                    }
                    // to pesos
                    if (model.IdMoneda > 1 && idMoneda == 1)
                    {
                        ValorActualizado = model.Saldo * Convert.ToDecimal(CotizacionActual);
                    }
                    model.aplicacion = decimal.Round(ValorActualizado, 2); ;
                   
                }
                else
                {
                    model.aplicacion = model.Saldo;
                }
            }
                    
            if (idModoCobro == 2)
            {
                
                decimal ValorActualizado = 0;
                if (idMoneda != model.IdMoneda)
                {   // pesos to new money
                    if (model.IdMoneda == 1 && idMoneda != 1)
                    {
                        ValorActualizado = Convert.ToDecimal(MontoCobro) / Convert.ToDecimal(CotizacionActual);
                    }
                    // to pesos
                    if (model.IdMoneda > 1 && idMoneda == 1)
                    {
                        ValorActualizado = Convert.ToDecimal(MontoCobro) * Convert.ToDecimal(CotizacionActual);
                    }
                    model.aplicacion = decimal.Round(ValorActualizado, 2);
                    }
                else
                {
                    model.aplicacion = Convert.ToDecimal(MontoCobro);
                }
            }
          
         
            if (idModoCobro == 3)
            {
                decimal ValorActualizado = 0;
                if (idMoneda != model.IdMoneda)
                {   // pesos to new money
                    if (model.IdMoneda == 1 && idMoneda != 1)
                    {
                        ValorActualizado = model.Saldo / Convert.ToDecimal(CotizacionActual);
                    }
                    // to pesos
                    if (model.IdMoneda > 1 && idMoneda == 1)
                    {
                        ValorActualizado = model.Saldo * Convert.ToDecimal(CotizacionActual);
                    }
                    model.aplicacion = decimal.Round(ValorActualizado, 2); ;
                }
                else
                {
                    ValorActualizado = model.Saldo;
                }
                if (model.IdTipoComprobante != 34)
                {
                    model.aplicacion = -ValorActualizado;
                }
                else
                {
                    model.cobro = model.Saldo;

                    model.aplicacion = ValorActualizado;
                }

            }

            if (idMoneda != model.IdMoneda)
            {
                if (model.IdMoneda == 1 && idMoneda != 1)
                {
                    model.saldoCobro = (model.Saldo / Convert.ToDecimal(CotizacionActual)) - model.aplicacion;
                }
                // to pesos
                if (model.IdMoneda > 1 && idMoneda == 1)
                {
                    model.saldoCobro = (model.Saldo * Convert.ToDecimal(CotizacionActual)) - model.aplicacion;
                }
                
            }else
            {
                model.saldoCobro = model.Saldo - model.aplicacion;
            }
                

            
            //add cbt
            if (Session["Facturas_Cobro"] != null)
            {
                listaFacturasSeleccionadas = Session["Facturas_Cobro"] as List<CobroFacturaModelView>;
                bool existe = false;
                existe = listaFacturasSeleccionadas.Find(f => f.Id == idFacturaSeleccionada) != null ? true : false;
                if (existe == false)
                {
                    listaFacturasSeleccionadas.Add(model);
                }
            }
            else
            {
                listaFacturasSeleccionadas.Add(model);
            }


            model.Total = (model.Saldo != 0 ? model.Saldo : model.Total);
            var TotalesAFavor = listaFacturasSeleccionadas.Where(p => p.IdTipoComprobante == 34) 
                                                          .GroupBy(c => new { c.IdTipoComprobante })
                                                          .Select(c => new
                                                          {
                                                              TotalesSaldoCbtCobro = c.Sum(x => x.Total ) ,
                                                          }).FirstOrDefault();

            var totalCobro = (TotalesAFavor != null) ? TotalesAFavor.TotalesSaldoCbtCobro * -1 : 0;

            if (totalCobro != 0)
            {
               foreach (CobroFacturaModelView factura in listaFacturasSeleccionadas.Where(p => p.IdTipoComprobante == 44))
                {
                    if (factura.aplicacion <= totalCobro)
                    {
                        factura.Saldo = 0;
                        factura.saldoCobro = 0;
                        totalCobro = totalCobro - factura.aplicacion;
                    }
                    else
                    {
                        factura.aplicacion = totalCobro;
                        factura.saldoCobro = factura.Total - factura.aplicacion;
                        factura.Saldo = factura.Total - factura.aplicacion;
                    }
                }
                if (totalCobro >= 0)
                {

                    var totalfactura= TotalesAFavor.TotalesSaldoCbtCobro  + totalCobro;

                    foreach (CobroFacturaModelView cobroAFavor in listaFacturasSeleccionadas.Where(p => p.IdTipoComprobante == 34))
                    {
                        if (totalCobro >= (cobroAFavor.Total*-1) )
                        {
                            totalCobro = totalCobro + cobroAFavor.Total;
                            totalfactura = totalfactura - cobroAFavor.Total;
                            cobroAFavor.saldoCobro = 0;
                            cobroAFavor.Saldo = 0;
                            cobroAFavor.aplicacion = cobroAFavor.Total;
                            cobroAFavor.cobro = cobroAFavor.Total;

                        }
                        else
                        {

                            cobroAFavor.saldoCobro = cobroAFavor.Total - totalfactura;
                            cobroAFavor.aplicacion = totalfactura;
                            cobroAFavor.Saldo = cobroAFavor.Total - totalfactura;
                            cobroAFavor.cobro = totalfactura;

                        }
                    }
                }

            }











            CobroFacturaModoModelView cobro = new CobroFacturaModoModelView();
            Session["Facturas_Cobro"] = listaFacturasSeleccionadas;
            cobro.ResumenPago = listaFacturasSeleccionadas.OrderBy(c => c.NumeroFactura).ToList();
            return PartialView("_TablaFacturasCobro", cobro);

        }

        [HttpPost]
        public ActionResult IngresoDelCobro(CobroFacturaModoModelView medioCobro)
        {
            try
            {
                medioCobro.IdBancoCuenta = medioCobro.idCuentaBancariaSelec_;

                Session["mediosCobro"] = medioCobro;

                List<CobroFacturaModelView> listaFacturasSeleccionadas = Session["Facturas_Cobro"] as List<CobroFacturaModelView>;

                decimal totalMontoFacturas = 0;
                decimal totalMontoAplicacionFacturas = 0;
                bool existe = false;
                var IdTipoComprobante = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["IdTipoComprobanteVenta"].ToString());

                CobroFacturaModelView cobroFactura = new CobroFacturaModelView();

                if (listaFacturasSeleccionadas != null)
                {
                    foreach (CobroFacturaModelView item in listaFacturasSeleccionadas)
                    {
                        decimal valSal = Convert.ToDecimal(item.Saldo);
                        decimal valApli = Convert.ToDecimal(item.aplicacion);
                        totalMontoFacturas += valSal;
                        totalMontoAplicacionFacturas += valApli;
                        if (item.IdTipoComprobante == IdTipoComprobante && item.NumeroFactura == 0) // es porque ya hay un cobro)
                        {
                            existe = true;
                        }
                    }
                    cobroFactura.Saldo = 0;
                    cobroFactura.cobro = -medioCobro.montoTotal;
                    cobroFactura.aplicacion = -medioCobro.montoTotal;
                }
                else
                {
                    // adelanto se podria 
                    listaFacturasSeleccionadas = new List<CobroFacturaModelView>();
                    cobroFactura.Saldo = -medioCobro.montoTotal;
                    cobroFactura.cobro = -medioCobro.montoTotal;
                    cobroFactura.aplicacion = 0;
                }



                cobroFactura.IdTipoComprobante = IdTipoComprobante;
                cobroFactura.NumeroFactura = 0;                
                cobroFactura.IdCliente = medioCobro.IdCliente;
                cobroFactura.IdMoneda = medioCobro.IdMonedaDeOperacion;
                cobroFactura.Cotiza = medioCobro.Cotizacion.Monto;
                cobroFactura.Recibo = medioCobro.NumeroRecibo.ToString();
                cobroFactura.Concepto = medioCobro.ConceptoCobro;
                cobroFactura.Fecha = Convert.ToDateTime(medioCobro.Fecha);

                if (totalMontoAplicacionFacturas >= medioCobro.montoTotal)
                {
                    cobroFactura.saldoCobro = 0;
                }
                else
                {
                    cobroFactura.saldoCobro = (totalMontoFacturas > 0 ? medioCobro.montoTotal - totalMontoAplicacionFacturas : medioCobro.montoTotal) * -1;
                    cobroFactura.aplicacion = medioCobro.montoTotal + cobroFactura.saldoCobro;
                    cobroFactura.aplicacion = cobroFactura.aplicacion * -1;
                }

                if (existe == false)
                {
                    listaFacturasSeleccionadas.Add(cobroFactura);
                }

                CobroFacturaModoModelView cobro = new CobroFacturaModoModelView();
                Session["Facturas_Cobro"] = listaFacturasSeleccionadas;
                cobro.ResumenPago = listaFacturasSeleccionadas;
                return PartialView("_TablaFacturasCobro", cobro);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return null;
            }

        }

        [HttpPost]
        public ActionResult AgregarRetencion(CobroFacturaModoModelView medioCobro)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            RetencionModelView retencion = new RetencionModelView();
            retencion = medioCobro.Retencion;
            retencion.Periodo = int.Parse(DateTime.Now.ToString("yyMM"));
            retencion.IdCompraFactura = null;
            retencion.UltimaModificacion = DateTime.Now;
            retencion.Idusuario = usuario.IdUsuario;
            retencion.Fecha = DateTime.Now;
            retencion.Activo = true;

            RetencionModel retencionModel = servicioRetencion.Agregar(Mapper.Map<RetencionModelView, RetencionModel>(retencion));

            List<RetencionModelView> retencionModelView = Mapper.Map<List<RetencionModel>, List<RetencionModelView>>(servicioRetencion.GetAllRetencionVenta(retencionModel.IdFactVenta ?? 0));

            CobroFacturaModoModelView retencionCobro = new CobroFacturaModoModelView();
            retencionCobro.ListadoRetenciones = retencionModelView;

            try
            {
                return PartialView("_TablaRetenciones", retencionCobro);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult EliminarRetencion(int IdRetencionEliminar, int idFacturaEliminar)
        {

            var oRetencionModel = servicioRetencion.GetRetencionOu(IdRetencionEliminar);
            servicioRetencion.Eliminar(oRetencionModel);

            List<RetencionModelView> ListaretencionModelView = Mapper.Map<List<RetencionModel>, List<RetencionModelView>>(servicioRetencion.GetAllRetencionVenta(idFacturaEliminar));


            CobroFacturaModoModelView retencionCobro = new CobroFacturaModoModelView();
            retencionCobro.ListadoRetenciones = ListaretencionModelView;

            try
            {
                return PartialView("_TablaRetenciones", retencionCobro);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return null;
            }

        }

        private void ListaCuentaBancaria()
        {
            List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioBancoCuenta.GetAllCuenta());
            var SelectCuentasBancarias = (ListaCuentaBancaria.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Banco.Nombre + ' ' + x.BancoDescripcion
            })).ToList();
            SelectCuentasBancarias.Insert(0, new SelectListItem() { Value = "0", Text = "Cuentas " });
            ViewBag.listaCuentaBancariaDrop = SelectCuentasBancarias;

        }


        //-----------------
        [HttpPost]
        public ActionResult CancelarPago()
        {
            try
            {
                Session["Facturas_Cobro"] = null;
                return Json(new { result = true, data = 1 }, JsonRequestBehavior.AllowGet);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return Json(new { result = false, data = "Ops!, A ocurriodo un error. Contacte al Administrador" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Cobrar()
        {
            var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            var IdTipoComprobanteVenta = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["IdTipoComprobanteVenta"].ToString());


            List<CobroFacturaModelView> cobroFacturaModelViews = Session["Facturas_Cobro"] as List<CobroFacturaModelView>;
            if (Session["mediosCobro"] != null)
            {
                CobroFacturaModoModelView mediosCobro = Session["mediosCobro"] as CobroFacturaModoModelView;
                mediosCobro.IdUsuario = datosUsuario.IdUsuario;               
                servicioFacturaVenta.RegistroDeCobro(Mapper.Map<List<CobroFacturaModelView>, List<CobroFacturaModel>>(cobroFacturaModelViews)
                                                    , Mapper.Map<CobroFacturaModoModelView, CobroFacturaModoModel>(mediosCobro)
                                                    , IdTipoComprobanteVenta);
            }
            else {
                /// aplica  cbt cobro a favor a fact

                servicioFacturaVenta.RegistroDeCobroCbtAFavor(Mapper.Map<List<CobroFacturaModelView>, List<CobroFacturaModel>>(cobroFacturaModelViews), IdTipoComprobanteVenta);


            }

       
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult ObtenerDatos(int IdFact)
        {
            CobroFacturaModelView cobroFacturaModelView = new CobroFacturaModelView();
            cobroFacturaModelView = Mapper.Map<CobroFacturaModel, CobroFacturaModelView>(servicioFacturaVenta.ObtenerFacturaPorID(IdFact));
            return Json(cobroFacturaModelView, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IngresarChequeClienteAjax(ChequeModelView model)
        {
            try
            {

                List<ChequeModelView> listaCheque = new List<ChequeModelView>();
                ChequeModel chequeModel = Mapper.Map<ChequeModelView, ChequeModel>(model);
                ChequeModel cheque = servicioCheque.ExisteCheque(chequeModel);
                if (cheque == null)
                {
                    var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    chequeModel.Fecha = DateTime.Now;
                    chequeModel.Activo = true;
                    chequeModel.IdMoneda = 1;
                    chequeModel.Endosado = false;
                    chequeModel.Descripcion = "Cobro a Cliente";
                    chequeModel.UltimaModificacion = DateTime.Now;
                    chequeModel.IdUsuario = usuario.IdUsuario;

                    cheque = servicioCheque.IngresarChequeCliente(chequeModel);
                    cheque.BancoCheque = servicioBancoCuenta.GetBancoPorIdLazy(cheque.IdBanco);
                    listaCheque.Add(Mapper.Map<ChequeModel, ChequeModelView>(cheque));
                }
                //else
                //{
                //    listaCheque.Add(Mapper.Map<ChequeModel, ChequeModelView>(cheque));
                //}

                return PartialView("~/Views/Cobro/_TablaNewCheque.cshtml", listaCheque);
            }
            catch (Exception ex)
            {
                return JsonView(false, ex.Message.ToString(), "~/Views/Cobro/_TablaChequesCliente.cshtml", model);
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
        public ActionResult QuitarCheque(int IdCheque = 0)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            servicioCheque.DeleteCheque(IdCheque, usuario.IdUsuario);

            List<ChequeModelView> listaCheque = new List<ChequeModelView>();

            return PartialView("~/Views/Cobro/_TablaNewCheque.cshtml", listaCheque);

            //return PartialView("_RDChequesPropios", listChequeraModelView);
            //return PartialView("~/Views/CuentaCteProveedor/_RDChequesPropios.cshtml", listChequeraModelView);

        }







    }

}