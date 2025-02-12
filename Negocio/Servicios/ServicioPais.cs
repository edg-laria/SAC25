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
   public class ServicioPais: ServicioBase
    {
        private PaisRepositorio paisRepositorio;

        public ServicioPais()
        {
            paisRepositorio = kernel.Get<PaisRepositorio>();
        }

        public List<PaisModel> GetAllPais()
        {
            try
            {
                var paises = Mapper.Map<List<Pais>, List<PaisModel>>(paisRepositorio.GetAllPais());              
                return paises;
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public PaisModel GetPais (int idPais)
        {
            Pais oPais = paisRepositorio.ObtenerPaisPorId(idPais);
            PaisModel oPaisModel = new PaisModel();

            oPaisModel.Id = oPais.Id;
            oPaisModel.Nombre = oPais.Nombre;
            oPaisModel.CodigoAfip = oPais.CodigoAfip;
            oPaisModel.Cuit = oPais.Cuit;
            oPaisModel.Activo = oPais.Activo;

            return oPaisModel;
        }

        public int GuardarPais(PaisModel oPaisModel)
        {
            //controlar que no exista 
            Pais oPais = paisRepositorio.ObtenerPaisPorNombre(oPaisModel.Nombre);
            if (oPais != null)
            {
                return -2;
            }
            else
            {
                Pais oPaisNuevo = new Pais();
                Pais oPaisRespuesta = new Pais();
                oPaisNuevo.Nombre = oPaisModel.Nombre;
                oPaisNuevo.CodigoAfip = oPaisModel.CodigoAfip;
                oPaisNuevo.Cuit = oPaisModel.Cuit;
                oPaisNuevo.Activo = oPaisModel.Activo;
                oPaisNuevo.IdUsuario = oPaisModel.IdUsuario;
                oPaisNuevo.UltimaModificacion = oPaisModel.UltimaModificacion;

                oPaisRespuesta = paisRepositorio.InsertarPais(oPaisNuevo);
                if (oPaisRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
                
            }
        }

        public int ActualizarPais(PaisModel oPaisModel)
        {
            //controlar que no exista 
            Pais oPais = paisRepositorio.ObtenerPaisPorNombre(oPaisModel.Nombre, oPaisModel.Id);
            if (oPais != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {
                Pais oPaisNuevo = new Pais();
                Pais oPaisRespuesta = new Pais();
                oPaisNuevo.Id = oPaisModel.Id;
                oPaisNuevo.Nombre = oPaisModel.Nombre;
                oPaisNuevo.CodigoAfip = oPaisModel.CodigoAfip;
                oPaisNuevo.Cuit = oPaisModel.Cuit;
                oPaisNuevo.Activo = oPaisModel.Activo;
                oPaisNuevo.UltimaModificacion = oPaisModel.UltimaModificacion;

                oPaisRespuesta = paisRepositorio.ActualizarPais(oPaisNuevo);

                if (oPaisRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int EliminarPais (int idPais)
        {
            var retorno = paisRepositorio.EliminarPais(idPais);
            if (retorno == 1)
            {
                return 0; //ok
            }
            else
            {
                return -1;//paso algo
            }
        }
    }
}
