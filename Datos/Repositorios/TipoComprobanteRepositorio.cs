using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
    public class TipoComprobanteRepositorio : RepositorioBase<TipoComprobante>
    {
        private SAC_Entities context;

        public TipoComprobanteRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
        public TipoComprobante InsertarTipoComprobante(TipoComprobante tipoComprobante)
        {
            return Insertar(tipoComprobante);
        }


        public TipoComprobante GetTipoComprobantePorId(int id)
        {
            var TipoComprobante = context.TipoComprobante.Where(p => p.Id == id).FirstOrDefault();
            return TipoComprobante;
        }

        public TipoComprobante ActualizarTipoComprobante(TipoComprobante model)
        {
            TipoComprobante TipoComprobanteExistente = GetTipoComprobantePorId(model.Id);

            TipoComprobanteExistente.Id = model.Id;
            TipoComprobanteExistente.Denominacion = model.Denominacion;
            TipoComprobanteExistente.Activo = model.Activo;

            context.SaveChanges();
            return TipoComprobanteExistente;
        }

        public TipoComprobante ObtenerPorNombre(string nombre)
        {
            return context.TipoComprobante.Where(p => p.Denominacion == nombre).FirstOrDefault();
        }

        public List<TipoComprobante> GetTipoComprobantePorTipoIvaProveedor(int idTipoIva)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return (from c in context.TipoComprobante
                    join ci in context.TipoComprobanteTipoIva on c.Id equals ci.IdTipoComprobante
                    where c.Activo == true && ci.IdTipoIva == idTipoIva
                    select c).ToList();
          
        }

        public List<TipoComprobante> ComprobantesAfip()
        {
            context.Configuration.LazyLoadingEnabled = false;
            int[] ids = new int[] {11, 12, 13, 19, 20 ,21, 211, 212, 213};
            List<TipoComprobante> listModel = context.TipoComprobante.Where(p => p.Activo == true && ids.Contains(p.Id)).ToList();
            return listModel;
        }

        public List<TipoComprobante> GetAllTipoComprobante()
        {
            context.Configuration.LazyLoadingEnabled = false;
               
            List<TipoComprobante> listModel = context.TipoComprobante.Where(p => p.Activo == true).ToList();
            return listModel;
        }

        public List<TipoComprobante> GetTipoComprobanteLocalesVentaSinFactura()
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<TipoComprobante> listModel = context.TipoComprobante
                                                .Where(p => p.Id == 11 || p.Id==12 || p.Id== 13 && p.Activo ==true).ToList();
            return listModel;
        }
        public List<TipoComprobante> GetTipoComprobanteExtranjerosVenta()
        {
            context.Configuration.LazyLoadingEnabled = false;

            List<TipoComprobante> listModel = context.TipoComprobante
                                             .Where(p => p.Id == 19 || p.Id == 20 || p.Id == 21 && p.Activo == true).ToList();
            return listModel;
        }


        public int EliminarTipoComprobanteTipoMoneda(int id)
        {
            TipoComprobante model = GetTipoComprobantePorId(id);
            model.Activo = false;
            context.SaveChanges();
            return 1;

        }

    }
}
