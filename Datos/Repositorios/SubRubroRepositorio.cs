using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class SubRubroRepositorio : RepositorioBase<SubRubro>
    {
        private SAC_Entities context;

        public SubRubroRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public SubRubro InsertarSubRubro(SubRubro SubRubro)
        {
            return Insertar(SubRubro);
        }


        public SubRubro ObtenerSubRubro(int idSubRubro)
        {
            SubRubro SubRubro = context.SubRubro.Where(p => p.Id == idSubRubro).First();
            return SubRubro;
        }


        public SubRubro ObtenerSubRubroPorId(int idSubRubro)
        {
            var SubRubro = context.SubRubro.Where(p => p.Id == idSubRubro).FirstOrDefault();
            return SubRubro;
        }

        public SubRubro ActualizarSubRubro(SubRubro model)
        {
            SubRubro SubRubroExistente = ObtenerSubRubroPorId(model.Id);

            SubRubroExistente.Id = model.Id;
          
            SubRubroExistente.Descripcion = model.Descripcion;
            SubRubroExistente.Activo = model.Activo;

            context.SaveChanges();
            return SubRubroExistente;
        }

        public SubRubro ObtenerSubRubroPorNombre(string nombre)
        {
            return context.SubRubro.Where(p => p.Descripcion == nombre).FirstOrDefault();
        }

        public SubRubro ObtenerSubRubroPorNombre(string nombre, string codigo)
        {
            return context.SubRubro.Where(p => p.Descripcion == nombre).FirstOrDefault();
        }


        /// <summary>
        /// verifica que el nombre ingresado no exista para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idSubRubro"></param>
        /// <returns></returns>
        public SubRubro ObtenerSubRubroPorNombre(string nombre, string codigo, int idSubRubro)
        {
            return context.SubRubro.Where(p => p.Descripcion == nombre && p.Id != idSubRubro).FirstOrDefault();
        }


        public List<SubRubro> GetAllSubRubro()
        {
            List<SubRubro> listaSubRubro = context.SubRubro.ToList();
            return listaSubRubro;
        }
       

        public int EliminarSubRubro(int idSubRubro)
        {
            SubRubro SubRubroExistente = ObtenerSubRubroPorId(idSubRubro);
            SubRubroExistente.Activo = false;
            context.SaveChanges();
            return 1;

            //var oSubRubro = context.SubRubro.Where(r => r.Id == idSubRubro).FirstOrDefault();
            //context.SubRubro.Remove(oSubRubro);
            //var retorno = context.SaveChanges();
            //return retorno;
        }



    }
}
