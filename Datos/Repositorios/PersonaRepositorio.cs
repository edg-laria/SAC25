using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class PersonaRepositorio : RepositorioBase<Persona>
    {
       private SAC_Entities context;
    
        public PersonaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
    
        public Persona CrearPersona(Persona persona)
        {
            return Insertar(persona);
        }

        public List<Persona> ObtenerPersonas()
        {
            return context.Persona.ToList();
        }

        public Persona ObtenerPersonaPorId(int id)
        {           
            return context.Persona.Where(per => per.Id == id).FirstOrDefault(); 
        }

        public Persona ObtenerPersonaPorDocumento(string documento)
        {
            return context.Persona.Where(per => per.Documento == documento).FirstOrDefault();
        }

        public Persona ActualizarPersonaPorDocumento(Persona personaParaActualizar)
        {
            Persona persona = ObtenerPersonaPorDocumento(personaParaActualizar.Documento);
            persona.Email = personaParaActualizar.Email ?? persona.Email;
            persona.CodigoValidacion = personaParaActualizar.CodigoValidacion ?? persona.CodigoValidacion;
            persona.TelefonoMovil = personaParaActualizar.TelefonoMovil ?? persona.TelefonoMovil;
            persona.TelefonoAlternativo = personaParaActualizar.TelefonoAlternativo ?? persona.TelefonoAlternativo;
            persona.TelefonoFijo = personaParaActualizar.TelefonoFijo ?? persona.TelefonoFijo;
            context.SaveChanges();
        
            return persona;
        }
        public Persona ActualizarPersona(Persona personaParaActualizar)
        {
            Persona persona = ObtenerPersonaPorId(personaParaActualizar.Id);
            persona.Email = personaParaActualizar.Email ?? persona.Email;
            persona.CodigoValidacion = personaParaActualizar.CodigoValidacion ?? persona.CodigoValidacion;
            persona.TelefonoMovil = personaParaActualizar.TelefonoMovil ?? persona.TelefonoMovil;
            persona.TelefonoAlternativo = personaParaActualizar.TelefonoAlternativo ?? persona.TelefonoAlternativo;
            persona.TelefonoFijo = personaParaActualizar.TelefonoFijo ?? persona.TelefonoFijo;
            context.SaveChanges();

            return persona;
        }

      
        public Persona BuscarPrePostulacionPorId(int id)
        {
            return context.Persona.Where(p => p.Id == id).FirstOrDefault();
        }

        public Persona BuscarPrePostulacion(string documento, string code)
        {
           return context.Persona.Where(p => p.CodigoValidacion == code && p.Documento == documento && p.Activo == false).FirstOrDefault();
        }

        public bool CodeDisponible(string code)
        {
            Persona perDB = context.Persona.Where(p => p.CodigoValidacion == code).FirstOrDefault(); 
            if ((perDB == null))
            {
                return  true;
            }
            else {
                return false;
            }
        }

        //public  List<Especialidad> ObtenerListaEspecialidadPorTipoPos(int idTipoPostulacion)
        //{
        //    return  context.Especialidad
        //            .Where(c => c.Vacante.Any(i => i.idTipoPostulacion == idTipoPostulacion))
        //              .ToList();
        //}

        //public List<Unidad> ObtenerListaUnidadPorEspecialidad(int idEspecialidad, int idTipoPostulacion)
        //{
        //    return context.Unidad
        //           .Where(c => c.Vacante.Any(i => i.idTipoPostulacion == idTipoPostulacion && i.idEspecialidad == idEspecialidad))
        //           .ToList();
        //}
    }
}