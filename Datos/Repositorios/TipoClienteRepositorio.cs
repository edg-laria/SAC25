using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class TipoClienteRepositorio : RepositorioBase<TipoCliente>
    {

        private SAC_Entities context;

        public TipoClienteRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public TipoCliente InsertarTipoCliente(TipoCliente OTipoCliente)
        {
            return Insertar(OTipoCliente);
        }

        public List<TipoCliente> GetAllTipoCliente()
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<TipoCliente> listaTipoCliente = context.TipoCliente.ToList();
            return listaTipoCliente;
        }


   

    }
}
