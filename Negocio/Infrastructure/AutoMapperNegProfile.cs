using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Negocio.Modelos;
using Datos.ModeloDeDatos;

namespace Agenda.Infrastructure
{
    public class AutoMapperNegProfile: AutoMapper.Profile
    {
        public AutoMapperNegProfile()
        {


            CreateMap<ConsultaFacturaVentaIva, FacturaVentaIvaModel>();
            CreateMap<FacturaVentaIvaModel, ConsultaFacturaVentaIva>();

            CreateMap<ConsultaIvaTotales, ConsultaIvaTotalesModel>();
            CreateMap<ConsultaIvaTotalesModel, ConsultaIvaTotales>();



            CreateMap<CteCteClienteResumen, CteCteClienteResumenModel>();
            CreateMap<CteCteClienteResumenModel, CteCteClienteResumen>();

            CreateMap<ConsultaIvaVenta, ConsultaIvaVentaModel>();
            CreateMap<ConsultaIvaVentaModel, ConsultaIvaVenta>();

            CreateMap<FacturaElectronica, FacturaElectronicaModel>();
            CreateMap<FacturaElectronicaModel, FacturaElectronica>();

            CreateMap<FactVenta, CobroFacturaModel>();
            CreateMap<CobroFacturaModel, FactVenta>();

            CreateMap<FactVentaCobro, CobroFacturaModoModel>();
            CreateMap<CobroFacturaModoModel, FactVentaCobro>();


            CreateMap<DiarioModel, Diario>();
            CreateMap<Diario, DiarioModel>();

            CreateMap<CompraRegistroDetalle, CompraRegistroDetalleModel>();
            CreateMap<CompraRegistroDetalleModel, CompraRegistroDetalle>();

            CreateMap<TipoPago, TipoPagoModel>();
            CreateMap<TipoPagoModel, TipoPago>();

            CreateMap<Buque, BuqueModel>();
            CreateMap<BuqueModel, Buque>();

            CreateMap<Dto, DtoModel>();
            CreateMap<DtoModel, Dto>();

            CreateMap<FactVenta, FacturaVentaModel>();
            CreateMap<FacturaVentaModel, FactVenta>();

            CreateMap<TrackingFacturaPagoCompraModel, TrackingFacturaPagoCompra>();
            CreateMap<TrackingFacturaPagoCompra, TrackingFacturaPagoCompraModel>();
            CreateMap<TrackingFacturaCobroModel, TrackingFacturaCobroVenta>();
            CreateMap<TrackingFacturaCobroVenta, TrackingFacturaCobroModel>();

            CreateMap<ItemImprModel, ItemImpre>();
            CreateMap<ItemImpre, ItemImprModel>();

            CreateMap<Departamento, DepartamentoModel>();
            CreateMap<DepartamentoModel, Departamento>();

            CreateMap<Articulo, ArticuloModel>();
            CreateMap<ArticuloModel, Articulo>();

            CreateMap<TipoClienteModel, TipoCliente>();
            CreateMap<TipoCliente, TipoClienteModel>();

            CreateMap<ClienteDireccionModel, ClienteDireccion>();
            CreateMap<ClienteDireccion, ClienteDireccionModel>();

            CreateMap<ClienteModel, Cliente>();
            CreateMap<Cliente, ClienteModel>();

            CreateMap<PieNotaModel, PieNota>();
            CreateMap<PieNota, PieNotaModel>();
           
            CreateMap<TipoIdiomaModel, TipoIdioma>();
            CreateMap<TipoIdioma, TipoIdiomaModel>();

            CreateMap<RetencionModel, Retencion>();
            CreateMap<Retencion, RetencionModel>();

            CreateMap<TipoRetencionModel, TipoRetencion>();
            CreateMap<TipoRetencion, TipoRetencionModel>();

            CreateMap<CuentaCorrienteProveedorDetalles, CuentaCorrienteProveedorDetallesModel>();
            CreateMap<CuentaCorrienteProveedorDetallesModel, CuentaCorrienteProveedorDetalles>();


            CreateMap<TarjetaOperacionModel, TarjetaOperacion>();
            CreateMap<TarjetaOperacion, TarjetaOperacionModel>();

            CreateMap<PresupuestoItemModel, PresupuestoItem>();
            CreateMap<PresupuestoItem, PresupuestoItemModel>();

            CreateMap<PresupuestoCostoModel, PresupuestoCosto>();
            CreateMap<PresupuestoCosto, PresupuestoCostoModel>();

            CreateMap<CajaSaldoModel, CajaSaldo>();
            CreateMap<CajaSaldo, CajaSaldoModel>();

            CreateMap<CajaModel, Caja>();
            CreateMap<Caja, CajaModel>();
            CreateMap<CajaGrupoModel, GrupoCaja>();
            CreateMap<GrupoCaja, CajaGrupoModel>();
            CreateMap<ImputacionModel, Imputacion>();
            CreateMap<Imputacion, ImputacionModel>();

            CreateMap<TipoMonedaModel, TipoMoneda>();
            CreateMap<TipoMoneda, TipoMonedaModel>();


            CreateMap<TipoComprobanteModel, TipoComprobante>();
            CreateMap<TipoComprobante, TipoComprobanteModel>();

            CreateMap<TipoComprobanteVentaModel, TipoComprobanteVenta>();
            CreateMap<TipoComprobanteVenta, TipoComprobanteVentaModel>();

            CreateMap<CompraFacturaModel, CompraFactura>();
            CreateMap<CompraFactura, CompraFacturaModel>();

            CreateMap<CompraIvaModel, CompraIva>();
            CreateMap<CompraIva, CompraIvaModel>();
          

            CreateMap<ValorCotizacionModel, ValorCotizacion>();
            CreateMap<ValorCotizacion, ValorCotizacionModel>();

            CreateMap<PresupuestoActualModel, PrespuestoActual>();
            CreateMap<PrespuestoActual, PresupuestoActualModel>();

            CreateMap<CompraFacturaPagoModel, CompraFacturaPago>();
            CreateMap<CompraFacturaPago, CompraFacturaPagoModel>();

            CreateMap<TarjetaModel, Tarjetas>();
            CreateMap<Tarjetas, TarjetaModel>();

            CreateMap<BancoModel, Banco>();
            CreateMap<Banco, BancoModel>();

            CreateMap<BancoCuentaModel, BancoCuenta>();
            CreateMap<BancoCuenta, BancoCuentaModel>();

            CreateMap<BancoCuentaBancariaModel, BancoCuentaBancaria>();
            CreateMap<BancoCuentaBancaria, BancoCuentaBancariaModel>();

            CreateMap<ChequeraModel, Chequera>();
            CreateMap<Chequera, ChequeraModel>();

            CreateMap<ChequeModel, Cheque>();
            CreateMap<Cheque, ChequeModel>();

            CreateMap<ImputacionModel, Imputacion>();
            CreateMap<Imputacion, ImputacionModel>();

            CreateMap<TipoComprobanteModel, TipoComprobante>();
            CreateMap<TipoComprobante, TipoComprobanteModel>();

            CreateMap<CompraFacturaModel, CompraFactura>();
            CreateMap<CompraFactura, CompraFacturaModel>();

            CreateMap<CuentaCteProveedorModel, CuentaCorriente>();
            CreateMap<CuentaCorriente, CuentaCteProveedorModel>();

            CreateMap<ProveedorModel, Proveedor>();
            CreateMap<Proveedor, ProveedorModel>();

            CreateMap<PersonaModel, Persona>();
            CreateMap<Persona, PersonaModel>();

          

            CreateMap<Pais, PaisModel>();
            CreateMap<PaisModel, Pais>();

            CreateMap<Provincia, ProvinciaModel>();
            CreateMap<ProvinciaModel, Provincia>();

            CreateMap<Localidad, LocalidadModel > ();
            CreateMap< LocalidadModel, Localidad>();

            CreateMap<TipoIva, TipoIvaModel>();
            CreateMap<TipoIvaModel, TipoIva>();

            CreateMap<SubRubro, SubRubroModel>();
            CreateMap<SubRubroModel, SubRubro>();

            CreateMap<TipoProveedor, TipoProveedorModel>();
            CreateMap<TipoProveedorModel, TipoProveedor>();

            CreateMap<TipoMoneda, TipoMonedaModel>();
            CreateMap<TipoMonedaModel, TipoMoneda>();

            //CreateMap<Evento, EventoModel>();
            //CreateMap<EventoModel, Evento>();

            CreateMap<Accion, AccionModel>();
            CreateMap<AccionModel, Accion>();

            CreateMap<AccionPorRol, AccionPorRolModel>();
            CreateMap<AccionPorRolModel, AccionPorRol>();

            CreateMap<UsuarioModel, Usuario>();
            CreateMap<Usuario, UsuarioModel>();


            CreateMap<RolModel, Rol>();
            CreateMap<Rol, RolModel>();

            CreateMap<MenuSidebar, MenuSideBarModel>()
                .ForMember(
                    dest => dest.Group,
                    opt => opt.MapFrom(src => src.MenuSidebar1)
                );

            CreateMap<MenuSideBarModel, MenuSidebar>();



        }      
    }
}