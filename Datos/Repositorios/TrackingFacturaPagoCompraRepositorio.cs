using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class TrackingFacturaPagoCompraRepositorio : RepositorioBase<TrackingFacturaPagoCompra>
    {
       private SAC_Entities context;
    
        public TrackingFacturaPagoCompraRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
            
        public TrackingFacturaPagoCompra CreateTrackingFacturaPagoCompra(TrackingFacturaPagoCompra model)
        {
           return  Insertar(model);
        }

        public List<TrackingFacturaPagoCompra> GetTrackingFacturaPagoCompra()
        {
            return context.TrackingFacturaPagoCompra.Where(acc => acc.Activo == true).ToList();
        }

      
    }
}