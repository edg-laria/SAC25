using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
using System.Web;
using System.Web.Mvc;
//agregadas
using AutoMapper;
using Negocio.Modelos;
using Negocio.Servicios;
using SAC.Atributos;
using SAC.Models;

namespace SAC.Controllers
{
    public class PaisController : BaseController
    {

        private ServicioPais servicioPais = new ServicioPais();
        //contructor de la clase
        public PaisController()
        {
            servicioPais._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
        }

        // GET: Pais

        public ActionResult Index()
        {
            List<PaisModelView> usuarioModelView = Mapper.Map<List<PaisModel>, List<PaisModelView>>(servicioPais.GetAllPais());
            return View(usuarioModelView);
        }
      
        public ActionResult Agregar()
        {
            return View();
        }

        public ActionResult Editar(int _id)
        {
            PaisModelView oPaisModel = new PaisModelView();
            oPaisModel = Mapper.Map<PaisModel, PaisModelView>(servicioPais.GetPais(_id));
            return View(oPaisModel);
        }

        [HttpPost]
        public ActionResult Agregar(PaisModelView oPaisModel)
        {

            int respuesta = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oPaisModel);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

                    PaisModel op = new PaisModel();
                    var oPais = Mapper.Map<PaisModelView, PaisModel>(oPaisModel);

                    op.Nombre = oPais.Nombre;
                    op.CodigoAfip = oPais.CodigoAfip;
                    op.Cuit = oPais.Cuit;
                    op.Activo = true;
                    op.IdUsuario = datosUsuario.IdUsuario;//hay que poner el id del usuario logueado
                    op.UltimaModificacion = DateTime.Now.Date;

                    respuesta = servicioPais.GuardarPais(op);

                    if (respuesta == 0) //grabo
                    {
                        servicioPais._mensaje("El país se registró correctamente", "ok");
                    }
                    else if (respuesta == -1) // paso algo
                    {
                        servicioPais._mensaje("El país ingresado No se pudo registrar", "error");
                    }
                    else //ya existe
                    {
                        servicioPais._mensaje("El país que intenta resitrar ya se encuentra cargado", "error");
                    }
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                servicioPais._mensaje(ex.Message, "error");
                return View(oPaisModel);
            }

        }

        [HttpPost]
        public ActionResult Editar(PaisModelView oPaisModel)
        {
            int respuesta = -1;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oPaisModel);
                }
                else
                {
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

                    PaisModel op = new PaisModel();
                    var oPais = Mapper.Map<PaisModelView, PaisModel>(oPaisModel);
                    op.Id = oPais.Id;
                    op.Nombre = oPais.Nombre;
                    op.CodigoAfip = oPais.CodigoAfip;
                    op.Cuit = oPais.Cuit;
                    op.Activo = true;
                    op.IdUsuario = datosUsuario.IdUsuario;//hay que poner el id del usuario logueado
                    op.UltimaModificacion = DateTime.Now.Date;

                    respuesta = servicioPais.ActualizarPais(op);

                    if (respuesta == 0) //grabo
                    {
                        servicioPais._mensaje("El país se registró correctamente", "ok");
                    }
                    else if (respuesta == -1) // paso algo
                    {
                        servicioPais._mensaje("El país ingresado No se pudo registrar", "error");
                    }
                    else //-2 existe
                    {
                        servicioPais._mensaje("El país que intenta resitrar ya se encuentra cargado", "error");
                    }

                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                servicioPais._mensaje(ex.Message, "error");
                return View(oPaisModel);
            }


        }

        [HttpPost]
        public ActionResult Eliminar (int idPais)
        {

            try
            {
                servicioPais.EliminarPais(idPais);
                servicioPais._mensaje("El país se eliminó correctamente", "ok");
            }
            catch (Exception ex)
            {
                servicioPais._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }

    }
}