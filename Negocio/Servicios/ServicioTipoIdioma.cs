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
 public class ServicioTipoIdioma: ServicioBase
    {
        private TipoIdiomaRepositorio oTipoIdiomaRepositorio;
       

        public ServicioTipoIdioma()
        {
            oTipoIdiomaRepositorio = kernel.Get<TipoIdiomaRepositorio>();
        }

        public List<TipoIdiomaModel> GetAllTipoIdioma()
        {
            return Mapper.Map<List<TipoIdioma>, List<TipoIdiomaModel>>(oTipoIdiomaRepositorio.GetAllTipoIdioma());
        }


    }
}
