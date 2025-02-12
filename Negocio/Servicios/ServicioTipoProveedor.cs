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
  public class ServicioTipoProveedor:ServicioBase
    {
        private TipoProveedorRepositorio TipoProveedorRepositorio;

        public ServicioTipoProveedor()
        {
            TipoProveedorRepositorio = kernel.Get<TipoProveedorRepositorio>();
        }


        public List<TipoProveedorModel> GetAllTipoProveedors()
        {
            try
            {
                var TipoProveedors = Mapper.Map<List<TipoProveedor>, List<TipoProveedorModel>>(TipoProveedorRepositorio.GetAllTipoProveedor());
                return TipoProveedors;
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke(ex.Message, "error");
                return null;
            }
        }


        public TipoProveedorModel GetTipoProveedor(int _id)
        {
            TipoProveedor oTipoProveedor = TipoProveedorRepositorio.GetTipoProveedorPorId(_id);
            TipoProveedorModel oTipoProveedorModel = new TipoProveedorModel();

            oTipoProveedorModel.Id = oTipoProveedor.Id;
            oTipoProveedorModel.Descripcion = oTipoProveedor.Descripcion;
            oTipoProveedorModel.Codigo = oTipoProveedor.Codigo;
           
            oTipoProveedorModel.Activo = oTipoProveedor.Activo;

            return oTipoProveedorModel;
        }

        public int ActualizarPais(TipoProveedorModel oTipoProveedorModel)
        {
            //controlar que no exista 
            TipoProveedor oTipoProveedor = TipoProveedorRepositorio.ObtenerTipoProveedorPorNombre(oTipoProveedorModel.Descripcion, oTipoProveedorModel.Codigo, oTipoProveedorModel.Id);
            if (oTipoProveedor != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {
                TipoProveedor oPaisNuevo = new TipoProveedor();
                TipoProveedor oPaisRespuesta = new TipoProveedor();

                oPaisNuevo.Id = oTipoProveedorModel.Id;
                oPaisNuevo.Descripcion = oTipoProveedorModel.Descripcion;
                oPaisNuevo.Codigo = oTipoProveedorModel.Codigo;
                oPaisNuevo.Activo = true;

                oPaisRespuesta = TipoProveedorRepositorio.ActualizarTipoProveedor(oPaisNuevo);

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

        public int GuardarTipoProveedor(TipoProveedorModel oTipoProveedorModel)
        {
            //controlar que no exista 
            TipoProveedor oTipoProveedor = TipoProveedorRepositorio.ObtenerTipoProveedorPorNombre(oTipoProveedorModel.Descripcion);
            if (oTipoProveedor != null)
            {
                return -2;
            }
            else
            {
                TipoProveedor oTipoProveedorNuevo = new TipoProveedor();
                TipoProveedor oTipoProveedorRespuesta = new TipoProveedor();

                oTipoProveedorNuevo.Descripcion = oTipoProveedorModel.Descripcion;
                oTipoProveedorNuevo.Codigo = oTipoProveedorModel.Codigo;
                oTipoProveedorNuevo.Activo = oTipoProveedorModel.Activo;
                oTipoProveedorNuevo.IdUsuario = oTipoProveedorModel.IdUsuario;
                oTipoProveedorNuevo.UltimaModificacion = oTipoProveedorModel.UltimaModificacion;

                oTipoProveedorRespuesta = TipoProveedorRepositorio.InsertarTipoProveedor(oTipoProveedorNuevo);
                if (oTipoProveedorRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int Eliminar(int idTipoProveedor)
        {
            var retorno = TipoProveedorRepositorio.DeleteTipoProveedor(idTipoProveedor);
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
