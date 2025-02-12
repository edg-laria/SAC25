using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class CotizaRepositorio : RepositorioBase<Cotiza>
    {
        private SAC_Entities context;

        public CotizaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public Cotiza obtenerCotiza(DateTime fecha)
        {
            return context.Cotiza.Where(p => p.Fecha == fecha && p.Activo == true).First();
        }


        public Cotiza Agregar(Cotiza oCotiza)
        {
            return Insertar(oCotiza);
        }




    }
}
