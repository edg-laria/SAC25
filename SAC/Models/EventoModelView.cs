using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class EventoModelView
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public bool TodoElDia { get; set; }
        public string Asisten { get; set; }
        public string Recibido { get; set; }
        public string Enviado { get; set; }
        public string Pasado { get; set; }
        public string Ubicacion { get; set; }
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Organizador { get; set; }
        public string Obs { get; set; }
        public int IdPrioridad { get; set; }
        public PrioridadModel Prioridad { get; set; }

    }
}