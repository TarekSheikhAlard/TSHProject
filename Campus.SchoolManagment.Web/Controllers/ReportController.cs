using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    public class ReportController : BaseController
    {

       public ActionResult Index()
        {
             return PartialView();
        }
        // GET: Reports
        public ActionResult ReportsIndex()
        {
             return PartialView();
        }

    }
}
