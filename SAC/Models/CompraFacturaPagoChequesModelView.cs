using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class CompraFacturaPagoChequesModelView
    {
        public List<ChequeModelView> ListaChequesTerceros { get; set; }
        public List<ChequeraModelView> ListaChequesPropios{ get; set; }

    }
}