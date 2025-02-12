using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio.Modelos
{
    public class PrioridadModel
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public Nullable<bool> Activo { get; set; }
        public string Nombre { get; set; }
    }
}