using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using Negocio.Modelos;
using AutoMapper;
using System.Net.Mail;
using System.IO;
using System.Net;
using Negocio.Servicios;
using System.Net.Mime;
using System.Text;

//de estas clases o capa paso a la de datos 
namespace Negocio.Servicios
{
   public class ServicioArticulo: ServicioBase
    {
        private ArticuloRepositorio oArticuloRepositorio;

        public ServicioArticulo()
        {
            oArticuloRepositorio = kernel.Get<ArticuloRepositorio>();
        }

        public List<ArticuloModel> GetAllArticulo()
        {
            try
            {
                var model= Mapper.Map<List<Articulo>, List<ArticuloModel>>(oArticuloRepositorio.GetAllArticulo());              
                return model;
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public List<ArticuloModel> GetArticulosPorCodigo(string strCodigo)
        {
            try
            {
                return Mapper.Map<List<Articulo>, List<ArticuloModel>>(oArticuloRepositorio.GetArticulosPorCodigo(strCodigo));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }
        public ArticuloModel GetArticuloPorId(int id)
        {
            return Mapper.Map<Articulo, ArticuloModel>(oArticuloRepositorio.GetArticuloPorId(id));
        }


        public ArticuloModel GuardarArticulo(ArticuloModel oArticuloModel)
        {
            try
            {
                var oModel = Mapper.Map<ArticuloModel, Articulo>(oArticuloModel);

                oModel.UltimaModificacion = DateTime.Now;
                oModel.Activo = true;
                return Mapper.Map<Articulo, ArticuloModel>(oArticuloRepositorio.Insertar(oModel));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


        public ArticuloModel ActualizarArticulo(ArticuloModel oArticuloModel)
        {
            try
            {
                var oModel = Mapper.Map<ArticuloModel, Articulo>(oArticuloModel);
                return Mapper.Map<Articulo, ArticuloModel>(oArticuloRepositorio.Actualizar(oModel));

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


        public int Eliminar(int Id)
        {
            var retorno = oArticuloRepositorio.DeleteArticulo(Id);
            if (retorno == 1)
            {
                return 0; //ok
            }
            else
            {
                return -1;//paso algo
            }
        }

     public ArticuloModel GetArticulo(int idArticulo)
        {
            ArticuloModel Articulo = Mapper.Map<Articulo, ArticuloModel>(oArticuloRepositorio.GetArticulo(idArticulo));
            return Articulo;
        }

        public ArticuloModel GetArticuloOuCodigo(string Codigo)
        {
            ArticuloModel Articulo = Mapper.Map<Articulo, ArticuloModel>(oArticuloRepositorio.GetArticuloOuCodigo(Codigo));
            return Articulo;
        }
    }
}
