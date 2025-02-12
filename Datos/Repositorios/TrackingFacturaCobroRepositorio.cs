using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class TrackingFacturaCobroRepositorio : RepositorioBase<TrackingFacturaCobroVenta>
    {
       private SAC_Entities context;
    
        public TrackingFacturaCobroRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
            
        public TrackingFacturaCobroVenta CreateTrackingFacturaCobroVenta(TrackingFacturaCobroVenta model)
        {
           return  Insertar(model);
        }

        public List<TrackingFacturaCobroVenta> GetTrackingFacturaCobroVenta()
        {
            return context.TrackingFacturaCobroVenta.Where(acc => acc.Activo == true).ToList();
        }

      
    }
}