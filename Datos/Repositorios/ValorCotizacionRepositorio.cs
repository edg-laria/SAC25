using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
    public class ValorCotizacionRepositorio : RepositorioBase<ValorCotizacionRepositorio>
    {
        private SAC_Entities context;

        public ValorCotizacionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
        public TipoMoneda Insertar(ValorCotizacion  cotizacion)
        {
            return Insertar(cotizacion);
        }


        public List<ValorCotizacion> GetCotizacionMoneda(DateTime f)
        {
           return context.ValorCotizacion.Where(p => p.Activo == true && p.Fecha >= f).ToList();
        }
        public ValorCotizacion GetCotizacionPorIdMoneda(int idMoneda)
        {
            var cotizacion = context.ValorCotizacion.Where(p => p.Activo == true
                                                   && p.Id == idMoneda).FirstOrDefault();           
            return cotizacion;
        }
        public ValorCotizacion GetCotizacionPorIdMoneda(DateTime f, int idMoneda)
        {
            var cotizacion =  context.ValorCotizacion.Where(p => p.Activo == true
                                                    && p.UltimaModificacion >= f                          
                                                    && p.Id == idMoneda).FirstOrDefault();
          
            return cotizacion;
        }
        public ValorCotizacion GetUltimaCotizacion(int idMoneda)
        {
            return context.ValorCotizacion.Where(p => p.Activo == true && p.IdTipoMoneda == idMoneda).FirstOrDefault();
        }

        public void updateCotizacionPorIdMoneda(ValorCotizacion moneda)
        {
            var cotizacion = context.ValorCotizacion.Where(p => p.Activo == true
                                                   && p.Id == moneda.IdTipoMoneda).FirstOrDefault();
            cotizacion.Monto = moneda.Monto;
            cotizacion.Fecha = moneda.Fecha;
            cotizacion.UltimaModificacion = moneda.UltimaModificacion;
            context.SaveChanges();

        }
    }
}
