using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NoAccess()
        {
            return View();
        }
    }
}