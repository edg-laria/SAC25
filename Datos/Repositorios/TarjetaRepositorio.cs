using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class TarjetaRepositorio : RepositorioBase<Tarjetas>
    {
        private SAC_Entities context;

        public TarjetaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<Tarjetas> GetAllTarjetas()
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.Tarjetas.ToList();

        }

        public void ConciliarMovimiento(int id)
        {
            throw new NotImplementedException();
        }
    }
}
