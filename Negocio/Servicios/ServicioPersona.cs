using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using AutoMapper;
using Negocio.Modelos;

namespace Negocio.Servicios
{   
    public class ServicioPersona : ServicioBase
    {
        private Datos.Repositorios.PersonaRepositorio _personaRepositorio;
#pragma warning disable CS0108 // 'ServicioPersona._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.
        public Action<string, string> _mensaje;
#pragma warning restore CS0108 // 'ServicioPersona._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.
        public ServicioPersona()
        {        
            _personaRepositorio = kernel.Get<PersonaRepositorio>();
        }

        public Modelos.PersonaModel CrearPersona(Modelos.PersonaModel persona)
        {
            try
            {
                Persona p = Mapper.Map< Modelos.PersonaModel, Persona>(persona);
            return Mapper.Map< Persona, Modelos.PersonaModel>(_personaRepositorio.CrearPersona(p));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }

    }

}
