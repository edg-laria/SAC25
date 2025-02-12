using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;
using System.Data.Entity.Validation;

namespace Negocio.Servicios
{
    public class ServicioFacturaVenta : ServicioBase
    {
        private FacturaVentasRepositorio oFacturaVentasRepositorio;

        private ServicioTipoComprobanteVenta oServicioTipoComprobanteVenta = new ServicioTipoComprobanteVenta();
        private ServicioTipoMoneda oServicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioChequera oServicioChequera = new ServicioChequera();
        private ServicioCheque oServicioCheque = new ServicioCheque();
        private ServicioCobroFacturaModo servicioCobroFacturaModo = new ServicioCobroFacturaModo();
        private ServicioBancoCuentaBancaria oServicioBancoCuentaBancaria = new ServicioBancoCuentaBancaria();
        private ServicioCliente oServicioCliente = new ServicioCliente();
        private ServicioPresupuestoActual oServicioPresupuestoActual = new ServicioPresupuestoActual();
        private ServicioPresupuestoCosto oServicioPresupuestoCosto = new ServicioPresupuestoCosto();
        private ServicioPresupuestoItem oServicioPresupuestoItem = new ServicioPresupuestoItem();
        private ServicioCaja oServicioCaja = new ServicioCaja();
        private ServicioTarjetaOperacion oServicioTarjetaOperacion = new ServicioTarjetaOperacion();
        private ServicioImputacion oServicioImputacion = new ServicioImputacion();
        private ServicioContable oServicioContable = new ServicioContable();
        private ServicioTrackingFacturaCobro ServicioTrackingFacturaCobro = new ServicioTrackingFacturaCobro();
        private ServicioBancoCuenta oServicioBancoCuenta = new ServicioBancoCuenta();
        private ServicioCajaGrupo servicioCajaGrupo = new ServicioCajaGrupo();
        private ServicioRetencion servicioRetencion = new ServicioRetencion();


        public ServicioFacturaVenta()
        {
            oFacturaVentasRepositorio = kernel.Get<FacturaVentasRepositorio>();
        }


        public List<FacturaVentaModel> GetAllFacturaVenta()
        {
            List<FacturaVentaModel> listaFacturaVenta = Mapper.Map<List<FactVenta>, List<FacturaVentaModel>>(oFacturaVentasRepositorio.GetAllFacturaVenta());
            return listaFacturaVenta;
        }

        public List<FacturaVentaModel> GetAllFacturaVentaCliente(int idCliente)
        {
            List<FacturaVentaModel> listaFacturaVenta = Mapper.Map<List<FactVenta>, List<FacturaVentaModel>>(oFacturaVentasRepositorio.GetAllFacturaVentaCliente(idCliente));
            return listaFacturaVenta;
        }
        public List<FacturaVentaModel> GetAllFacturaVentaPorNumero(int nroBuscado, int idCliente)
        {
            List<FacturaVentaModel> listaFacturaVenta = Mapper.Map<List<FactVenta>, List<FacturaVentaModel>>(oFacturaVentasRepositorio.GetAllFacturaVentaPorNumero(nroBuscado, idCliente));
            return listaFacturaVenta;
        }

       public FacturaVentaModel GetFacturaVentaPorNumero(int nroBuscado, int idCliente)
        {
            FacturaVentaModel FacturaVenta = Mapper.Map<FactVenta, FacturaVentaModel>(oFacturaVentasRepositorio.GetFacturaVentaPorNumero(nroBuscado, idCliente));
            return FacturaVenta;
        }

        public FacturaVentaModel Agregar(FacturaVentaModel oFacturaVentaModel)
        {
            try
            {
                var oModel = Mapper.Map<FacturaVentaModel, FactVenta>(oFacturaVentaModel);
                return Mapper.Map<FactVenta, FacturaVentaModel>(oFacturaVentasRepositorio.Agregar(oModel));
            }
           // catch (Exception ex)
            //{
            ///    _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
            //    return null;
           /// }

          catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        return null;
                    }
            
        }

        public List<CobroFacturaModel> GetClienteCtaCteCbte(int idCliente)
        {
            try
            {
              /*
                var list = Mapper.Map<List<FactVenta>, List<CobroFacturaModel>>(oFacturaVentasRepositorio.GetClienteCtaCteCbte(idCliente));
                return list;
              */

                var list = oFacturaVentasRepositorio.GetClienteCtaCteCbte(idCliente);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FactVenta, CobroFacturaModel>()
                     .ForMember(dest => dest.FacturaElectronica, opt => opt.Ignore())
                    .ForMember(dest => dest.Retencion, opt => opt.Ignore());
                   

                    cfg.CreateMap<FacturaElectronica, FacturaElectronicaModel>(); // Suponiendo que tienes estas clases
                   
                });

                var mapper = config.CreateMapper();

                var result = mapper.Map<List<FactVenta>, List<CobroFacturaModel>>(list);
                
                return result;
                
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador " + ex.Message, "error");
                return null;
            }
        }

        private object List(List<FactVenta> factVentas)
        {
            throw new NotImplementedException();
        }

        public List<CobroFacturaModel> GetClienteCtaCteCbte(int idCliente, int? idTipoMoneda)
        {
            try
            {
                return Mapper.Map<List<FactVenta>, List<CobroFacturaModel>>(oFacturaVentasRepositorio.GetCompraFacturaPorIdProveedor_Moneda(idCliente, idTipoMoneda));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador " + ex.Message, "error");
                return null;
            }
        }

        public CobroFacturaModel ObtenerFacturaPorID(int idFactura)
        {
            try
            {
                return Mapper.Map<FactVenta, CobroFacturaModel>(oFacturaVentasRepositorio.GetFacturaPorId(idFactura));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador ", "error");
                return null;
            }
        }

        public List<CobroFacturaModel> ObtenerPorIdCliente_Moneda(int idCliente, int? idTipoMoneda)
        {
            try
            {
                return Mapper.Map<List<FactVenta>, List<CobroFacturaModel>>(oFacturaVentasRepositorio.GetCompraFacturaPorIdProveedor_Moneda(idCliente, idTipoMoneda));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador " +ex.Message, "error");
                return null;
            }
        }

        public void RegistroDeCobro(List<CobroFacturaModel> cobroFacturaModel, CobroFacturaModoModel medioCobro, int IdTipoComprobanteVenta)
        {
            try
            {
                var tipoComprobante = oServicioTipoComprobanteVenta.GetNuevoNumeroCobro(IdTipoComprobanteVenta);
                var cliente = oServicioCliente.GetClientePorId(cobroFacturaModel[0].IdCliente ?? 0);
                decimal saldoPagoTotalAFavor = cobroFacturaModel.Where(x => x.IdTipoComprobante.Equals(IdTipoComprobanteVenta)).Sum(x => x.Saldo);

                decimal saldoPagoTotal = saldoPagoTotalAFavor;


                decimal[] aDifCot = new decimal[3];
                aDifCot[0] = 0;

                CobroFacturaModel cbtCobroFactura = (from cbtCobro in cobroFacturaModel
                                            where cbtCobro.IdTipoComprobante == IdTipoComprobanteVenta                                                         
                                            select cbtCobro).FirstOrDefault();
             

                var CodigoAsiento = oServicioContable.GetNuevoCodigoAsiento() + 1;
                DiarioModel asiento = new DiarioModel();
                asiento.Codigo = CodigoAsiento;
                asiento.Fecha = cbtCobroFactura.Fecha;
                asiento.Periodo = cbtCobroFactura.Fecha.ToString("yyMM");
                asiento.Tipo = "VP";                    
                asiento.Balance = int.Parse(cbtCobroFactura.Fecha.ToString("yyyy"));
                asiento.Moneda = oServicioTipoMoneda.GetTipoMoneda(cbtCobroFactura.IdMoneda).Descripcion;
        

                foreach (var Factura in cobroFacturaModel)
                {
                    CobroFacturaModoModel cobroFacturaModo = new CobroFacturaModoModel();
                    // set importes para grabar en caja
                    decimal ValorFactura = medioCobro.montoTotal;
                    decimal ImporteCheque = medioCobro.montoChequesSeleccionados;
                    decimal ImporteTarjeta = medioCobro.montoTarjeta;
                    decimal ImporteTranferencia = medioCobro.montoCuentaBancaria;
                    //instancio una nueva factura para obtener los valores de retorno de la capa de datos
                    CobroFacturaModel newFactura = new CobroFacturaModel();

                    //--------- Registro de Facturas a Cobrar -----------
                    if (Factura.IdTipoComprobante != IdTipoComprobanteVenta)
                    {
                        //// -------------- registro de asientos                              
                        asiento.Importe = (Factura.IdMoneda == 1) ? -Factura.Saldo : -(Factura.Saldo * Factura.Cotiza);
                        asiento.Cotiza = Factura.Cotiza;
                        asiento.Descripcion = "Deudores por Ventas";
                        asiento.DescripcionMa = "Cobro Fact Nº " + Factura.NumeroFactura + ", Cliente:" + cliente.Nombre;
                        asiento.Titulo = "Cobro de Factura";

                        if (Factura.TipoIva == "4") // exterior
                        {
                            var asientoVEXT = oServicioContable.InsertAsientoContable("VEXT", asiento, 0);
                            if (asientoVEXT != null) { oServicioImputacion.AsintoContableGeneral(asientoVEXT); }
                        }
                        else //local
                        {
                            var asientoVEXT = oServicioContable.InsertAsientoContable("VLOC", asiento, 0);
                            if (asientoVEXT != null) { oServicioImputacion.AsintoContableGeneral(asientoVEXT); }
                        }

                        if (Factura.IdMoneda != 1 && cbtCobroFactura.Cotiza != Factura.Cotiza)
                        {                         
                      
                            aDifCot[1] = Factura.Cotiza;
                            aDifCot[0] += Factura.Saldo * Factura.Cotiza;
                        }

                        Factura.FechaCobro = cbtCobroFactura.Fecha;
                        Factura.NumeroCobro = tipoComprobante.Numero; //si en fact hay un cbt de cobro 
                        Factura.Vencimiento = cbtCobroFactura.Fecha;
                        Factura.CotizaP = cbtCobroFactura.Cotiza;
                        //esto se hace para saber si el pago es mayos a todas las facturas pagadas
                        saldoPagoTotal = saldoPagoTotal - Factura.Saldo;

                        //if (medioCobro.montoTotal >= Factura.Total)
                        //{
                        //    Factura.Saldo = 0;
                        //    Factura.Parcial = 0;
                        //}
                        //else
                        //{
                        //   // Factura.Parcial += medioCobro.montoTotal ;                             
                        //   // var nuevoSaldo = Factura.Total - Factura.Parcial;
                        //   // Factura.Saldo = nuevoSaldo;
                        //    Factura.Parcial = Factura.Saldo == 0 ? 0 : Factura.Parcial;
                        //}    


                        Factura.Saldo = Factura.saldoCobro;
                        if (Factura.Saldo == 0)
                        {
                            Factura.Parcial = 0;
                        }
                        else { Factura.Parcial += Factura.aplicacion; }
                        
                        //enviamos los datos a modificar y recuperamos la entidad actualizada
                        newFactura = ActualizaVentaFacturaCobro(Factura);
                       // seteo el tipo de moneda para grabar en caja
                       // idMoneda = Factura.IdMoneda;
                       // asiento de Retencion sobre la factura 
                   
                    }
                    else
                    {
                        // 1 cbte de pago con saldo a favor utilizado para pagar la factura 
                        if (Factura.NumeroFactura > 0 && Factura.IdTipoComprobante == IdTipoComprobanteVenta)
                        {
                            Factura.FechaCobro = cbtCobroFactura.Fecha;
                            Factura.NumeroCobro = tipoComprobante.Numero;
                            saldoPagoTotal -= Factura.Saldo;
                            if (medioCobro.montoTotal >= Factura.Saldo)
                            {
                                Factura.Saldo = 0;
                                Factura.Parcial = 0;
                            }
                            else
                            {
                                Factura.Parcial = medioCobro.montoTotal;
                                var nuevoSaldo = Factura.Saldo - Factura.Parcial;
                                Factura.Saldo = nuevoSaldo;
                            }
                            //enviamos los datos a modificar y recupetamos la entidad actualizada
                            newFactura = ActualizaVentaFacturaCobro(Factura);

                            // seteo el tipo de moneda para grabar en caja
                            //idMoneda = Factura.IdMoneda;
                        }
                        else
                        {
                           
                            // crea el comprobante de pago de las facturas seleccionadas
                            Factura.NumeroFactura = tipoComprobante.Numero;
                            Factura.Codigo = cliente.Codigo;
                            Factura.Tipo = "P";
                            Factura.Baja = "*";
                            Factura.Impre = "verdadero";
                            Factura.TipoIva = "0";
                            Factura.Condicion = "0";
                            Factura.Descuento = "0";
                            Factura.NumeroTra = "0";
                            Factura.Anula = "0";
                            Factura.NotaBanco = " ";
                            Factura.IdTipoComprobante = IdTipoComprobanteVenta;                                                    
                            if (Factura.IdMoneda == 2)
                            {
                                Factura.TotalDolares = Factura.Total;
                            }                           
                            Factura.Fecha = cbtCobroFactura.Fecha;
                            Factura.FechaCobro = cbtCobroFactura.Fecha;                                                     
                            Factura.NumeroCobro = tipoComprobante.Numero;
                            Factura.Activo = true;
                            Factura.IdUsuario = medioCobro.IdUsuario;
                            Factura.UltimaModificacion = DateTime.Now;
                            Factura.Vencimiento = cbtCobroFactura.Fecha;
                            Factura.Periodo = int.Parse(cbtCobroFactura.Fecha.ToString("yyMM"));                                                      
                            Factura.CodigoDiario = CodigoAsiento;
                            Factura.Saldo = Factura.saldoCobro;
                            if (Factura.Saldo == 0)
                            {
                                Factura.Parcial = 0;
                            }else {
                                Factura.Parcial = Factura.aplicacion;
                            }
                            Factura.Total = Factura.cobro;

  //Factura.Total = - medioCobro.montoTotal;
 //Factura.Saldo = saldoPagoTotal;
                           // Factura.Saldo = Factura.Total - Factura.Parcial;

                            //----------- Registro la "cbt cobro" --------------
                            var nuevoPagoRegistrado = GuardarCobro(Factura);

                            SaveTrackingComprasList(cobroFacturaModel, nuevoPagoRegistrado);

                            if (nuevoPagoRegistrado != null)
                            {
                                ///------------Registro Caja----------
                                CajaModel oCajaModel = new CajaModel();
                                oCajaModel.IdTipoMovimiento = 1;
                                oCajaModel.Concepto = Factura.Concepto + ", Nro. Cobro: " + Factura.NumeroFactura;
                                oCajaModel.Fecha = nuevoPagoRegistrado.Fecha;
                                oCajaModel.Tipo = "1";
                                oCajaModel.Saldo = "";
                                oCajaModel.Recibo = nuevoPagoRegistrado.Recibo.ToString();
                                if (nuevoPagoRegistrado.IdMoneda == 1)
                                        { oCajaModel.ImportePesos = -ValorFactura; }
                                else 
                                        { oCajaModel.ImporteDolar = -ValorFactura; }
                                oCajaModel.ImporteCheque = -ImporteCheque;
                                oCajaModel.ImporteDeposito = -ImporteTranferencia;
                                oCajaModel.ImporteTarjeta = -ImporteTarjeta;
                                if (cobroFacturaModo.IdBancoCuenta != 0)
                                     { oCajaModel.IdCuentaBanco = cobroFacturaModo.IdBancoCuenta; }
                                else 
                                     { oCajaModel.IdCuentaBanco = null; }

                                CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("COBRO");
                                if (cajaGrupoModel != null)
                                {
                                    oCajaModel.IdGrupoCaja = cajaGrupoModel.Id;
                                }
                                CajaModel inCajaModel = oServicioCaja.IngresoPagoCaja(oCajaModel);

                                //// -------------- registro de asientos                              
                                asiento.Importe = (nuevoPagoRegistrado.IdMoneda == 1) ? nuevoPagoRegistrado.Total :  (nuevoPagoRegistrado.Total * nuevoPagoRegistrado.Cotiza);                               
                                asiento.Descripcion = "Cobro Nº " + nuevoPagoRegistrado.NumeroCobro;
                                asiento.DescripcionMa = "Cobro Nº" + nuevoPagoRegistrado.NumeroCobro + ", Cliente:" + cliente.Nombre;
                                asiento.Titulo = "Cobro de Factura";


                                /// asiento Inputacion en valor - negativo?
                                //var asientoDiario = oServicioContable.InsertAsientoContable(null, asiento, cliente.IdImputacion);
                                ///// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
                                //oServicioImputacion.AsintoContableGeneral(asientoDiario); //----------------Consultar a Edgardo

                                if (aDifCot[0] != 0)
                                {
                                    //// -------------- registro de asientos por diferencia    
                                   
                                    asiento.Importe = aDifCot[0] - nuevoPagoRegistrado.Total * nuevoPagoRegistrado.Cotiza;
                                    asiento.Cotiza = aDifCot[1];
                                    asiento.Descripcion = "Diferencia de Cotizacion";
                                    asiento.DescripcionMa = "Diferencia de Cotizacion Vta " + aDifCot[1] + ", Cobro " + nuevoPagoRegistrado.Cotiza;
                                    asiento.Titulo = "Cobro de Factura";                                    
                                    var asientoVEXT = oServicioContable.InsertAsientoContable("DIFER", asiento, 0);
                                    if (asientoVEXT != null) { oServicioImputacion.AsintoContableGeneral(asientoVEXT); }
                                    
                                }

                                //-------------aca insertamos los medios de pago

                                // pago con transferencia
                                if (medioCobro.IdBancoCuenta > 0)
                                {
                                    cobroFacturaModo.IdFactVenta = nuevoPagoRegistrado.Id;
                                    cobroFacturaModo.IdTipoPago = 4;
                                    cobroFacturaModo.IdBancoCuenta = medioCobro.IdBancoCuenta;
                                    cobroFacturaModo.Monto = medioCobro.montoCuentaBancaria;
                                    cobroFacturaModo.Observaciones = "Tranferecia Bancaria";
                                    cobroFacturaModo.Activo = true;
                                    cobroFacturaModo.IdUsuario = medioCobro.IdUsuario;
                                    cobroFacturaModo.UltimaModificacion = DateTime.Now;

                                    servicioCobroFacturaModo.InsertarCobroModo(cobroFacturaModo);

                                    //-------Registrar movimiento Cuenta Bancaria 

                                    var bancoCuenta = oServicioBancoCuenta.GetCuentaPorId(medioCobro.IdBancoCuenta ?? 0);
                                    //--------------------------
                                    //registrar movimiento cuenta bancaria 
                                    BancoCuentaBancariaModel oBancoCuentaBancariaModel = new BancoCuentaBancariaModel();
                                    oBancoCuentaBancariaModel.NumeroOperacion = nuevoPagoRegistrado.NumeroCobro;
                                    oBancoCuentaBancariaModel.IdBancoCuenta = bancoCuenta.Id;
                                    oBancoCuentaBancariaModel.CuentaDescripcion =  "Cliente - " + cliente.Nombre;
                                    oBancoCuentaBancariaModel.Fecha = nuevoPagoRegistrado.Fecha;
                                    oBancoCuentaBancariaModel.FechaEfectiva = nuevoPagoRegistrado.Fecha;
                                    oBancoCuentaBancariaModel.DiaClearing = "";
                                    oBancoCuentaBancariaModel.Importe = decimal.Parse(medioCobro.montoCuentaBancaria.ToString());
                                    oBancoCuentaBancariaModel.IdCliente = Factura.IdCliente.ToString();
                                    oBancoCuentaBancariaModel.Conciliacion = true;
                                    oBancoCuentaBancariaModel.FechaIngreso = nuevoPagoRegistrado.Fecha;
                                    oBancoCuentaBancariaModel.IdImputacion = "0";
                                    oBancoCuentaBancariaModel.IdGrupoCaja = cajaGrupoModel.Id; //"cobro";                                       
                                    var pagoBancoCuentaBancaria = oServicioBancoCuentaBancaria.Agregar(oBancoCuentaBancariaModel);


                                    // ------------- Inicio registro de asientos Por Transferencia Bancaria ----------dev-a                                   
                                    if (pagoBancoCuentaBancaria.Id > 0)
                                    {
                                        asiento.Descripcion = "Cobro en Cta. Cte." + bancoCuenta.CNombre;
                                        asiento.Importe = - pagoBancoCuentaBancaria.Importe;
                                        var asientoBanco = oServicioContable.InsertAsientoContable(bancoCuenta.Codigo, asiento, 0);
                                        if (asientoBanco != null) { oServicioImputacion.AsintoContableGeneral(asientoBanco); }
                                    }
                                }
                                   

                                if (medioCobro.montoEfectivo != 0)
                                {
                                    cobroFacturaModo.IdFactVenta = nuevoPagoRegistrado.Id;
                                    cobroFacturaModo.IdTipoPago = 2;
                                    cobroFacturaModo.Monto = medioCobro.montoEfectivo;
                                    cobroFacturaModo.Observaciones = "Efectivo";
                                    cobroFacturaModo.Activo = true;
                                    cobroFacturaModo.IdUsuario = medioCobro.IdUsuario;
                                    cobroFacturaModo.UltimaModificacion = DateTime.Now;
                                    var pagoEfectivo = servicioCobroFacturaModo.InsertarCobroModo(cobroFacturaModo);

                                    //------------ asiento pago efectivo -------------
                                    if (pagoEfectivo.Id > 0)
                                    {
                                        decimal importeEfectivo = pagoEfectivo.Monto ?? 0;
                                        var alias = (nuevoPagoRegistrado.IdMoneda == 1) ? ("PESOS") : ("DOLAR");
                                        asiento.Importe = (nuevoPagoRegistrado.IdMoneda == 1) ? (importeEfectivo) : (importeEfectivo * nuevoPagoRegistrado.Cotiza);
                                        asiento.Descripcion = "Cobro en Efectivo (" + alias + ")";
                                        asiento.DescripcionMa = "Cobro en Efectivo (" + alias + ")";
                                        var asientoCaja = oServicioContable.InsertAsientoContable(alias, asiento , 0);
                                        if (asientoCaja != null) { oServicioImputacion.AsintoContableGeneral(asientoCaja); }
                                    }
                                }


                                if (medioCobro.montoComision != 0)
                                {
                                    cobroFacturaModo.IdFactVenta = nuevoPagoRegistrado.Id;
                                    cobroFacturaModo.IdTipoPago = 2;
                                    cobroFacturaModo.Monto = medioCobro.montoComision;
                                    cobroFacturaModo.Observaciones = "Comisión";
                                    cobroFacturaModo.Activo = true;
                                    cobroFacturaModo.IdUsuario = medioCobro.IdUsuario;
                                    cobroFacturaModo.UltimaModificacion = DateTime.Now;
                                    var pagoComision = servicioCobroFacturaModo.InsertarCobroModo(cobroFacturaModo);

                                    //------------ asiento pago efectivo -------------
                                    if (pagoComision.Id > 0)
                                    {
                                        decimal importeComision = pagoComision.Monto ?? 0;
                                        var alias = "PCOMI";
                                        asiento.Importe = (nuevoPagoRegistrado.IdMoneda == 1) ? (importeComision) : (importeComision * nuevoPagoRegistrado.Cotiza);
                                        asiento.Descripcion = " Comision sobre Cobros al exterior";
                                        asiento.DescripcionMa = "Comision sobre Cobros al exterior";
                                        var asientoCaja = oServicioContable.InsertAsientoContable(alias, asiento, 0);
                                        if (asientoCaja != null) { oServicioImputacion.AsintoContableGeneral(asientoCaja); }
                                    }
                                }

                                // Pago con tarjeta
                                if (medioCobro.IdTarjeta != 0)
                                {
                                    cobroFacturaModo.IdFactVenta = nuevoPagoRegistrado.Id;
                                    cobroFacturaModo.IdTipoPago = 5;
                                    cobroFacturaModo.IdTarjeta = medioCobro.IdTarjeta;
                                    cobroFacturaModo.Monto = medioCobro.montoTarjeta;
                                    cobroFacturaModo.Observaciones = "Tarjeta";
                                    cobroFacturaModo.Activo = true;
                                    cobroFacturaModo.IdUsuario = medioCobro.IdUsuario;
                                    cobroFacturaModo.UltimaModificacion = DateTime.Now;
                                    var pagoTarjeta = servicioCobroFacturaModo.InsertarCobroModo(cobroFacturaModo);

                                    //obtengo idGrupo caja de presupuesto
                                    PresupuestoActualModel oPresupuestoActualModel = new PresupuestoActualModel();
                                    oPresupuestoActualModel = oServicioPresupuestoActual.GetAllPresupuestos(medioCobro.IdPresupuesto);
                                    int idGrupoCaja = oPresupuestoActualModel.IdGrupoCaja;
                                    
                                    //registra tarjeta
                                    TarjetaOperacionModel oTarjetaOperacionModel = new TarjetaOperacionModel();
                                    oTarjetaOperacionModel.IdTarjeta = medioCobro.IdTarjeta ?? 0;
                                    oTarjetaOperacionModel.Descripcion = "Pago Factura: " + nuevoPagoRegistrado.NumeroFactura;
                                    oTarjetaOperacionModel.IdGrupoCaja = idGrupoCaja;
                                    oTarjetaOperacionModel.Conciliacion = false;
                                    oTarjetaOperacionModel.NumeroPago = nuevoPagoRegistrado.NumeroCobro.ToString();
                                    oTarjetaOperacionModel.Activo = true;
                                    oTarjetaOperacionModel.IdUsuario = medioCobro.IdUsuario;
                                    oTarjetaOperacionModel.UltimaModificacion = DateTime.Now;
                                    oServicioTarjetaOperacion.Insertar(oTarjetaOperacionModel);

                                    if (pagoTarjeta.Monto > 0)
                                    {
                                        
                                        asiento.Importe = pagoTarjeta.Monto ?? 0;
                                        asiento.Descripcion = "Cliente Pago con Tarjeta ";
                                        var asientoBanco = oServicioContable.InsertAsientoContable("TARJE", asiento, 0);
                                        if (asientoBanco != null) { oServicioImputacion.AsintoContableGeneral(asientoBanco); }
                                    }

                                }
                                                          
                                //Pago con Cheque
                                decimal importePagoEnCheque = 0;                                
                                if ((medioCobro.idChequesCliente != null))
                                {
                                    ChequeModel oCheque = new ChequeModel();
                                    string[] chequesSeleccionados = medioCobro.idChequesCliente.Split(';');
                                    foreach (var itemCheque in chequesSeleccionados)
                                    {
                                        oCheque = oServicioCheque.obtenerCheque(int.Parse(itemCheque));
                                        if (oCheque != null)
                                        {
                                            importePagoEnCheque += oCheque.Importe;
                                            cobroFacturaModo.IdFactVenta = nuevoPagoRegistrado.Id;
                                            cobroFacturaModo.IdTipoPago = 3;
                                            cobroFacturaModo.IdCheque = oCheque.Id;                                            
                                            cobroFacturaModo.Monto = decimal.Parse(oCheque.Importe.ToString());
                                            cobroFacturaModo.Observaciones = "Cobro en Cheque Nro.: " + oCheque.NumeroCheque;
                                            cobroFacturaModo.Activo = true;
                                            cobroFacturaModo.IdUsuario = medioCobro.IdUsuario;
                                            cobroFacturaModo.UltimaModificacion = DateTime.Now;                                        
                                            servicioCobroFacturaModo.InsertarCobroModo(cobroFacturaModo);
                                            // ------------- Inicio registro de asientos cheque---------                                  
                                            if (oCheque.Importe > 0)
                                            {
                                                var alias = "CHTER";
                                                asiento.Importe = (nuevoPagoRegistrado.IdMoneda == 1) ? (importePagoEnCheque) : (importePagoEnCheque * nuevoPagoRegistrado.Cotiza);
                                                asiento.Descripcion = "Cobro en Cheque Nro.: " + oCheque.NumeroCheque;
                                                var asientoBanco = oServicioContable.InsertAsientoContable(alias, asiento, 0);
                                                if (asientoBanco != null) { oServicioImputacion.AsintoContableGeneral(asientoBanco); }
                                            }


                                        }
                                        //else
                                        //{
                                        //    // no se encuentra el cheque
                                        //}                                                                          
                                    }
                                }

                                //Asiento de Retenciones                                 
                                if ((medioCobro.idRetencionesCliente != null))
                                {
                                    RetencionModel retencionModel = new RetencionModel();
                                    string[] idRetenciones = medioCobro.idRetencionesCliente.Split(';');
                                    decimal importeRetencion = 0;
                                    foreach (var idRetencion in idRetenciones)
                                    {
                                        retencionModel = servicioRetencion.GetRetencionOu(int.Parse(idRetencion));
                                        if (retencionModel != null)
                                        {
                                            importeRetencion += retencionModel.Importe;
                                        }
                                        // ak se podria cociliar la retencion con la factura y quitar las demas
                                    }
                                    if (importeRetencion > 0)
                                    {
                                        var alias = "VRET";
                                        asiento.Importe = - importeRetencion;
                                        asiento.Descripcion = "Retencion sobre Cobros (Nro." + nuevoPagoRegistrado.NumeroCobro + ")";
                                        var asientoBanco = oServicioContable.InsertAsientoContable(alias, asiento, 0);
                                        if (asientoBanco != null) { oServicioImputacion.AsintoContableGeneral(asientoBanco); }

                                    }

                                }


                              
                                //validar y actualizar presupuesto 
                                PresupuestoActualModel oPrespuestoActual = oServicioPresupuestoActual.GetAllPresupuestos(medioCobro.IdPresupuesto);
                                oPrespuestoActual.Ejecutado += Factura.Total;
                                oPrespuestoActual.IdUsuario = medioCobro.IdUsuario;
                                oPrespuestoActual.UltimaModificacion = DateTime.Now;

                                oServicioPresupuestoActual.ActualizarPresupuesto(oPrespuestoActual);
                                if (oPrespuestoActual.Id != 0)
                                {
                                    PresupuestoCostoModel oPresupuestoCosto = oServicioPresupuestoCosto.GetAllPresupuestoCosto(oPrespuestoActual.Codigo);
                                    if (oPresupuestoCosto != null)
                                    {
                                        oPresupuestoCosto.Ejecutado += Factura.Total;
                                        oPresupuestoCosto.IdUsuario = medioCobro.IdUsuario;
                                        oPresupuestoCosto.UltimaModificacion = DateTime.Now;
                                       oServicioPresupuestoCosto.ActualizarPresupuesto(oPresupuestoCosto);
                                    }
                                }

                                                    
                                PresupuestoItemModel presupuestoItem = new PresupuestoItemModel();
                                presupuestoItem.Codigo = oPrespuestoActual.Codigo;
                                presupuestoItem.Concepto = cliente.Nombre + "Nro Factura: " + Factura.NumeroFactura;
                                presupuestoItem.Pagado = Factura.Total;
                                presupuestoItem.Ejecutado = "true";
                                presupuestoItem.Periodo = int.Parse(nuevoPagoRegistrado.Fecha.ToString("yyMM"));

                               PresupuestoItemModel oPresupuestoItem = oServicioPresupuestoItem.Insertar(presupuestoItem);


                                cliente.IdGrupoPresupuesto = medioCobro.IdPresupuesto;
                                cliente.UltimaModificacion = DateTime.Now;
                                cliente.IdUsuario = medioCobro.IdUsuario ?? 0;
                                oServicioCliente.ActualizarPresupuestoCliente(cliente);


                                _mensaje?.Invoke("Cobro efectuado correctamente", "ok");

                            }

                        }
                    }
                }





            }
            catch (Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioFacturaVenta >> RegistroDeCobro");
              
                _mensaje?.Invoke("Ops!, Ocurrio un error al ejecutar el método de Pago. Comuníquese en contacto con el administrador del sistema " , "error");
            }
        }

        public void RegistroDeCobroCbtAFavor(List<CobroFacturaModel> cobroFacturaModel, int IdTipoComprobanteVenta)
        {
            try
            {               
                var cliente = oServicioCliente.GetClientePorId(cobroFacturaModel[0].IdCliente ?? 0);
                decimal saldoPagoTotalAFavor = cobroFacturaModel.Where(x => x.IdTipoComprobante.Equals(IdTipoComprobanteVenta)).Sum(x => x.Saldo);              
                decimal saldoPagoTotal = saldoPagoTotalAFavor; 
                decimal[] aDifCot = new decimal[3];
                aDifCot[0] = 0;
                CobroFacturaModel cbtCobroFactura = (from cbtCobro in cobroFacturaModel
                                                     where cbtCobro.IdTipoComprobante == IdTipoComprobanteVenta
                                                     select cbtCobro).FirstOrDefault();            
                var CodigoAsiento = oServicioContable.GetNuevoCodigoAsiento() + 1;
                DiarioModel asiento = new DiarioModel();
                asiento.Codigo = CodigoAsiento;
                asiento.Fecha = cbtCobroFactura.Fecha;
                asiento.Periodo = cbtCobroFactura.Fecha.ToString("yyMM");
                asiento.Tipo = "VP";
                asiento.Balance = int.Parse(cbtCobroFactura.Fecha.ToString("yyyy"));
                asiento.Moneda = oServicioTipoMoneda.GetTipoMoneda(cbtCobroFactura.IdMoneda).Descripcion;
              
                foreach (var Factura in cobroFacturaModel)
                {
                    CobroFacturaModoModel cobroFacturaModo = new CobroFacturaModoModel();
           
                    //--------- Registro de Facturas a Cobrar -----------
                    if (Factura.IdTipoComprobante != IdTipoComprobanteVenta)
                    {
                        //// -------------- registro de asientos                              
                        asiento.Importe = (Factura.IdMoneda == 1) ? -Factura.Saldo : -(Factura.Saldo * Factura.Cotiza);
                        asiento.Cotiza = Factura.Cotiza;
                        asiento.Descripcion = "Deudores por Ventas";
                        asiento.DescripcionMa = "Cobro Fact Nº " + Factura.NumeroFactura + ", Cliente:" + cliente.Nombre;
                        asiento.Titulo = "Cobro de Factura";

                        if (Factura.TipoIva == "4") // exterior
                        {
                            var asientoVEXT = oServicioContable.InsertAsientoContable("VEXT", asiento, 0);
                            if (asientoVEXT != null) { oServicioImputacion.AsintoContableGeneral(asientoVEXT); }
                        }
                        else //local
                        {
                            var asientoVEXT = oServicioContable.InsertAsientoContable("VLOC", asiento, 0);
                            if (asientoVEXT != null) { oServicioImputacion.AsintoContableGeneral(asientoVEXT); }
                        }

                        if (Factura.IdMoneda != 1 && cbtCobroFactura.Cotiza != Factura.Cotiza)
                        {                           
                            aDifCot[1] = Factura.Cotiza;
                            aDifCot[0] += Factura.Saldo * Factura.Cotiza;
                        }

                        Factura.FechaCobro = cbtCobroFactura.Fecha;
                        Factura.NumeroCobro = cbtCobroFactura.NumeroCobro; 
                        Factura.Vencimiento = cbtCobroFactura.Fecha;
                        Factura.CotizaP = cbtCobroFactura.Cotiza;
                        Factura.Saldo = Factura.saldoCobro;
                        
                        if (Factura.Saldo == 0)
                        {
                            Factura.Parcial = 0;
                        }
                        else { Factura.Parcial += Factura.aplicacion; }
                        ActualizaVentaFacturaCobro(Factura);
                       
                        SaveTrackingCompras(Factura.Id, cbtCobroFactura.Id);


                    }
                    else
                    {
                        
                        if (Factura.NumeroFactura > 0 && Factura.IdTipoComprobante == IdTipoComprobanteVenta)
                        {
                            Factura.FechaCobro = cbtCobroFactura.Fecha;
                            Factura.Parcial += Factura.aplicacion;
                            Factura.Saldo = Factura.saldoCobro;

                                                   
                            ActualizaVentaFacturaCobro(Factura);
                        }                       
                        _mensaje?.Invoke("Cobro efectuado correctamente", "ok");
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioFacturaVenta >> RegistroDeCobroCbtAFavor");
                _mensaje?.Invoke("Ops!, Ocurrio un error al ejecutar el método de Pago. Comuníquese en contacto con el administrador del sistema ", "error");
            }
        }
        public FacturaVentaModel ActualizaFacturaNC(FacturaVentaModel Factura)
        {
            // Crear una nueva instancia de FacturaVentaModel  
            FactVenta Fact = new FactVenta();

            // Asignar manualmente las propiedades que deseas copiar de `fact` a `factura`  
            Fact.FechaCobro = Factura.FechaCobro;
            Fact.Activo = Factura.Activo;
            Fact.NumeroCobro = Factura.NumeroCobro;
            Fact.Cotiza = Factura.Cotiza;
            Fact.CotizaP = Factura.CotizaP;
            Fact.Parcial = Factura.Parcial;
            Fact.Saldo = Factura.Saldo;
            Fact.Id = Factura.Id;

            oFacturaVentasRepositorio.ActualizaVentaFacturaCobro(Fact);

            return Factura;
        }
            

        private void SaveTrackingCompras(int idcbtCobro, int idcbtFactura)
        {
                    TrackingFacturaCobroModel tracking = new TrackingFacturaCobroModel();
                    tracking.IdFactura = idcbtFactura;
                    tracking.IdCobro = idcbtCobro;
                    ServicioTrackingFacturaCobro.Insert(tracking);               
        }

        private void SaveTrackingComprasList(List<CobroFacturaModel> modelFacturas, CobroFacturaModel modelCobro)
        {
            foreach (var item in modelFacturas)
            {
                TrackingFacturaCobroModel tracking = new TrackingFacturaCobroModel();
                tracking.IdFactura = item.Id;
                tracking.IdCobro = modelCobro.Id;
                ServicioTrackingFacturaCobro.Insert(tracking);
            }
         

        }


        public FacturaVentaModel GetFacturaVentaPorId(int idcliente, int idFactura)
        {
            try
            {/*
                var fact = oFacturaVentasRepositorio.GetFacturaVentaPorId(idcliente,idFactura);
                
                return Mapper.Map<FactVenta, FacturaVentaModel>(fact);
                */


                var fact = oFacturaVentasRepositorio.GetFacturaVentaPorId(idcliente, idFactura);

                // Probar el mapeo de forma aislada
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FactVenta, FacturaVentaModel>()
                       .ForMember(dest => dest.Cliente, opt => opt.Ignore()); // Ignorar la propiedad Cliente

                    cfg.CreateMap<Cliente, ClienteModel>(); // Suponiendo que tienes estas clases
                });

                var mapper = config.CreateMapper();

                var result = mapper.Map<FactVenta, FacturaVentaModel>(fact);

                result.Cliente = oServicioCliente.GetClientePorId(idcliente);

                ServicioPieNota Servicio = new ServicioPieNota();
                int IdPieNota = result.Cliente.IdPieNota;
                PieNotaModel PieNota = Servicio.GetPieNotaPorId(IdPieNota);
                result.Cliente.PieNota = PieNota;

                return result;


            }

#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (StackOverflowException ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurrido un error de desbordamiento de pila. Contacte al Administrador", "error");
                return null;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public CobroFacturaModel GuardarCobro(CobroFacturaModel model)
        {
            try
            {
                FactVenta facventa = oFacturaVentasRepositorio.CreateFactura(Mapper.Map<CobroFacturaModel, FactVenta>(model));
                return Mapper.Map<FactVenta, CobroFacturaModel>(facventa);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }

        }
        private CobroFacturaModel ActualizaVentaFacturaCobro(CobroFacturaModel factura)
        {
            try
            {
                return  Mapper.Map<FactVenta, CobroFacturaModel>(oFacturaVentasRepositorio.ActualizaVentaFacturaCobro(Mapper.Map<CobroFacturaModel, FactVenta>(factura)));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();
            }
        }

        public FactVenta ActualizaProForma(int NumeroFactura, string idComprobante)
        {
            try
            {
                var fac = oFacturaVentasRepositorio.ActualizaProforma(NumeroFactura, idComprobante);
                return fac;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();
            }
        }
    }
}
