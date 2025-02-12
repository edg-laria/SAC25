using Ninject;
using System.Configuration;
using Datos;
using Datos.Interfaces;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
namespace Negocio
{
   public class Bindings: Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
          
            Bind<IUsuarioRepositorio>().To<UsuarioRepositorio>().WithConstructorArgument("contextoDeDatos", new SAC_Entities());





            //if ((ConfigurationManager.AppSettings("env") == null & ConfigurationManager.AppSettings("env") != "TEST"))

            //    Bind<IUsuarioRepositorio>().To<UsuarioRepositorio>().WithConstructorArgument("contextoDeDatos", new HCIEntities());
            //else
            //    Bind<IUsuarioRepositorio>().To<MockUsuarioRepositorio>().WithConstructorArgument("contextoDeDatos", new HCIEntities());
        }

    }
}
