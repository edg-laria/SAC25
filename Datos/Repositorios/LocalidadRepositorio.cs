using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Datos.Repositorios
{
    public class LocalidadRepositorio : RepositorioBase<Localidad>
    {
        private SAC_Entities context;

        public LocalidadRepositorio(SAC_Entities contexto) : base(contexto)
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

        public Localidad ObtenerLocalidadPorNombre(string nombre, string CodigoPostal)
        {
            return context.Localidad.Where(p => p.Nombre == nombre && p.CodigoPostal == CodigoPostal).FirstOrDefault();
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
            listaLocalidad = listaLocalidad.OrderBy(p => p.Nombre).ToList();
            return listaLocalidad;
        }


        public List<Localidad> GetAllLocalidad(int idProvincia)
        {
            context.Configuration.LazyLoadingEnabled = false;
            if(idProvincia > 25) { idProvincia = 29; }  // LO ESTOY HACIENDO PARA QUE NO TOME LAS DEL EXTERIOR
            List<Localidad> listaLocalidad = context.Localidad.Where(p => p.Activo == true && p.IdProvincia == idProvincia).ToList();
            listaLocalidad = listaLocalidad.OrderBy(p => p.Nombre).ToList();
            return listaLocalidad;
        }

        public List<Localidad> GetAllLocalidad(int idPais, int idProvincia)
        {
            context.Configuration.LazyLoadingEnabled = false;
            if (idPais > 1) { idProvincia = 29; }
            List<Localidad> listaLocalidad = context.Localidad.Where(p => p.Activo == true && p.IdPais == idPais && p.IdProvincia == idProvincia).ToList();
            listaLocalidad = listaLocalidad.OrderBy(p => p.Nombre).ToList();
            return listaLocalidad;
        }

        public Localidad GetCodigoPostalDeLocalidad(int idLocalidad)
        {
            context.Configuration.LazyLoadingEnabled = false;
            Localidad oLocalidad = context.Localidad.Where(p => p.Activo == true && p.Id == idLocalidad).First();
            return oLocalidad;
        }
        public Localidad GetCodigoPostal(int idLocalidad)
        {
            context.Configuration.LazyLoadingEnabled = false;
            Localidad oLocalidad = context.Localidad.Where(p => p.Activo == true && p.Id == idLocalidad).First();
            return oLocalidad;
        }

        public Localidad GetIdLocalidadCodigoPostal(string oCodigoPostal)
        {
            context.Configuration.LazyLoadingEnabled = false;
            Localidad oLocalidad = context.Localidad.Where(p => p.Activo == true && p.CodigoPostal == oCodigoPostal).FirstOrDefault();
            return oLocalidad;
        }

        //public Localidad GetIdLocalidadCodigoPostal(int oCodigoPostal)
        //{
        //    context.Configuration.LazyLoadingEnabled = false;
        //    Localidad oLocalidad = context.Localidad.Where(p => p.Activo == true && p.Codigo == oCodigoPostal).First();
        //    return oLocalidad;
        //}



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