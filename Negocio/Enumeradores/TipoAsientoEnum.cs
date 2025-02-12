using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Enumeradores
{
    public enum TipoAsientoEnum
    {
        [Display(Name = "CF", Description = "CF - Compra Facturas")]           
         CF = 0,
        [Display(Name = "CP", Description = "CP - Compra Pago")]
         CP = 1,
        [Display(Name = "VF", Description = "VF - Ventas Facturas")]
        VF = 2,
        [Display(Name = "VP", Description = "VP - Ventas Cobros")]
        VP = 3,
    }
 }
