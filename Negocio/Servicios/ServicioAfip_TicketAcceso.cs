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
  public class ServicioAfip_TicketAcceso : ServicioBase
    {

        private Afip_TicketAccesoRepositorio oAfip_TicketAccesoRepositorio;

        public ServicioAfip_TicketAcceso()
        {
            oAfip_TicketAccesoRepositorio = kernel.Get<Afip_TicketAccesoRepositorio>();
        }


        public Afip_TicketAccesoModel CrearTicketAcceso(Afip_TicketAccesoModel oArticuloModel)
        {
            try
            {
                var oModel = Mapper.Map<Afip_TicketAccesoModel, Afip_TicketAcceso>(oArticuloModel);
                return Mapper.Map<Afip_TicketAcceso, Afip_TicketAccesoModel>(oAfip_TicketAccesoRepositorio.Insertar(oModel));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


        public Afip_TicketAccesoModel GetTicketAccesoUltimo()
        {
            Afip_TicketAccesoModel Articulo = Mapper.Map<Afip_TicketAcceso, Afip_TicketAccesoModel>(oAfip_TicketAccesoRepositorio.GetTicketAccesoUltimo());
            return Articulo;
        }


        public Afip_TicketAccesoModel GetTicketAccesoUltimoPorServicio(string servicio)
        {
            Afip_TicketAccesoModel Articulo = Mapper.Map<Afip_TicketAcceso, Afip_TicketAccesoModel>(oAfip_TicketAccesoRepositorio.GetTicketAccesoUltimoPorServicio(servicio));
            return Articulo;
        }

    }
}
