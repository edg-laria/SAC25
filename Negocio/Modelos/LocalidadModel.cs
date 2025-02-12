using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregado
using Datos.ModeloDeDatos;


namespace Negocio.Modelos
{
   public class LocalidadModel
    {
        public int Id { get; set; }

        //public int Codigo { get; set; }

        public string CodigoPostal { get; set; }

        public string Nombre { get; set; }

        public string CodigoProvincia { get; set; }

        public Nullable<int> IdPais { get; set; }

        public Nullable<int> IdProvincia { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }


        public virtual Pais Pais { get; set; }

        public virtual Provincia Provincia { get; set; }



    }
}
