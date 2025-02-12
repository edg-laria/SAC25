using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;

namespace SAC.Models
{
    public class TipoComprobanteVentaModelView
    {

        public int Id { get; set; }

        public int PuntoVenta { get; set; }

        public string Denominacion { get; set; }

        public int Numero { get; set; }

        public string Abreviatura { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

    }
}