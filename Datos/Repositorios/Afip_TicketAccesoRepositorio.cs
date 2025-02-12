using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
  public class Afip_TicketAccesoRepositorio : RepositorioBase<Afip_TicketAcceso>
    {
        private SAC_Entities context;

        public Afip_TicketAccesoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public Afip_TicketAcceso CrearTicketAcceso(Afip_TicketAcceso model)
        {
            return Insertar(model);
        }

        public Afip_TicketAcceso GetTicketAccesoUltimo()
        {
            return context.Afip_TicketAcceso
                    .OrderByDescending(p => p.fecha_expiracion)
                    .FirstOrDefault();
        }

        public Afip_TicketAcceso GetTicketAccesoUltimoPorServicio(string servicio)
        {
            return context.Afip_TicketAcceso.Where(p => p.servicio == servicio)
                    .OrderByDescending(p => p.fecha_expiracion)
                    .FirstOrDefault();
        }

    }
}
