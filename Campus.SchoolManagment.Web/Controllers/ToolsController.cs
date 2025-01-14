using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class ToolsController : Controller
    {
        // GET: Tools
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Quotation()
        {
            return PartialView();
        }
        public ActionResult Quotation(int id)
        {
            return PartialView();
        }




    }
}