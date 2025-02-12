using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;

namespace Negocio.Servicios
{
  public class ServicioCobroFacturaModo : ServicioBase
    {

        private CobroFacturaModoRepositorio cobroFacturaModoRepositorio;


        public ServicioCobroFacturaModo()
        {
            cobroFacturaModoRepositorio = kernel.Get<CobroFacturaModoRepositorio>();
        }


        public FactVentaCobro InsertarCobroModo(CobroFacturaModoModel cobroModo)
        {
            
          return cobroFacturaModoRepositorio.InsertarCobroModo(Mapper.Map<CobroFacturaModoModel, FactVentaCobro>(cobroModo));
           
        }


    }
}
