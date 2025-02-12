using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class TipoIdiomaRepositorio : RepositorioBase<TipoIdioma>
    {

        private SAC_Entities context;

        public TipoIdiomaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        #region "Metodos de Actualizacion"

        /// <summary>
        ///  Agregar Tipo Idioma
        /// </summary>

        public TipoIdioma InsertarTipoIdioma(TipoIdioma model)
        {
            return Insertar(model);
        }



        /// <summary>
        ///  Modificar Tipo de Idioma
        /// </summary>



        public TipoIdioma ActualizarTipoIdioma(TipoIdioma model)
        {
            TipoIdioma TipoIdiomaExistente =ObtenerTipoIdiomaPorId(model.Id);

            TipoIdiomaExistente.Id = model.Id;
            TipoIdiomaExistente.Descripcion = model.Descripcion;
            

            context.SaveChanges();
            return TipoIdiomaExistente;
        }





        #endregion



        #region "Metodos de Lectura"

        /// <summary>
        /// Listar todos lo tipos de Idioma
        /// </summary>


        public List<TipoIdioma> GetAllTipoIdioma()
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<TipoIdioma> Lista = context.TipoIdioma.ToList();
            return Lista;
        }


        /// <summary>
        /// Obtener Idioma por Id
        /// </summary>



        public TipoIdioma ObtenerTipoIdiomaPorId(int idTipoIva)
        {
            var TipoIdioma = context.TipoIdioma.Where(p => p.Id == idTipoIva).FirstOrDefault();
            return TipoIdioma;
        }

      
      


        #endregion
       

    }
}
