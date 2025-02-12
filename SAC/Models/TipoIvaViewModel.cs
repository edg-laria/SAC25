using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class TipoIvaViewModel
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public decimal Porcentaje { get; set; }

        public string Tipo { get; set; }


        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
}