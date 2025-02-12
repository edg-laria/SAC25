using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;

namespace Negocio.Modelos
{
   public class FacturaElectronicaModel
    {
        public int ID { get; set; }
        public Nullable<int> ID_TIPOCBTE { get; set; }
        public Nullable<int> PUNTOVTA { get; set; }
        public Nullable<int> NROCBTE_AFIP { get; set; }
        public Nullable<System.DateTime> FECHACBTE { get; set; }
        public Nullable<int> TIPODOC { get; set; }
        public string NRODOC { get; set; }
        public string IDIVA { get; set; }
        public Nullable<decimal> NETO { get; set; }
        public Nullable<decimal> IVA { get; set; }
        public Nullable<decimal> NETO10 { get; set; }
        public Nullable<decimal> IVA10 { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public Nullable<System.DateTime> FECHAVEN { get; set; }
        public Nullable<int> TIPOSERV { get; set; }
        public Nullable<System.DateTime> FDESDE { get; set; }
        public Nullable<System.DateTime> FHASTA { get; set; }
        public string CAE { get; set; }
        public Nullable<System.DateTime> FECHAVTO { get; set; }
        public string RESULTADO { get; set; }
        public string MOTIVO { get; set; }
        public string REPROCESO { get; set; }
        public string NOMBRE { get; set; }
        public string DOMICILIO { get; set; }
        public string ATENCION { get; set; }
        public string PAIS { get; set; }
        public string LOCALIDAD { get; set; }
        public string TELEFONO { get; set; }
        public string CATEGORIA { get; set; }
        public string EMAIL { get; set; }
        public string ESTADO { get; set; }
        public string NRONC { get; set; }
        public string NROAUX { get; set; }
        public string CODCLI { get; set; }
        public string PRECEP { get; set; }
        public string ALICUOTA { get; set; }
        public string CODPAIS { get; set; }
        public string IDMONEDA { get; set; }
        public Nullable<decimal> COTIZA { get; set; }
        public string IDIOMA { get; set; }
        public string FORMAPAGO { get; set; }
        public string XMLREQ { get; set; }
        public string XMLRES { get; set; }
        public string OBS { get; set; }
        public string CODBARRA { get; set; }
        public string QR { get; set; }
        public Nullable<long> ID_CBTE_WSAFIP { get; set; }

        public ICollection<FacturaVentaModel> FactVenta { get; set; }
    }
}
