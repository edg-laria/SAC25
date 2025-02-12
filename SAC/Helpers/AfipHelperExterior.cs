using HtmlAgilityPack;
using Negocio.Helpers;
using Negocio.Modelos;
using Negocio.Servicios;
using SAC.afip.wswhomo_Exportacion;
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
    public class AfipHelperExterior
    {

        public FEXGetCMPResponse getComprobanteAfipExterior(short comprobante, int puntoVenta, long comprobanteNro)
        {

            // 1 ) verificar en la base si el token esta vencido 
            Afip_TicketAccesoModel login;
            AfipHelper afipHelper = new AfipHelper();
            login = afipHelper.VerificarTicketAcceso("wsfex");
            ClaseLoginAfip TicketAcceso = null;
#pragma warning disable CS0168 // La variable 'responseAfip' se ha declarado pero nunca se usa
            FEXResponseAuthorize responseAfip;
#pragma warning restore CS0168 // La variable 'responseAfip' se ha declarado pero nunca se usa

            var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            int IdPuntoVenta = AfipHelper.getPuntoVentaAfip(puntoVenta);

            if (login == null)
            {
                TicketAcceso = afipHelper.ObtenerTicketAccesoWS("wsfex", OUsuario.IdUsuario);
            }
            else
            {
                TicketAcceso = afipHelper.ObtenerTicketAccesoSinWS("wsfex", OUsuario.IdUsuario);
                TicketAcceso.Token = login.token;
                TicketAcceso.Sign = login.sing;
            }

            // 2 ) AfipExterior 
            // 2.1 instancio objeto autenticacion
            long cuitOriginador = long.Parse(System.Configuration.ConfigurationManager.AppSettings["cuitUserAfip"].ToString());
            ClsFEXAuthRequest Autenticacion = new ClsFEXAuthRequest();
            Autenticacion.Cuit = cuitOriginador;
            Autenticacion.Sign = TicketAcceso.Sign;
            Autenticacion.Token = TicketAcceso.Token;

            // 2.2 se prepara el servicio para enviar
            afip.wswhomo_Exportacion.Service ServicioWebFacturaExterior = new afip.wswhomo_Exportacion.Service();
            ServicioWebFacturaExterior.Url = @"https://servicios1.afip.gov.ar/wsfexv1/service.asmx?WSDL";
            ServicioWebFacturaExterior.ClientCertificates.Add(TicketAcceso.certificado);

            // 2.3 Recupera los datos completos de un comprobante ya autorizado
            ClsFEXGetCMP getComprobanteAutorizadoAFIP = new ClsFEXGetCMP();
            getComprobanteAutorizadoAFIP.Cbte_nro = comprobanteNro;
            getComprobanteAutorizadoAFIP.Cbte_tipo = comprobante;
            getComprobanteAutorizadoAFIP.Punto_vta = puntoVenta;

            FEXGetCMPResponse responseAFIP = ServicioWebFacturaExterior.FEXGetCMP(Autenticacion, getComprobanteAutorizadoAFIP);



            return responseAFIP;
        }
    }
}