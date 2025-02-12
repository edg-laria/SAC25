using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Web;
using System.IO;
using System.Diagnostics;


namespace Negocio.Servicios
{
    class ServicioElog
    {
        public static void Log(object obj, Exception ex)
        {
            string fecha = System.DateTime.Now.ToString("yyyyMMdd");
            string hora = System.DateTime.Now.ToString("HH:mm:ss");

            String FilePath = "";
            // Dim FilePath As String
            FilePath = System.AppDomain.CurrentDomain.BaseDirectory;

            string path = FilePath + "logs/" + fecha + ".txt"; //context.Current.Server.MapPath("~/log/" + fecha + ".txt");

          //  Dim path As String = FilePath & "logs/" & fecha & ".txt"

            StreamWriter sw = new StreamWriter(path, true);

            StackTrace stacktrace = new StackTrace();
            sw.WriteLine(obj.GetType().FullName + " " + hora);
            sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + ex.Message);
            sw.WriteLine("");

            sw.Flush();
            sw.Close();
        }






    }
}
