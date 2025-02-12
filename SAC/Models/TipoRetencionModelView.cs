using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class TipoRetencionModelView
    {

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public int Idusuario { get; set; }
        public System.DateTime UltimaModificacion { get; set; }

    }
}