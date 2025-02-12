using Negocio.Enumeradores;
using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Models
{
    public class DiarioModelView
    {


        public int Id { get; set; }
        public int Codigo { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdImputacion { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
        public string Titulo { get; set; }
        public string Periodo { get; set; }
        public string Tipo { get; set; }
        public string DescripcionMa { get; set; }
        public string Moneda { get; set; }
        public decimal Cotiza { get; set; }
        public Nullable<int> Balance { get; set; }
        public Nullable<int> Asiento { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<decimal> Debe { get; set; }
        public Nullable<decimal> Haber { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public virtual ImputacionModelView Imputacion { get; set; }

        //  public List<Anios> ListaAnio { get; set; };

        public List<DiarioModelView> ListaDiario { get; set; }

        public SelectList TipoAsiento { get; set; }


    }


    public class Meses
    {

        public string Id { get; set; }
        public string Descripcion { get; set; }
    }


    public class Anios
    {
       
        public string Id { get; set; }
        public string Descripcion { get; set; }
    }
}