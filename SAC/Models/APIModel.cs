using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{

    public class APIComprobante
    {
        public int Id { get; set; }
        public string NumeroComprobante { get; set; }
        public int IDCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FECHACBTE { get; set; }
        public string OurRef { get; set; }
        public string YourRef { get; set; }
        public string RazonSocial { get; set; }
        public string Domicilio { get; set; }
        public string Atencion { get; set; }
        public string Pais { get; set; }
        public string idDto { get; set; }
        public string Header { get; set; }
        public string Nota { get; set; }
        public List<APIItem> Items { get; set; }
    }

    public class APIItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }

}