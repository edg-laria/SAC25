using SAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio.Servicios;
using Negocio.Modelos;
using AutoMapper;
using Newtonsoft.Json;
using SAC.Models.Request;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using System.Text;
using SAC.Atributos;

namespace SAC.Controllers
{
    public class ComprasController : BaseController
    {
        private ServicioImputacion servicioImputacion = new ServicioImputacion();
        private ServicioContable servicioContable = new ServicioContable();
        private ServicioCompra servicioCompra = new ServicioCompra();
        private ServicioTipoComprobante servicioTipoComprobante = new ServicioTipoComprobante();
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioProveedor servicioProveedor = new ServicioProveedor();

        //si factura es C o B el unico importe que se registra es noto no gravado

        //CompraIva REgistra valores en pesos Cuando la factura es en dolares se realiza la conversion
        //y los valores en dolares se registran en la tabla CompraFactura


        public ComprasController()
        {
            servicioCompra._mensaje += (msg_, tipo_)  => AddMessage(tipo_, msg_);
            servicioProveedor._mensaje += (msg_, tipo_)  => AddMessage(tipo_, msg_);
            servicioImputacion._mensaje += (msg_, tipo_)  => AddMessage(tipo_, msg_);
            servicioContable._mensaje += (msg_, tipo_)  => AddMessage(tipo_, msg_);
        }

        // GET: Compras
        public ActionResult FacturaCompras()
        {
            CompraFacturaViewModel model = new CompraFacturaViewModel();

            model.Periodo = int.Parse(DateTime.Now.ToString("yyMM"));
            getTipo(model);
            return View(model);
        }
        [AutorizacionDeSistema]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacturaCompras(CompraFacturaViewModel model)
        {
            //bool hasErrors = ViewData.ModelState.Values.Any(x => x.Errors.Count > 1);
            //foreach (ModelState state in ViewData.ModelState.Values.Where(x => x.Errors.Count > 0))
            //{
            //    servicioCompra._mensaje("noValido", "ok");
            //}

            try
            {
                // validar numero de cactura para el proveedor no debe existir            
                if (servicioCompra.ValidarFacturaPorNroFacturaIdProveedor(model.NumeroFactura, model.IdProveedor))
                {
                    throw new Exception();
                }
            
                model.CodigoDiario = servicioContable.GetNuevoCodigoAsiento() + 1;

                CompraFacturaModel facturaRegistrada = servicioCompra.CreateFactura(Mapper.Map<CompraFacturaViewModel, CompraFacturaModel>(model));
                // update proveedor
                ProveedorModel proveedor = servicioProveedor.GetProveedor(facturaRegistrada.IdProveedor);
                if (proveedor.UltimoPuntoVenta != facturaRegistrada.PuntoVenta)
                {
                    proveedor.UltimoPuntoVenta = facturaRegistrada.PuntoVenta;
                    proveedor.UltimaModificacion = DateTime.Now;
                    servicioProveedor.ActualizarProveedor(proveedor);
                }

                // inicio registro de asientos
                DiarioModel asiento = new DiarioModel();
                asiento.Codigo = facturaRegistrada.CodigoDiario;
                asiento.Fecha = facturaRegistrada.Fecha;
                asiento.Periodo = facturaRegistrada.Periodo.ToString();
                asiento.Tipo = "CF"; //Compras Facturas
                asiento.Cotiza = facturaRegistrada.Cotizacion;
                asiento.Asiento = facturaRegistrada.CodigoDiario;
                asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
                asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(facturaRegistrada.IdMoneda).Descripcion;
                asiento.DescripcionMa =  proveedor.Nombre + ' ' + model.NumeroFactura;

                /// asiento Inputacion Proveedor en valor 
                var asientoDiario = servicioContable.InsertAsientoContable(null
                                                          , (facturaRegistrada.CompraIva.Total * (-1)) 
                                                          , asiento
                                                          , facturaRegistrada
                                                          , proveedor.IdImputacionProveedor ?? -1);
                /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
                servicioImputacion.AsintoContableGeneral(asientoDiario);
                

                decimal ImporteImputacionFactura = (facturaRegistrada.CompraIva.Total);
                /// asiento Inputacion Total IVA               
                if (facturaRegistrada.CompraIva.TotalIva > 0)
                {                    
                    ImporteImputacionFactura -= facturaRegistrada.CompraIva.TotalIva;

                    var asientoTotalIva = servicioContable.InsertAsientoContable("IVA", facturaRegistrada.CompraIva.TotalIva, asiento, facturaRegistrada, 0);
                    if(asientoTotalIva != null) { servicioImputacion.AsintoContableGeneral(asientoTotalIva); }

                }

                /// asiento Inputacion IVA provincia Factura Proveedor
                if (facturaRegistrada.CompraIva.PercepcionImporteIva > 0)
                {                   
                   ImporteImputacionFactura -= facturaRegistrada.CompraIva.PercepcionImporteIva;
                    var asientoIva = servicioContable.InsertAsientoContable("IVA", facturaRegistrada.CompraIva.PercepcionImporteIva, asiento, facturaRegistrada, 0);
                    if (asientoIva != null) { servicioImputacion.AsintoContableGeneral(asientoIva); }
                }

                /// asiento Inputacion IB capital Factura Proveedor         
                if (facturaRegistrada.CompraIva.PercepcionImporteIB > 0)
                {
                    ImporteImputacionFactura -= facturaRegistrada.CompraIva.PercepcionImporteIB;
                    var asinetoIB = servicioContable.InsertAsientoContable("ISIBC", facturaRegistrada.CompraIva.PercepcionImporteIB, asiento, facturaRegistrada, 0);
                    if (asinetoIB != null) { servicioImputacion.AsintoContableGeneral(asinetoIB); }
                }

                /// asiento Inputacion ganancias capital Factura Proveedor
                if (facturaRegistrada.CompraIva.OtrosImpuestos > 0)
                {
                    ImporteImputacionFactura -= facturaRegistrada.CompraIva.OtrosImpuestos;
                    var asientoGanan = servicioContable.InsertAsientoContable("GANAN", facturaRegistrada.CompraIva.OtrosImpuestos, asiento, facturaRegistrada, 0);
                    if (asientoGanan != null) { servicioImputacion.AsintoContableGeneral(asientoGanan); }
                }

                /// asiento Inputacion Factura Proveedor - negativo
                servicioImputacion.AsintoContableGeneral(servicioContable.InsertAsientoContable(null
                                                                                                  , ImporteImputacionFactura 
                                                                                                  , asiento
                                                                                                  , facturaRegistrada
                                                                                                  , facturaRegistrada.IdImputacion));
                servicioCompra._mensaje("Operacion realizada correctamente... ", "ok");
                return RedirectToAction("FacturaCompras");

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                getTipo(model);
                return View("FacturaCompras", model);
            }
        }


        private void getTipo(CompraFacturaViewModel model)
        {
            try
            {
                model.TipoMonedas = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());
                model.ListTipoComprobante = Mapper.Map<List<TipoComprobanteModel>, List<TipoComprobanteModelView>>(servicioTipoComprobante.GetAllTipoComprobante());
            }
            catch (Exception)
            {

            }
        }

        [HttpGet()]
        public ActionResult GetListProveedorJson(string term)
        {
            try
            {
                List<ProveedorModel> proveedor = servicioCompra.GetProveedorPorNombre(term);
                var arrayProveedor = (from prov in proveedor
                                      select new AutoCompletarViewModel()
                                      {
                                          id = prov.Id,
                                          label = prov.Nombre
                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                servicioCompra._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetProveedorJson(int idProveedor)
        {
            string strJson;
            try
            {

                ProveedorModelView proveedor = Mapper.Map<ProveedorModel, ProveedorModelView>(servicioProveedor.GetProveedorCompleto(idProveedor));
                proveedor.ListTipoComprobante = Mapper.Map<List<TipoComprobanteModel>, List<TipoComprobanteModelView>>(servicioTipoComprobante.GetTipoComprobantePorTipoIvaProveedor(proveedor.IdTipoIva));
               
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(proveedor);
                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                servicioCompra._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        public ActionResult GetCotizacionMoneda(int IdMoneda)
        {
            CotizacionAFIP cotizacion = new CotizacionAFIP();
            var f = DateTime.Today;
            string strJson;
            try
            {
                 var moneda = servicioTipoMoneda.GetCotizacionPorIdMoneda(IdMoneda);
                if (cotizacion == null)
                {
                    cotizacion.Importe = 1;
                    cotizacion.Fecha = f.ToString("dd/MM/yyyy");
                }
                else
                {
 cotizacion.Importe = moneda.Monto;
                cotizacion.IdMoneda = moneda.Id.ToString();
                cotizacion.Fecha = moneda.Fecha.ToString();
                }

               
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(cotizacion);
                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                servicioCompra._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
            }

            return Json(null, JsonRequestBehavior.AllowGet);

        }

        [HttpGet()]
        public ActionResult GetCompraFactura(int idCompraFactura)
        {
            string strJson;
            try
            {
                CompraFacturaModel facturaIva = servicioCompra.GetCompraFacturaIVAPorNro(idCompraFactura);
                if (facturaIva != null)
                {
                    if (facturaIva.CompraIva != null)
                    {
                        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(facturaIva.CompraIva);
                        return Json(new { data = strJson, result = true }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { result = false, msj = "No hay Datos de la factura" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = false, msj = "La factura NO existe" }, JsonRequestBehavior.AllowGet);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return Json(new { result = false, msj = "Ops!, A ocurriodo un error. Contacte al Administrador" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet()]
        public ActionResult GetListCompraFacturaJson(string term)
        {
            try
            {
                List<CompraFacturaModel> factura = servicioCompra.GetAllCompraFacturaPorNro(int.Parse(term));

                var arrayProveedor = (from f in factura
                                      select new AutoCompletarViewModel()
                                      {
                                          id = f.Id,
                                          label = f.NumeroFactura.ToString()
                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
              
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                servicioCompra._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        /*esto se usa para registro compras*/
        public ActionResult RegistroCompras(string mesFecha, string anioFecha)
        {
            CompraRegistroModelView model = new CompraRegistroModelView();

          
            if (string.IsNullOrEmpty(mesFecha) )
            {
                mesFecha = DateTime.Now.ToString("MM");
                             
            }
            if ( string.IsNullOrEmpty(anioFecha))
            {
                
                anioFecha = DateTime.Now.ToString("yyyy");
            }

            model.DetalleFacturas = Mapper.Map<List<CompraRegistroDetalleModel>, List<CompraRegistroDetalleModelView>>(servicioCompra.RegistroComprasDetalle(int.Parse(mesFecha), int.Parse(anioFecha)));
                Session["Datos"] = model.DetalleFacturas;

            

            return View(model);

        }


        public ActionResult GenerarTxtComprasComprobante()
        {

            List<CompraRegistroDetalleModelView> listResultado = new List<CompraRegistroDetalleModelView>();
             List<string> Archivo = new List<string>();
            listResultado = Session["Datos"] as List<CompraRegistroDetalleModelView>;

              StringBuilder sb = new StringBuilder();
            if (listResultado != null)
            {
                foreach (CompraRegistroDetalleModelView i in listResultado)
                {
                    string anio = "";
                    string mes = "";
                    string dia = "";
                    string comprobante = "";
                    string ptoVenta = "";
                    string nroComprobante = "";
                    string despachoImportacion = "                ";//16 espacios ??
                    string codigoVendedor = "80";
                    string idVendedor = "";
                    string nombreVendedor = "";
                    string importeNeto = "";
                    // string alicuotaIva = "";
                    string impuestoLiquidado = "";
                    string total = "";

                    //string netoNoGravado = "               ";//15 espacios 
                    //string operacionesExentas = "               ";//15 espacios 
                    //string impuestosNacionales = "               ";//15 espacios 
                    //string ingresosBrutos = "";
                    //string impuestosMunicipales = "               ";//15 espacios 
                    //string impuestosInternos = "               ";//15 espacios 
                    //string nombreMoneda = "";
                    //string tipoCambio = "0001000000";
                    //string cantidadAlicuota = "1";
                    //string codigoOperacion = " ";
                    //string creditoFiscalComputable = "               ";//no se que es
                    //string otrosTributos = "               ";//no se que es
                    //string cuitEmisor = "           ";
                    //string denominacionEmisor = "                              ";
                    //string ivaComision = "               ";

                    string netoNoGravado = "000000000000000";//15 espacios 
                    string operacionesExentas = "000000000000000";//15 espacios 
                    string impuestosNacionales = "000000000000000";//15 espacios 
                    string ingresosBrutos = "";
                    string impuestosMunicipales = "000000000000000";//15 espacios 
                    string impuestosInternos = "000000000000000";//15 espacios 
                    string nombreMoneda = "";
                    string tipoCambio = "0001000000";
                    string cantidadAlicuota = "1";
                    string codigoOperacion = " ";
                    string creditoFiscalComputable = "000000000000000";//no se que es
                    string otrosTributos = "000000000000000";//no se que es
                    string cuitEmisor = "00000000000";
                    string denominacionEmisor = "                              ";
                    string ivaComision = "000000000000000";


                    int importeNetoInt = 0;
                    // int alicuotaIvaInt = 0;
                    int impuestoLiquidadoInt = 0;
                    int totalInt = 0;
                    int ingresosBrutosInt = 0;

                    anio = i.Fecha.Year.ToString();

                    if (i.Fecha.Month < 10)
                    {
                        mes = "0" + i.Fecha.Month.ToString();
                    }
                    else
                    {
                        mes = i.Fecha.Month.ToString();
                    }

                    if (i.Fecha.Day < 10)
                    {
                        dia = "0" + i.Fecha.Day.ToString();
                    }
                    else
                    {
                        dia = i.Fecha.Day.ToString();
                    }

                    comprobante = i.IdComprobante.ToString("D3");
                    ptoVenta = i.PuntoVenta.ToString("D5");
                    nroComprobante = i.NumeroFactura.ToString("D20");
                    despachoImportacion = despachoImportacion.ToString();
                    netoNoGravado = netoNoGravado.ToString();
                    operacionesExentas = operacionesExentas.ToString();
                    impuestosNacionales = impuestosNacionales.ToString();

                    if (i.ISIB != null)
                    {
                        ingresosBrutos = i.ISIB.ToString().Replace(".", "");
                        ingresosBrutosInt = int.Parse(ingresosBrutos);
                        ingresosBrutos = ingresosBrutosInt.ToString("D15");
                    }
                    else
                    {
                        ingresosBrutos = "               ";
                    }


                    //PadLeft se usa con los string
                    idVendedor = i.Cuit.PadLeft(20, '0');
                    nombreVendedor = i.Empresa.PadRight(30, ' ');
                    denominacionEmisor = denominacionEmisor.ToString();

                    impuestosMunicipales = impuestosMunicipales.ToString();

                    impuestosInternos = impuestosInternos.ToString();
                    //esto cambiar
                    nombreMoneda = "PES";
                    codigoOperacion = codigoOperacion.ToString();

                    if (i.Total != null)
                    {
                        total = i.Total.ToString().Replace(".", "");
                        totalInt = int.Parse(total);
                        total = totalInt.ToString("D15");
                    }
                    else
                    {
                        total = "               ";
                    }


                    importeNeto = i.Neto.ToString().Replace(".", "");
                    importeNetoInt = int.Parse(importeNeto);
                    importeNeto = importeNetoInt.ToString("D15");

                    //alicuotaIva = i.PercepcionIva.ToString().Replace(".", "");
                    //alicuotaIvaInt = int.Parse(alicuotaIva);
                    //alicuotaIva = alicuotaIvaInt.ToString("D4");

                    impuestoLiquidado = i.PercepcionImporteIva.ToString().Replace(".", "");
                    impuestoLiquidadoInt = int.Parse(impuestoLiquidado);
                    impuestoLiquidado = impuestoLiquidadoInt.ToString("D15");

                    var Datos = anio + mes + dia + comprobante + ptoVenta + nroComprobante + despachoImportacion + codigoVendedor + idVendedor + nombreVendedor + total + netoNoGravado + operacionesExentas + impuestoLiquidado + impuestosNacionales + ingresosBrutos + impuestosMunicipales + impuestosInternos + nombreMoneda + tipoCambio + cantidadAlicuota + codigoOperacion + creditoFiscalComputable + otrosTributos + cuitEmisor + denominacionEmisor + ivaComision;

                    //Archivo.Add(anio + mes + dia + comprobante + ptoVenta + nroComprobante + despachoImportacion + codigoVendedor + idVendedor + total + netoNoGravado + operacionesExentas + impuestoLiquidado + impuestosNacionales + ingresosBrutos + impuestosMunicipales + impuestosInternos + nombreMoneda + tipoCambio + cantidadAlicuota + codigoOperacion + creditoFiscalComputable + otrosTributos + cuitEmisor + denominacionEmisor + ivaComision);
                    sb.AppendLine(Datos);
                }

            }
            byte[] byteBuffer = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
            sb.Clear();
            // returno el archivo creado
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.BinaryWrite(byteBuffer);
            Response.AddHeader("content-disposition", "attachment;filename=LIBRO_IVA_DIGITAL_COMPRAS_CBTE.txt");
            Response.End();

            return RedirectToAction("Index");
        }

        public ActionResult GenerarTxtComprasAlicuotas()
        {

            List<CompraRegistroDetalleModelView> listResultado = new List<CompraRegistroDetalleModelView>();
           // List<string> Archivo = new List<string>();
            listResultado = Session["Datos"] as List<CompraRegistroDetalleModelView>;

            StringBuilder sb = new StringBuilder();
            if (listResultado !=null)
            {          
                foreach (CompraRegistroDetalleModelView i in listResultado)
                {
                    string comprobante = "";
                    string ptoVenta = "";
                    string nroComprobante = "";
                    string codigoVendedor = "80";
                    string idVendedor = "";
                    string importeNeto = "";
                    string alicuotaIva = "";
                    string impuestoLiquidado = "";
                    int importeNetoInt = 0;
                    int alicuotaIvaInt = 0;
                    int impuestoLiquidadoInt = 0;

                    comprobante = i.IdComprobante.ToString("D3");
                    ptoVenta = i.PuntoVenta.ToString("D5");
                    nroComprobante = i.NumeroFactura.ToString("D20");

                    idVendedor = i.Cuit.PadLeft(20, '0');
                    importeNeto = i.Neto.ToString().Replace(".", "");
                    importeNetoInt = int.Parse(importeNeto);
                    importeNeto = importeNetoInt.ToString("D15");

                    alicuotaIva = i.PercepcionIva.ToString().Replace(".", "");
                    alicuotaIvaInt = int.Parse(alicuotaIva);
                    alicuotaIva = alicuotaIvaInt.ToString("D4");

                    impuestoLiquidado = i.PercepcionImporteIva.ToString().Replace(".", "");
                    impuestoLiquidadoInt = int.Parse(impuestoLiquidado);
                    impuestoLiquidado = impuestoLiquidadoInt.ToString("D15");

                    var Datos = comprobante + ptoVenta + nroComprobante + codigoVendedor + idVendedor + importeNeto + alicuotaIva + impuestoLiquidado;

                    //Archivo.Add(comprobante + ptoVenta + nroComprobante + codigoVendedor + idVendedor + importeNeto + alicuotaIva + impuestoLiquidado);
                    sb.AppendLine(Datos);
                }
               }
            byte[] byteBuffer = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
            sb.Clear();
            // returno el archivo creado
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.BinaryWrite(byteBuffer);
            Response.AddHeader("content-disposition", "attachment;filename=LIBRO_IVA_DIGITAL_COMPRAS_ALICUOTAS.txt");
            Response.End();

            return RedirectToAction("Index");
        }

    }
}