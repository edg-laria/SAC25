using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Datos.Repositorios
{
    public class PaisRepositorio : RepositorioBase<Pais>
    {
        private SAC_Entities context;

        public PaisRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public Pais InsertarPais(Pais pais)
        {
            return Insertar(pais);
        }


        public Pais ObtenerPais(int idPais)
        {
            Pais pais = context.Pais.Where(p => p.Id == idPais).First();
            return pais;
        }



        //public List<Pais> ObtenerPais(int idPais)
        //{
        //    List<Pais> pais = context.Pais.Where(p => p.Id == idPais).ToList();
        //    return pais;
        //}



        public Pais ObtenerPaisPorId(int idPais)
        {
            var pais = context.Pais.Where(p => p.Id == idPais).FirstOrDefault();
            return pais;
        }

        public Pais ActualizarPais(Pais model)
        {
            Pais paisExistente = ObtenerPaisPorId(model.Id);

            paisExistente.Id = model.Id;
            paisExistente.Nombre = model.Nombre;
            paisExistente.CodigoAfip = model.CodigoAfip;
            paisExistente.Cuit = model.Cuit;
            paisExistente.Activo = model.Activo;

            context.SaveChanges();
            return paisExistente;
        }

        public Pais ObtenerPaisPorNombre(string nombre)
        {
            return context.Pais.Where(p => p.Nombre == nombre).FirstOrDefault();
        }


        /// <summary>
        /// verifica que el nombre ingresado no exista para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idPais"></param>
        /// <returns></returns>
        public Pais ObtenerPaisPorNombre(string nombre, int idPais)
        {
            return context.Pais.Where(p => p.Nombre == nombre && p.Id != idPais).FirstOrDefault();
        }


        public Pais ObtenerPaisPorCodigoAfip(string codigoafip)
        {
            return context.Pais.Where(p => p.CodigoAfip == codigoafip).FirstOrDefault();
            //return context.Pais.FirstOrDefault(l => l.CodigoAfip == codigoafip);
        }


        public List<Pais> GetAllPais()
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Pais> listaPais = context.Pais.Where(p => p.Activo == true).ToList();

            return listaPais;
        }

        public int EliminarPais(int idPais)
        {
            Pais paisExistente = ObtenerPaisPorId(idPais);
            paisExistente.Activo = false;
            context.SaveChanges();
            return 1;

            //var oPais = context.Pais.Where(r => r.Id == idPais).FirstOrDefault();
            //context.Pais.Remove(oPais);
            //var retorno = context.SaveChanges();
            //return retorno;
        }


    }
}