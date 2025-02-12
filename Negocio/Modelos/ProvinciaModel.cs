using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregado
using Datos.ModeloDeDatos;

namespace Negocio.Modelos
{
    public class ProvinciaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public Nullable<int> CodigoNumero { get; set; }
        public Nullable<int> IdPais { get; set; }
        public Nullable<int> CodigoAfip { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public PaisModel Pais { get; set; }

        //agregados
        public string nombrePais { get; set; }
    }
}
