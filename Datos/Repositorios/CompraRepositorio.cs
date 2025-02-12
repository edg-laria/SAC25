using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class CompraRepositorio : RepositorioBase<CompraFactura>
    {
       private SAC_Entities context;
    
        public CompraRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
            
        public CompraFactura CreateFactura(CompraFactura compraFactura)
        {
            context.Configuration.LazyLoadingEnabled = false;
            CompraFactura nuevaEntidad = DbSet.Add(compraFactura);
            Contexto.SaveChanges();              
           return  nuevaEntidad;
        }

        public List<CompraFactura> GetAllCompraFactura()
        {
            return context.CompraFactura.Where(acc => acc.Activo == true).OrderBy(acc => acc.Fecha).ToList();
        }

        public CompraFactura GetCompraFacturaPorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.CompraFactura.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }

        public List<CompraFactura> GetCompraFacturaListaPorId(int id)
        {
            List<CompraFactura> oListaFacturas =context.CompraFactura.Where(acc => acc.Id == id && acc.Activo == true).ToList();
            return oListaFacturas;
        }

        public List<CompraFactura> GetCompraFacturaPorIdProveedor(int idProveedor)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.CompraFactura.Where(p => p.IdProveedor == idProveedor && p.IdTipoComprobante == 11 &&  p.NumeroPago == 0  ).ToList();
            
        }

        public List<CompraFactura> GetCompraFacturaPorIdProveedor_Moneda(int idProveedor, int idMoneda)
        {
            context.Configuration.LazyLoadingEnabled = false;

            return context.CompraFactura
                         .Include("TipoComprobante")
                         .Where(p => p.IdProveedor == idProveedor && p.Saldo != 0).OrderByDescending(x => x.NumeroFactura).ToList();
                        
        }

        //esta actualizacion es solo para el pago de facturas
        public CompraFactura ActualizarCompraFacturaPago(CompraFactura model)
        {

            CompraFactura CompraFactura = GetCompraFacturaPorId(model.Id);

            CompraFactura.FechaPago= model.FechaPago;
            CompraFactura.NumeroPago = model.NumeroPago;
            CompraFactura.CotizacionDePago= model.CotizacionDePago;
            CompraFactura.Saldo = model.Saldo;
            CompraFactura.Parcial = model.Parcial;
            context.SaveChanges();

            return CompraFactura;
        }
        
        public CompraFactura DeleteCompraFactura(int id)
        {

            CompraFactura CompraFactura = GetCompraFacturaPorId(id);
            CompraFactura.Activo = false;
            CompraFactura.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();

            return CompraFactura;
        }

        public List<Proveedor> GetProveedorPorNombre(string strProveedor)
        {
            List<Proveedor> p = (from c in context.Proveedor
                                 where c.Activo == true && c.Nombre.Contains(strProveedor)
                                 select c).ToList();
            return p;                
        }

        public CompraFactura GetCompraFacturaPorNroFacturaIdProveedor(int numeroFactura, int idProveedor)
        {
            context.Configuration.LazyLoadingEnabled = false;
            var fact = context.CompraFactura
                            .Include("CompraIva")
                            .Where(f => f.Activo == true
                            && f.NumeroFactura==numeroFactura
                            && f.IdProveedor == idProveedor
                            ).FirstOrDefault();
            return fact;
        }


        public CompraFactura ObtenerPorID_paraPagos(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.CompraFactura.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault();
        }

        public List<CompraFactura> GetAllCompraFacturaPorNro(int nroFactura)
        {
          var fact =   context.CompraFactura.Where(f => f.Activo == true && f.NumeroFactura.ToString().Contains(nroFactura.ToString())).OrderBy(acc => acc.NumeroFactura).ToList();
            return fact;

        }
        public CompraFactura GetCompraFacturaIVAPorNro(int nroFactura)
        {
            var fact = context.CompraFactura.Where(f => f.Activo == true && f.NumeroFactura == nroFactura).OrderBy(acc => acc.NumeroFactura).FirstOrDefault();            
            return fact;

        }
        //public CompraIva GetCompraFacturaIVAPorNro(int nroFactura)
        //{
        //    var fact = context.CompraFactura.Where(f => f.Activo == true && f.NumeroFactura == nroFactura).OrderBy(acc => acc.NumeroFactura).FirstOrDefault();
        //    var cfi = (from f in context.CompraFactura
        //               where f.NumeroFactura == nroFactura
        //               select f.CompraIva).FirstOrDefault();
        //    return cfi;

        //}


        public List<CompraRegistroDetalle> GetCompraRegistroDetalle(int mesFecha, int anioFecha)
        {
            context.Configuration.LazyLoadingEnabled = false;
            int periodo = int.Parse(anioFecha.ToString().Substring(2) + mesFecha.ToString().PadLeft(2, '0'));

            //.Where(p => p.Activo == true && p.Fecha.Month.Equals(mesFecha) && p.Fecha.Year.Equals(anioFecha) && p.IdTipoComprobante != 98)

            return context.CompraFactura
                            .Include("Proveedor")
                            .Include("CompraIva")
                            .Include("TipoComprobante")
                            .Where(p => p.Activo == true && p.Periodo == periodo && p.IdTipoComprobante != 98)
                            .Select(p => new CompraRegistroDetalle()
                            {
                                IdComprobante = p.TipoComprobante.Id,
                                Cuit = p.Proveedor.Cuit,
                                Empresa = p.Proveedor.Nombre,
                                Cbte = p.TipoComprobante.Denominacion,
                                PuntoVenta = p.PuntoVenta,
                                NumeroFactura = p.NumeroFactura,
                                Fecha = p.Fecha,
                                PercepcionIva = p.CompraIva.PercepcionIva,
                                PercepcionImporteIva = p.CompraIva.PercepcionImporteIva,
                                Neto = p.CompraIva.NetoGravado,
                                NetoNoGravado = p.CompraIva.NetoNoGravado,
                                Gasto = 0,
                               // Iva = p.CompraIva.TotalIva,
                                Iva = 0,
                                ISIB = p.CompraIva.TotalPercepciones,
                                Total = p.CompraIva.Total
                            }).ToList();


            ////1) se obtiene un objeto anonimo y no es posible pasarlo a una entidad
            //var iQuery = (from c in context.Caja
            //              where c.IdGrupoCaja == idgrupocaja && c.Fecha <= fecha
            //              group c by c.IdGrupoCaja into g
            //              select new
            //              {
            //                  IdGrupoCaja = g.Key,
            //                  ImportePesos = g.Sum(c => c.ImportePesos),
            //                  ImporteCheque = g.Sum(c => c.ImporteCheque),
            //                  ImporteDolar = g.Sum(c => c.ImporteDolar),
            //                  ImporteDeposito = g.Sum(c => c.ImporteDeposito),
            //                  ImporteTarjeta = g.Sum(c => c.ImporteTarjeta)
            //              });
            ////2) convertimos el objeto anonimo en una entidad
            //var caja = iQuery.ToList().Select(i => new Caja
            //{
            //    IdGrupoCaja = i.IdGrupoCaja,
            //    ImportePesos = i.ImportePesos,
            //    ImporteCheque = i.ImporteCheque,
            //    ImporteDolar = i.ImporteDolar,
            //    ImporteDeposito = i.ImporteDeposito,
            //    ImporteTarjeta = i.ImporteTarjeta
            //}).FirstOrDefault();
        }

    }
}