using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class TarjetaOperacionModelView
    {

        public int Id { get; set; }
        public int IdTarjeta { get; set; }
        public int IdGrupoCaja { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Conciliacion { get; set; }
        public string NumeroPago { get; set; }

        public decimal Importe { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    
        public TarjetaModelView Tarjetas { get; set; }


        [Display(Name = "Fecha Desde")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime cFechaDesde { get; set; }


        [Display(Name = "Fecha Hasta")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime cFechaHasta { get; set; }

        public List<TarjetaOperacionModelView> ListaTarjetaOperacion { get; set; }

        public string IdTarjetaConciliar { get; set; }


    }
}