using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class LocalidadRespositorio: RepositorioBase<Localidad>
    {
        private SAC_Entities context;

        public LocalidadRespositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public Localidad InsertarLocalidad(Localidad Localidad)
        {
            return Insertar(Localidad);
        }


        public Localidad ObtenerLocalidad(int idLocalidad)
        {
            Localidad Localidad = context.Localidad.Where(p => p.Id == idLocalidad).First();
            return Localidad;
        }


        public Localidad ObtenerLocalidadPorId(int idLocalidad)
        {
            return context.Localidad.Where(p => p.Id == idLocalidad).FirstOrDefault();
        }

        public Localidad ActualizarLocalidad(Localidad model)
        {
            Localidad LocalidadExistente = ObtenerLocalidadPorId(model.Id);

            LocalidadExistente.Id = model.Id;
            LocalidadExistente.Nombre = model.Nombre;
            LocalidadExistente.CodigoPostal = model.CodigoPostal;
            LocalidadExistente.IdPais = model.IdPais;
            LocalidadExistente.Activo = model.Activo;

            context.SaveChanges();
            return LocalidadExistente;
        }

        public Localidad ObtenerLocalidadPorNombre(string nombre)
        {
            return context.Localidad.Where(p => p.Nombre == nombre).FirstOrDefault();
        }

        public Localidad ObtenerLocalidadPorNombre(string nombre, string codigo)
        {
            return context.Localidad.Where(p => p.Nombre == nombre && p.CodigoPostal == codigo).FirstOrDefault();
        }


        /// <summary>
        /// verifica que el nombre ingresado no exista para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idLocalidad"></param>
        /// <returns></returns>
        public Localidad ObtenerLocalidadPorNombre(string nombre, string CodigoPostal, int idLocalidad)
        {
            return context.Localidad.Where(p => p.Nombre == nombre && p.CodigoPostal == CodigoPostal && p.Id != idLocalidad).FirstOrDefault();
        }
       

        public List<Localidad> GetAllLocalidad()
        {
            List<Localidad> listaLocalidad = context.Localidad.Where(p => p.Activo == true).ToList();
            return listaLocalidad;
        }


        public List<Localidad> GetAllLocalidad(int idProvincia)
        {
            List<Localidad> listaLocalidad = context.Localidad.Where(p => p.Activo == true && p.IdProvincia == idProvincia).ToList();
            return listaLocalidad;
        }

        public List<Localidad> GetAllLocalidad(int idPais, int idProvincia)
        {
            List<Localidad> listaLocalidad = context.Localidad.Where(p => p.Activo == true && p.IdPais == idPais && p.IdProvincia == idProvincia).ToList();
            return listaLocalidad;
        }

        public Localidad GetCodigoPostal(int idProvincia)
        {
            Localidad oLocalidad = context.Localidad.Where(p => p.Activo == true && p.IdProvincia == idProvincia).First();
            return oLocalidad;
        }


        public int EliminarLocalidad(int idLocalidad)
        {
            Localidad LocalidadExistente = ObtenerLocalidadPorId(idLocalidad);
            LocalidadExistente.Activo = false;
            context.SaveChanges();
            return 1;

            //var oLocalidad = context.Localidad.Where(r => r.Id == idLocalidad).FirstOrDefault();
            //context.Localidad.Remove(oLocalidad);
            //var retorno = context.SaveChanges();
            //return retorno;
        }


    }
}
