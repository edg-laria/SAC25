using SAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Negocio.Servicios;
using Negocio.Modelos;
using AutoMapper;
using Newtonsoft.Json;
using SAC.Models.Request;

//tema afip
using SAC.Models.Afip;
using SAC.afip.wswhomo; //ws facturas locales
using SAC.afip.wswhomo_Exportacion; //ws facturas externas

using SAC.Helpers;
using SAC.QR;

using System.Drawing;
using Negocio.Helpers;
using System.Globalization;
using System.Text;

namespace SAC.Controllers
{
    public class FacturaController : BaseController
    {
        
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioCliente servicioCliente = new ServicioCliente();
        private ServicioClienteDireccion servicioClienteDireccion = new ServicioClienteDireccion();
        private ServicioDepartamento servicioDepartamento = new ServicioDepartamento();
        private ServicioTipoComprobanteVenta servicioTipoComprobanteVenta = new ServicioTipoComprobanteVenta();
        private ServicioTipoComprobante servicioTipoComprobante = new ServicioTipoComprobante();
        private ServicioArticulo servicioArticulo = new ServicioArticulo();
        private ServicioPieNota servicioPieNota = new ServicioPieNota();
        private ServicioBancoCuenta servicioBancoCuenta = new ServicioBancoCuenta();
        private ServicioItemImpr servicioItemImpr = new ServicioItemImpr();
        private ServicioDto servicioDto = new ServicioDto();
        private ServicioFacturaVenta servicioFacturaVenta = new ServicioFacturaVenta();
        private ServicioIvaVenta servicioIvaVenta = new ServicioIvaVenta();
        private ServicioBuque servicioBuque = new ServicioBuque();
        private ServicioCotiza servicioCotiza = new ServicioCotiza();
        private ServicioContable servicioContable = new ServicioContable();
        private ServicioImputacion servicioImputacion = new ServicioImputacion();
        private ServicioAfip_TicketAcceso servicioAfip_TicketAcceso = new ServicioAfip_TicketAcceso();
        private ServicioFacturaVentaItems servicioFacturaVentaItems = new ServicioFacturaVentaItems();
        private ServicioFacturaElectronica servicioFacturaElectronica = new ServicioFacturaElectronica();
        private AfipHelper afipHelper = new AfipHelper();


        public FacturaController()
        {
            servicioFacturaVenta._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioFacturaVentaItems._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioCliente._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioArticulo._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioIvaVenta._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioContable._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioImputacion._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioFacturaElectronica._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioAfip_TicketAcceso._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
            servicioPieNota._mensaje += (msg_, tipo_) => AddMessage(tipo_, msg_);
        }

        // GET: Factura
        public ActionResult Index()
        {
            /*
            var cotizacionAfip = afipHelper.GetCotizacion("DOL");
            if (cotizacionAfip.ResultGet == null)
            {
                return View("ApifFueraServicio");
            }
            else
            {
*/
            var cotizacionMoneda = servicioTipoMoneda.ObtenerCotizacion(DateTime.Now);
            decimal MonCotiz;

            if (cotizacionMoneda.Count == 0)
               {
                 var cotizacionAfip = afipHelper.GetCotizacion("DOL");
                    if (cotizacionAfip.ResultGet == null)
                    {
                        return View("ApifFueraServicio");
                    }
                    else
                    {

                      ValorCotizacionModel CotizacionModel = new ValorCotizacionModel();
                      CotizacionModel.IdTipoMoneda = 2;
                      CotizacionModel.Fecha = DateTime.Now;
                      CotizacionModel.Monto = (decimal)cotizacionAfip.ResultGet.MonCotiz;
                      CotizacionModel.UltimaModificacion = DateTime.Now;
                      CotizacionModel.Activo = true;

                      servicioTipoMoneda.updateCotizacionPorIdMoneda(CotizacionModel);
                    MonCotiz = (decimal)cotizacionAfip.ResultGet.MonCotiz;
                }
            }else
            {
                MonCotiz = 1;
            }  
            
            FacturaModelView model = new FacturaModelView();

            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());
            List<SelectListItem> lstTipoMoneda = null;
            lstTipoMoneda = (ListaTipoMoneda.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();

            List<SelectListItem> listFormaPago = new List<SelectListItem>();
            listFormaPago.Add(new SelectListItem() { Text = "Cuenta Corriente", Value = "96" });
            listFormaPago.Add(new SelectListItem() { Text = "Contado", Value = "1" });
            listFormaPago.Add(new SelectListItem() { Text = "Tarjeta de Crédito", Value = "68" });
            listFormaPago.Add(new SelectListItem() { Text = "Tarjeta de Débito",  Value = "69" });
            listFormaPago.Add(new SelectListItem() { Text = "Cheque",  Value = "97" });
            listFormaPago.Add(new SelectListItem() { Text = "Ticket",  Value = "98" });
            listFormaPago.Add(new SelectListItem() { Text = "Otra",    Value = "99" });
            listFormaPago.Add(new SelectListItem() { Text = "30 días", Value = "93" });
            listFormaPago.Add(new SelectListItem() { Text = "60 días", Value = "94" });
            listFormaPago.Add(new SelectListItem() { Text = "90 días", Value = "95" });

            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Exterior", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Local", Value = "1" });
            model.SelectPuntoVenta = lst;

            List<SelectListItem> lstIdioma = new List<SelectListItem>();
            lstIdioma.Add(new SelectListItem() { Text = "Español", Value = "1" });
            lstIdioma.Add(new SelectListItem() { Text = "Ingles", Value = "2" });
            model.TipoIdioma = lstIdioma;
            model.Cotizacion = MonCotiz;  //(decimal)cotizacionAfip.ResultGet.MonCotiz;

            List<TipoComprobanteVentaModelView> ListaComprobantes = Mapper.Map<List<TipoComprobanteVentaModel>, List<TipoComprobanteVentaModelView>>(servicioTipoComprobanteVenta.GetAllTipoComprobante());
            List<SelectListItem> lstTipoComprobante = (ListaComprobantes.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Denominacion
                                  })).ToList();

            List<DepartamentoModelView> ListaDepartamentos = Mapper.Map<List<DepartamentoModel>, List<DepartamentoModelView>>(servicioDepartamento.GetAllDepartamento());
            List<SelectListItem> lstDepartamentos = (ListaDepartamentos.Select(x =>
                                 new SelectListItem()
                                 {
                                     Value = x.Id.ToString(),
                                     Text = x.Descripcion
                                 })).ToList();
            lstDepartamentos.Insert(0, new SelectListItem { Value = "0", Text = "Sin Especificar" });

            List<BancoCuentaModelView> ListaCuentasBancarias = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioBancoCuenta.GetAllCuenta());
            List<SelectListItem> lstCuentasBancarias = null;
            lstCuentasBancarias = (ListaCuentasBancarias.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.BancoDescripcion
                                  })).ToList();
            lstCuentasBancarias.Insert(0, new SelectListItem { Value = "0", Text = "Sin Especificar" });
            model.TipoComprobante = lstTipoComprobante;
            model.Departamentos = lstDepartamentos;
            model.TipoMonedas = lstTipoMoneda;
            model.CuentaBancaria = lstCuentasBancarias;
            model.FormaPago = listFormaPago;
            model.ClienteDirecciones = null;
            model.Fecha = DateTime.Now;
/*
            ValorCotizacionModel valorCotizacion = servicioTipoMoneda.GetCotizacionPorIdMoneda(DateTime.Now, 2);
            if (valorCotizacion != null)
            {
                model.Cotizacion = valorCotizacion.Monto;
            }
            else
            {
                model.Cotizacion = 1;
            }
 */          

            return View(model);
        }


        [HttpGet()]
        public ActionResult GetProximoNroCBTJson(string IdPuntoVenta, string tipoComprobante, bool miPyme)
             
        {
            
            try
            {
                int retorno = 0;

                //if (IdPuntoVenta == "1")  // factura Local
                switch (IdPuntoVenta)
                {
                    case "1":
                        //if (miPyme == true && TotalFactura > 500000) esto nova porque todavia no tgo el total
                        if (miPyme == true)
                        {
                            //facturacion electronica
                            switch (tipoComprobante)
                            {
                                case "1":
                                    retorno = 211;
                                    break;
                                case "2":
                                    retorno = 212;
                                    break;
                                case "3":
                                    retorno = 213;
                                    break;
                            }
                        }

                        if (miPyme == false)
                        {//documentacion C
                            switch (tipoComprobante)
                            {
                                case "1":
                                    retorno = 11;
                                    break;
                                case "2":
                                    retorno = 12;
                                    break;
                                case "3":
                                    retorno = 13;
                                    break;
                                case "4":
                                    retorno = 60;
                                    break;
                            }
                        }
                        break;
                    case "2":
                            //documentacion exterior
                            switch (tipoComprobante)
                            {
                                case "1":
                                    retorno = 19;
                                    break;
                                case "2":
                                    retorno = 20;
                                    break;
                                case "3":
                                    retorno = 21;
                                    break;
                                case "4":
                                    retorno = 61;
                                    break;
                        }
                        break;
                    
                }
                // var cbt = servicioTipoComprobanteVenta.getTipoComprobanteVentaNewNumeroFactura(retorno, AfipHelper.getPuntoVentaAfip(int.Parse(IdPuntoVenta)));
                var cbt = servicioTipoComprobanteVenta.ObtenerNroFactura(retorno, AfipHelper.getPuntoVentaAfip(int.Parse(IdPuntoVenta)));

                //return Json(new { result = true, data = cbt.Numero }, JsonRequestBehavior.AllowGet);
                return Json(new { result = true, data = cbt }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador" + ex.Message);
                return Json(new { result = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { result = false , data = 0 }, JsonRequestBehavior.AllowGet);
            
        }


        [HttpPost]
        public ActionResult GrabarFactura(FacturaModelView model)
        {
            UsuarioModel OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            try
            {
                ClienteModel cliente = servicioCliente.GetClientePorId(model.IdCliente);
                //se verifica que tipo de comprobante se selecciono
                // string tipoComprobante = "";
                string tipoComprobanteAbreviado = "";
                string idComprobante = "";
                switch (model.idTipoComprobanteSeleccionado)
                {
                    case 1:
                        //tipoComprobante = "Factura";
                        tipoComprobanteAbreviado = "F";

                        break;
                    case 2:
                        // tipoComprobante = "Debito";
                        tipoComprobanteAbreviado = "D";
                        break;
                    case 3:
                        // tipoComprobante = "Credito";
                        tipoComprobanteAbreviado = "C";
                        break;
                    case 4:
                        // tipoComprobante = "ProForma";
                        tipoComprobanteAbreviado = "X";
                        break;
                }


                model.AplicaTipoCBT = DeterminarNroComprobante(model.IdPuntoVenta.ToString(), model.mipyme, model.TotalFactura, model.AplicaTipoCBT).ToString();
                decimal TotalFacturaPesos = Math.Round(model.TotalFactura, 2);  //model.TotalFactura; 
                if (model.idTipoMoneda== 2)
                        {
                            TotalFacturaPesos = Math.Round(model.TotalFactura * model.Cotizacion,2);
                        }
                //model.AplicaTipoCBT = DeterminarNroComprovante(model.IdPuntoVenta.ToString(), model.mipyme, TotalFacturaPesos, model.idTipoComprobanteSeleccionado.ToString()).ToString();
                int comprobanteActualizado = DeterminarNroComprobante(model.IdPuntoVenta.ToString(), model.mipyme, TotalFacturaPesos, model.idTipoComprobanteSeleccionado.ToString());            

                var _tipoComprobanteVenta = new TipoComprobanteVentaModel();

                if (model.NumeroFactura == 0)
                {
                    _tipoComprobanteVenta = servicioTipoComprobanteVenta.getTipoComprobanteVentaNewNumeroFactura(comprobanteActualizado, AfipHelper.getPuntoVentaAfip(model.IdPuntoVenta));
                }
                else
                {
                    _tipoComprobanteVenta = servicioTipoComprobanteVenta.getTipoComprobanteVenta(comprobanteActualizado, AfipHelper.getPuntoVentaAfip(model.IdPuntoVenta));
                }
                
              
                int nFactor = (tipoComprobanteAbreviado == "C") ? -1 : 1;
                decimal totalGastosPesos = 0;
     
               //preparo e inserto la factura electronica en base datos + ws
                FECAEResponse RetornoAfip = new FECAEResponse();
                long cuitOriginador = long.Parse(System.Configuration.ConfigurationManager.AppSettings["cuitUserAfip"].ToString());
               
                #region saveCBT

                string cae = "71359963662296";
                string caeFechaVenc = "20230701";
                long idWsAfip = 0;
                string mensajeErrorAfip = null;
                string ResultadoAfip = "";

                // si factura es manual, no ir a afip(no inserta la factura electronica)
/*
                FacturaElectronicaModel FacturaElectronica = new FacturaElectronicaModel();
                FacturaElectronica.ALICUOTA = "0";
                FacturaElectronica.CATEGORIA = "Exento";
                FacturaElectronica.CODBARRA = "";
                FacturaElectronica.NRODOC = model.Cuit;
                FacturaElectronica.TIPOSERV = 2;
                FacturaElectronica.CODCLI = model.CodigoCliente;
                FacturaElectronica.CODPAIS = model.CodPaisAfip.ToString();
                FacturaElectronica.COTIZA = model.Cotizacion;
                FacturaElectronica.NOMBRE = model.NombreComp;
                FacturaElectronica.DOMICILIO = model.DireccionCompuesta;
                FacturaElectronica.ATENCION = model.Atencion;
                FacturaElectronica.PAIS = model.PaisComp;
                FacturaElectronica.ESTADO = "2";
                FacturaElectronica.FDESDE = model.Fecha;
                FacturaElectronica.FECHACBTE = model.Fecha;
                FacturaElectronica.FECHAVEN = model.Fecha.AddDays(180);
                FacturaElectronica.FHASTA = null;
                FacturaElectronica.FORMAPAGO = model.IdTipoPago.ToString();
                FacturaElectronica.TOTAL = model.TotalFactura;
                FacturaElectronica.NETO = model.TotalFactura;
                FacturaElectronica.NROCBTE_AFIP = model.NumeroFactura;
                FacturaElectronica.PUNTOVTA = _tipoComprobanteVenta.PuntoVenta;
                FacturaElectronica.ID_TIPOCBTE = _tipoComprobanteVenta.Id;          
                FacturaElectronica.IVA = 0;
                FacturaElectronica.IDIVA = "3";
                FacturaElectronica.TIPODOC = 80;
                FacturaElectronica.IVA10 = 0;
                FacturaElectronica.NETO10 = 0;
                string moneda = (model.idTipoMoneda == 2) ? "DOL" : "PES" ;
                FacturaElectronica.IDMONEDA = moneda;
              
                FacturaElectronicaModel FacturaElectronicaInsertada = servicioFacturaElectronica.Agregar(FacturaElectronica);
*/

                if (model.FacturaManual == false && model.idTipoComprobanteSeleccionado != 4)
                {
                    //if (model.CodPaisAfip != 200) // extranjero/exterior

                   

                    if (model.IdPuntoVenta == 2) // extranjero/exterior
                    {
                        Afip_TicketAccesoModel login;
                        AfipHelper afipHelper = new AfipHelper();
                        login = afipHelper.VerificarTicketAcceso("wsfex");
                        ClaseLoginAfip ClaseLogin = null;
                        FEXResponseAuthorize responseAfip;
                        if (login == null)
                        {
                            ClaseLogin = afipHelper.ObtenerTicketAccesoWS("wsfex", OUsuario.IdUsuario);
                        }
                        else
                        {
                            ClaseLogin = afipHelper.ObtenerTicketAccesoSinWS("wsfex", OUsuario.IdUsuario);
                            ClaseLogin.Token = login.token;
                            ClaseLogin.Sign = login.sing;
                        }

                        responseAfip = InsertarCBTAfipExterior(ClaseLogin, model, _tipoComprobanteVenta, cuitOriginador);
                        if (responseAfip != null)
                        {
                            if (responseAfip.FEXErr.ErrMsg == "OK")
                            {
                                ResultadoAfip = "A";
                                idWsAfip = responseAfip.FEXResultAuth.Id;
                                cae = responseAfip.FEXResultAuth.Cae;
                                caeFechaVenc = responseAfip.FEXResultAuth.Fch_venc_Cae;
                                model.NumeroFactura = int.Parse(responseAfip.FEXResultAuth.Cbte_nro.ToString());
                                AddMessage("success", "Se Registro en AFIP, Nº de Cbt: " + model.NumeroFactura);
                            }
                            else
                            {
                                ResultadoAfip = responseAfip.FEXErr.ErrCode.ToString();
                                mensajeErrorAfip = responseAfip.FEXErr.ErrMsg;
                                NLogHelper.Instance.ErrorLog("FacturaController", "GrabarFactura", "Ops!, Notificacion de Afip: " + mensajeErrorAfip + "(" + ResultadoAfip + ")");
                                AddMessage("error", "Ops!, Notificacion de Afip: " + mensajeErrorAfip + "(" + ResultadoAfip + ")");
                               // return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            AddMessage("error", "Ops!, El servicio de AFIP no responde...");
                            NLogHelper.Instance.ErrorLog("FacturaController", "GrabarFactura", "Ops!, El servicio de AFIP no responde...");

                            return RedirectToAction("Index");
                        }

                    }
                    else
                    {
                        Afip_TicketAccesoModel login;
                        login = afipHelper.VerificarTicketAcceso("wsfe");
                        ClaseLoginAfip ClaseLogin = null;
                        if (login == null)
                        {
                            ClaseLogin = afipHelper.ObtenerTicketAccesoWS("wsfe", OUsuario.IdUsuario);
                        }
                        else
                        {
                            ClaseLogin = afipHelper.ObtenerTicketAccesoSinWS("wsfe", OUsuario.IdUsuario);
                            ClaseLogin.Token = login.token;
                            ClaseLogin.Sign = login.sing;
                        }
                        RetornoAfip = InsertarCBTAfipLocal(ClaseLogin, model, _tipoComprobanteVenta , cuitOriginador);

                        if (RetornoAfip != null)
                        {
                            if (RetornoAfip.Errors != null)
                            {
                                //mostrar mensaje error;
                                foreach (var er in RetornoAfip.Errors)
                                {
                                    mensajeErrorAfip += string.Format("Er: {0}: {1}", er.Code, er.Msg);
                                }
                                cae = "0";
                                AddMessage("Warning", "Ops!, " + mensajeErrorAfip);
                                                           }
                            else //son observaciones pero puede rechazar la factura
                            {
                                if (RetornoAfip.FeDetResp[0].Observaciones != null && RetornoAfip.FeDetResp[0].Resultado != "A")
                                {
                                    foreach (var obs in RetornoAfip.FeDetResp[0].Observaciones)
                                    {
                                        mensajeErrorAfip += string.Format("Er: {0}: {1}", obs.Msg, obs.Code);
                                    }
                                    cae = "0";
                                    AddMessage("Warning", "Ops!, " + mensajeErrorAfip);
                                    NLogHelper.Instance.ErrorLog("FacturaController", "GrabarFactura", "Ops!, Notificacion de Afip: " + mensajeErrorAfip);
                                }
                                else
                                {
                                    ResultadoAfip = RetornoAfip.FeDetResp[0].Resultado;
                                    if (RetornoAfip.FeDetResp[0].Resultado == "A")
                                    {
                                        cae = RetornoAfip.FeDetResp[0].CAE;
                                        caeFechaVenc = RetornoAfip.FeDetResp[0].CAEFchVto;
                                        model.NumeroFactura =  int.Parse(RetornoAfip.FeDetResp[0].CbteDesde.ToString());
                                    }
                                }

                            }
                        }
                        else
                        {

                            AddMessage("Warning", "Ops!, El servicio de AFIP no responde...");
                            NLogHelper.Instance.ErrorLog("FacturaController", "GrabarFactura", "Ops!, El servicio de AFIP no responde...");
                            return RedirectToAction("Index");
                        }
                    }

                }
                else
                {
                    try
                    {
                        if (model.idTipoComprobanteSeleccionado != 4 && model.AplicaTipoCBT != "61" && model.AplicaTipoCBT != "0")
                        {
                            decimal ImporteAfip = 0;
                            string CuitAfip = "";
                            if (model.IdPuntoVenta == 1)
                            {
                                //FECompConsultaResponse responseAfip = afipHelper.ConsultaFacturaLocal(_tipoComprobanteVenta.CodigoAfip, model.IdPuntoVenta, model.NumeroFactura);
                                
                                FECompConsultaResponse responseAfip = afipHelper.ConsultaFacturaLocal(short.Parse(model.AplicaTipoCBT), model.IdPuntoVenta, int.Parse(model.AplicaNC));
                                if (responseAfip.ResultGet != null)
                                {
                                    ResultadoAfip = "A";
                                    idWsAfip = 0;
                                    CuitAfip = responseAfip.ResultGet.DocNro.ToString();
                                    cae = responseAfip.ResultGet.CodAutorizacion;
                                    caeFechaVenc = responseAfip.ResultGet.FchVto;
                                    ImporteAfip = (decimal)responseAfip.ResultGet.ImpTotal;
                                    model.Cotizacion = (decimal)responseAfip.ResultGet.MonCotiz;
                                }
                                else
                                {
                                    ResultadoAfip = responseAfip.Errors[0].Msg;
                                } 
                                
                            }

                            else
                            {
                                AfipHelperExterior afipHelperExterior = new AfipHelperExterior();
                                //FEXGetCMPResponse responseAfip = afipHelperExterior.getComprobanteAfipExterior((short)_tipoComprobanteVenta.CodigoAfip, _tipoComprobanteVenta.PuntoVenta, model.NumeroFactura);
                                //_tipoComprobanteVenta = servicioTipoComprobanteVenta.getTipoComprobanteVenta(comprobanteActualizado, AfipHelper.getPuntoVentaAfip(model.IdPuntoVenta));

                                FEXGetCMPResponse responseAfip = afipHelperExterior.getComprobanteAfipExterior(short.Parse(model.AplicaTipoCBT), _tipoComprobanteVenta.PuntoVenta, int.Parse(model.AplicaNC));

                                ResultadoAfip = "A";
                                idWsAfip = responseAfip.FEXResultGet.Id;
                                CuitAfip = responseAfip.FEXResultGet.Cuit_pais_cliente.ToString();
                                cae = responseAfip.FEXResultGet.Cae;
                                caeFechaVenc = responseAfip.FEXResultGet.Fch_venc_Cae;
                                ImporteAfip = (decimal)responseAfip.FEXResultGet.Imp_total;
                                model.Cotizacion = responseAfip.FEXResultGet.Moneda_ctz;
                            }
                            if (ResultadoAfip == "A")
                            {
                                //if (model.TotalFactura != ImporteAfip || model.Cuit != CuitAfip)
                                if (model.Cuit != CuitAfip)
                                {
                                    AddMessage("Error", "Ops!, No se pudo guardar la factura. Los datos no coinciden con la AFIP");
                                    mensajeErrorAfip = "Factura Afip Cuit " + CuitAfip + " Monto " + ImporteAfip.ToString() + "Factura Cuit " + model.Cuit + " Monto " + model.TotalFactura.ToString();
                                    return Json(new { result = false, data = mensajeErrorAfip }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                AddMessage("Error", "Ops!, No se pudo guardar la factura. Los datos no coinciden con la AFIP");
                                mensajeErrorAfip = ResultadoAfip;
                                return Json(new { result = false, data = mensajeErrorAfip }, JsonRequestBehavior.AllowGet);

                            }

                            AddMessage("success", "Se Registro en AFIP, Nº de Cbt: " + model.NumeroFactura);
                            NLogHelper.Instance.Info("FacturaController", "GrabarFactura", "Factura Afip Valor" + ImporteAfip.ToString());
                        }
                    }
                    catch (Exception ex)
                    {

                        AddMessage("Error", "Ops!, No se pudo guardar la factura. Los datos no coinciden con la AFIP");
                        NLogHelper.Instance.LogExcepcion(ex, "No se pudo guardar la factura");
                        return Json(new { result = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
                    }
                }

                // si factura es manual, no ir a afip(no inserta la factura electronica)
                if (mensajeErrorAfip != null) {
                    return Json(new { result = false , data = mensajeErrorAfip }, JsonRequestBehavior.AllowGet);

                }
                else 
                { 
                string moneda = (model.idTipoMoneda == 2) ? "DOL" : "PES";
                 if (model.FacturaManual == false)
                 {
                    var cbt = servicioTipoComprobanteVenta.ActualizarNroFactura(_tipoComprobanteVenta.CodigoAfip, _tipoComprobanteVenta.PuntoVenta, model.NumeroFactura + 1);
                 }
                        

                DatosQrAfip qrAfip = new DatosQrAfip();
                qrAfip.ver = 1;
                qrAfip.fecha = model.Fecha.ToString("yyyy-MM-dd"); 
                qrAfip.cuit = long.Parse(System.Configuration.ConfigurationManager.AppSettings["cuitUserAfip"].ToString());
                qrAfip.ptoVta = _tipoComprobanteVenta.PuntoVenta;
                qrAfip.tipoCmp = _tipoComprobanteVenta.CodigoAfip;
                qrAfip.nroCmp = model.NumeroFactura;
                qrAfip.importe = Math.Round(model.TotalFactura,2);
                qrAfip.moneda = moneda;
                qrAfip.ctz = model.Cotizacion;
                qrAfip.tipoDocRec = 80;
                qrAfip.nroDocRec = long.Parse(model.Cuit);
                qrAfip.tipoCodAut = "E";
                qrAfip.codAut = long.Parse(cae);
                string jsonQrAfip = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(qrAfip)));

               
                FacturaElectronicaModel FacturaElectronica = new FacturaElectronicaModel();
                FacturaElectronica.ALICUOTA = "0";
                FacturaElectronica.CATEGORIA = "Exento";
                FacturaElectronica.CODBARRA = "";
                FacturaElectronica.NRODOC = model.Cuit;
                FacturaElectronica.TIPOSERV = 2;
                FacturaElectronica.CODCLI = model.CodigoCliente;
                FacturaElectronica.CODPAIS = model.CodPaisAfip.ToString();
                FacturaElectronica.COTIZA = model.Cotizacion;
                FacturaElectronica.NOMBRE = model.NombreComp;
                FacturaElectronica.DOMICILIO = model.DireccionCompuesta;
                FacturaElectronica.ATENCION = model.Atencion;
                FacturaElectronica.PAIS = model.PaisComp;
                FacturaElectronica.ESTADO = "2";
                FacturaElectronica.FDESDE = model.Fecha;
                FacturaElectronica.FECHACBTE = model.Fecha;
                FacturaElectronica.FECHAVEN = model.Fecha.AddDays(180);
                FacturaElectronica.FHASTA = null;
                FacturaElectronica.FORMAPAGO = model.IdTipoPago.ToString();
                FacturaElectronica.TOTAL = Math.Round(model.TotalFactura, 2);
                FacturaElectronica.NETO = Math.Round(model.TotalFactura, 2);
                FacturaElectronica.NROCBTE_AFIP = model.NumeroFactura;
                FacturaElectronica.PUNTOVTA = _tipoComprobanteVenta.PuntoVenta;
                FacturaElectronica.ID_TIPOCBTE = _tipoComprobanteVenta.Id;
                FacturaElectronica.IVA = 0;
                FacturaElectronica.IDIVA = "3";
                FacturaElectronica.TIPODOC = 80;
                FacturaElectronica.IVA10 = 0;
                FacturaElectronica.NETO10 = 0;
 //               string moneda = (model.idTipoMoneda == 2) ? "DOL" : "PES";
                FacturaElectronica.IDMONEDA = moneda;
                FacturaElectronica.QR = jsonQrAfip;
                FacturaElectronica.OBS = mensajeErrorAfip;
                FacturaElectronica.CAE = cae;
                FacturaElectronica.FECHAVTO = DateTime.ParseExact(caeFechaVenc, "yyyyMMdd", CultureInfo.InvariantCulture);
                FacturaElectronica.ID_CBTE_WSAFIP = idWsAfip;               
                FacturaElectronica.NROAUX = model.NumeroFactura.ToString();
                FacturaElectronicaModel FacturaElectronicaInsertada = servicioFacturaElectronica.Agregar(FacturaElectronica);

                FacturaVentaModel facturaVentaModel = new FacturaVentaModel();
                facturaVentaModel.IdTipoComprobante = _tipoComprobanteVenta.Id;
                facturaVentaModel.IdFacturaElectronica = FacturaElectronicaInsertada.ID;
                facturaVentaModel.NumeroFactura = model.NumeroFactura; // FacturaElectronicaInsertada.NROCBTE_AFIP != null ? int.Parse(FacturaElectronicaInsertada.NROCBTE_AFIP.ToString()) : model.NumeroFactura;
                facturaVentaModel.Codigo = model.CodigoCliente;
                facturaVentaModel.IdCliente = model.IdCliente;
                facturaVentaModel.Fecha = model.Fecha;
                facturaVentaModel.Impre = "true";
                facturaVentaModel.Vencimiento = model.Fecha.AddDays(1);
                facturaVentaModel.Concepto = model.EncabezadoFact;
                facturaVentaModel.Condicion = "1";
                facturaVentaModel.IdProvincia = model.idProvincia;
                facturaVentaModel.IdPais = model.idPais;

                //Monto de factura
                decimal TotalFactura = Math.Round(model.TotalFactura * nFactor,2);
                if (model.idTipoMoneda == 2)
                {
                    facturaVentaModel.TotalDolares = TotalFactura;
                }
                facturaVentaModel.Total = Math.Round(model.TotalFactura * nFactor,2);
                
                facturaVentaModel.TipoIva = model.idTipoIva.ToString();
                facturaVentaModel.Cotiza = model.Cotizacion;
                facturaVentaModel.YRef = model.YREf;
                facturaVentaModel.ORef = model.ORef;
                facturaVentaModel.IdDto = model.idDepartamento;
                facturaVentaModel.Tipo = tipoComprobanteAbreviado;

                facturaVentaModel.TipoFac =(model.IdPuntoVenta == 2)? facturaVentaModel.TipoFac = "E" : facturaVentaModel.TipoFac = "L";
                
                facturaVentaModel.Periodo = int.Parse(model.Fecha.ToString("yyMM"));
                facturaVentaModel.IdImputacion = 0;
                facturaVentaModel.NumeroCobro = 0;
                facturaVentaModel.IdMoneda = model.idTipoMoneda;
                facturaVentaModel.Descuento = "1";
                facturaVentaModel.Recibo = "0";
                facturaVentaModel.NotaBanco = model.Nota;
                facturaVentaModel.NumeroTra = FacturaElectronicaInsertada.CAE;
                facturaVentaModel.Saldo = Math.Round(model.TotalFactura * nFactor, 2);
                if (model.AplicaNC != null)
                {
                   facturaVentaModel.Anula = model.AplicaTipoCBT + "-" + int.Parse(model.AplicaNC).ToString("D8");
                        if (model.AplicaTipoCBT != "61")
                        {
                            if (model.idTipoComprobanteSeleccionado == 3 && model.idTipoComprobanteSeleccionado == 8)
                            {
                                decimal nSaldo = model.TotalFactura - model.TotalAplicaCBT;
                                if (nSaldo <= 0)
                                {
                                    nSaldo = 0;
                                    facturaVentaModel.Parcial = 0;
                                }
                                else
                                {
                                    facturaVentaModel.Parcial = Math.Round(model.TotalAplicaCBT, 2);
                                };
                                facturaVentaModel.Saldo = Math.Round(nSaldo, 2);
                            };
                        };
                }
                else {
                   facturaVentaModel.Anula = "0";        
                };
                facturaVentaModel.Activo = true;
                facturaVentaModel.IdUsuario = OUsuario.IdUsuario;
                facturaVentaModel.UltimaModificacion = DateTime.Now;
                facturaVentaModel.TipoMoneda = null;
                facturaVentaModel.TipoComprobanteVenta = null;
                facturaVentaModel.FactVentaCobro = null;
                facturaVentaModel.ItemImpre = null;
                facturaVentaModel.Retencion = null;
                facturaVentaModel.Baja = "*";

                // add codigo al cbte del pago  y utilizar el mismo para todos los asientos de pago                  
                var CodigoAsiento = servicioContable.GetNuevoCodigoAsiento() + 1;
                facturaVentaModel.CodigoDiario = CodigoAsiento;

                FacturaVentaModel FacturaInsertada = servicioFacturaVenta.Agregar(facturaVentaModel);

                // p/ generara pdf de factura
                idComprobante = FacturaInsertada.Id.ToString();
                //inserto items de la factura
                var ListadoItemsFactura = JsonConvert.DeserializeObject<List<ItemImprFacturaModelView>>(model.hdnArticulos);
                foreach (ItemImprFacturaModelView item in ListadoItemsFactura)
                {
                    ItemImprModelView itemImpr = new ItemImprModelView();
                    itemImpr.IdTipoComprobante = _tipoComprobanteVenta.Id;
                    itemImpr.PuntoVenta = FacturaElectronicaInsertada.PUNTOVTA.ToString();
                    itemImpr.IdFactVenta = FacturaInsertada.Id;
                    itemImpr.Factura = model.NumeroFactura;   // FacturaElectronicaInsertada.NROCBTE_AFIP != null ? int.Parse(FacturaElectronicaInsertada.NROCBTE_AFIP.ToString()) : model.NumeroFactura;
                    itemImpr.Codigo = item.codigo;
                    itemImpr.Descripcion = item.descripcion;
                    itemImpr.Precio = item.valor;
                    itemImpr.Activo = true;
                    itemImpr.Cantidad = 1;
                    itemImpr.IdUsuario = OUsuario.IdUsuario;
                    itemImpr.UltimaModificacion = DateTime.Now;
                    //agrego item
                    ItemImprModel itemInsertado = servicioItemImpr.Agregar(Mapper.Map<ItemImprModelView, ItemImprModel>(itemImpr));

                    ArticuloModel artModel = servicioArticulo.GetArticuloOuCodigo(item.codigo);
                    //agrego, actualizo dto
                    DtoModel dtoModel = servicioDto.ActualizarDatosDto(DateTime.Now, itemInsertado, artModel.Codigo, model.idTipoIva, model.idDepartamento, nFactor, model.idTipoMoneda, model.Cotizacion, artModel, OUsuario);

                    //asiento contable
                    if (artModel.Tipo.Contains("Gastos"))
                    {
                        if (FacturaInsertada.IdMoneda == 2)
                        {
                            totalGastosPesos += (item.valor * model.Cotizacion) * nFactor;
                        }
                        else
                        {
                            totalGastosPesos += item.valor * nFactor;
                        }
                    }
                }


                var ImportePesos = (FacturaInsertada.IdMoneda == 1) ? (FacturaInsertada.Total * nFactor) : (FacturaInsertada.TotalDolares * FacturaInsertada.Cotiza * nFactor);
                /// asientos de ventas
                if (FacturaInsertada != null)
                {
                    DiarioModel asiento = new DiarioModel();
                    asiento.Codigo = FacturaInsertada.CodigoDiario;
                    asiento.Fecha = FacturaInsertada.Fecha;
                    asiento.Periodo = FacturaInsertada.Fecha.ToString("yyMM");
                    asiento.Tipo = "VF";
                    asiento.Cotiza = FacturaInsertada.Cotiza;
                    asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
                    asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(FacturaInsertada.IdMoneda).Descripcion;
                    asiento.Descripcion = "Deudores por Ventas Cliente " + FacturaInsertada.NumeroFactura;
                    asiento.DescripcionMa = "Asiento de Factura Venta " + FacturaInsertada.NumeroFactura;
                    asiento.Titulo = "Asiento de Venta";
                    if (model.IdPuntoVenta == 2) // exterior
                    {
                        // 1 
                        asiento.Importe = (FacturaInsertada.IdMoneda == 1) ? (FacturaInsertada.Total) : ((FacturaInsertada.TotalDolares * FacturaInsertada.Cotiza));
                        var asientoVEXT = servicioContable.InsertAsientoContable("VEXT", asiento, 0);
                        if (asientoVEXT != null) { servicioImputacion.AsintoContableGeneral(asientoVEXT); }

                        //2
                        asiento.Descripcion = "Servicios";
                        asiento.Importe = -(ImportePesos - totalGastosPesos);
                        if (asiento.Importe != 0)
                        {
                            var asientoSEXT = servicioContable.InsertAsientoContable("SEXT", asiento, 0);
                            if (asientoSEXT != null) { servicioImputacion.AsintoContableGeneral(asientoSEXT); }
                        }

                        //3 totalGastosPesos
                        asiento.Importe = -totalGastosPesos;
                        if (asiento.Importe != 0)
                        {
                            asiento.Descripcion = "Recupero de Gastos";
                            var asientoGastos = servicioContable.InsertAsientoContable("VGAS", asiento, 0);
                            if (asientoGastos != null) { servicioImputacion.AsintoContableGeneral(asientoGastos); }
                        }

                    }
                    else //local
                    {
                        // 1 
                        asiento.Importe = (FacturaInsertada.IdMoneda == 1) ? (FacturaInsertada.Total) : ((FacturaInsertada.TotalDolares * FacturaInsertada.Cotiza));
                        var asientoVEXT = servicioContable.InsertAsientoContable("VLOC", asiento, 0);
                        if (asientoVEXT != null) { servicioImputacion.AsintoContableGeneral(asientoVEXT); }
                        //2
                        asiento.Descripcion = "Servicios";
                        asiento.Importe = -(ImportePesos - totalGastosPesos);
                        var asientoSEXT = servicioContable.InsertAsientoContable("SLOC", asiento, 0);
                        if (asientoSEXT != null) { servicioImputacion.AsintoContableGeneral(asientoSEXT); }

                        //3 totalGastosPesos
                        asiento.Descripcion = "Recupero de Gastos";
                        asiento.Importe = -totalGastosPesos;
                        var asientoGastos = servicioContable.InsertAsientoContable("VGAS", asiento, 0);
                        if (asientoGastos != null) { servicioImputacion.AsintoContableGeneral(asientoSEXT); }


                    }

                }

                //agrega en tbl IvaVenta
                IvaVentaModel ivaVenta = new IvaVentaModel();
                ivaVenta.IdTipoComprobantes = _tipoComprobanteVenta.Id;
                ivaVenta.PuntoVenta = FacturaElectronicaInsertada.PUNTOVTA.ToString();
                ivaVenta.NumeroFactura = model.NumeroFactura;  // FacturaElectronicaInsertada.NROCBTE_AFIP != null ? int.Parse(FacturaElectronicaInsertada.NROCBTE_AFIP.ToString()) : model.NumeroFactura;
                ivaVenta.NroEmp = model.IdCliente;
                ivaVenta.NomEmp = model.CodigoCliente;
                ivaVenta.IdImputacion = model.idImputacion.ToString();
                ivaVenta.Fecha = model.Fecha;
                ivaVenta.Periodo = model.Fecha.ToString("yyMM");
                ivaVenta.Neto = ImportePesos - totalGastosPesos;
                ivaVenta.Total = ImportePesos;
                ivaVenta.Gasto = totalGastosPesos;
                ivaVenta.Isib = 0;
                ivaVenta.Moneda = model.idTipoMoneda.ToString();
                ivaVenta.TipoIva = model.idTipoIva.ToString();
                ivaVenta.Dolar = model.Cotizacion;
                ivaVenta.Activo = true;
                ivaVenta.IdUsuario = OUsuario.IdUsuario;
                ivaVenta.UltimaModificacion = DateTime.Now;
                ivaVenta.AuxiliarNumero = "0";
                ivaVenta.Diario = FacturaInsertada.CodigoDiario.ToString();
                ivaVenta.ClaseFac = model.CodPaisAfip.ToString();
                if (model.IdPuntoVenta == 2)
                {
                    ivaVenta.TipoFac = "E";
                }
                else
                {
                    ivaVenta.TipoFac = "L";
                }
                ivaVenta.Cuit = model.Cuit.ToString();
                ivaVenta.Clase = tipoComprobanteAbreviado;

                IvaVentaModel ivaModelInsertado = servicioIvaVenta.Agregar(ivaVenta);

                //agrega registro tbl Buque si no es nota credito!!!

                BuqueModel buqueModel = new BuqueModel();
                buqueModel.NumeroFactura = model.NumeroFactura; //FacturaElectronicaInsertada.NROCBTE_AFIP != null ? int.Parse(FacturaElectronicaInsertada.NROCBTE_AFIP.ToString()) : model.NumeroFactura;
                buqueModel.Cliente = model.IdCliente.ToString();
                buqueModel.Fecha = model.Fecha;
                
                /* lo saque porque daba error cuando media mas de 80 caracteres
                if (model.EncabezadoFact != null)
                {
                    buqueModel.Buque1 = model.EncabezadoFact.Substring(0, model.EncabezadoFact.Length > 80 ? 80 : model.EncabezadoFact.Length);
                    buqueModel.Descripcion = model.EncabezadoFact.Substring(model.EncabezadoFact.Length > 80 ? 80 : 0, model.EncabezadoFact.Length > 80 ? 80 : model.EncabezadoFact.Length);
                }
                */
                if (!string.IsNullOrEmpty(model.EncabezadoFact))
                {
                    buqueModel.Buque1 = model.EncabezadoFact.Substring(0, Math.Min(model.EncabezadoFact.Length, 80));
                    buqueModel.Descripcion = model.EncabezadoFact.Substring(0,Math.Min(model.EncabezadoFact.Length, 80));
                }
                

                buqueModel.Monto = Math.Round(model.TotalFactura, 2);

                buqueModel.YRef = model.YREf;
                buqueModel.ORef = model.ORef;
                buqueModel.Carpeta = model.nroCarpera.ToString();
                buqueModel.Legajo = model.nroCarperaFinal.ToString();
                buqueModel.Activo = true;
                buqueModel.IdUsuario = OUsuario.IdUsuario;
                buqueModel.UltimaModificacion = DateTime.Now;
                BuqueModel buqueModelInsertado = servicioBuque.Agregar(buqueModel);

                // add Edgardo   Actualiza Proforma  N/C
                if(short.Parse(model.AplicaTipoCBT) > 0) {
                        if (short.Parse(model.AplicaTipoCBT) == 61)
                        {
                            FacturaVentaModel Fact = new FacturaVentaModel();

                            // Asignar manualmente las propiedades que deseas copiar de `fact` a `factura`  
                            Fact.FechaCobro = model.Fecha;
                            Fact.Id = model.IdAplicaCBT;
                            
                            Fact.NumeroCobro = short.Parse(model.AplicaNC);
                            Fact.Cotiza = model.Cotizacion;
                            Fact.CotizaP = model.Cotizacion;
                            Fact.Parcial = facturaVentaModel.Parcial;
                            Fact.Saldo = facturaVentaModel.Saldo;
                            Fact.Activo = false;
                            
                            FacturaVentaModel FacturaModificar = servicioFacturaVenta.ActualizaFacturaNC(Fact);
                        }
                        else {
                            if (model.idTipoComprobanteSeleccionado == 3 && model.idTipoComprobanteSeleccionado == 8)
                            {
                                decimal nSaldo = model.TotalFactura - model.TotalAplicaCBT;

                                FacturaVentaModel Fact = new FacturaVentaModel();

                                // Asignar manualmente las propiedades que deseas copiar de `fact` a `factura`  
                                Fact.FechaCobro = model.Fecha;
                                Fact.Id = model.IdAplicaCBT;
                                Fact.Activo = true;
                                Fact.NumeroCobro = short.Parse(model.AplicaNC);
                                Fact.Cotiza = model.Cotizacion;
                                Fact.CotizaP = model.Cotizacion;
                                Fact.Parcial = facturaVentaModel.Parcial;
                                Fact.Saldo = facturaVentaModel.Saldo;

                                FacturaVentaModel FacturaModificar = servicioFacturaVenta.ActualizaFacturaNC(Fact);
                            }
                        };
                 };

                AddMessage("success", "se guardo la Factura correctamente");
                NLogHelper.Instance.Info("Factura Registrada: " + FacturaInsertada.Id);

                #endregion
                
                return Json(new { result = true, data = new { idCliente = cliente.Id.ToString(), idComprobante = idComprobante, qr = FacturaElectronicaInsertada.QR } }, JsonRequestBehavior.AllowGet);
                    
                }
            }
            catch (Exception ex)
            {
                AddMessage("error", "Ops!, No se pudo guardar la factura. Contacte al Administrador" + ex.Message.ToString());
                NLogHelper.Instance.LogExcepcion(ex, "No se pudo guardar la factura");
                return RedirectToAction("Index");
            }

        }


        [HttpGet()]
        public ActionResult GetFacturaJson(string idCliente, string idComprobante)
        {
            try
            {
                string strJson;

                FacturaVentaModel factura = servicioFacturaVenta.GetFacturaVentaPorId(int.Parse(idCliente), int.Parse(idComprobante));

                ImprimirFacturaModelView iFactura = new ImprimirFacturaModelView();
                iFactura.Id = factura.Id;
                // lo saque porque esta repetido
                // iFactura.NombreComprobante = factura.TipoComprobanteVenta.Denominacion.ToString();
                iFactura.NombreComprobante = obtenerNombreCbt(factura.TipoComprobanteVenta.CodigoAfip, factura.Cliente.IdIdioma);
                iFactura.PuntoVenta = factura.FacturaElectronica.PUNTOVTA.ToString();
                iFactura.NumeroComprobante = factura.NumeroFactura.ToString(); //   factura.FacturaElectronica.NROCBTE_AFIP.ToString();
                iFactura.TipoComprobante = factura.TipoComprobanteVenta.CodigoAfip.ToString();

                iFactura.LetraComprobante = factura.TipoComprobanteVenta.Abreviatura.Substring(factura.TipoComprobanteVenta.Abreviatura.Length - 1, 1);
                var f = (DateTime)(factura.FacturaElectronica.FECHACBTE != null ? factura.FacturaElectronica.FECHACBTE : DateTime.Now);
                iFactura.FechaEmision = f.ToString("dd/MM/yyyy");
                iFactura.OurRef = factura.ORef;
                iFactura.YourRef = factura.YRef;
                iFactura.RazonSocial = factura.FacturaElectronica.NOMBRE;
                iFactura.Domicilio = factura.FacturaElectronica.DOMICILIO;
                //add edgardo
                iFactura.Atencion = (factura.FacturaElectronica.ATENCION == null) ? "" : factura.FacturaElectronica.ATENCION;
                iFactura.Pais = (factura.FacturaElectronica.PAIS == null) ? "" : factura.FacturaElectronica.PAIS;
                iFactura.CondicionIVA = factura.FacturaElectronica.CATEGORIA;
                iFactura.NumeroDocumentoReceptor = factura.FacturaElectronica.NRODOC;
                iFactura.TipoDocumentoReceptor = factura.FacturaElectronica.TIPODOC.ToString();

                List<ItemFactura> itemFacturas = new List<ItemFactura>();

                foreach (var i in factura.ItemImpre)
                {
                    ItemFactura item = new ItemFactura();
                    // item.Nombre = i.Codigo + " " + i.Descripcion;
                    item.Nombre = i.Descripcion;
                    item.Precio = i.Precio;
                    itemFacturas.Add(item);
                }
                iFactura.Items = itemFacturas;
                var idDto = factura.IdDto ?? 0;
                iFactura.Dto = servicioDepartamento.GetDepartamentoPorId(idDto).Descripcion.ToString() + "/" + factura.Codigo;
                iFactura.codAut = factura.FacturaElectronica.CAE;

                var VtoCAE = (DateTime)factura.FacturaElectronica.FECHAVTO;
                iFactura.FechaVtoCodAut = VtoCAE.ToString("dd/MM/yyyy");
                iFactura.ImporteTotal = factura.FacturaElectronica.TOTAL.ToString();
                if (factura.Cliente.IdIdioma == 2)
                {
                    if (factura.FacturaElectronica.IDMONEDA == "PES")
                    {
                        MonedaNroStr oPesos = new MonedaNroStr();
                        iFactura.ImporteEnLetra = "  Pesos " + MonedaNroStr.Convertir(factura.FacturaElectronica.TOTAL.ToString(), true);
                    }
                    else
                    {
                        MonedaNroStr oPesos = new MonedaNroStr();
                        iFactura.ImporteEnLetra = " Dolares " + MonedaNroStr.Convertir(factura.FacturaElectronica.TOTAL.ToString(), true);
                    }
                }
                else
                {
                    NumToStr oMoneda = new NumToStr();
                    iFactura.ImporteEnLetra = "Dolar  " + NumToStr.ConvertToWords(factura.FacturaElectronica.TOTAL.ToString());
                }

                iFactura.Header = factura.Concepto != null ? factura.Concepto : "";
                //iFactura.Nota = factura.Cliente.PieNota.Nota != null ? factura.Cliente.PieNota.Nota : "";
                iFactura.Nota = factura.NotaBanco != null ? factura.NotaBanco : "";
                iFactura.QrBase64 = factura.FacturaElectronica.QR != null ? factura.FacturaElectronica.QR : "";
                string NotaI = "Las prestaciones detalladas en el presente comprobante se encuentran exentas de conformidad con la normativa establecida en el artículo 34 del Decreto 692 / 98 por tratarse de servicios conexos al transporte internacional de pasajeros y / o cargas, que complementan y tienen por objeto exclusivo servir al mismo en los términos del artículo 7, inciso h), acápite 13 de la Ley 23.349(T.O.Decreto 280 / 97).";
                string NotaS = "La presente factura corresponde a la cantidad de DOLARES ESTADOUNIDENSES("+ factura.FacturaElectronica.TOTAL.ToString()+") en concepto de contraprestación por los servicios prestados.La misma debe ser cancelada en moneda de curso legal al tipo de cambio Vendedor del BNA al día anterior a la fecha efectiva de pago.";
               // string NotaS = "Cuando existan diferencias de cotización se emitirán las correspondientes NC/ND, según corresponda.-Queda establecido que, a todos los efectos legales, la valorización del presente documento en moneda de curso legal, se realiza a consecuencia de suexteriorización al momento de emisión de la misma y/ o prestación del servicio.- Total";
                if (factura.FacturaElectronica.CODPAIS == "200")
                {
                    iFactura.NotaAfipSuperior = NotaS;
                    iFactura.NotaAfipInferior = NotaI;
                }
                else
                {
                    iFactura.NotaAfipSuperior =  "" ;
                    iFactura.NotaAfipInferior = (factura.FacturaElectronica.ID_TIPOCBTE < 19) ? NotaI : "";
                }


                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(iFactura);
                if ((strJson != null))
                {
                    var rJson = Json(new { result = true, data = strJson }, JsonRequestBehavior.AllowGet);
                    return rJson;
                }
                return Json(new { result = true, data = strJson }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador");
                NLogHelper.Instance.LogExcepcion(ex, "No se pudo guardar la factura");
                return Json(new { result = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        private string obtenerNombreCbt(int CodigoAfip, int IdIdioma)
        {
            if (CodigoAfip == 211 || CodigoAfip == 11 || CodigoAfip == 19)
            {
                return (IdIdioma == 1) ? "Factura" : "Invoice";
            }
            if (CodigoAfip == 212 || CodigoAfip == 12 || CodigoAfip == 20)
            {
                return (IdIdioma == 1) ? "Débito" : "Debit";
            }
            if (CodigoAfip == 213 || CodigoAfip == 13 || CodigoAfip == 21)
            {
                return (IdIdioma == 1) ? "Crédito " : "Credit";
            }
            return "Factura";
        }


        public FEXResponseAuthorize InsertarCBTAfipExterior(ClaseLoginAfip TicketAcceso, FacturaModelView model, TipoComprobanteVentaModel Comprobante, long cuitPropietario)
        {
            try
            {
                var totalImporte = Math.Round(model.TotalFactura,2);

                //instancio objeto autenticacion
                ClsFEXAuthRequest Autenticacion = new ClsFEXAuthRequest();
                Autenticacion.Cuit = cuitPropietario;
                Autenticacion.Sign = TicketAcceso.Sign;
                Autenticacion.Token = TicketAcceso.Token;

                //se prepara el servicio para enviar
                afip.wswhomo_Exportacion.Service ServicioWebFacturaExterior = new afip.wswhomo_Exportacion.Service();
                ServicioWebFacturaExterior.Url = @"https://servicios1.afip.gov.ar/wsfexv1/service.asmx?WSDL";

                ServicioWebFacturaExterior.ClientCertificates.Add(TicketAcceso.certificado);

                //verificaciona ws 
                var EstadoWs = ServicioWebFacturaExterior.FEXDummy();

                ClsFEX_LastCMP ultimoComprobante = new ClsFEX_LastCMP();
                ultimoComprobante.Cbte_Tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                ultimoComprobante.Pto_venta = Comprobante.PuntoVenta;
                ultimoComprobante.Cuit = cuitPropietario;
                ultimoComprobante.Sign = TicketAcceso.Sign;
                ultimoComprobante.Token = TicketAcceso.Token;

                var cbte_nroObternido = ServicioWebFacturaExterior.FEXGetLast_CMP(ultimoComprobante);
                ClsFEXGetCMP clsFEXGetCMP = new ClsFEXGetCMP();
                if (cbte_nroObternido.FEXResult_LastCMP != null)
                {
                    clsFEXGetCMP.Cbte_nro = cbte_nroObternido.FEXResult_LastCMP.Cbte_nro;
                }
                else
                {
                    clsFEXGetCMP.Cbte_nro = 0;
                }
                clsFEXGetCMP.Cbte_tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                clsFEXGetCMP.Punto_vta = Comprobante.PuntoVenta;

                var UltimoCbte_Existente = ServicioWebFacturaExterior.FEXGetCMP(Autenticacion, clsFEXGetCMP);
                var ultimo_id = ServicioWebFacturaExterior.FEXGetLast_ID(Autenticacion);

                FEXResponseAuthorize Respuesta = new FEXResponseAuthorize();

                
                ClsFEXRequest solicitud = new ClsFEXRequest();
                solicitud.Id = ultimo_id.FEXResultGet.Id + 1;
                solicitud.Cbte_Tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                solicitud.Fecha_cbte = model.Fecha.ToString("yyyyMMdd");
                solicitud.Punto_vta = Comprobante.PuntoVenta;
                solicitud.Cbte_nro = clsFEXGetCMP.Cbte_nro + 1;   
                solicitud.Tipo_expo = 2; //servicios
                solicitud.Permiso_existente = "";
                solicitud.Dst_cmp = short.Parse(model.CodPaisAfip.ToString()); //pais destino
                solicitud.Cliente = model.Cliente.Nombre;
                solicitud.Cuit_pais_cliente = long.Parse(model.Cuit);
                solicitud.Domicilio_cliente = model.DireccionCompuesta;
                solicitud.Id_impositivo = null; //averiguar

                AfipHelper afipHelper = new AfipHelper();
                switch (model.idTipoMoneda)
                {
                    case 1:
                        solicitud.Moneda_Id = "PES";
                        solicitud.Moneda_ctz = 1;
                        break;
                    case 2:
                        solicitud.Moneda_Id = "DOL";
                        //solicitud.Moneda_ctz = decimal.Parse(afipHelper.GetCotizacion("DOL").ResultGet.MonCotiz.ToString()); /// se deberia borrar y utilizar la guardada en db 
                        solicitud.Moneda_ctz = decimal.Parse(model.Cotizacion.ToString());
                        break;
                }
                ///-----////
            
                if (Comprobante.CodigoAfip == 12 || Comprobante.CodigoAfip == 13 || Comprobante.CodigoAfip == 20 || Comprobante.CodigoAfip == 21)
                {
                    //para nota credito/debito

                    /*defino el comprobante asociado ej Factura E*/
                    Cmp_asoc ComprobanteAsociado = new Cmp_asoc();
                    ComprobanteAsociado.Cbte_tipo = short.Parse(model.AplicaTipoCBT);
                    ComprobanteAsociado.Cbte_punto_vta = Comprobante.PuntoVenta;
                    ComprobanteAsociado.Cbte_nro = long.Parse(model.AplicaNC);
                   // ComprobanteAsociado.Cbte_cuit = long.Parse(model.Cuit);
                    solicitud.Cmps_asoc = new[] { ComprobanteAsociado };
                }
                ///----////
                ///
                solicitud.Obs_comerciales = "Observaciones comerciales";
                solicitud.Imp_total = Math.Round(model.TotalFactura, 2);
                solicitud.Obs = "Sin observaciones";
                solicitud.Forma_pago = model.IdTipoPago.ToString();
                if (Comprobante.CodigoAfip < 20 ) { 
                    solicitud.Fecha_pago = (model.Fecha.AddDays(180)).ToString("yyyyMMdd");
                }
                var icoterm = ServicioWebFacturaExterior.FEXGetPARAM_Incoterms(Autenticacion);

                solicitud.Incoterms = null;
                solicitud.Incoterms_Ds = null;
                solicitud.Idioma_cbte = short.Parse(model.IdTipoIdioma.ToString());
                solicitud.Permisos = null; // ver este item

                /*agrego el item*/
                Item item = new Item();
                item.Pro_codigo = null;
                item.Pro_ds = null;

                

                var ListadoItemsFactura = JsonConvert.DeserializeObject<List<ItemImprFacturaModelView>>(model.hdnArticulos);

                List<Item> items = new List<Item>(); // Crear una lista para almacenar los items

                foreach (ItemImprFacturaModelView itemIterar in ListadoItemsFactura)
                {
                    Item itemGrabar = new Item();
                    itemGrabar.Pro_codigo = itemIterar.codigo;
                    itemGrabar.Pro_ds = itemIterar.descripcion;
                    itemGrabar.Pro_qty = 1;
                    itemGrabar.Pro_umed = 1;
                    itemGrabar.Pro_precio_uni = itemIterar.valor;
                    itemGrabar.Pro_bonificacion = 0;
                    itemGrabar.Pro_total_item = itemGrabar.Pro_qty * itemGrabar.Pro_precio_uni - itemGrabar.Pro_bonificacion; 
                    items.Add(itemGrabar); // Agregar el item a la lista de items
                }

                solicitud.Items = items.ToArray();
               
                //se envia el servicio al WS
                Respuesta = ServicioWebFacturaExterior.FEXAuthorize(Autenticacion, solicitud);
                return Respuesta;

            }
            catch (Exception ex)
            {
                AddMessage("Error", "Ops!, Se genera un error cuando se quiere enviar una factura a AFIP. " + ex.Message.ToString());
                return null;
            }

        }

        public FECAEResponse InsertarCBTAfipLocal(ClaseLoginAfip TicketAcceso, FacturaModelView model, TipoComprobanteVentaModel Comprobante, long cuitPropietario)
        {
            try
            {
                //instancio objeto autenticacion
                FEAuthRequest Autenticacion = new FEAuthRequest();
                Autenticacion.Cuit = cuitPropietario;//long.Parse(model.Cuit);
                Autenticacion.Sign = TicketAcceso.Sign;
                Autenticacion.Token = TicketAcceso.Token;

                //se prepara el servicio para enviar
                afip.wswhomo.Service ServicioWebFactura = new afip.wswhomo.Service();
                ServicioWebFactura.Url = @"https://servicios1.afip.gov.ar/wsfev1/service.asmx?WSDL";

                ServicioWebFactura.ClientCertificates.Add(TicketAcceso.certificado);
                //cargo los datos de la factura
                int puntoVenta = Comprobante.PuntoVenta;
                int tipoComprobante = Comprobante.CodigoAfip;

                //inicio solicitud
                FECAERequest Solicitud = new FECAERequest();
                //encabezado solicitud
                FECAECabRequest EncabezadoSolicitud = new FECAECabRequest();
                //cuerpo solicitud 
                FECAEDetRequest CuerpoSolicitud = new FECAEDetRequest();

                EncabezadoSolicitud.CantReg = 1;
                EncabezadoSolicitud.PtoVta = puntoVenta;
                EncabezadoSolicitud.CbteTipo = tipoComprobante;
                Solicitud.FeCabReq = EncabezadoSolicitud;

                //cargamos el cuerpo

                CuerpoSolicitud.Concepto = 2;//servicios
                CuerpoSolicitud.DocTipo = 80; //model.IdTipoPago;
                CuerpoSolicitud.DocNro = long.Parse(model.Cuit);

                //autorizarse
                FERecuperaLastCbteResponse UltimoRes = ServicioWebFactura.FECompUltimoAutorizado(Autenticacion, puntoVenta, tipoComprobante);

                OpcionalTipoResponse opcional = ServicioWebFactura.FEParamGetTiposOpcional(Autenticacion);

                int ultimoNroComprobante = UltimoRes.CbteNro + 1;

                FECAEResponse Respuesta = new FECAEResponse();

                CuerpoSolicitud.CbteDesde = ultimoNroComprobante;
                CuerpoSolicitud.CbteHasta = ultimoNroComprobante;
                CuerpoSolicitud.CbteFch = model.Fecha.ToString("yyyyMMdd");
                CuerpoSolicitud.ImpTotal = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                CuerpoSolicitud.ImpNeto = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                // CuerpoSolicitud.ImpIVA = 0;
                CuerpoSolicitud.ImpTotConc = 0;
                CuerpoSolicitud.ImpOpEx = 0;
                CuerpoSolicitud.ImpTrib = 0;
                CuerpoSolicitud.FchServDesde = model.Fecha.ToString("yyyyMMdd");
                CuerpoSolicitud.FchServHasta = model.Fecha.ToString("yyyyMMdd");
                if (Comprobante.CodigoAfip > 211) {
                    CuerpoSolicitud.FchVtoPago = "";
                }
                else {
                    CuerpoSolicitud.FchVtoPago = (model.Fecha.AddDays(180)).ToString("yyyyMMdd");
                }
                if (Comprobante.CodigoAfip > 200)
                {

                    // Crear una lista de Opcionales y agregar las instancias creadas
                    List<afip.wswhomo.Opcional> listaOpcionales = new List<afip.wswhomo.Opcional>
                        {
                            new afip.wswhomo.Opcional { Id = "2101", Valor = "2850540430094203070471" },
                            new afip.wswhomo.Opcional { Id = "27", Valor = "SCA" }
                        };

                    // Asignar la lista de Opcionales a la propiedad Opcionales de CuerpoSolicitud
                    CuerpoSolicitud.Opcionales = listaOpcionales.ToArray();

                }
                switch (model.idTipoMoneda)
                {
                    case 1:
                        CuerpoSolicitud.MonId = "PES";
                        CuerpoSolicitud.MonCotiz = 1;
                        break;
                    case 2:
                        FECotizacionResponse paramCoti = ServicioWebFactura.FEParamGetCotizacion(Autenticacion, "DOL");
                        CuerpoSolicitud.MonId = "DOL";
                        CuerpoSolicitud.MonCotiz = paramCoti.ResultGet.MonCotiz;
                        break;
                }

                if (Comprobante.CodigoAfip == 12  || Comprobante.CodigoAfip == 13 ||
                    Comprobante.CodigoAfip == 20  || Comprobante.CodigoAfip == 21 ||
                    Comprobante.CodigoAfip == 212 || Comprobante.CodigoAfip == 213)
                {
                    CbteAsoc cbteAsoc = new CbteAsoc();
                    cbteAsoc.Nro = long.Parse(model.AplicaNC); //nro factura;
                    cbteAsoc.Tipo = short.Parse(model.AplicaTipoCBT);
                    cbteAsoc.PtoVta = puntoVenta;
                    cbteAsoc.Cuit = model.Cuit;

                    CuerpoSolicitud.CbtesAsoc = new[] { cbteAsoc };
                }

                Solicitud.FeDetReq = new[] { CuerpoSolicitud };

                
                //se envia el servicio al WS
                Respuesta = ServicioWebFactura.FECAESolicitar(Autenticacion, Solicitud);
                return Respuesta;

            }
            catch (Exception ex)
            {
                AddMessage("Error", "Ops!, Se genera un error cuando se quiere enviar una factura a AFIP. " + ex.Message.ToString());
                return null;
            }

        }

        public int DeterminarNroComprobante(string puntoVta, bool miPyme, decimal TotalFactura, string tipoComprobante)
        {
            int retorno = 0;
            if (puntoVta != "2")
            {
                if (miPyme == true && TotalFactura > 1300000)
                {
                    //facturacion electronica
                    switch (tipoComprobante)
                    {
                        case "1":
                            retorno = 211;
                            break;
                        case "2":
                            retorno = 212;
                            break;
                        case "3":
                            retorno = 213;
                            break;
                    }
                }

                if (miPyme == false || TotalFactura< 1250000)
                {//documentacion C
                    switch (tipoComprobante)
                    {
                        case "1":
                            retorno = 11;
                            break;
                        case "2":
                            retorno = 12;
                            break;
                        case "3":
                            retorno = 13;
                            break;
                        case "4":
                            retorno = 60;
                            break;

                    }
                }
            }
            else
            {//documentacion exterior
                switch (tipoComprobante)
                {
                    case "1":
                        retorno = 19;
                        break;
                    case "2":
                        retorno = 20;
                        break;
                    case "3":
                        retorno = 21;
                        break;
                    case "4":
                        retorno = 61;
                        break;
                }
            }
            return retorno;
        }

        [HttpGet()]
        public ActionResult GetListClienteJson(string term)
        {
            try
            {
                // List<ClienteModel> cliente = servicioCliente.GetClientePorCodigo(term);
                List<ClienteModel> cliente = servicioCliente.GetClientePorNombre(term);
                var arrayProveedor = (from cli in cliente
                                      select new AutoCompletarViewModel()
                                      {
                                          id = cli.Id,
                                          label = cli.Nombre

                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                //servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetExisteFacturaJson(string term, string idCliente)
        {
            try
            {
                //if (idCliente == null)
                //{
                //    idCliente = "0";
                //}
                List<FacturaVentaModel> Listadofactura = servicioFacturaVenta.GetAllFacturaVentaPorNumero(int.Parse(term), int.Parse(idCliente));
                var arrayProveedor = (from fact in Listadofactura
                                      select new AutoCompletarViewModel()
                                      {
                                          //id = cli.Id,
                                          label = fact.NumeroFactura.ToString()
                                      }).ToArray();

                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador "+ex.Message);
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetObtenerFacturaJson(string idCliente, string nroFactura, string tipocbt, string idPuntoVenta)
        {
            try
            {
                string strJson = null;
                ClienteModelView Cliente = Mapper.Map<ClienteModel, ClienteModelView>(servicioCliente.GetClientePorId(int.Parse(idCliente)));

                int comprobanteActualizado = DeterminarNroComprobante(idPuntoVenta, Cliente.MiPyme, 0, tipocbt);
                TipoComprobanteVentaModelView tipoComprobante = Mapper.Map<TipoComprobanteVentaModel, TipoComprobanteVentaModelView>(servicioTipoComprobanteVenta.GetTipoComprobanteVentaPorNroAfip(comprobanteActualizado, AfipHelper.getPuntoVentaAfip(int.Parse(idPuntoVenta))));
                if (tipoComprobante != null)
                {
                    FacturaVentaItemsModel FacturaItems = servicioFacturaVentaItems.ObtenerDatosFacturaItems(int.Parse(idCliente), int.Parse(nroFactura), tipoComprobante.Id);
                    //add Edgardo
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        MaxDepth = 1 // Limita la profundidad de serialización  
                    };

                    strJson = Newtonsoft.Json.JsonConvert.SerializeObject(FacturaItems, settings);


                    //strJson = Newtonsoft.Json.JsonConvert.SerializeObject(FacturaItems);

                    if ((strJson != null))
                    {
                        var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                        return rJson;
                    }

                }

                return Json(strJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error "+ex.Message);
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetDireccionJson(string idDireccion)
        {
            try
            {
                string strJson;
                ClienteDireccionModel direccion = servicioClienteDireccion.ObtenerPorID(int.Parse(idDireccion));

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(direccion);
                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }

                return Json(strJson, JsonRequestBehavior.AllowGet);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                //servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetClienteJson(int IdCliente)
        {
            string strJson;
            try
            {

                ClienteModelView Cliente = Mapper.Map<ClienteModel, ClienteModelView>(servicioCliente.GetClientePorId(IdCliente));

                Cliente.ClienteDireccion = Mapper.Map<List<ClienteDireccionModel>, List<ClienteDireccionModelView>>(servicioClienteDireccion.GetDireccionPorcliente(Cliente.Id));
                ServicioPieNota oServicioPieNota = new ServicioPieNota();
                var cta = oServicioPieNota.GetPieNotaPorId(Cliente.IdPieNota).BancoCuenta;
                Cliente.IdBancoCuenta = cta.Id;
                Cliente.PieNota = Mapper.Map<PieNotaModel, PieNotaModelView>(servicioPieNota.GetPieNotaPorCodigo(cta.Codigo));

                List<SelectListItem> tipoComprobantes = new List<SelectListItem>();
                tipoComprobantes.Add(new SelectListItem() { Text = "Factura", Value = "1" });
                tipoComprobantes.Add(new SelectListItem() { Text = "Nota Debito", Value = "2" });
                tipoComprobantes.Add(new SelectListItem() { Text = "Nota Credito", Value = "3" });
                tipoComprobantes.Add(new SelectListItem() { Text = "Pro-Forma", Value = "4" });

                Cliente.ListaComprobantesDrop = tipoComprobantes;

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(Cliente);
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
                // servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador");
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        public ActionResult GetListCodigoJson(string term)
        {
            try
            {
                List<ArticuloModel> articulos = servicioArticulo.GetArticulosPorCodigo(term);
                var arrayArticulos = (from cli in articulos
                                      select new AutoCompletarViewModel()
                                      {
                                          id = cli.Id,
                                          label = cli.DescripcionCastellano
                                      }).ToArray();
                return Json(arrayArticulos, JsonRequestBehavior.AllowGet);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                //servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetCodigoJson(int IdArticulo)
        {
            string strJson;
            try
            {

                ArticuloModelView Codigo = Mapper.Map<ArticuloModel, ArticuloModelView>(servicioArticulo.GetArticulo(IdArticulo));

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(Codigo);
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
                // servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador");
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        public ActionResult GetCotizacionJson(string IdMoneda)
        {

            CotizacionAFIP cotizacion = new CotizacionAFIP();
            var f = DateTime.Now;
            string strJson;
            try
            {

                var moneda = servicioTipoMoneda.GetCotizacionPorIdMoneda(int.Parse(IdMoneda));
                if (moneda == null)
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
                //servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador");
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        public ActionResult GetPieNotaJson(string idCodigoCuentaBancaria)
        {
            try
            {
                
                BancoCuentaModelView cuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBancoCuenta.GetCuentaPorId(int.Parse(idCodigoCuentaBancaria)));

                string strJson;
                PieNotaModelView nota = Mapper.Map<PieNotaModel, PieNotaModelView>(servicioPieNota.GetPieNotaPorCodigo(cuenta.Codigo));
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(nota);
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
                // servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                AddMessage("Error", "Ops!, No se pudo guardar la factura. Contacte al Administrador");
                return null;
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }

    
    }
}