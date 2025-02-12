using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
 public class BuqueRespositorio : RepositorioBase<Buque>
    {
        private SAC_Entities context;

        public BuqueRespositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<Buque> GetAllBuque()
        {
            List<Buque> listaoBuque = context.Buque.Where(p => p.Activo == true ).ToList();
            return listaoBuque;
        }

        public Buque Agregar(Buque oBuque)
        {
            return Insertar(oBuque);
        }

    }
}
