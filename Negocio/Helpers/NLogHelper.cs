using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
namespace Negocio.Helpers
{
  public class NLogHelper
    {
        private static NLogHelper _instance  ;
        private Logger _logger;
        public NLogHelper(){
             _logger = LogManager.GetCurrentClassLogger();
        }
        public static NLogHelper Instance
        {
            get
            {
                if(_instance == null ){
                    _instance = new NLogHelper();
                }
                return _instance;
            }
            set
            {
                Instance = value;
            }
        }

        public void Info(string nombreDeClase, string metodo, string mensaje)
        {
            var mensajeDeLog = string.Format("{0}.{1}():{2}", nombreDeClase, metodo, mensaje);
            _logger.Info(mensajeDeLog);
        }

        public void Info(string mensaje)
        {
            _logger.Info(mensaje);
        }

        public void Alert(string nombreDeClase, string metodo, string mensaje)
        {
            _logger.Warn(string.Format("{0}.{1}():{2}", nombreDeClase, metodo, mensaje));
        }
        public void ErrorLog(string nombreDeClase, string metodo, string mensaje)
        {
            _logger.Error(string.Format("{0}.{1}():{2}", nombreDeClase, metodo, mensaje));
        }

        public void LogExcepcion(Exception ex, string mensaje="Error")
        {
            LogEventInfo evento = new LogEventInfo(NLog.LogLevel.Error, String.Empty, mensaje);
            // Warning!!! Optional parameters not supported
            // Valores customizados
            evento.Properties["ExceptionTitle"] = mensaje;
            evento.Properties["ExceptionSummary"] = ex.Message;
            evento.Properties["ExceptionStack"] = ex.StackTrace.ToString();
            _logger.Log(evento);
        }
     

    }
}
