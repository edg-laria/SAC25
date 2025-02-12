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
    public class CajaCierreController : BaseController
    {

        private ServicioCaja servicioCaja = new ServicioCaja();

        private ServicioCajaGrupo servicioCajaGrupo = new ServicioCajaGrupo();
        private ServicioCajaSaldo servicioCajaSaldo = new ServicioCajaSaldo();
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioImputacion servicioImputacion = new ServicioImputacion();
        private ServicioContable servicioContable = new ServicioContable();

        public CajaCierreController()
        {
            servicioCaja._mensaje = (msg_, tipo_)  => AddMessage(tipo_, msg_);
        }

        public ActionResult Index(int searchIdCierre = 0)
        {
            CajaModelView model = new CajaModelView();
           
            model.ListaCaja = Mapper.Map<List<CajaModel>, List<CajaModelView>>(servicioCaja.GetAllCajaPorIdCierre(searchIdCierre));

            if (searchIdCierre == 0)
            {
                model.CajaSaldoInicial = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(servicioCajaSaldo.GetUltimoCierre());
            }
            else
            {
                model.CajaSaldoInicial = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(servicioCajaSaldo.GetCajaSaldoPorId(searchIdCierre));
            }
            
            model.FechaCierre = DateTime.Now;
            CargarCierreCaja();
            if (searchIdCierre == 0)
            {
                ViewBag.HabilitarCierre = true;
            }
            else
            {
                ViewBag.HabilitarCierre = false;
            }



            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CierreDeCaja(CajaModelView model)
        {
            {
                try
                {           
                    
                    var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

                    List<CajaModel> caja = servicioCaja.GetAllCajaPorIdCierre(0);
                    //-- p/ registro importe cierre por grupo
                    List<CajaGrupoModel> cajaGrupoModel = new List<CajaGrupoModel>();
                    
                    //---- instancia para registrar los asientos manuales 
                    CajaModel asientoCajaModel = new CajaModel();

                    /// p/ registro de cierre Cajasaldo 
                    CajaSaldoModel ultimoCierreCajaSaldoModel = new CajaSaldoModel();
                    ultimoCierreCajaSaldoModel = servicioCajaSaldo.GetUltimoCierre();

                    CajaSaldoModel cajaSaldoModel = new CajaSaldoModel();
                    cajaSaldoModel.Fecha = model.FechaCierre;
                    cajaSaldoModel.NumeroCierrre = servicioCajaSaldo.GetNuevoNumeroCierre();
                    cajaSaldoModel.ImporteInicialCheques = ultimoCierreCajaSaldoModel.ImporteFinalCheques;
                    cajaSaldoModel.ImporteInicialDepositos = ultimoCierreCajaSaldoModel.ImporteFinalDepositos;
                    cajaSaldoModel.ImporteInicialDolares = ultimoCierreCajaSaldoModel.ImporteFinalDolares;
                    cajaSaldoModel.ImporteInicialPesos = ultimoCierreCajaSaldoModel.ImporteFinalPesos;
                    cajaSaldoModel.ImporteInicialTarjetas = ultimoCierreCajaSaldoModel.ImporteFinalTarjetas;
                    cajaSaldoModel.ImporteFinalCheques = ultimoCierreCajaSaldoModel.ImporteFinalCheques;
                    cajaSaldoModel.ImporteFinalDepositos = ultimoCierreCajaSaldoModel.ImporteFinalDepositos;
                    cajaSaldoModel.ImporteFinalDolares = ultimoCierreCajaSaldoModel.ImporteFinalDolares;
                    cajaSaldoModel.ImporteFinalPesos = ultimoCierreCajaSaldoModel.ImporteFinalPesos;
                    cajaSaldoModel.ImporteFinalTarjetas = ultimoCierreCajaSaldoModel.ImporteFinalTarjetas;
                    cajaSaldoModel.Activo = true;
                    cajaSaldoModel.IdUsuario = usuario.IdUsuario;

                    // Registro Nuevo Caja Saldo 
                    CajaSaldoModel nuevoCierreCajaSaldoModel = servicioCajaSaldo.GuardarCajaSaldo(cajaSaldoModel);
                  

                    // recorrer CajaActual para poder obtener los nuevos importes

                    foreach (var item in caja)
                    {
                        // acumula los totales por grupo
                        CajaGrupoModel grupo = cajaGrupoModel.FirstOrDefault(i => i.Id == item.IdGrupoCaja);
                        if (grupo == null)
                        {   
                            var g = new CajaGrupoModel { Id = item.IdGrupoCaja ?? 0,                                                        
                                                        TotalPesos = item.ImportePesos,
                                                        TotalDolares = item.ImporteDolar,
                                                        TotalCheques = item.ImporteCheque,
                                                        TotalDepositos=item.ImporteDeposito,
                                                        TotalTarjetas = item.ImporteTarjeta,
                                                        ParcialPesos = item.ImportePesos,
                                                        ParcialDolares = item.ImporteDolar,
                                                        ParcialCheques = item.ImporteCheque,
                                                        ParcialDepositos = item.ImporteDeposito,
                                                        ParcialTarjetas = item.ImporteTarjeta
                                                        };
                            cajaGrupoModel.Add(g);
                        }
                        else
                        {
                            grupo.TotalPesos += item.ImportePesos;
                            grupo.TotalDolares += item.ImporteDolar;
                            grupo.TotalCheques += item.ImporteCheque;
                            grupo.TotalDepositos += item.ImporteDeposito;
                            grupo.TotalTarjetas += item.ImporteTarjeta;
                            grupo.ParcialPesos += item.ImportePesos;
                            grupo.ParcialDolares += item.ImporteDolar;
                            grupo.ParcialCheques += item.ImporteCheque;
                            grupo.ParcialDepositos += item.ImporteDeposito;
                            grupo.ParcialTarjetas += item.ImporteTarjeta;                           
                            cajaGrupoModel.Remove(cajaGrupoModel.FirstOrDefault(i => i.Id == item.IdGrupoCaja));
                            cajaGrupoModel.Add(grupo);                           
                        }

                        //-----acumulo los importes para Asiento de ingresos manuales
                        if (item.IdTipoMovimiento != 1)
                        {
                            asientoCajaModel.ImporteCheque = asientoCajaModel.ImporteCheque ?? 0 + item.ImporteCheque;
                            asientoCajaModel.ImporteDeposito = asientoCajaModel.ImporteDeposito ?? 0 +item.ImporteDeposito;
                            asientoCajaModel.ImporteDolar = asientoCajaModel.ImporteDolar ?? 0 + item.ImporteDolar;
                            asientoCajaModel.ImportePesos = asientoCajaModel.ImportePesos ?? 0 + item.ImportePesos;
                            asientoCajaModel.ImporteTarjeta = asientoCajaModel.ImporteTarjeta ?? 0 + item.ImporteTarjeta;                            
                        }
                        /// acumulo importes finales
                        nuevoCierreCajaSaldoModel.ImporteFinalCheques += item.ImporteCheque ?? 0;
                        nuevoCierreCajaSaldoModel.ImporteFinalDepositos += item.ImporteDeposito ?? 0;
                        nuevoCierreCajaSaldoModel.ImporteFinalDolares += item.ImporteDolar ?? 0;
                        nuevoCierreCajaSaldoModel.ImporteFinalPesos += item.ImportePesos ?? 0;
                        nuevoCierreCajaSaldoModel.ImporteFinalTarjetas += item.ImporteTarjeta ?? 0;

                        // actualizamos el registro de caja con el id del Cierre
                        item.IdCajaSaldo = nuevoCierreCajaSaldoModel.Id;
                        servicioCaja.ActualizarCierreCaja(item);
                    }
                    /// actualizo con los importe finales el cierre 
                    servicioCajaSaldo.ActualizarImporteCierreCajaSaldo(nuevoCierreCajaSaldoModel);


                    var cotizacionDolar = servicioTipoMoneda.GetCotizacionPorIdMoneda(2);
                    


                    /// REGISTRO DE ASIENTOS CONTABLES
                    var codigo = servicioContable.GetNuevoCodigoAsiento() + 1;
                    DiarioModel asiento = new DiarioModel();
                    asiento.Codigo = codigo;
                   // asiento.Fecha = DateTime.Now;
                   // asiento.Periodo = DateTime.Now.ToString("yyMM");
                    asiento.Fecha = model.FechaCierre;
                    asiento.Periodo = model.FechaCierre.ToString("yyMM");
                    asiento.Tipo = "CA";
                    asiento.Cotiza = cotizacionDolar.Monto;// 0; /// obtener de Afip
                    asiento.Asiento = codigo;
                    asiento.Balance = int.Parse(model.FechaCierre.ToString("yyyy")); //int.Parse(DateTime.Now.ToString("yyyy"));
                    asiento.Moneda = "";
                    asiento.DescripcionMa = "Cierre de Caja";
                 
                    if (asientoCajaModel.ImportePesos > 0)
                    {
                        CompraFacturaModel compraFacturaModel = new CompraFacturaModel();
                        compraFacturaModel.Cotizacion = asiento.Cotiza;
                        compraFacturaModel.IdMoneda = 1;
                        asiento.Moneda = "Pesos";
                        var asientoGanan = servicioContable.InsertAsientoContable("PESOS", (asientoCajaModel.ImportePesos ?? 0), asiento, compraFacturaModel, 0);
                        if (asientoGanan != null) { servicioImputacion.AsintoContableGeneral(asientoGanan); }
                    }

                    if (asientoCajaModel.ImporteCheque > 0)
                    {
                        CompraFacturaModel compraFacturaModel = new CompraFacturaModel();
                        compraFacturaModel.Cotizacion = asiento.Cotiza;
                        compraFacturaModel.IdMoneda = 1; //?
                        asiento.Moneda = "Pesos"; //?
                        var asientoGanan = servicioContable.InsertAsientoContable("CHTER", (asientoCajaModel.ImportePesos ?? 0), asiento, compraFacturaModel, 0);
                        if (asientoGanan != null) { servicioImputacion.AsintoContableGeneral(asientoGanan); }
                    }

                    if (asientoCajaModel.ImporteTarjeta > 0)
                    {
                        CompraFacturaModel compraFacturaModel = new CompraFacturaModel();
                        compraFacturaModel.Cotizacion = asiento.Cotiza;
                        compraFacturaModel.IdMoneda = 1;
                        asiento.Moneda = "Pesos";
                        var asientoGanan = servicioContable.InsertAsientoContable("PESOS", (asientoCajaModel.ImporteTarjeta ?? 0), asiento, compraFacturaModel, 0);
                        if (asientoGanan != null) { servicioImputacion.AsintoContableGeneral(asientoGanan); }
                    }

                    if (asientoCajaModel.ImporteDolar > 0)
                    {
                        CompraFacturaModel compraFacturaModel = new CompraFacturaModel();
                        compraFacturaModel.Cotizacion = asiento.Cotiza;
                        compraFacturaModel.IdMoneda = 2;
                        asiento.Moneda = "Dolar";
                        var asientoGanan = servicioContable.InsertAsientoContable("DOLAR", (asientoCajaModel.ImporteDolar ?? 0), asiento, compraFacturaModel, 0);
                        if (asientoGanan != null) { servicioImputacion.AsintoContableGeneral(asientoGanan); }
                    }
                 
       
                    return RedirectToAction("Index");


                }
                catch (Exception)
                {
                    return View("Index");
                }
            }


        }



        public ActionResult AddOrEdit(int id = 0)
        {

            //  CargarCajaGrupo();

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
                    //servicioCaja._mensaje("","ok");
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
        //public void CargarCajaGrupo()
        //{
        //    List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
        //    List<SelectListItem> retornoListaCajaGrupo = null;
        //    retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
        //    {
        //        Value = x.Id.ToString(),
        //        Text = x.Codigo
        //    })).ToList();
        //    retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "--Seleccione Codigo de Caja--", Value = "" });
        //    ViewBag.Listapagina = retornoListaCajaGrupo;
        //}




        public void CargarCierreCaja()
        {


            List<CajaSaldoModelView> ListaCajaGrupo = Mapper.Map<List<CajaSaldoModel>, List<CajaSaldoModelView>>(servicioCajaSaldo.GetAllCajaSaldo());
            List<SelectListItem> retornoListaCajaCierre = null;

            retornoListaCajaCierre = (ListaCajaGrupo.OrderByDescending( x => x.NumeroCierrre ).Select(x => new SelectListItem()
                                                                {
                                                                    Value = x.Id.ToString(),
                                                                    Text = "Nro: " + x.NumeroCierrre.ToString() + "- Fecha: " + x.Fecha.ToString("dd/MM/yyyy")
                                                                })).ToList();

            retornoListaCajaCierre.Insert(0, new SelectListItem { Text = "-- Cierre Actual --", Value = "0" });
            ViewBag.Listapagina = retornoListaCajaCierre;

        }



    }




}



