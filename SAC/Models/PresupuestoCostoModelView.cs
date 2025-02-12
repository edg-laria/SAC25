using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class PresupuestoCostoModelView
    {

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public decimal Actual { get; set; }
        public decimal Ejecutado { get; set; }
        public decimal Historico { get; set; }
        public int IdImputacion { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

    }
}