using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class TipoMonedaModelView
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public int IdUsuario { get; set; }

        public DateTime UltimaModificacion { get; set; }
       

    }
}