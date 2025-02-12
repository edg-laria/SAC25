using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using SAC.afip.wswhomo_Exportacion;
using SAC.afip.wswhomo;
using System.Reflection;

namespace SAC.Models
{
    public class ComprobanteAfipModelView
    {
       
        //public FEXGetCMPResponse fEXGetCMPResponse { get; set; }

        public ClsFEXErr Error { get; set; }
        public PropertyInfo[] propertyInfosResult { get; set; }
        public ClsFEXGetCMPR comprobante { get; set; }
        public FECompConsResponse comprobanteLocal { get; set; }
    }

}