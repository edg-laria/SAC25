using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class TipoPagoRepositorio : RepositorioBase<TipoPago>
    {
        private SAC_Entities context;

        public TipoPagoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<TipoPago> GetAllTipoPago()
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.TipoPago.ToList();

        }


    }
}
