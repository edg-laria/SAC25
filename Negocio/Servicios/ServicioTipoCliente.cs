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
 public class ServicioTipoCliente: ServicioBase
    {
        private TipoClienteRepositorio oTipoClienteRepositorio;
       

        public ServicioTipoCliente()
        {
            oTipoClienteRepositorio = kernel.Get<TipoClienteRepositorio>();
        }

        public List<TipoClienteModel> GetAllTipoCliente()
        {
            return Mapper.Map<List<TipoCliente>, List<TipoClienteModel>>(oTipoClienteRepositorio.GetAllTipoCliente());
        }


    }
}
