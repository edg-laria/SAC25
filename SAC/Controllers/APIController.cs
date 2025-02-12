using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SAC.Models;

namespace SAC.Controllers
{
    public class APIComprobantesController : ApiController
    {
        // Simula una base de datos en memoria
        private static List<APIComprobante> comprobantes = new List<APIComprobante>
    {
        new APIComprobante { Id = 1, NumeroComprobante = "1", FECHACBTE = DateTime.Now ,IDCliente = 123,NombreCliente="MILLE",
                            OurRef = "Nuestra Referencia", YourRef = "Su referencia", RazonSocial = "Nombre del cliente 123",
                            Domicilio="Domicilio cliente 123",Atencion="Datos de encabezado", Pais="Pais",idDto="Dto",
                            Header="Encabezado 2",Nota="Notas"},
        new APIComprobante { Id = 2, NumeroComprobante = "2", FECHACBTE = DateTime.Now.AddDays(1) ,IDCliente = 123,NombreCliente="MILLE",
                            OurRef = "Nuestra Referenccis", YourRef = "Su referencia", RazonSocial = "Nombre del cliente 123",
                            Domicilio="Domicilio cliente 123",Atencion="Datos de encabezado", Pais="Pais",idDto="Dto",
                            Header="Encabezado 2",Nota="Notas"},
    };


        // GET api/comprobantes
        public IEnumerable<APIComprobante> Get()
        {
            return comprobantes;
        }

        // GET api/comprobantes/5
        public IHttpActionResult Get(int id)
        {
            var comprobante = comprobantes.FirstOrDefault(c => c.Id == id);
            if (comprobante == null)
            {
                return NotFound(); // Devuelve un 404 si no se encuentra el comprobante
            }
            return Ok(comprobante); // Devuelve el comprobante encontrado
        }

    }
}

