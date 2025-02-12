using System;
using System.Reflection;
using Ninject;


using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
namespace Negocio.Servicios
{
    public class ServicioCorreo : ServicioBase
    {

        public string  EnviarCorreo(string nombreCorreo, string asunto, string contenido, string rutaError)
        {
            string rpta = "";
            try
            {


                   //<add key = "correo" value = "eli_udemy@hotmail.com" />
   
                   //<add key = "clave" value = "12345678" />
      
                   //<add key = "servidor" value = "smtp.office365.com" />
         
                   //< add key = "puerto" value = "25" />


                //string correo = "eli_udemy@hotmail.com"; //  ConfigurationManager.AppSettings["correo"];
                //string clave = "12345678"; // ConfigurationManager.AppSettings["clave"];
                //string servidor = "smtp.office365.com"; // ConfigurationManager.AppSettings["servidor"];
                //int puerto = int.Parse( "25" );//ConfigurationManager.AppSettings["puerto"]);



                string correo = "estesor2000@gmail.com"; //  ConfigurationManager.AppSettings["correo"];
                string clave = "01Aldana2020"; // ConfigurationManager.AppSettings["clave"];
                string servidor = "smtp.gmail.com"; // ConfigurationManager.AppSettings["servidor"];
                int puerto = int.Parse("465");//ConfigurationManager.AppSettings["puerto"]);

                //Data del correo (definimos)
                MailMessage mail = new MailMessage();
                mail.Subject = asunto;
                mail.IsBodyHtml = true;
                mail.Body = contenido;
                mail.From = new MailAddress(correo);
                mail.To.Add(new MailAddress(nombreCorreo));
                //Envio de correo
                SmtpClient smtp = new SmtpClient();
                smtp.Host = servidor;
                smtp.EnableSsl = true;
                smtp.Port = puerto;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(correo, clave);
                smtp.Send(mail);
               
                //rpta = 1;
                rpta= "Se Envio el Correo";


            }
            catch (Exception ex)
            {
               //rpta = 0;

                rpta= "Hubo un error, en el envio del Correo" + ex.Message;

                //File.AppendAllText(rutaError, nombreCorreo);
            }

            // return rpta;

            return rpta;

        }


    }
}
