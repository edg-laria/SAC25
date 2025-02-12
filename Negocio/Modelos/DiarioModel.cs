using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class DiarioModel
    {

        public int Id { get; set; }
        public int Codigo { get; set; }
        public DateTime Fecha { get; set; }
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

        public ImputacionModel Imputacion { get; set; }
    }

}
