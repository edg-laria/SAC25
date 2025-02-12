using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Enumeradores
{
    public enum TipoElementoEnum
    {
        [Display(Name = "G", Description = "Grupo")]           
        G = 0,
        [Display(Name = "R", Description = "Rubro")]
        R = 1,
        [Display(Name = "S", Description = "SubRubro")]
        S = 2,
        [Display(Name = "C", Description = "Cuenta")]
        C = 3,
    }
 }
