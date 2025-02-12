using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
    public class ProvinciaRepositorio : RepositorioBase<Provincia>
    {
        private SAC_Entities context;

        public ProvinciaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public Provincia InsertarProvincia(Provincia Provincia)
        {
            return Insertar(Provincia);
        }


        public Provincia ObtenerProvincia(int idProvincia)
        {
            Provincia Provincia = context.Provincia.Where(p => p.Id == idProvincia).First();
            return Provincia;
        }


        public Provincia ObtenerProvinciaPorId(int idProvincia)
        {
            var Provincia = context.Provincia.Where(p => p.Id == idProvincia).FirstOrDefault();
            return Provincia;
        }

        public Provincia ActualizarProvincia(Provincia model)
        {
            Provincia ProvinciaExistente = ObtenerProvinciaPorId(model.Id);

            ProvinciaExistente.Id = model.Id;
            ProvinciaExistente.Nombre = model.Nombre;
            ProvinciaExistente.CodigoAfip = model.CodigoAfip;
            ProvinciaExistente.Codigo = model.Codigo;
            ProvinciaExistente.CodigoNumero = model.CodigoNumero;
            ProvinciaExistente.IdPais = model.IdPais;
            ProvinciaExistente.Activo = model.Activo;

            context.SaveChanges();
            return ProvinciaExistente;
        }

        public Provincia ObtenerProvinciaPorNombre(string nombre)
        {
            return context.Provincia.Where(p => p.Nombre == nombre).FirstOrDefault();
        }

        public Provincia ObtenerProvinciaPorNombre(string nombre, string codigo)
        {
            return context.Provincia.Where(p => p.Nombre == nombre && p.Codigo == codigo).FirstOrDefault();
        }

      
        /// <summary>
        /// verifica que el nombre ingresado no exista para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idProvincia"></param>
        /// <returns></returns>
        public Provincia ObtenerProvinciaPorNombre(string nombre, string codigo,int idProvincia)
        {
            return context.Provincia.Where(p => p.Nombre == nombre && p.Codigo == codigo && p.Id != idProvincia).FirstOrDefault();
        }


        public Provincia ObtenerProvinciaPorCodigoAfip(int codigoafip)
        {
            return context.Provincia.Where(p => p.CodigoAfip == codigoafip).FirstOrDefault();
            //return context.Provincia.FirstOrDefault(l => l.CodigoAfip == codigoafip);
            
        }


        public List<Provincia> GetAllProvinciasNombreId(int idPais)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Provincia> listaProvincia = context.Provincia.Where(p => p.Activo == true && p.IdPais == idPais).ToList();
            listaProvincia = listaProvincia.OrderBy(p => p.Nombre).ToList();
            return listaProvincia;
        }


        public List<Provincia> GetAllProvincia()
        {
            List<Provincia> listaProvincia = context.Provincia.Where(p => p.Activo == true).ToList();
            listaProvincia = listaProvincia.OrderBy(p => p.Nombre).ToList();
            return listaProvincia;
        }
        public List<Provincia> GetAllProvincia(int idPais)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Provincia> listaProvincia = context.Provincia
                                            .Where(p => p.Activo == true && p.IdPais == idPais).ToList();
            listaProvincia = listaProvincia.OrderBy(p => p.Nombre).ToList();
            return listaProvincia;
        }

        public int EliminarProvincia(int idProvincia)
        {
            Provincia ProvinciaExistente = ObtenerProvinciaPorId(idProvincia);
            ProvinciaExistente.Activo = false;
            context.SaveChanges();
            return 1;

            //var oProvincia = context.Provincia.Where(r => r.Id == idProvincia).FirstOrDefault();
            //context.Provincia.Remove(oProvincia);
            //var retorno = context.SaveChanges();
            //return retorno;
        }


    }
}