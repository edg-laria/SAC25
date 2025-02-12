using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class GrupoPresupuestoModelView





    {


        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public virtual ICollection<ClienteModelView> Cliente { get; set; }



    }
}