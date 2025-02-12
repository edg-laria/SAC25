using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Controllers
{
    public class UnauthorisedController : BaseController
    {
        // GET: Unauthorised
        public ActionResult Index()
        {
            return View();
        }
    }
}