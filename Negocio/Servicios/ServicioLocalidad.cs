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

namespace Negocio.Servicios
{
   public class ServicioLocalidad : ServicioBase
    {
        private LocalidadRepositorio LocalidadRepositorio;

        public ServicioLocalidad()
        {
            LocalidadRepositorio = kernel.Get<LocalidadRepositorio>();
        }

        public List<LocalidadModel> GetAllLocalidads(int idPais, int idProvincia)
        {
            try
            {
                var Localidads = Mapper.Map<List<Localidad>, List<LocalidadModel>>(LocalidadRepositorio.GetAllLocalidad(idPais, idProvincia));
                return Localidads;
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

         public List<LocalidadModel> GetAllLocalidads(int idProvincia)
        {
            try
            {
                var Localidads = Mapper.Map<List<Localidad>, List<LocalidadModel>>(LocalidadRepositorio.GetAllLocalidad(idProvincia));
                
                return Localidads;
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public string GetCodigoPostalDeLocalidad(int idLocalidad)
        {
            try
            {
                var Localidads = LocalidadRepositorio.GetCodigoPostal(idLocalidad);
                return Localidads.CodigoPostal;
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return string.Empty;
            }
        }
   

    

        public string GetCodigoPostal(int idLocalidad)
        {
            try
            {
                var Localidads = LocalidadRepositorio.GetCodigoPostal(idLocalidad);
                return Localidads.CodigoPostal;
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return string.Empty;
            }
        }

        public string GetIdLocalidadCodigoPostal(int idLocalidad)
        {
            try
            {
                var Localidads = LocalidadRepositorio.GetCodigoPostal(idLocalidad);
                return Localidads.CodigoPostal;
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return string.Empty;
            }
        }


        public List<LocalidadModel> GetAllLocalidads()
        {
            try
            {
                var Localidads = Mapper.Map<List<Localidad>, List<LocalidadModel>>(LocalidadRepositorio.GetAllLocalidad());
                return Localidads;
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke(ex.Message, "error");
                return null;
            }
        }


        public LocalidadModel GetLocalidad(int _id)
        {
            Localidad oLocalidad = LocalidadRepositorio.ObtenerLocalidadPorId(_id);
            LocalidadModel oLocalidadModel = new LocalidadModel();
            //en el atriburo codigo doy la opcion que si no trae grabe 0
            oLocalidadModel.Id = oLocalidad.Id;
            oLocalidadModel.Nombre = oLocalidad.Nombre;
            oLocalidadModel.CodigoPostal = oLocalidad.CodigoPostal ;
            oLocalidadModel.IdPais = oLocalidad.IdPais;
            oLocalidadModel.IdProvincia = oLocalidad.IdProvincia;
            oLocalidadModel.Activo = oLocalidad.Activo;

            return oLocalidadModel;
        }

        public int ActualizarPais(LocalidadModel oLocalidadModel)
        {
            //controlar que no exista 
            Localidad oLocalidad = LocalidadRepositorio.ObtenerLocalidadPorNombre(oLocalidadModel.Nombre, oLocalidadModel.CodigoPostal, oLocalidadModel.Id);
            if (oLocalidad != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {
                Localidad oLocalidadNuevo = new Localidad();
                Localidad oLocalidadRespuesta = new Localidad();

                oLocalidadNuevo.Id = oLocalidadModel.Id;
                oLocalidadNuevo.Nombre = oLocalidadModel.Nombre;
                oLocalidadNuevo.CodigoPostal = oLocalidadModel.CodigoPostal;
                oLocalidadNuevo.CodigoProvincia = oLocalidadModel.CodigoProvincia;
                oLocalidadNuevo.IdPais = oLocalidadModel.IdPais;
                oLocalidadNuevo.IdProvincia = oLocalidadModel.IdProvincia;
                oLocalidadNuevo.Activo = true;

                oLocalidadRespuesta = LocalidadRepositorio.ActualizarLocalidad(oLocalidadNuevo);

                if (oLocalidadRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int GuardarLocalidad(LocalidadModel oLocalidadModel)
        {
            //controlar que no exista 
            Localidad oLocalidad = LocalidadRepositorio.ObtenerLocalidadPorNombre(oLocalidadModel.Nombre, oLocalidadModel.CodigoPostal, oLocalidadModel.Id);
            if (oLocalidad != null)
            {
                return -2;
            }
            else
            {
                Localidad oLocalidadNuevo = new Localidad();
                Localidad oLocalidadRespuesta = new Localidad();

                oLocalidadNuevo.Nombre = oLocalidadModel.Nombre;
                oLocalidadNuevo.CodigoPostal = oLocalidadModel.CodigoPostal;
                oLocalidadNuevo.IdPais = oLocalidadModel.IdPais;
                oLocalidadNuevo.IdProvincia = oLocalidadModel.IdProvincia;
                oLocalidadNuevo.CodigoProvincia = oLocalidadModel.CodigoProvincia;
                oLocalidadNuevo.Activo = oLocalidadModel.Activo;
                oLocalidadNuevo.IdUsuario = oLocalidadModel.IdUsuario;
                oLocalidadNuevo.UltimaModificacion = oLocalidadModel.UltimaModificacion;

                oLocalidadRespuesta = LocalidadRepositorio.InsertarLocalidad(oLocalidadNuevo);
                if (oLocalidadRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int Eliminar(int idLocalidad)
        {
            var retorno = LocalidadRepositorio.EliminarLocalidad(idLocalidad);
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
