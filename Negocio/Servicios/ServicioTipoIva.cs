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
 public class ServicioTipoIva: ServicioBase
    {
        private TipoIvaRepositorio oTipoIvaRepositorio;
#pragma warning disable CS0108 // 'ServicioTipoIva._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.
        public Action<string, string> _mensaje;
#pragma warning restore CS0108 // 'ServicioTipoIva._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.

        public ServicioTipoIva()
        {
            oTipoIvaRepositorio = kernel.Get<TipoIvaRepositorio>();
        }

        public List<TipoIvaModel> GetAllTipoIva()
        {
            return Mapper.Map<List<TipoIva>, List<TipoIvaModel>>(oTipoIvaRepositorio.GetAllTipoIva());
        }


    }
}
