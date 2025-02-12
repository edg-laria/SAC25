using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class ArticuloRepositorio : RepositorioBase<Articulo>
    {
       private SAC_Entities context;
    
        public ArticuloRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

 public List<Articulo> GetAllArticulo()
        {
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.Articulo.Where(acc => acc.Activo == true).ToList();
            List<Articulo> listaArticulo = context.Articulo.Where(p => p.Activo == true).OrderBy(acc => acc.Id).ToList();
            return listaArticulo;
        }
        public Articulo GetArticulo(int idArticulo)
        {
            //context.Configuration.LazyLoadingEnabled = false;
            Articulo Articulo = context.Articulo.Where(p => p.Activo == true && p.Id== idArticulo).First();
            return Articulo;
        }


  public List<Articulo> GetArticulosPorCodigo(string strCodigo)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Articulo> listaArticulos = context.Articulo
              .Where(p => p.Activo == true && p.Codigo.Contains(strCodigo)).ToList();

            return listaArticulos;
        }

        public Articulo GetArticuloOuCodigo(string strCodigo)
        {
            context.Configuration.LazyLoadingEnabled = false;
            Articulo Articulo = context.Articulo.Where(p => p.Activo == true && p.Codigo.Contains(strCodigo)).First();
            return Articulo;
        }

            
        public Articulo CrearArticulo(Articulo model)
        {
           return  Insertar(model);
        }


        public Articulo Actualizar(Articulo Model)
        
        {
            Articulo ArticuloExistente = GetArticuloPorId(Model.Id);

            ArticuloExistente.Id = Model.Id;
            ArticuloExistente.Codigo = Model.Codigo;
            ArticuloExistente.DescripcionCastellano = Model.DescripcionCastellano;
            ArticuloExistente.DescripcionIngles = Model.DescripcionIngles;
            ArticuloExistente.Grupo = Model.Grupo;

            ArticuloExistente.Honorario = Model.Honorario;
            ArticuloExistente.Tipo = Model.Tipo;
            ArticuloExistente.Imputacion = Model.Imputacion;
            ArticuloExistente.Activo = true;
            ArticuloExistente.IdUsuario = Model.IdUsuario;
            ArticuloExistente.UltimaModificacion = Model.UltimaModificacion;
            ArticuloExistente.IdUsuario = Model.IdUsuario;

            context.SaveChanges();

            return ArticuloExistente;
        }



        public int DeleteArticulo(int Id)
        {
            Articulo Model = GetArticuloPorId(Id);
            Model.Activo = false;
            Model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }


       public Articulo GetArticuloPorId(int id)
        {           
            return context.Articulo.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }


      



   
    }
}