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
   public class ServicioTarjeta : ServicioBase
    {
        private TarjetaRepositorio oTarjetaRepositorio;

        public ServicioTarjeta()
        {
            oTarjetaRepositorio = kernel.Get<TarjetaRepositorio>();
        }


        public List<TarjetaModel> GetAllTarjetas()
        {
            return Mapper.Map<List<Tarjetas>, List<TarjetaModel>>(oTarjetaRepositorio.GetAllTarjetas());
        }

    }
}
