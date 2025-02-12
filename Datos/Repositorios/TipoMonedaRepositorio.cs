using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
    public class TipoMonedaRepositorio : RepositorioBase<TipoMoneda>
    {
        private SAC_Entities context;

        public TipoMonedaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
        public TipoMoneda InsertarTipoMoneda(TipoMoneda TipoMoneda)
        {
            return Insertar(TipoMoneda);
        }

        public TipoMoneda ObtenerTipoMonedaPorId(int idTipoMoneda)
        {
            context.Configuration.LazyLoadingEnabled = false;
            var TipoMoneda = context.TipoMoneda.Where(p => p.Id == idTipoMoneda).FirstOrDefault();
            return TipoMoneda;
        }

        public TipoMoneda ActualizarTipoMoneda(TipoMoneda model)
        {

            TipoMoneda TipoMonedaExistente = ObtenerTipoMonedaPorId(model.Id);

            TipoMonedaExistente.Id = model.Id;
            TipoMonedaExistente.Descripcion = model.Descripcion;
            TipoMonedaExistente.Activo = model.Activo;

            context.SaveChanges();
            return TipoMonedaExistente;
        }

        public TipoMoneda ObtenerTipoMonedaPorNombre(string nombre)
        {
            return context.TipoMoneda.Where(p => p.Descripcion == nombre).FirstOrDefault();
        }

       

      

        /// <summary>
        /// verifica que el nombre ingresado no exista para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idTipoMoneda"></param>
        /// <returns></returns>
        public TipoMoneda ObtenerTipoMonedaPorNombre(string nombre, int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.TipoMoneda.Where(p => p.Descripcion == nombre && p.Id != id).FirstOrDefault();
        }
     

        public List<TipoMoneda> GetAllTipoMoneda()
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<TipoMoneda> listaTipoMoneda = context.TipoMoneda.Where(p => p.Activo == true).OrderBy(p => p.Id).ToList();
            return listaTipoMoneda;
        }
       

        public int EliminarTipoMoneda(int idTipoMoneda)
        {
            TipoMoneda TipoMonedaExistente = ObtenerTipoMonedaPorId(idTipoMoneda);
            TipoMonedaExistente.Activo = false;
            context.SaveChanges();
            return 1;

        }

    }
}
