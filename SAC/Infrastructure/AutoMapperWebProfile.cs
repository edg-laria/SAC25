
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAC.Models;
using Entidad.Modelos;
using Negocio.Modelos;


namespace SAC.Infrastructure
{
    public class AutoMapperWebProfile : AutoMapper.Profile
    {
        public AutoMapperWebProfile()
        {


          
            CreateMap<FacturaVentaIvaModel, FacturaVentaIvaModelView>();
            CreateMap<FacturaVentaIvaModelView, FacturaVentaIvaModel>();

            CreateMap<ConsultaIvaTotalesModel, ConsultaIvaTotalesModelView>();
            CreateMap<ConsultaIvaTotalesModelView, ConsultaIvaTotalesModel>();
          
            CreateMap<FacturaElectronicaModel, FacturaElectronicaModelView>();
            CreateMap<FacturaElectronicaModelView, FacturaElectronicaModel>();

            CreateMap<CteCteClienteResumenModel, CteCteClienteResumenModelView>();
            CreateMap<CteCteClienteResumenModelView, CteCteClienteResumenModel>();


            CreateMap<ConsultaIvaVentaModel, ConsultaIvaVentaModelView>();
            CreateMap<ConsultaIvaVentaModelView, ConsultaIvaVentaModel>();


            CreateMap<CobroFacturaModoModel, CobroFacturaModoModelView>();

            CreateMap<CobroFacturaModoModelView, CobroFacturaModoModel>();

            CreateMap<CobroFacturaModel, CobroFacturaModelView>();

            CreateMap<CobroFacturaModelView, CobroFacturaModel>();
                 //.ForMember(cf => cf.Saldo, cfm => cfm.MapFrom(i => i.Tipo != null ? i.saldoCobro : i.Saldo )) 
                 //.ForMember(cf => cf.Parcial, cfm => cfm.MapFrom(i =>  i.aplicacion + i.Parcial))
                 //.ForMember(cf => cf.Total, cfm => cfm.MapFrom(i => i.Tipo != null ? i.Total : i.cobro));
            //.ForMember(cf => cf.Parcial, cfm => cfm.MapFrom(i => i.Tipo != null ? 0 : i.aplicacion + i.Parcial))

            CreateMap<FacturaModelView, FacturaVentaModel>()
                    .ForMember(fv => fv.IdMoneda, fvm => fvm.MapFrom(i => i.idTipoMoneda));
            CreateMap<FacturaVentaModel, FacturaModelView>()
                    .ForMember(fv => fv.idTipoMoneda, fvm => fvm.MapFrom(i => i.IdMoneda));


            CreateMap<CuentaPlanContableModelView, CuentaPlanContableModel>();
            CreateMap<CuentaPlanContableModel, CuentaPlanContableModelView>();

            CreateMap<CompraRegistroDetalleModel, CompraRegistroDetalleModelView>();
            CreateMap<CompraRegistroDetalleModelView, CompraRegistroDetalleModel>();

            CreateMap<DiarioModel, DiarioModelView>();
            CreateMap<DiarioModelView, DiarioModel>();

            CreateMap<ItemImprModel, ItemImprModelView>();
            CreateMap<ItemImprModelView, ItemImprModel>();

            CreateMap<TipoPagoModel, TipoPagoModelView>();
            CreateMap<TipoPagoModelView, TipoPagoModel>();

            CreateMap<ArticuloModel, ArticuloModelView>();
            CreateMap<ArticuloModelView, ArticuloModel>();

            CreateMap<DepartamentoModel, DepartamentoModelView>();
            CreateMap<DepartamentoModelView, DepartamentoModel>();


            CreateMap<TipoIdiomaModel,TipoIdiomaModelView>();
            CreateMap<TipoIdiomaModelView, TipoIdiomaModel>();


            CreateMap<PieNotaModel, PieNotaModelView>();
            CreateMap<PieNotaModelView, PieNotaModel>();

            CreateMap<TipoClienteModel, TipoClienteModelView>();
            CreateMap<TipoClienteModelView, TipoClienteModel>();

            CreateMap<ClienteDireccionModel, ClienteDireccionModelView>();
            CreateMap<ClienteDireccionModelView, ClienteDireccionModel>();


            CreateMap<ClienteModel, ClienteModelView>();
            CreateMap<ClienteModelView, ClienteModel>();


            CreateMap<TarjetaOperacionModel, TarjetaOperacionModelView>();
            CreateMap<TarjetaOperacionModelView, TarjetaOperacionModel>();



            CreateMap<RetencionModel, RetencionModelView>()
                .ForMember(rmv => rmv.VentaFactura, rm => rm.MapFrom(i => i.FactVenta));
            CreateMap<RetencionModelView, RetencionModel>()
            .ForMember(rm => rm.FactVenta, rmv => rmv.MapFrom(i => i.VentaFactura));

            CreateMap<TipoRetencionModel, TipoRetencionModelView>();
            CreateMap<TipoRetencionModelView, TipoRetencionModel>();

            CreateMap<PresupuestoItemModel, PresupuestoItemModelView>();
            CreateMap<PresupuestoItemModelView, PresupuestoItemModel>();

            CreateMap<PresupuestoCostoModel, PresupuestoCostoModelView>();
            CreateMap<PresupuestoCostoModelView, PresupuestoCostoModel>();

            CreateMap<CajaModel, CajaModelView>();
            CreateMap<CajaModelView, CajaModel>();


            CreateMap<CajaGrupoModel, CajaGrupoModelView>();
            CreateMap<CajaGrupoModelView, CajaGrupoModel>();


            CreateMap<TipoMonedaModel, TipoMonedaModelView>();
            CreateMap<TipoMonedaModelView, TipoMonedaModel>();


            CreateMap<TipoComprobanteModel, TipoComprobanteModelView>();
            CreateMap<TipoComprobanteModelView, TipoComprobanteModel>();

            CreateMap<TipoComprobanteVentaModel, TipoComprobanteVentaModelView>();
            CreateMap<TipoComprobanteVentaModelView, TipoComprobanteVentaModel>();

            CreateMap<CompraFacturaModel, CompraFacturaViewModel>();
            CreateMap<CompraFacturaViewModel, CompraFacturaModel>();

            CreateMap<CompraIvaModel, CompraIvaModelView>();
            CreateMap<CompraIvaModelView, CompraIvaModel>();


            CreateMap<PresupuestoActualModel, PresupuestoActualModelView>();
            CreateMap<PresupuestoActualModelView, PresupuestoActualModel>();

            CreateMap<TarjetaModel, TarjetaModelView>();
            CreateMap<TarjetaModelView, TarjetaModel>();

            CreateMap<BancoCuentaModel, BancoCuentaModelView>();
            CreateMap<BancoCuentaModelView, BancoCuentaModel>();

            CreateMap<BancoCuentaBancariaModel, BancoCuentaBancariaModelView>();
            CreateMap<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>();

            CreateMap<ChequeraModel, ChequeraModelView>();
            CreateMap<ChequeraModelView, ChequeraModel>();

            CreateMap<ChequeModel, ChequeModelView>();
            CreateMap<ChequeModelView, ChequeModel>();
     
            CreateMap<FacturaPagoModel, FacturaPagoViewModel>();
            CreateMap<FacturaPagoViewModel, FacturaPagoModel>();

            CreateMap<ImputacionModel, ImputacionModelView>();
            CreateMap<ImputacionModelView, ImputacionModel>();

            CreateMap<TipoComprobanteModel, TipoComprobanteModelView>();
            CreateMap<TipoComprobanteModelView, TipoComprobanteModel>();

            CreateMap<TipoMonedaModel, TipoMonedaModelView>();
            CreateMap<TipoMonedaModelView, TipoMonedaModel>();

            CreateMap<CompraFacturaModel, CompraFacturaViewModel>();
            CreateMap<CompraFacturaViewModel, CompraFacturaModel>();

            CreateMap<CuentaCteProveedorModel, CuentaCteProveedorModelView>();
            CreateMap<CuentaCteProveedorModelView, CuentaCteProveedorModel>();

            CreateMap<PersonaModel,PersonaModelView>();
            CreateMap<PersonaModelView,PersonaModel >();

            CreateMap<PrioridadModel, PrioridadModelView>();
            CreateMap<PrioridadModelView , PrioridadModel>();

            CreateMap<EventoModel, EventoModelView>();             
            CreateMap<EventoModelView, EventoModel>();

            CreateMap<PaisModel, PaisModelView>();
            CreateMap<PaisModelView, PaisModel>();

            CreateMap<ProvinciaModel, ProvinciaModelView>();
            CreateMap<ProvinciaModelView, ProvinciaModel>();

            CreateMap<LocalidadModel, LocalidadModelView>();
            CreateMap<LocalidadModelView, LocalidadModel>();

            CreateMap<ProveedorModel, ProveedorModelView>();
            CreateMap<ProveedorModelView, ProveedorModel>();

            CreateMap<AfipRegimenModel, AfipRegimenModelView>();
            CreateMap<AfipRegimenModelView, AfipRegimenModel>();

            CreateMap<TipoIvaModel, TipoIvaViewModel>();
            CreateMap<TipoIvaViewModel, TipoIvaModel>();


            CreateMap<SubRubroModel, SubRubroModelView>();
            CreateMap<SubRubroModelView, SubRubroModel>();

            CreateMap<AccionModel, AccionModelView>();
            CreateMap<AccionModelView, AccionModel>();
          

            CreateMap<RolModel, RolModelView>();
            CreateMap<RolModelView, RolModel>();
            CreateMap<AccionPorRolModel, AccionPorRolModelView>();
            CreateMap<AccionPorRolModelView, AccionPorRolModel>();
            CreateMap<ConfiguracionModel, ConfiguracionModelView>();
            CreateMap<ConfiguracionModelView, ConfiguracionModel>();
            CreateMap<UsuarioModel, UsuarioModelView>();
            CreateMap<UsuarioModelView, UsuarioModel>();

            CreateMap<UsuarioModelView, UsuarioModel>();

            CreateMap<UsuarioModel, UsuarioModelView>();

            CreateMap<MenuSideBarModel, MenuItemModel>()
            .ForMember(mi => mi.Icono, msb => msb.MapFrom(i => i.Icono))
             .ForMember(mi => mi.Url, msb => msb.MapFrom(i => i.Url))
             .ForMember(mi => mi.Titulo, msb => msb.MapFrom(i => i.Titulo))
             .ForMember(mi => mi.Metodo, msb => msb.MapFrom(i => i.Accion.Nombre))
            .ForMember(mi => mi.Controller, msb => msb.MapFrom(i => i.Accion.Controlador));
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfiles(new[] {
                                                        "SAC",
                                                        "Negocio"
                                                    }));
            //AutoMapper.Mapper.Initialize(a =>
            //{
            //    a.AddProfile<AutoMapperWebProfile>();               
            //});
        }
    }
}