using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;


namespace SAC.Models
{
    public class ImputacionModelView
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public Nullable<int> IdSubRubro { get; set; }

        public Nullable<decimal> SaldoInicial { get; set; }

        public Nullable<decimal> SaldoFin { get; set; }

        public Nullable<int> IdTipo { get; set; }

        public string Alias { get; set; }

        public Nullable<decimal> Enero { get; set; }

        public Nullable<decimal> Febrero { get; set; }

        public Nullable<decimal> Marzo { get; set; }

        public Nullable<decimal> Abril { get; set; }

        public Nullable<decimal> Mayo { get; set; }

        public Nullable<decimal> Junio { get; set; }

        public Nullable<decimal> Julio { get; set; }

        public Nullable<decimal> Agosto { get; set; }

        public Nullable<decimal> Septiembre { get; set; }

        public Nullable<decimal> Octubre { get; set; }

        public Nullable<decimal> Noviembre { get; set; }

        public Nullable<decimal> Diciembre { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }


    }

}