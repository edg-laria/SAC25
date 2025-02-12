using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
  public class CobroFacturaModoRepositorio : RepositorioBase<FactVentaCobro>

    {

        private SAC_Entities context;

        public CobroFacturaModoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public FactVentaCobro InsertarCobroModo(FactVentaCobro factVentaCobro)
        {
            factVentaCobro.Activo = true;
            factVentaCobro.UltimaModificacion = DateTime.Now;
            return Insertar(factVentaCobro);
        }



    }
}
