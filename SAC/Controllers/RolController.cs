using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Models;
using Negocio.Servicios;
using Negocio.Modelos;
using AutoMapper;
using System.Web.Script.Serialization;
using Entidad.Models;

namespace SAC.Controllers
{
    public class RolController : BaseController
    {

        private ServicioConfiguracion servicioConfiguracion = new ServicioConfiguracion();
        private String JsonTreeView;
       
        private JavaScriptSerializer jsonString = new JavaScriptSerializer();

        public RolController()
        {
            servicioConfiguracion._mensaje = (msg_, tipo_) => AddMessage(tipo_, msg_);
        }


        // GET: Rol
        public ActionResult Index()
        {
            ConfigRolModelView model = new ConfigRolModelView
            {
                Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetAllRoles())               
            };
            return View(model);
        }
    
        [HttpPost, ActionName("AddAccionPorRol")]
        [ValidateAntiForgeryToken]
        public ActionResult AddAccionPorRol(RolModelView rolModelView)
        {
            var accionPorRol = new AccionPorRolModelView {
                                                    idRol = rolModelView.idRol,
                                                    idAccion = rolModelView.idAccionPorRol
                                                };           
            servicioConfiguracion.InsertarAccionPorRol(Mapper.Map<AccionPorRolModelView, AccionPorRolModel>(accionPorRol));
            return RedirectToAction("Edit", new { id = rolModelView.idRol });
         
        }

        [HttpPost, ActionName("DeleteAccionPorRol")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccionPorRol(int id)
        {
            var rol = (servicioConfiguracion.DeleteAccionPorRol(id));
            return RedirectToAction("Edit", new { id= rol.IdRol });
        }
     
        // GET: Rol/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rol/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rol/Create
        [HttpPost]
        public ActionResult Create(ConfigRolModelView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Rol.esAdministrador = false;
                    var rol = servicioConfiguracion.CrearRol(Mapper.Map<RolModelView, RolModel>(model.Rol));
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var rol = Mapper.Map<RolModel, RolModelView>(servicioConfiguracion.GetRolPorId(id));
           // acciones que no esten en el menu y tampoco en el rol es decir podrian ser dadas de alta para el rol
            List<AccionModelView> accionesfuerademune  = Mapper.Map<List<AccionModel>, List<AccionModelView>>(servicioConfiguracion.GetAccionNoMenu(rol.idRol));
            rol.Acciones = accionesfuerademune;

            //accione por rol que no esten el el menu ?
            rol.AccionPorRol =Mapper.Map<List<AccionPorRolModel>,List<AccionPorRolModelView>>(servicioConfiguracion.GetAllAccionPorRol(rol.idRol));

       

            //acciones por rol que se encuentran el el menu
            List<MenuSideBarModel> menuSiderPorRol =servicioConfiguracion.GetMenuSidebarPorRol(rol.idRol); 
            //para chek los las acciones del menu que tiene el usuario
            ViewBag.JsonMenuSiderPorRol= getMenuSideBarModelSelect(menuSiderPorRol);
            /// menu
            ViewBag.JsonMenuSider = TreeView(servicioConfiguracion.GetMenuSidebar());

          
            return View("Edit", rol);
        }      
       
        public string getMenuSideBarModelSelect(List<MenuSideBarModel> model)
        {
            String[] menuIds = new String[model.Count];
            for (int i = 0; i < model.Count; i++)
            {
                    menuIds[i] = model[i].Titulo;
            }              
            return string.Join(",", menuIds);
        }
       
        public String TreeView(ICollection<MenuSideBarModel> model) {
            List<TreeViewModel> ListTreeView = new List<TreeViewModel>();
            foreach (var i in model)
            {
                TreeViewModel item = new TreeViewModel();
                item.text = i.Titulo;
                item.href = i.IdMenuSidebar.ToString();
                if (i.Group.Count > 0)
                {
                    List<TreeViewModel> ListNode = new List<TreeViewModel>();
                    foreach (var n in i.Group)
                    {
                        TreeViewModel nodo = new TreeViewModel();
                        nodo.text = n.Titulo;
                        nodo.href = n.IdMenuSidebar.ToString();
                        ListNode.Add(nodo);                      
                    }
                    item.nodes = ListNode;
                }
                ListTreeView.Add(item);
            }            
            JsonTreeView += jsonString.Serialize(ListTreeView);
            return JsonTreeView; 
        }
      
        // POST: Rol/Edit/5
        [HttpPost]
        public ActionResult Edit( RolModelView model )
        {
            try
            {
                //acciones q debe tener el usuario
                List<AccionModel> accionModels = getMenuSideBarModelCheck(model.MenuSidePorRol);                
                List<AccionPorRolModelView> ListAccionPorRol = new List<AccionPorRolModelView>();                
                foreach (var item in accionModels)
                {
                    // existe accion para el rol?
                    var apr = servicioConfiguracion.GetAccionPorRol(model.idRol,item.IdAccion);
                    if(apr is null) {
                        // agregar nuevo menu para el rol
                        AccionPorRolModelView accionPorRol = new AccionPorRolModelView {                         
                            idRol = model.idRol,
                            idAccion = item.IdAccion
                            };
                         ListAccionPorRol.Add(accionPorRol); 
                     }
                }

                QuitarMenusiderAlRol(accionModels, model.idRol);
                 model.AccionPorRol = ListAccionPorRol;

                 servicioConfiguracion.ActualizarRol(Mapper.Map<RolModelView, RolModel>(model));
                 ViewBag.info = "Se Guardo Correctamente";
                
                return RedirectToAction("Edit", model.idRol);
            }
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            catch (Exception e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {
                return View();
            }
        }
    
        public List<AccionModel> getMenuSideBarModelCheck(string idMenusider)
        {
            var ids = idMenusider.Split(',').ToArray();
            List<AccionModel> accionDelMenu = new List<AccionModel>();
            foreach (var idmenusider in ids)
            {
                if(idmenusider.Length > 0)
                {
                    /// obtener por idmenusider la accion
                     var m =Mapper.Map<MenuSideBarModel,MenuSideBarModelView>( servicioConfiguracion.GetMenuSidebarPorIdFull(int.Parse(idmenusider)));                
                    accionDelMenu.Add(m.Accion);
                }             
            }
            return accionDelMenu;
        }

        public void QuitarMenusiderAlRol(List<AccionModel> accionModels ,int idRol )
        {
            //menu actual del usuario
            List<AccionModel> accionMenuSiderPorRol = servicioConfiguracion.GetAccionesEnMenuSidebarPorRol(idRol);
            // del menu nuevo que acciones no estan segun menu actual        
            var menuNuevo = (from a in accionModels  select a.IdAccion).ToList();
            var menuActual= (from m in accionMenuSiderPorRol select m.IdAccion).ToList();
            var result = (menuActual.Except(menuNuevo)).ToList();
            foreach (var item in result)
            {
                var acc = servicioConfiguracion.GetAccionPorRol(idRol, item);
                if(acc != null)
                {
                    DeleteAccionPorRol(acc.idRolPorAccion);
                }
            }

        }

        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(int Id)
        {
           // servicioConfiguracion.DeleteMenusidebar(Id);
            return Json(new { status = "Success" });
        }

        private SelectList selectListAccion(List<AccionModelView> acc)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (var item in acc)
            {
                dic.Add(item.IdAccion, item.Controlador + " - " + item.Nombre);
            }
            return new SelectList(dic, "Key", "Value");
        }


    }
}
