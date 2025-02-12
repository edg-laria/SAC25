using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.Entity;
namespace Datos.Repositorios
{
public abstract class RepositorioBase<T> : Interfaces.IRepositorio<T> where T : class
{
    protected DbSet<T> DbSet;
    protected DbContext Contexto;
     
    public RepositorioBase(DbContext contexto)
    {
        this.DbSet = contexto.Set<T>();
        this.Contexto = contexto;
    }


        // METODOS ACTUALIZADOR
    public T Insertar(T entidad)
    {
        T nuevaEntidad;
        nuevaEntidad = DbSet.Add(entidad);
        Contexto.SaveChanges();
        return nuevaEntidad;
    }


        
        public IQueryable<T> Obtener()
    {
        return DbSet;
    }

    }

}
