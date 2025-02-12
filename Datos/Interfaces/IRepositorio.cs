using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    public interface IRepositorio<T>
    {

        T Insertar(T entidad);

        IQueryable<T> Obtener();


    }
}
