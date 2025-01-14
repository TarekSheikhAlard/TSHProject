using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.BusinessLayer;
using System.Threading;
using System.Globalization;
using System.Net;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    public class BusTripController : Controller
    {
      
      private  BusTripHandler _BusTripHandler = new BusTripHandler();
       


        // GET: School
        public ActionResult Index()
        {
             return PartialView(_BusTripHandler.GetAll());
        }

      
       
    }
}