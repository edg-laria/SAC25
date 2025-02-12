using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Models
{
    public class CuentaPlanContableModelView
    {

        public int? Id { get; set; }
     
        [Required]   
        public int Codigo { get; set; }
        [Required(ErrorMessage = "Ops!, complete el campo.")]
        public int IdNuevo { get; set; }// si para cdo quiera actualizar el codigo 
        [Required(ErrorMessage = "Ops!, complete el campo.")]
        [StringLength(100)]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public int IdCuentaSuperior { get; set; }

        public List<CuentaPlanContableModelView> ListCuentaSuperior { get; set; }
        public List<SelectListItem> selectListCuentaSuperior { get; set; }

        public string IdTipoElemento { get; set; }
        public SelectList TipoElemento { get; set; }


    }
}