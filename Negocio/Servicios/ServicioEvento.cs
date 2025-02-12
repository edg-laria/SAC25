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
   
   public class ServicioEvento : ServicioBase
    {
        private Datos.Repositorios.EventoRepositorio _eventoRepositorio;
        public Action<string, string> _mensaje;
        public ServicioEvento()
        {
            _eventoRepositorio = kernel.Get<EventoRepositorio>();
        }
        public List<EventoModel> GetAllEvento()
        {
            try
            {
                var eventos  = Mapper.Map<List<Evento>, List<EventoModel> > (_eventoRepositorio.GetAllEvento());

                return eventos;
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
        }

        public EventoModel CrearEvento(EventoModel nuevoEvento)
        {
            try
            {           
            Evento Evento = Mapper.Map<EventoModel, Evento>(nuevoEvento);
               
            var evento =   Mapper.Map<Evento, EventoModel>(_eventoRepositorio.CrearEvento(Evento));
 _mensaje("Evento Guardado correctamente!", "sucesso");
                return evento;
            }
            catch(Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor" + ex.Message, "erro");
                return new EventoModel();
            }
        }

        public List<PrioridadModel> GetAllPrioridad()
        {
            try
            {
                return Mapper.Map< List<Prioridad>, List<PrioridadModel>>(_eventoRepositorio.GetAllPrioridad());

            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return new List<PrioridadModel>();
            }
        }

        public EventoModel getEventoPorID(int id   )
        {
            return Mapper.Map<Evento, EventoModel>(_eventoRepositorio.ObtenerEventoPorID(id));
        }

        public void Delete(int id)
        {
            try
            {
                _eventoRepositorio.EventoDelete(id);
                _mensaje("Evento elimindado correctamente!", "sucesso");
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");                
            }

        }

        public void UpdateEvento(EventoModel updateEvento)
        {
            try
            {
                Evento evento = Mapper.Map<EventoModel, Evento>(updateEvento);
                _eventoRepositorio.UpdateEvento(evento);
                _mensaje("Evento Actualizado correctamente!", "sucesso");
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
            }


        }
    }

}
