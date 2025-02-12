using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class TipoIvaRepositorio : RepositorioBase<TipoIva>
    {

        private SAC_Entities context;

        public TipoIvaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public TipoIva InsertarTipoIva(TipoIva TipoIva)
        {
            return Insertar(TipoIva);
        }

        public List<TipoIva> GetAllTipoIva()
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<TipoIva> listaTipoIva = context.TipoIva.ToList();
            return listaTipoIva;
        }


        public TipoIva ObtenerTipoIva(int idTipoIva)
        {
            context.Configuration.LazyLoadingEnabled = false;
            TipoIva TipoIva = context.TipoIva.Where(p => p.Id == idTipoIva).First();
            return TipoIva;
        }

        public TipoIva ObtenerTipoIvaPorId(int idTipoIva)
        {
            var TipoIva = context.TipoIva.Where(p => p.Id == idTipoIva).FirstOrDefault();
            return TipoIva;
        }

        public TipoIva ActualizarTipoIva(TipoIva model)
        {
            TipoIva TipoIvaExistente = ObtenerTipoIvaPorId(model.Id);

            TipoIvaExistente.Id = model.Id;
            TipoIvaExistente.Descripcion = model.Descripcion;
            TipoIvaExistente.Porcentaje = model.Porcentaje;
            TipoIvaExistente.Proveedor = model.Proveedor;
            TipoIvaExistente.Tipo = model.Tipo;

            context.SaveChanges();
            return TipoIvaExistente;
        }

        public TipoIva ObtenerTipoIvaPorNombre(string nombre)
        {
            return context.TipoIva.Where(p => p.Descripcion == nombre).FirstOrDefault();
        }

        //public int EliminarTipoIva(int idTipoIva)
        //{
        //    TipoIva TipoIvaExistente = ObtenerTipoIvaPorId(idTipoIva);
        //    TipoIvaExistente.Activo = false;
        //    context.SaveChanges();
        //    return 1;

        //    //var oTipoIva = context.TipoIva.Where(r => r.Id == idTipoIva).FirstOrDefault();
        //    //context.TipoIva.Remove(oTipoIva);
        //    //var retorno = context.SaveChanges();
        //    //return retorno;
        //}

    }
}
