using HtmlAgilityPack;
using Negocio.Helpers;
using Negocio.Modelos;
using Negocio.Servicios;
using SAC.afip.wswhomo;
using SAC.Models.Afip;
using SAC.Models.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace SAC.Helpers
{
    public class AfipHelper
    {

        private ServicioAfip_TicketAcceso servicioAfip_TicketAcceso = new ServicioAfip_TicketAcceso();

        public static int getPuntoVentaAfip(int idPuntoVenta)
        {
            if (idPuntoVenta == 1)
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["PuntaLocalAfip"].ToString());
            }
            else
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["PuntaExteriorAfip"].ToString());
            }
        }
      
        public FECotizacionResponse GetCotizacion(string moneda)
        {
            try
            {       
            //instancio objeto autenticacion
            var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            Afip_TicketAccesoModel login = VerificarTicketAcceso("wsfe");
            ClaseLoginAfip ClaseLogin = null;          
                // revisar
                // deberia buscar en la base si el token esta activo de lo contrario obtener un new token
            if (login == null)
            {
                //busca el token nuevo y graba en la bd
                ClaseLogin = ObtenerTicketAccesoWS("wsfe", OUsuario.IdUsuario);
                login = VerificarTicketAcceso("wsfe");
            }
            else {
                    //usa el token de la base
                    //ClaseLogin = ObtenerTicketAccesoSinWS("wsfe", OUsuario.IdUsuario);
                   
                    ClaseLogin = ObtenerTicketAccesoSinWS("wsfe", (int)login.usuario);
                }
                ClaseLogin.Token = login.token;
           
            FEAuthRequest Autenticacion = new FEAuthRequest();
            Autenticacion.Cuit = long.Parse(ConfigurationManager.AppSettings["cuitUserAfip"].ToString());
            Autenticacion.Sign = login.sing;
            Autenticacion.Token = login.token;

            //se prepara el servicio para enviar
            Service ServicioWebFactura = new Service();
            ServicioWebFactura.Url = ConfigurationManager.AppSettings["url_wsdlAfip"].ToString();
            ServicioWebFactura.ClientCertificates.Add(ClaseLogin.certificado);

            var paramCoti = ServicioWebFactura.FEParamGetCotizacion(Autenticacion, moneda);
            return paramCoti;
         
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                //hacer log del servicio
                //throw new Exception(ex.Message);
                var response = new FECotizacionResponse();
                response.ResultGet = null;
                return response;
            }
           
        }

        public FECompConsultaResponse ConsultaFacturaLocal(int tipoCbte,int PuntoVta,int NroCbte)
        {
            try
            {
                string IngresoServicioAfip;
                IngresoServicioAfip = "wsfe";
                //instancio objeto autenticacion
                var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                Afip_TicketAccesoModel login = VerificarTicketAcceso(IngresoServicioAfip);
                ClaseLoginAfip ClaseLogin = null;
                // revisar
                // deberia buscar en la base si el token esta activo de lo contrario obtener un new token
                if (login == null)
                {
                    //busca el token nuevo y graba en la bd
                    ClaseLogin = ObtenerTicketAccesoWS(IngresoServicioAfip, OUsuario.IdUsuario);
                    login = VerificarTicketAcceso(IngresoServicioAfip);
                }
                else
                {
                    //usa el token de la base
                    ClaseLogin = ObtenerTicketAccesoSinWS(IngresoServicioAfip, OUsuario.IdUsuario);
                }
                ClaseLogin.Token = login.token;

                FEAuthRequest Autenticacion = new FEAuthRequest();
                Autenticacion.Cuit = long.Parse(ConfigurationManager.AppSettings["cuitUserAfip"].ToString());
                Autenticacion.Sign = login.sing;
                Autenticacion.Token = login.token;

                FECompConsultaReq FeCompConsReq = new FECompConsultaReq();
                FeCompConsReq.CbteTipo = tipoCbte; // Ejemplo de valor para tipocbte
                FeCompConsReq.PtoVta = getPuntoVentaAfip(PuntoVta); // Ejemplo de valor para puntovta
                FeCompConsReq.CbteNro = NroCbte; // Ejemplo de valor para nrocbte
            
                //se prepara el servicio para enviar
                Service ServicioWebFactura = new Service();
                ServicioWebFactura.Url = ConfigurationManager.AppSettings["url_wsdlAfip"].ToString();
                ServicioWebFactura.ClientCertificates.Add(ClaseLogin.certificado);

               return ServicioWebFactura.FECompConsultar(Autenticacion, FeCompConsReq);
                
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                //hacer log del servicio
                //throw new Exception(ex.Message);
                var response = new FECompConsultaResponse();
                response.ResultGet = null;
                return response;
            }

        }





        public ClaseLoginAfip LoginSacAfip()
        {
            var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            //verificar en la base si el token esta vencido 
            Afip_TicketAccesoModel login;
            login = VerificarTicketAcceso("wsfe");


            ClaseLoginAfip ClaseLogin = null;
            if (login == null)
            {
                //busca el token nuevo y graba en la bd
                ClaseLogin = ObtenerTicketAccesoWS("wsfe", OUsuario.IdUsuario);
            }
            else
            {
                //usa el token de la base
                ClaseLogin = ObtenerTicketAccesoSinWS("wsfe", OUsuario.IdUsuario);
                ClaseLogin.Token = login.token;
                ClaseLogin.Sign = login.sing;
            }
            return ClaseLogin;
        }

        public Afip_TicketAccesoModel VerificarTicketAcceso(string servicio)
        {
            DateTime today = DateTime.Now;
            Afip_TicketAccesoModel Ta = servicioAfip_TicketAcceso.GetTicketAccesoUltimoPorServicio(servicio);
            if (Ta != null && Ta.fecha_expiracion >= today)
            {               
                return Ta;               
            }
            else
            {
                return null;
            }

        }

        public ClaseLoginAfip ObtenerTicketAccesoWS(string servicio, int usuario)
        {
            ClaseLoginAfip LoginAfip;          
            string url =  ConfigurationManager.AppSettings["url_wsAfip"].ToString();
            //string pathCertificado = AppContext.BaseDirectory+ @"Pandi2023.pfx";
            string pathCertificado = System.Configuration.ConfigurationManager.AppSettings["pathFullCertificado"].ToString();

            string claveAfip = System.Configuration.ConfigurationManager.AppSettings["claveAfip"].ToString();

            LoginAfip = new ClaseLoginAfip(servicio, url, pathCertificado, claveAfip);
            LoginAfip.hacerLogin();

  
            Afip_TicketAccesoModel Ta = new Afip_TicketAccesoModel();
            Ta.servicio = LoginAfip.serv;
            Ta.sing = LoginAfip.Sign;
            Ta.token = LoginAfip.Token;
            Ta.fecha_solicitud = LoginAfip.GenerationTime;
            Ta.fecha_expiracion = LoginAfip.ExpirationTime;
            Ta.usuario = usuario;
            //me falta la url

            Afip_TicketAccesoModel TaReq = servicioAfip_TicketAcceso.CrearTicketAcceso(Ta);
            if (TaReq != null)
            {
                return LoginAfip;
            }
            else
            {
                return null;
            }
        }

        public ClaseLoginAfip ObtenerTicketAccesoSinWS(string servicio, int usuario)
        {
            ClaseLoginAfip LoginAfip;          
            string url = ConfigurationManager.AppSettings["url_wsAfip"].ToString();
            string pathCertificado = System.Configuration.ConfigurationManager.AppSettings["pathFullCertificado"].ToString();
            //string path =  System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            //string pathCertificado = Path.Combine(path, directorioCetificado);
           
            string claveAfip = System.Configuration.ConfigurationManager.AppSettings["claveAfip"].ToString();
            LoginAfip = new ClaseLoginAfip(servicio, url, pathCertificado, claveAfip);
            LoginAfip.LoginSinWs();

            if (LoginAfip != null)
            {
                return LoginAfip;
            }
            else
            {
                return null;
            }
        }

        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image imagen = System.Drawing.Image.FromStream(ms, true);
            return imagen;
        }







       
    }

}