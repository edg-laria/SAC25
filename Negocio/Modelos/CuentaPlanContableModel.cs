using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class CuentaPlanContableModel
    {

        public int Id { get; set; }
        public int Codigo { get; set; }
        public int IdNuevo { get; set; }// si para cdo quiera actualizar el codigo 

        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public int IdCuentaSuperior { get; set; }

        public List<CuentaPlanContableModel> ListCuentaSuperior { get; set; }
       // public List<SelectListItem> selectListCuentaSuperior { get; set; }

        public string IdTipoElemento { get; set; }
       // public SelectList TipoElemento { get; set; }
    }
}
