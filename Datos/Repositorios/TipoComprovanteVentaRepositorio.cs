using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{

   public class TipoComprobanteVentaRepositorio : RepositorioBase<TipoComprobante>
    {
        private SAC_Entities context;

        public TipoComprobanteVentaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public TipoComprobanteVenta GetTipoComprobanteVentaPorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            var TipoComprobante = context.TipoComprobanteVenta.Where(p => p.Id == id).FirstOrDefault();
            return TipoComprobante;
        }


        public TipoComprobanteVenta GetTipoComprobanteVentaPorNroAfip(int id, int idPuntoVenta)
        {
            context.Configuration.LazyLoadingEnabled = false;
            var TipoComprobante = context.TipoComprobanteVenta.Where(p => p.CodigoAfip == id && p.PuntoVenta == idPuntoVenta).FirstOrDefault();
            return TipoComprobante;
        }

        
        public int ObtenerNroPago(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.TipoComprobanteVenta.Where(p => p.CodigoAfip == id).Select(p => p.Numero).FirstOrDefault();
             
        }

        public int ActualizarNroPago(int id, int nroPago)
        {
            
            var TipoComprobante = context.TipoComprobanteVenta.Where(p => p.Id == id).First();
            TipoComprobante.Numero = nroPago;
            return context.SaveChanges();
        }

        public int ObtenerNroFactura(int nroComprobante, int puntoVenta)
        {
            context.Configuration.LazyLoadingEnabled = false;
            if (nroComprobante == 60)
            {
                puntoVenta = 9;
            }
            return context.TipoComprobanteVenta.Where(p => p.CodigoAfip == nroComprobante && p.PuntoVenta == puntoVenta).First().Numero;

            //return context.TipoComprobanteVenta.Where(p => p.Id == nroComprobante && p.PuntoVenta == puntoVenta).First().Numero;           
            //var Factura = context.TipoComprobanteVenta.Where(p => p.CodigoAfip == nroComprobante && p.PuntoVenta == puntoVenta).First();
            //  Factura.Numero += 1;
            //  context.SaveChanges();           
            // return Factura.Numero;
            //return Factura;
        }

        public List<TipoComprobanteVenta> GetAllTipoComprobante()
        {
            context.Configuration.LazyLoadingEnabled = false;

            List<TipoComprobanteVenta> listModel = context.TipoComprobanteVenta.ToList();
            return listModel;
        }

        public TipoComprobanteVenta GetNuevoNumeroCobro(int codigoAfip, int puntoVenta)
        {

            context.Configuration.LazyLoadingEnabled = false;
            var result = context.TipoComprobanteVenta.Where(p => p.CodigoAfip == codigoAfip && p.PuntoVenta == puntoVenta && p.Activo == true).First();

            if (result != null)
            {
                result.Numero += 1;
                context.SaveChanges();
            }


            return result;
        }
       public TipoComprobanteVenta getTipoComprobanteVentaNewNumeroFactura(int nroComprobante, int puntoVenta)
        {
            context.Configuration.LazyLoadingEnabled = false;           
            var Factura = context.TipoComprobanteVenta.Where(p => p.CodigoAfip == nroComprobante && p.PuntoVenta == puntoVenta).First();
            Factura.Numero += 1;
            context.SaveChanges();
            return Factura;
        }
        public TipoComprobanteVenta GetNuevoNumeroCobro(int id)
        {

            context.Configuration.LazyLoadingEnabled = false;
          
            var result = context.TipoComprobanteVenta.Where(p => p.Id == id).FirstOrDefault();

            if (result != null)
            {
                result.Numero += 1;
                context.SaveChanges();
            }

            return result;
        }
        

        public int ActualizarNroFactura(int nroComprobante, int puntoVenta, int nroFactura)
        {

            var Factura = context.TipoComprobanteVenta.Where(p => p.CodigoAfip == nroComprobante && p.PuntoVenta == puntoVenta).First();
            Factura.Numero = nroFactura;
            return context.SaveChanges();
        }

        public TipoComprobanteVenta getTipoComprobanteVenta(int codigoAfip, int idPuntoVenta)
        {
            context.Configuration.LazyLoadingEnabled = false;
          return context.TipoComprobanteVenta.Where(p => p.CodigoAfip == codigoAfip && p.PuntoVenta == idPuntoVenta).First();           
             
        }
    }
}
