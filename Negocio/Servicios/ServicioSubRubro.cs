using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using Negocio.Modelos;
using AutoMapper;
using System.Net.Mail;
using System.IO;
using System.Net;
using Negocio.Servicios;
using System.Net.Mime;
using System.Text;

namespace Negocio.Servicios
{
   public class ServicioSubRubro : ServicioBase
    {
        private SubRubroRepositorio oSubRubroRepositorio;

        public ServicioSubRubro()
        {
            oSubRubroRepositorio = kernel.Get<SubRubroRepositorio>();
        }


        public List<SubRubroModel> GetAllSubRubro()
        {
            try
            {
                var SubRubro = Mapper.Map<List<SubRubro>, List<SubRubroModel>>(oSubRubroRepositorio.GetAllSubRubro());
                return SubRubro;
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke(ex.Message, "error");
                return null;
            }
        }


        public SubRubroModel GetSubRubro(int _id)
        {
            SubRubro oSubRubro = oSubRubroRepositorio.ObtenerSubRubroPorId(_id);
            SubRubroModel oSubRubroModel = new SubRubroModel();
            //en el atriburo codigo doy la opcion que si no trae grabe 0
            oSubRubroModel.Id = oSubRubro.Id;
           
            oSubRubroModel.Activo = oSubRubro.Activo;

            return oSubRubroModel;
        }

        public int ActualizarPais(SubRubroModel oSubRubroModel)
        {
            //controlar que no exista 
            SubRubro oSubRubro = oSubRubroRepositorio.ObtenerSubRubroPorNombre(oSubRubroModel.Descripcion, oSubRubroModel.codigo, oSubRubroModel.Id);
            if (oSubRubro != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {
                SubRubro oSubRubroNuevo = new SubRubro();
                SubRubro oSubRubroRespuesta = new SubRubro();

                oSubRubroNuevo.Id = oSubRubroModel.Id;
                oSubRubroNuevo.Activo = true;
                oSubRubroRespuesta = oSubRubroRepositorio.ActualizarSubRubro(oSubRubroNuevo);

                if (oSubRubroRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int GuardarSubRubro(SubRubroModel oSubRubroModel)
        {
            //controlar que no exista 
            SubRubro oSubRubro = oSubRubroRepositorio.ObtenerSubRubroPorNombre(oSubRubroModel.Descripcion, oSubRubroModel.codigo, oSubRubroModel.Id);
            if (oSubRubro != null)
            {
                return -2;
            }
            else
            {
                SubRubro oSubRubroNuevo = new SubRubro();
                SubRubro oSubRubroRespuesta = new SubRubro();

              
                oSubRubroNuevo.Activo = oSubRubroModel.Activo;
                oSubRubroNuevo.Idusuario = oSubRubroModel.IdUsuario;
                oSubRubroNuevo.UltimaModificacion = oSubRubroModel.UltimaModificacion;

                oSubRubroRespuesta = oSubRubroRepositorio.InsertarSubRubro(oSubRubroNuevo);
                if (oSubRubroRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int Eliminar(int idSubRubro)
        {
            var retorno = oSubRubroRepositorio.EliminarSubRubro(idSubRubro);
            if (retorno == 1)
            {
                return 0; //ok
            }
            else
            {
                return -1;//paso algo
            }
        }

    }


}

