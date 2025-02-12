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
using System.Web.Script.Serialization;

namespace SAC.Controllers
{
    public class BancoController : BaseController
    {

        private ServicioCaja servicioCaja = new ServicioCaja();

        private ServicioCajaGrupo servicioCajaGrupo = new ServicioCajaGrupo();
        private ServicioCajaSaldo servicioCajaSaldo = new ServicioCajaSaldo();
        private ServicioBancoCuenta servicioBanco = new ServicioBancoCuenta();
        private ServicioBancoCuentaBancaria servicioBancoCuentaBancaria = new ServicioBancoCuentaBancaria();
        private ServicioCliente oservicioCliente = new ServicioCliente();
        private ServicioCheque oservicioCheque = new ServicioCheque();
        private ServicioTarjeta oservicioTarjeta = new ServicioTarjeta();
        private ServicioTarjetaOperacion oservicioTarjetaOperacion = new ServicioTarjetaOperacion();
        
        public ServicioPresupuestoActual servicioPresupuestoActual = new ServicioPresupuestoActual();
        public ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        public ServicioImputacion servicioImputacion = new ServicioImputacion();
        public ServicioContable servicioContable = new ServicioContable();
        private ServicioBancoCuenta oServicioBancoCuenta = new ServicioBancoCuenta();

        private ServicioCheque oServicioCheque = new ServicioCheque();

        public BancoController()
        {
            servicioBanco._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_); // CrearTempData(msg_, tipo_);
            servicioCaja._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);

        }


        public ActionResult Index()
        {
            List<BancoCuentaModelView> model = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioBanco.GetAllCuenta());

            return View(model);
        }
        public ActionResult Agregar()
        {
            BancoCuentaModelView model = new BancoCuentaModelView();
            SelectBanco();
            SelectMoneda();

            return View(model);
        }
        [HttpPost]
        public ActionResult Agregar(BancoCuentaModelView model)
        {           
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

                    BancoCuentaModel op = Mapper.Map<BancoCuentaModelView, BancoCuentaModel>(model);

                    op.IdMoneda = 1;
                    op.Activo = true;
                    op.IdUsuario = datosUsuario.IdUsuario;
                    op.Saldo = 0;
                    op.NumeroCierre = 0;

                    var now = DateTime.Now;
                    var date = new DateTime(now.Year, now.Month, now.Day,
                                            now.Hour, now.Minute,
                                            now.Second);
                    op.Fecha = date;
                    op.UltimaModificacion = date;
                    BancoCuentaModel respuesta = servicioBanco.GuardarCuentaBancaria(op);
                    if (respuesta != null)
                    {
                        AddMessage("success", "La cuenta se registro correctamente");
                        return RedirectToAction("Index");
                    }
                    SelectBanco();
                    SelectMoneda();
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                AddMessage("error", "Agregar Cuenta: " + ex.Message);
                return View(model);
            }

        }


        public ActionResult Editar(int _id)
        {
            BancoCuentaModelView model = new BancoCuentaModelView();
            model = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(_id));
            SelectBanco();
            SelectMoneda();
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(BancoCuentaModelView model)
        {   
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

                    BancoCuentaModel op = Mapper.Map<BancoCuentaModelView, BancoCuentaModel>(model);

                    
                    //op.Activo = true;
                    op.IdUsuario = datosUsuario.IdUsuario;                                 
                    op.UltimaModificacion = DateTime.Now;

                    BancoCuentaModel respuesta = servicioBanco.UpdateCuentaBancaria(op);
                    if (respuesta != null)
                    {
                        
                        AddMessage("success", "La cuenta se actualizo correctamente");
                        return RedirectToAction("Index");
                    }
                    SelectBanco();
                    SelectMoneda();
                    return View(model);

                 
                }
            }

            catch (Exception ex)
            {
                AddMessage("error", ex.Message);
                return View(model);
            }


        }



        [HttpPost]
        public ActionResult Eliminar(int idCuenta)
        {
            var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

            BancoCuentaModel op = new BancoCuentaModel
            {
                Id = idCuenta,
                Activo = false,
                IdUsuario = datosUsuario.IdUsuario,
                UltimaModificacion = DateTime.Now
            };

            BancoCuentaModel respuesta = servicioBanco.DeshabilitarCuentaBancaria(op);
            if (respuesta != null)
            {
                AddMessage("success", "La cuenta esta Deshabilitada");
                return RedirectToAction("Index");
            }

         
            return RedirectToAction("Index");
        }



        private void SelectMoneda()
        {
            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());
            List<SelectListItem> lstTipoMoneda = (ListaTipoMoneda.Select(x =>
                                                     new SelectListItem()
                                                     {
                                                         Value = x.Id.ToString(),
                                                         Text = x.Descripcion
                                                     })).ToList();
            ViewBag.SelectMoneda = lstTipoMoneda;
        }

        private void SelectBanco()
        {
            List<BancoModel> model = servicioBanco.GetAllBanco();
            List<SelectListItem> ListBancoCuenta = null;
            ListBancoCuenta = (model.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nombre
            })).ToList();         
            ViewBag.SelectBanco = ListBancoCuenta;
        }

        // cargar bancos 

        private void CargarBanco()
        {
            List<BancoCuentaModel> ListBancoCuentaModels = servicioBanco.GetAllCuenta();

            List<SelectListItem> ListBancoCuenta = null;
            ListBancoCuenta = (ListBancoCuentaModels.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.BancoDescripcion
            })).ToList();
            ListBancoCuenta.Insert(0, new SelectListItem { Text = "Seleccionar", Value = "" });
            ViewBag.CargarBanco = ListBancoCuenta;
        }

        // PRIMERA CARGA DE LA PAGINA  DE LA VISTA CHEQUES

        public ActionResult Cheques()
        {
            ChequeModelView model = new ChequeModelView();
            model.ListaCheque = new List<ChequeModelView>();
            model.cFechaDesde = DateTime.Now;
            CargarListaOpcion();
            return View(model);
        }

        [HttpPost]
        public ActionResult Cheques(string Cfechadesde, string Cfechahasta, int Idbanco = 0, int IdCliente = 0)
        {

            ChequeModelView model = new ChequeModelView();
            model.ListaCheque = new List<ChequeModelView>();

            DateTime fechaDesde = DateTime.Now;
            if (!string.IsNullOrEmpty(Cfechadesde))
            {
                fechaDesde = DateTime.ParseExact(Cfechadesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            DateTime fechaHasta = DateTime.Now;
            if (!string.IsNullOrEmpty(Cfechahasta))
            {
                fechaHasta = DateTime.ParseExact(Cfechahasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
           
            model.ListaCheque = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oservicioCheque.BuscarCheque(IdCliente, Idbanco, fechaDesde, fechaHasta));

            model.cFechaDesde = fechaDesde;
            model.cFechaHasta = fechaHasta;

            CargarListaOpcion();

            return View(model);

        }
 
        public ActionResult Tarjetas()
        {

            TarjetaOperacionModelView model = new TarjetaOperacionModelView();
            CargarTarjetas();
            model.ListaTarjetaOperacion = new List<TarjetaOperacionModelView>();
            model.cFechaDesde = DateTime.Today;
            model.cFechaHasta = DateTime.Today;
            return View(model);
        }

        [HttpPost]
        public ActionResult Tarjetas(DateTime Cfechadesde, DateTime Cfechahasta, int IdTipoTarjeta = 0)
        {
            TarjetaOperacionModelView model = new TarjetaOperacionModelView();
            model.ListaTarjetaOperacion = new List<TarjetaOperacionModelView>();
            if (IdTipoTarjeta != 0)
            {


                if (Cfechadesde == Cfechahasta)  // si la fecha es igual trae todos los movimientos
                {
                    model.ListaTarjetaOperacion = Mapper.Map<List<TarjetaOperacionModel>, List<TarjetaOperacionModelView>>(oservicioTarjetaOperacion.GetTarjetaOperacionGastos(IdTipoTarjeta));
                }
                else  // si la fecha es distintas filtra por las fecha
                {
                    model.ListaTarjetaOperacion = Mapper.Map<List<TarjetaOperacionModel>, List<TarjetaOperacionModelView>>(oservicioTarjetaOperacion.GetTarjetaOperacionGastos(IdTipoTarjeta, Cfechadesde, Cfechahasta));
                }
            }

            model.cFechaDesde = Cfechadesde;
            model.cFechaHasta = Cfechahasta;

            CargarTarjetas();

            return View(model);


        }

        [HttpPost]
        public ActionResult ConciliarTarjeta(TarjetaOperacionModelView model)
        {
            string[] movimientosSeleccionados = model.IdTarjetaConciliar.Split(';');
            foreach (var item in movimientosSeleccionados)
            {
                oservicioTarjetaOperacion.ConciliarMovimiento(int.Parse(item));
            }

            return RedirectToAction("Tarjetas");
        }



        public void CargarListaOpcion()
        {

            ViewBag.Opcion1 = GetOpcion1();
            ViewBag.Opcion2 = GetOpcion2();

        }

        private List<SelectListItem> GetOpcion1()
        {
            return Opcion1;
        }

        private static readonly List<SelectListItem> Opcion1 = new List<SelectListItem>
        {
            new SelectListItem() {Value = "0",Text = "Elija una opcion"},
            new SelectListItem() {Value = "1",Text = "Origen"},
            new SelectListItem() {Value = "2",Text = "Destino"},
            new SelectListItem() {Value = "3",Text = "Cheques en Cartera"}
        };

        private List<SelectListItem> GetOpcion2()
        {
            return Opcion2;
        }

        private static readonly List<SelectListItem> Opcion2 = new List<SelectListItem>
        {
            new SelectListItem() {Value = "0",Text = "Elija una opcion"},
            new SelectListItem() {Value = "1",Text = "Fecha de Ingreso"},
            new SelectListItem() {Value = "2",Text = "Clientes"},
            new SelectListItem() {Value = "3",Text = "Banco"}
        };

        // cargar  Clientes
        private void CargarClientes()
        {

            List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Codigo
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Seleccionar Grupo", Value = "" });
            ViewBag.CargarClientes = retornoListaCajaGrupo;


        }

        // metodos de autocompletar de Banco y de clientes

        [HttpGet()]
        public ActionResult GetListBancoJson(string term)
        {
            try
            {
                IList<BancoCuentaModel> proveedor = servicioBanco.GetBancoPorNombre(term);
                var arrayProveedor = (from prov in proveedor
                                      select new AutoCompletarViewModel()
                                      {
                                          id = prov.Id,
                                          label = prov.Banco.Nombre
                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                servicioBanco._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetListClienteJson(string term)
        {
            try
            {
                List<ClienteModel> proveedor = oservicioCliente.GetClientePorNombre(term);
                var arrayProveedor = (from prov in proveedor
                                      select new AutoCompletarViewModel()
                                      {
                                          id = prov.Id,
                                          label = prov.Nombre
                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                servicioBanco._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }

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
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Seleccionar una Tarjeta", Value = "" });
            ViewBag.ListaTarjeta = retornoListaCajaGrupo;


        }


        [HttpGet]
        public ActionResult IngresoCuentaBancaria(int IdBancoCuenta = 0, String searchFecha = null)
        
        {
            IngresoBancoModelView modelView = new IngresoBancoModelView();
            modelView.BancoCuenta = new BancoCuentaModelView();
            modelView.BancoCuenta.Fecha = DateTime.Now;
            modelView.BancoCuenta.IdMoneda = 1;
            modelView.Cotizacion = servicioTipoMoneda.GetCotizacionPorIdMoneda(2).Monto;
            if (IdBancoCuenta == 0) 
            {
                var a = servicioBanco.GetBancoCuentaPorNombre("Pesos").FirstOrDefault();
                modelView.BancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(a);
            }
            else { modelView.BancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(IdBancoCuenta)); }
            
                DateTime fecha = DateTime.Now;
               
                if (!string.IsNullOrEmpty(searchFecha))
                {
                    fecha = DateTime.ParseExact(searchFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    modelView.Fecha = fecha.ToString("dd/MM/yyyy");
            }
            else
            {
                modelView.Fecha = searchFecha;
            }
            // Assign the selected date to the model  
            
            

            modelView.ListaBancoCuenta = Mapper.Map<List<BancoCuentaBancariaModel>, List<BancoCuentaBancariaModelView>>(servicioBanco.GetmMovimientosPendientesCuentaBancaria(modelView.BancoCuenta.Id, fecha));
            
            CargarBanco();

            BancoCuentaBancariaModelView ingresos = new BancoCuentaBancariaModelView();
            ingresos.ListItemsGrupoCaja = CargarCajaGrupo();
            ingresos.ListItemsBancoCuenta = CargarBancoCuenta();
            ingresos.IdTipoMoneda = modelView.BancoCuenta.IdMoneda;

            //debe ser segun la cuenta banco seleccionada
            //---------para el PartialView cheques terceros

            List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());

            // validacion de fecha de efectivo <= a fecha actual
            ingresos.ListaChequesTerceros = (from c in ListaChequesTerceros
                                             where c.FechaEgreso >= DateTime.Now
                                             select c).ToList();

            ingresos.ListaChequesTerceros = ListaChequesTerceros;
            modelView.Ingresos = ingresos;
            modelView.IdBancoCuenta = modelView.BancoCuenta.Id;

            return View(modelView);
        }

        [HttpPost]
        public ActionResult Ingreso(BancoCuentaBancariaModelView modelView)
        {
            //IngresoBancoModelView model
            //BancoCuentaBancariaModelView modelView = model.
            //Ingresos;
            //modelView.Cotizacion = model.Cotizacion;
            //modelView.IdBancoCuenta = model.IdBancoCuenta;
            // el tipo de moneda esta determinado por la cta

            BancoCuentaModelView modelBancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(modelView.IdBancoCuenta));
            switch (modelView.TipoMovimiento)
            {
                case "cv":

                    RegistroIngresoPorCargosVarios(modelView, modelBancoCuenta);

                    break;

                case "de":
                    
                    RegistroIngresoPorDespositoEfectivo(modelView, modelBancoCuenta);
                    break;

                case "tc":

                    RegistroIngresoPorTrasnferenciaCaja(modelView, modelBancoCuenta);
                    break;

                case "tt":

                    RegistroIngresoPorTrasnferenciaEntreCuentas(modelView, modelBancoCuenta);
                    break;

                case "ch":

                    RegistroIngresoDepositoDeCheque(modelView, modelBancoCuenta);
                    break;
                default:
                    //ingreso por cheque

                    break;
            }


            return RedirectToAction("IngresoCuentaBancaria", new { IdBancoCuenta = modelBancoCuenta.Id});
        }


        [HttpPost]
        public ActionResult ConfirmarConciliacion(IngresoBancoModelView modelView)
        {
            //BancoCuentaModelView modelBancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(modelView.IdBancoCuenta));
            //modelView.ListaBancoCuenta = Mapper.Map<List<BancoCuentaBancariaModel>, List<BancoCuentaBancariaModelView>>(servicioBanco.GetmMovimientosPendientesCuentaBancaria(IdBancoCuenta, fecha));
            string[] movimientosSeleccionados = modelView.IdConciliacionMovimiento.Split(';');
            foreach (var item in movimientosSeleccionados)
            {
                servicioBancoCuentaBancaria.ConciliarMovimiento(int.Parse(item));                               
            }

            return RedirectToAction("IngresoCuentaBancaria");
        }


        [HttpPost]
        public ActionResult CierreCuenta(IngresoBancoModelView modelView)
        {
            BancoCuentaModelView modelBancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(modelView.IdBancoCuenta));

            List<BancoCuentaBancariaModel> bancoCuentaBancariaModels = servicioBancoCuentaBancaria.GetCtaBriaImpresion(0, modelBancoCuenta.Id);

            //decimal SaldoCierre = modelBancoCuenta.Saldo + bancoCuentaBancariaModels.Sum(s => s.Importe);
            decimal SaldoCierre = bancoCuentaBancariaModels.Sum(s => s.Importe);

            BancoCuentaModel cierre = servicioBanco.CierreDeCuentaBancaria(modelBancoCuenta.Id, SaldoCierre, modelBancoCuenta.Fecha);

            foreach (var item in bancoCuentaBancariaModels)
            {
                //actualizar nro cierre en BancoCuentaBancariaModel
                item.NumeroCierre = cierre.NumeroCierre;
                servicioBancoCuentaBancaria.UpdateNumeroCierreMovimiento(item);
            }

           RegistroIngresoPorCierre(modelView, modelBancoCuenta, SaldoCierre);


            return RedirectToAction("IngresoCuentaBancaria");
        }
        [HttpGet()]
        public ActionResult GetImprimirCtaBriaJson(int IdCierre, int IdBanco, string searchFecha = null)
        {
            try
            {
                
                DateTime fecha = DateTime.Now;
                if (!string.IsNullOrEmpty(searchFecha))
                {
                    fecha = DateTime.ParseExact(searchFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                

                BancoCuentaModelView modelBancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(IdBanco));

                decimal SaldoCierreAnterior = 0;
                IList<BancoCuentaBancariaModel> DatosCta; // Declare DatosCta outside the if-else blocks  

                if (IdCierre == 0)
                {
                    DatosCta = servicioBancoCuentaBancaria.GetCtaBriaImpresion0(IdCierre,IdBanco,fecha);
                }
                else
                {
                    List<BancoCuentaBancariaModel> bancoCuentaBancariaModels = servicioBancoCuentaBancaria.GetCtaBriaImpresion(modelBancoCuenta.NumeroCierre - 1, modelBancoCuenta.IdBanco);
                    SaldoCierreAnterior = bancoCuentaBancariaModels.Sum(s => s.Importe);
                    DatosCta = servicioBancoCuentaBancaria.GetCtaBriaImpresion(IdCierre, IdBanco);
                }
               

                decimal sumaImporteAcumulado = SaldoCierreAnterior;

                var tablaSinHeaderJson = DatosCta.Select(d =>
                {
                    //sumaImporteAcumulado += d.Importe;
                    // add Edgardo
                    if (d.Conciliacion)
                    {
                        sumaImporteAcumulado += d.Importe;
                    }
                    return new object[]
                        {
                            
                            d.CuentaDescripcion,
                            d.Fecha?.ToString("yyyy-MM-dd"),
                            d.FechaEfectiva?.ToString("yyyy-MM-dd"),
                            d.Importe <= 0 ? d.Importe : 0, // Si el importe es positivo, se muestra tal cual; si no, se muestra 0
                            d.Importe >= 0 ? d.Importe : 0, // Si el importe es positivo, se muestra tal cual; si no, se muestra 
                            sumaImporteAcumulado,  // Suma del importe del registro y los anteriores (si es positivo)
                            d.Conciliacion  
                        };
                }).ToArray();
               
                var tablaJson = new
                {
                    headers = new[] { "Descripcion", "Fecha Ingreso", "Fecha Efectivo", "Debe", "Haber", "Saldo","" }, // Encabezados de la tabla
                    data = tablaSinHeaderJson
                };

                var responseJson = new
                {
                    tablaJson = tablaJson,
                    NombreCuenta = modelBancoCuenta.BancoDescripcion,
                    FechaCierre = modelBancoCuenta.Fecha.ToString("yyyy-MM-dd")
                };
                return Json(responseJson, JsonRequestBehavior.AllowGet);
           }
            catch (Exception ex)
            {
                AddMessage("Error", "Ops!, No se pudo Imprimir la Factura. Contacte al Administrador" + ex.Message);
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
        }



        private void RegistroIngresoDepositoDeCheque(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            try
            {
                if (modelView.Importe <= 0)
                {
                    throw new Exception("Complete los Campos requeridos");
                }

                if ((modelView.idChequesTerceros != null) && (modelView.idChequesTerceros != ""))
                {

                    ChequeModel oCheque = new ChequeModel();
                    BancoModel bancoModel = new BancoModel();

                    var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
                    fecha = fecha.AddDays(2);

                    string[] chequesSeleccionados = modelView.idChequesTerceros.Split(';');
                    foreach (var itemCheque in chequesSeleccionados)
                    {

                        oCheque = oServicioCheque.obtenerCheque(int.Parse(itemCheque));
                        if (oCheque != null)
                        {


                            // obtener banco seleccionado para deposito de cheque
                            bancoModel = servicioBanco.GetBancoPorId(modelBancoCuenta.IdBanco);//obtener banco

                            //--------------------------- BancoCuentaBancaria ------
                            modelView.NumeroOperacion = oCheque.NumeroCheque;
                            modelView.IdBancoCuenta = modelBancoCuenta.Id; //obtener el id de la cuta seleccionada
                            modelView.Fecha = Convert.ToDateTime(DateTime.Now);
                            modelView.FechaIngreso = Convert.ToDateTime(DateTime.Now);
                            modelView.FechaEfectiva = fecha;
                            modelView.DiaClearing = "2";
                            modelView.Conciliacion = false;
                            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("TRANS");
                            if (cajaGrupoModel != null)
                            {
                                modelView.IdGrupoCaja = cajaGrupoModel.Id; //"TRANS"
                            }
                            else
                            {
                                modelView.IdGrupoCaja = 0;
                            }
                            modelView.IdCliente = "BANCO";
                            modelView.IdUsuario = usuario.IdUsuario;
                            modelView.Importe *= -1; /// valor en negativo 
                            modelView.CuentaDescripcion = "Deposito de Cheque Nº" + oCheque.NumeroCheque.ToString();//nro cheque

                            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));


                            //------------------- Actualizar cheques                    
                            oCheque.FechaEgreso = DateTime.Now;
                            oCheque.Destino = modelView.CuentaDescripcion + bancoModel.Nombre;
                            //oCheque.NumeroPago = null;
                            oCheque.Proveedor = "BANCO";
                            oCheque.Activo = false;
                            oCheque.Endosado = true;
                            oServicioCheque.Actualizar(oCheque);

                            ///------------------- Caja--------------------

                            cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("BANCH");
                            if (cajaGrupoModel != null)
                            {
                                modelView.IdGrupoCaja = cajaGrupoModel.Id;
                            }
                            else
                            {
                                modelView.IdGrupoCaja = 0;
                            }

                            servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView,
                                                                  BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);


                        }
                    }

                }

            }
            catch (Exception ex)
            {
                //podria guardar el log
                servicioCaja._mensaje?.Invoke("Ops!!. " + ex.Message.ToString(), "error");
                //return RedirectToAction(nameof(IngresoCuentaBancaria));
            }

        }

        private void RegistroIngresoPorTrasnferenciaEntreCuentas(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            //DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            DateTime fecha = Convert.ToDateTime(modelView.Fecha, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            modelView.IdBancoCuenta = modelBancoCuenta.Id; // el que es seleccionado por el cliente en la vntana consulta
            modelView.Fecha = fecha;
            modelView.FechaIngreso = Convert.ToDateTime(DateTime.Now);
            modelView.FechaEfectiva = fecha.AddDays(2); 
            modelView.DiaClearing = "2";
           /*
            var ViejoImporte = modelView.Importe;
            if (modelView.IdTipoMoneda ==2)
            {
                modelView.Importe = modelView.ImporteDolar;
            }
            */
            modelView.Importe *= -1; // paso a negativo

            modelView.Conciliacion = false;

            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("TRANS");
            if (cajaGrupoModel != null)
            { modelView.IdGrupoCaja = cajaGrupoModel.Id; }
            else { modelView.IdGrupoCaja = 0; }

            modelView.IdCliente = "BANCO";
            modelView.IdUsuario = usuario.IdUsuario;
            
            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));

            //Cta destino
            modelView.IdBancoCuenta = modelView.IdBancoCuentaDestino;                
            var bancoCuenta = oServicioBancoCuenta.GetCuentaPorId(modelView.IdBancoCuenta);
            if (bancoCuenta.IdMoneda == 2)
            {
                //modelView.Importe = modelView.ImporteDolar;
                modelView.Importe = modelView.Importe/ modelView.ImporteDolar;
            }
            else
            {
               modelView.Importe *= modelView.ImporteDolar;
            }
            modelView.Importe *= -1; // paso a positivo    
            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));
        }

        private void RegistroIngresoPorTrasnferenciaCaja(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            modelView.IdBancoCuenta = modelBancoCuenta.Id;
            modelView.Fecha = Convert.ToDateTime(modelView.Fecha);
            modelView.FechaIngreso = Convert.ToDateTime(modelView.Fecha);
            modelView.FechaEfectiva = fecha;
            modelView.DiaClearing = "2";
            modelView.Importe *= -1; // paso a negativo
            modelView.Conciliacion = false;
            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("TRANS");
            if (cajaGrupoModel != null)
            {
                modelView.IdGrupoCaja = cajaGrupoModel.Id; //"TRANS"
            }
            else { modelView.IdGrupoCaja = 0; }

            modelView.IdCliente = "BANCO";
            modelView.IdUsuario = usuario.IdUsuario;
            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));

            modelView.Importe *= -1; //paso a positivo    
            servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);

            //-- add 1209
            var CodigoDiario = servicioContable.GetNuevoCodigoAsiento() + 1;

            // inicio registro de asientos
            DiarioModel asiento = new DiarioModel();
            asiento.Codigo = CodigoDiario;
            asiento.Fecha = Convert.ToDateTime(modelView.Fecha);
            asiento.Periodo = Convert.ToDateTime(modelView.Fecha).ToString("yyMM");
            asiento.Tipo = "DE"; //Compras Facturas
            asiento.Cotiza = modelView.Cotizacion;
            asiento.Asiento = CodigoDiario;
            asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
            asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(modelView.IdTipoMoneda).Descripcion;
            asiento.DescripcionMa = modelBancoCuenta.BancoDescripcion;
            asiento.Descripcion = "Ingreso Cuenta Bancaria";
            asiento.Importe = modelView.Importe;  //(modelView.IdTipoMoneda == 1) ? (modelView.Importe) : (modelView.Importe * modelView.Cotizacion);
            asiento.Titulo = "Ingreso Cuenta Bancaria";

            string alias = "";
            if (modelView.IdTipoMoneda == 1)
            {
                alias = "PESOS";
            }
            else
            {
                alias = "DOLAR";
            }


            var asientoDiario = servicioContable.InsertAsientoContable(alias, asiento, 0);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


            asiento.Importe *= -1; // importe negativo
            asientoDiario = servicioContable.InsertAsientoContable("", asiento, modelBancoCuenta.IdImputacion);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


        }

        private void RegistroIngresoPorDespositoEfectivo(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            modelView.IdBancoCuenta = modelBancoCuenta.Id;
            modelView.Fecha = Convert.ToDateTime(modelView.Fecha);
            modelView.FechaIngreso = Convert.ToDateTime(modelView.Fecha);
            modelView.FechaEfectiva = fecha;
            modelView.DiaClearing = "2";
            modelView.Conciliacion = false;

            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("TRANS");
            if (cajaGrupoModel != null)
            {
                modelView.IdGrupoCaja = cajaGrupoModel.Id; //"TRANS"
            }
            else
            {
                modelView.IdGrupoCaja = 0;
            }
            modelView.IdCliente = "BANCO";
            modelView.IdUsuario = usuario.IdUsuario;
            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));

            modelView.Importe *= -1; /// valor en negativo 

            servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);
            //-- add 1209
            var CodigoDiario = servicioContable.GetNuevoCodigoAsiento() + 1;

            // inicio registro de asientos
            DiarioModel asiento = new DiarioModel();
            asiento.Codigo = CodigoDiario;

            //asiento.Fecha = Convert.ToDateTime(DateTime.Now); ;
            //asiento.Periodo = DateTime.Now.ToString("yyMM");
            asiento.Fecha = Convert.ToDateTime(modelView.Fecha);
            asiento.Periodo = Convert.ToDateTime(modelView.Fecha).ToString("yyMM");
            asiento.Tipo = "DE"; //Compras Facturas
            asiento.Cotiza = modelView.Cotizacion;
            asiento.Asiento = CodigoDiario;
            asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
            asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(modelView.IdTipoMoneda).Descripcion;
            asiento.Descripcion = "Ingreso Cuenta Bancaria";
            asiento.DescripcionMa = modelBancoCuenta.BancoDescripcion;
            // asiento.Importe = (modelView.IdTipoMoneda == 1) ? (modelView.Importe) : (modelView.Importe * modelView.Cotizacion);
            asiento.Titulo = "Ingreso Cuenta Bancaria";


            asiento.Importe *= -1;
            string alias = "";
            if (modelView.IdTipoMoneda == 1)
            {
                alias = "PESOS";
            }
            else
            {
                alias = "DOLAR";
            }


            var asientoDiario = servicioContable.InsertAsientoContable(alias, asiento, 0);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);

            modelView.Importe *= -1; /// valor en +  
            asientoDiario = servicioContable.InsertAsientoContable("", asiento, modelBancoCuenta.IdImputacion);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


        }

        private void RegistroIngresoPorCierre(IngresoBancoModelView modelView, BancoCuentaModelView modelBancoCuenta,Decimal SaldoCierre)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            DateTime FechaCierre = new DateTime(Convert.ToDateTime(modelView.Fecha).Year, Convert.ToDateTime(modelView.Fecha).Month, 1);
            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("INICI");
            
            BancoCuentaBancariaModel bancoCuentaBancariaModel = new BancoCuentaBancariaModel
            {
                
                IdBancoCuenta = modelView.IdBancoCuenta,
                Fecha = new DateTime(Convert.ToDateTime(modelView.Fecha).Year, Convert.ToDateTime(modelView.Fecha).Month, 1),
                FechaIngreso = Convert.ToDateTime(modelView.Fecha),
                FechaEfectiva = new DateTime(Convert.ToDateTime(modelView.Fecha).Year, Convert.ToDateTime(modelView.Fecha).Month, 1),
                DiaClearing = "0",
                Conciliacion = true,
                IdGrupoCaja = cajaGrupoModel.Id,
                IdCliente = "BANCO",
                IdUsuario = usuario.IdUsuario,
                CuentaDescripcion = "Saldo Inicial al "+ Convert.ToString(FechaCierre),   // .modelView.Fecha,
                NumeroCierre = 0,
                Importe= SaldoCierre,
                Activo = true,
                UltimaModificacion = fecha,
            };
            //servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));
            servicioBancoCuentaBancaria.IngresoCuentaBancaria(bancoCuentaBancariaModel);
        }


        private void RegistroIngresoPorCargosVarios(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            //fecha = fecha.AddDays(2);

            // CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorId(modelView.IdGrupoCaja);
            //modelView.NumeroOperacion  se asocia con nro de ID de la tabla ? no se ha definido
            modelView.IdBancoCuenta = modelBancoCuenta.Id;
            modelView.Fecha = modelView.Fecha;  // Convert.ToDateTime(DateTime.Now);
            modelView.FechaIngreso = fecha;
            modelView.FechaEfectiva = modelView.Fecha ?? Convert.ToDateTime(DateTime.Now);
            modelView.DiaClearing = "2";
            modelView.Importe *= -1; // importe negativo
            modelView.Conciliacion = false;
            modelView.IdUsuario = usuario.IdUsuario;


            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));


            servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);

            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("BANCH");
            if (cajaGrupoModel != null)
            {
                modelView.IdGrupoCaja = cajaGrupoModel.Id; //"BANCH"                       
                servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);
                servicioPresupuestoActual.UpdatePorIngreoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), cajaGrupoModel);
            }


            // Agregar asientos Contables
            //-- add 1209
            var CodigoDiario = servicioContable.GetNuevoCodigoAsiento() + 1;

            // inicio registro de asientos
            DiarioModel asiento = new DiarioModel();
            var fechaperiodo = 
            asiento.Codigo = CodigoDiario;
            asiento.Fecha = Convert.ToDateTime(modelView.Fecha); 
            asiento.Periodo = Convert.ToDateTime(modelView.Fecha).ToString("yyMM"); // DateTime.Now.ToString("yyMM");
            asiento.Tipo = "CV"; //Compras Facturas
            asiento.Cotiza = modelView.Cotizacion;
            asiento.Asiento = CodigoDiario;
            asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
            asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(modelView.IdTipoMoneda).Descripcion;
            asiento.Descripcion = "Ingreso Cuenta Bancaria";
            asiento.DescripcionMa =  modelBancoCuenta.BancoDescripcion ;
            // asiento.Importe = (modelView.IdTipoMoneda == 1) ? (modelView.Importe) : (modelView.Importe * modelView.Cotizacion);
            asiento.Titulo = "Ingreso Cuenta Bancaria";

            //imputacion por grupo caja
            var asientoDiario = servicioContable.InsertAsientoContable("", asiento, modelBancoCuenta.IdImputacion);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


            asiento.Importe *= -1;
            asientoDiario = servicioContable.InsertAsientoContable("", asiento, cajaGrupoModel.IdImputacion ?? 0);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);

        }




        public List<SelectListItem> CargarCajaGrupo()
        {
            List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Codigo
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Seleccionar Grupo", Value = "" });
            return retornoListaCajaGrupo;
        }
        public List<SelectListItem> CargarBancoCuenta()
        {
            List<BancoCuentaModel> ListBancoCuentaModels = servicioBanco.GetAllCuenta();

            List<SelectListItem> ListBancoCuenta = null;
            ListBancoCuenta = (ListBancoCuentaModels.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.BancoDescripcion
            })).ToList();
            ListBancoCuenta.Insert(0, new SelectListItem { Text = "Seleccionar", Value = "" });
            return ListBancoCuenta;
        }
    }






}



