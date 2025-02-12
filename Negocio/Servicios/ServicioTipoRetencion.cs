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
  public  class ServicioTipoRetencion : ServicioBase
    {

        private TipoRetencionRepositorio TipoProveedorRepositorio;

        public ServicioTipoRetencion()
        {
            TipoProveedorRepositorio = kernel.Get<TipoRetencionRepositorio>();
        }

        public List<TipoRetencionModel> GetAllTipoRetencion()
        {
            try
            {
                return  Mapper.Map<List<TipoRetencion>, List<TipoRetencionModel>>(TipoProveedorRepositorio.GetAllTipoRetencion());
                
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke(ex.Message, "error");
                return null;
            }
        }


    }
}
