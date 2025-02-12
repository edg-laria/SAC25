using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class ArticuloModelView
    {


        public int Id { get; set; }
        public string Codigo { get; set; }
        public string DescripcionCastellano { get; set; }
        public string DescripcionIngles { get; set; }
        public string Grupo { get; set; }
        public string Honorario { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> Imputacion { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    }


}
