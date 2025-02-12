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
   public class ServicioProvincia:ServicioBase
    {
        private ProvinciaRepositorio provinciaRepositorio;

        public ServicioProvincia()
        {
            provinciaRepositorio = kernel.Get<ProvinciaRepositorio>();
        }

        public List<ProvinciaModel> GetAllProvinciasNombreId(int idPais)
        {
            try
            {
                var Provincias = Mapper.Map<List<Provincia>, List<ProvinciaModel>>(provinciaRepositorio.GetAllProvinciasNombreId(idPais));
                return Provincias;
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }


        public List<ProvinciaModel> GetAllProvincias(int idPais)
        {
            try
            {
                var Provincias = Mapper.Map<List<Provincia>, List<ProvinciaModel>>(provinciaRepositorio.GetAllProvincia(idPais));
                return Provincias;
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }


        public List<ProvinciaModel> GetAllProvincias()
        {
            try
            {
                var Provincias = Mapper.Map<List<Provincia>, List<ProvinciaModel>>(provinciaRepositorio.GetAllProvincia());
                return Provincias;
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke(ex.Message, "error");
                return null;
            }
        }


        public ProvinciaModel GetProvincia (int _id)
        {
            Provincia oProvincia = provinciaRepositorio.ObtenerProvinciaPorId(_id);
            ProvinciaModel oProvinciaModel = new ProvinciaModel();

            oProvinciaModel.Id = oProvincia.Id;
            oProvinciaModel.Nombre = oProvincia.Nombre;
            oProvinciaModel.Codigo = oProvincia.Codigo;
            oProvinciaModel.CodigoNumero = oProvincia.CodigoNumero;
            oProvinciaModel.CodigoAfip = oProvincia.CodigoAfip;
            oProvinciaModel.IdPais = oProvincia.IdPais;
            oProvinciaModel.Activo = oProvincia.Activo;

            return oProvinciaModel;
        }

        public int ActualizarPais(ProvinciaModel oProvinciaModel)
        {
            //controlar que no exista 
            Provincia oProvincia = provinciaRepositorio.ObtenerProvinciaPorNombre(oProvinciaModel.Nombre,oProvinciaModel.Codigo, oProvinciaModel.Id);
            if (oProvincia != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {
                Provincia oPaisNuevo = new Provincia();
                Provincia oPaisRespuesta = new Provincia();

                oPaisNuevo.Id = oProvinciaModel.Id;
                oPaisNuevo.Nombre = oProvinciaModel.Nombre;
                oPaisNuevo.Codigo = oProvinciaModel.Codigo;
                oPaisNuevo.CodigoAfip = oProvinciaModel.CodigoAfip;
                oPaisNuevo.CodigoNumero = oProvinciaModel.CodigoNumero;
                oPaisNuevo.IdPais = oProvinciaModel.IdPais;
                oPaisNuevo.Activo = true;

                oPaisRespuesta = provinciaRepositorio.ActualizarProvincia(oPaisNuevo);

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

        public int GuardarProvincia(ProvinciaModel oProvinciaModel)
        {
            //controlar que no exista 
            Provincia oProvincia = provinciaRepositorio.ObtenerProvinciaPorNombre(oProvinciaModel.Nombre,oProvinciaModel.Codigo);
            if (oProvincia != null)
            {
                return -2;
            }
            else
            {
                Provincia oProvinciaNuevo = new Provincia();
                Provincia oProvinciaRespuesta = new Provincia();

                oProvinciaNuevo.Nombre = oProvinciaModel.Nombre;
                oProvinciaNuevo.Codigo = oProvinciaModel.Codigo;
                oProvinciaNuevo.IdPais = oProvinciaModel.IdPais;
                oProvinciaNuevo.CodigoNumero = oProvinciaModel.CodigoNumero;
                oProvinciaNuevo.CodigoAfip = oProvinciaModel.CodigoAfip;
                oProvinciaNuevo.Activo = oProvinciaModel.Activo;
                oProvinciaNuevo.IdUsuario = oProvinciaModel.IdUsuario;
                oProvinciaNuevo.UltimaModificacion = oProvinciaModel.UltimaModificacion;

                oProvinciaRespuesta = provinciaRepositorio.InsertarProvincia(oProvinciaNuevo);
                if (oProvinciaRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int Eliminar(int idProvincia)
        {
            var retorno = provinciaRepositorio.EliminarProvincia(idProvincia);
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
