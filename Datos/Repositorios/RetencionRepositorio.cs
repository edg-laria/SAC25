using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
  public class RetencionRepositorio : RepositorioBase<Retencion>
    {

        private SAC_Entities context;

        public RetencionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<Retencion> GetAllRetencion()
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.Retencion.Where(p => p.Activo == true).ToList();

        }

        public List<Retencion> GetAllRetencion(int idFactura)
        {
            context.Configuration.LazyLoadingEnabled = false;

            return context.Retencion
                        .Include("TipoRetencion")
                        .Include("Provincia")
                        .Include("CompraFactura")
                        .Where(p => p.IdCompraFactura == idFactura && p.Activo == true).ToList();
        }
        public List<Retencion> GetAllRetencionVenta(int idFactura)
        {
            context.Configuration.LazyLoadingEnabled = false;

            return context.Retencion
                        .Include("TipoRetencion")
                        .Include("Provincia")
                        .Include("FactVenta")
                        .Where(p => p.IdFactVenta == idFactura && p.Activo == true).ToList();
        }
        public Retencion GetRetencionOu (int idRetencion)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Retencion.Where(p => p.Id == idRetencion && p.Activo == true).First();

        }

        public Retencion Agregar(Retencion oRetencion)
        {
            return Insertar(oRetencion);
        }

        public Retencion Eliminar(Retencion oRetencion)
        {
            Retencion RetencionEliminar = GetRetencionOu(oRetencion.Id);
            if (RetencionEliminar != null)
            {
            RetencionEliminar.Activo = false;
            RetencionEliminar.UltimaModificacion = DateTime.Now;
            context.SaveChanges();
            }
      
            return RetencionEliminar;

        }


    }
}
