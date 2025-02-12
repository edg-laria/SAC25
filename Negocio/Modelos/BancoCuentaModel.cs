using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregado
using Datos.ModeloDeDatos;

namespace Negocio.Modelos
{
   public class BancoCuentaModel
    {
        public BancoCuentaModel()
        {
            UltimaModificacion = DateTime.Now;
        }

        public int Id { get; set; }

        public string Codigo { get; set; }

        public int IdBanco { get; set; }

        public string BancoDescripcion { get; set; }

        public int IdImputacion { get; set; }

        public string CNombre { get; set; }

        public decimal Saldo { get; set; }

        public int NumeroCierre { get; set; }
        public DateTime Fecha { get; set; }

        public int IdMoneda { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public DateTime UltimaModificacion { get; set; }

        public BancoModel Banco { get; set; }

    }
}
