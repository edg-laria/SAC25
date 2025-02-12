using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class TipoRetencionRepositorio : RepositorioBase<TipoRetencion>
    {
        private SAC_Entities context;

        public TipoRetencionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<TipoRetencion> GetAllTipoRetencion()
        {
            return context.TipoRetencion.Where(p => p.Activo == true).ToList();
        }

    }
}
