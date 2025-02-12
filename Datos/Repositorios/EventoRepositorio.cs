using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Datos.ModeloDeDatos;

namespace Datos.Repositorios
{
    public class EventoRepositorio : RepositorioBase<Evento>
    {
       private SAC_Entities context;

        public EventoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
    
        public Evento CrearEvento(Evento evento)
        {
            return Insertar(evento);
        }

        public List<Evento> GetAllEvento()
        {
            return context.Evento.Where( e => e.Activo == true).ToList();
        }

        public Evento ObtenerEventoPorID(int id)
        {           
            return context.Evento.Where(pos => pos.Id == id && pos.Activo == true).FirstOrDefault(); 
        }

        public void EventoDelete(int id)
        {
            Evento evento = ObtenerEventoPorID(id);
            evento.Activo = false;
            context.Entry(evento).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
             
        }

        public void UpdateEvento(Evento evento)
        {
            context.Entry(evento).State = EntityState.Modified;
            context.SaveChanges();
        }

        public List<Prioridad> GetAllPrioridad()
        {
            return context.Prioridad.Where(p => p.Activo == true).ToList();
        }
    }
}