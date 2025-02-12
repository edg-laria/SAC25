using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class TarjetaModelView
    {


        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public int IdImputacion { get; set; }

        public string Numero { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public List<Tarjetas> ListaTarjeta { get; set; }

        public ICollection<CajaModelView> Caja { get; set; }
    }
}