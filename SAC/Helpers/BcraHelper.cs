using HtmlAgilityPack;
using SAC.Models.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace SAC.Helpers
{
    //esta clase nos sirve para tener control del resultado regresado por la solicitud
    public class Reply
    {   
        public int success { get; set; }
        public object data { get; set; }
        public string menssage { get; set; }
      
        public Reply()
        {
            this.success = 0;
            this.menssage = "";
        }

    }

   
    public class BcraHelper
    {
        public List<CotizacionBNA> GetCotizacionBNAScraping()
        {
            string sUrl = "http://www.bna.com.ar/Personas";

            Encoding objEncoding = Encoding.GetEncoding("ISO-8859-1");
            WebProxy objWebProxy = new WebProxy("proxy", 80);
            CookieCollection objCookies = new CookieCollection();

            //USANDO GET
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(sUrl);
          //  getRequest.Proxy = objWebProxy;
            getRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
            getRequest.ProtocolVersion = HttpVersion.Version11;
            getRequest.UserAgent = ".NET Framework 4.0";
            getRequest.Method = "GET";

            getRequest.CookieContainer = new CookieContainer();
            getRequest.CookieContainer.Add(objCookies);

            string sGetResponse = string.Empty;

            using (HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse())
            {
                objCookies = getResponse.Cookies;

                using (StreamReader srGetResponse = new StreamReader(getResponse.GetResponseStream(), objEncoding))
                {
                    sGetResponse = srGetResponse.ReadToEnd();
                }
            }

            //Obtenemos Informacion
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(sGetResponse);
            HtmlNode tbl = document.DocumentNode.SelectNodes("// table [@ class =\"table cotizacion\"]//tr")[0];
                DataTable dt = new DataTable();
                dt.Columns.Add("Fecha", typeof(String));
                dt.Columns.Add("Moneda", typeof(String));
                dt.Columns.Add("Compra", typeof(String));
                dt.Columns.Add("Venta", typeof(String));

            if (tbl != null)
            {               
             
                int iNumFila = 0;                    
                int iNumColumna = 0;
                    DataRow dr = dt.NewRow();
                    foreach (HtmlNode subNode in tbl.SelectNodes("// td"))
                    {

                    if (iNumFila == 6)
                    {
                        break;
                    }
                    if (iNumColumna == 0)
                        { 
                            dr = dt.NewRow();
                            dr[iNumColumna] = tbl.SelectNodes("// th")[0].InnerHtml.ToString().Trim(); ;
                            iNumColumna++;
                        }                       
                        string sValue = subNode.InnerHtml.ToString().Trim();
                        sValue = System.Text.RegularExpressions.Regex.Replace(sValue, "<.*?>", " ");
                        dr[iNumColumna] = sValue;
                        iNumColumna++;                      
                        if (iNumColumna == 4 )
                        {
                            dt.Rows.Add(dr);
                            iNumColumna = 0;
                        }
                        iNumFila++;
                    }                              
                dt.AcceptChanges();
          
            }

            return  (from rw in dt.AsEnumerable()
                        select new CotizacionBNA()
                        {
                            Fecha = Convert.ToString(rw["Fecha"]),
                            Moneda = Convert.ToString(rw["Moneda"]),
                            Compra = Convert.ToString(rw["Compra"]),
                            Venta = Convert.ToString(rw["Compra"])
                        }).ToList();           
        }

        public Reply GetCotizacion(string url, string fecha, string method = "POST")
        {
            String result;
            Reply oReply = new Reply();
            try
            {
                //peticion ConfigurationManager.AppSettings["url_callback_bcra_cotizacion"]
                WebRequest request = WebRequest.Create(url);
                //headers
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                request.Timeout = 10000; //esto es opcional

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(fecha);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                oReply.success = 1;
                //y aquí va nuestra respuesta, la cual es lo que nos regrese el sitio solicitado
                oReply.data = result;
            }
            catch (Exception e)
            {

                oReply.data = 0;
                //en caso de error lo manejamos en el mensaje
                oReply.menssage = e.Message;

            }

            return oReply;
        }

        public Reply Send<T>(string url, T objectRequest, string method = "POST")
        {
            String result;
            Reply oReply = new Reply();
            try
            {               
                JavaScriptSerializer js = new JavaScriptSerializer();
                //serializamos el objeto
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectRequest);

                //peticion ConfigurationManager.AppSettings["url_callback_bcra_cotizacion"]
                WebRequest request = WebRequest.Create(url);
                //headers
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                request.Timeout = 10000; //esto es opcional

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                oReply.success = 1;
                //y aquí va nuestra respuesta, la cual es lo que nos regrese el sitio solicitado
                oReply.data = result;
            }
            catch (Exception e)
            {

                oReply.data = 0;
                //en caso de error lo manejamos en el mensaje
                oReply.menssage = e.Message;

            }

            return oReply;
        }
    

    //public static float GetCotizacion()
    //    {

    //       IEnumerable<bcraRequest> cotizacionBCRA = null;

    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri("https://api.estadisticasbcra.com/usd_of");
    //            //HTTP GET
    //            var responseTask = client.GetAsync("student");
    //            responseTask.Wait();

    //            var result = responseTask.Result;
    //            if (result.IsSuccessStatusCode)
    //            {
    //                var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
    //                readTask.Wait();

    //                students = readTask.Result;
    //            }
    //            else //web api sent error response 
    //            {
    //                //log response status here..

    //                students = Enumerable.Empty<StudentViewModel>();

    //                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
    //            }
    //        }

    //        return 0;
    //    }



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