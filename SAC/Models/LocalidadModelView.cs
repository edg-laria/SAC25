using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
using System.ComponentModel.DataAnnotations;

namespace SAC.Models
{
    public class LocalidadModelView
    {

        public int Id { get; set; }

        [Display(Name = "Codigo Postal")]           
        //hace referencia al codigo postal
        //public Nullable<int> CodigoPostal { get; set; }
        public string CodigoPostal { get; set; }


        [Display(Name = "Nombre localidad")]       
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        public string Nombre { get; set; }

        //no se que significa va ir con un dato fijo
        public string CodigoProvincia { get; set; }
       
        public Nullable<int> IdPais { get; set; }

        public Nullable<int> IdProvincia { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }


        //public virtual Pais Pais { get; set; }

        //public virtual Provincia Provincia { get; set; }


        //agregados para tomar valor de los combo

        [Required]
        public int idCmbPais { get; set; }

        [Required]
        public int idCmbProvincia { get; set; }


    }
}