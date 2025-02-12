using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;
using System.Data.Entity.Validation;

namespace Negocio.Servicios
{
    public class ServicioItemImpr : ServicioBase
    {
        private ItemImprRepositorio ItemImprRepositorio;

        public ServicioItemImpr()
        {
            ItemImprRepositorio = kernel.Get<ItemImprRepositorio>();
        }

        public List<ItemImprModel> GetAllItemImpre()
        {
            return Mapper.Map<List<ItemImpre>, List<ItemImprModel>>(ItemImprRepositorio.GetAllItemImpre());
        }

        public List<ItemImprModel> GetAllItemImpreNroFactura(int nroFactura, int idComprobante)
        {
            return Mapper.Map<List<ItemImpre>, List<ItemImprModel>>(ItemImprRepositorio.GetAllItemImpreNroFactura(nroFactura, idComprobante));
        }


        public ItemImprModel Agregar(ItemImprModel oItemImprModel)
        {
            try
            {
                var oModel = Mapper.Map<ItemImprModel, ItemImpre>(oItemImprModel);
                return Mapper.Map<ItemImpre, ItemImprModel>(ItemImprRepositorio.Insertar(oModel));
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
               return null;
            }
        }


        public ItemImprModel Actualizar(ItemImprModel oItemImprModel)
        {
            try
            {
                var oModel = Mapper.Map<ItemImprModel, ItemImpre>(oItemImprModel);
                return Mapper.Map<ItemImpre, ItemImprModel>(ItemImprRepositorio.ActualizarItemImpre(oModel));

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

    }
}
