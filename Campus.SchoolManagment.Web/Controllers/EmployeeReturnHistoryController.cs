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
using System.IO;
using Campus.SchoolManagment.Web.Models;

namespace Campus.SchoolManagment.Web.Controllers
{
   // [Filters.Compress]
    public class EmployeeReturnHistoryController : BaseController
    {
        private EmployeeHandler _Employee = new EmployeeHandler();
        private EmployeeReturnHistoryHandler _EmployeeReturnHistory = new EmployeeReturnHistoryHandler();

        // GET: EmployeeReturnHistory

        public ActionResult Index(int id)
        {
            ViewBag.empid = id;

            return PartialView();
        }
      

        //public ActionResult EmployeeList(int id)
        //{
        //    var model = _Employee.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID == id);

        //    return PartialView("_List", model);
        //}

        public ActionResult GetList(int id)
        {
            var model = _EmployeeReturnHistory.GetAllById(id);

            return PartialView("_List", model);
        }


        [HttpPost]
        public JsonResult Create(EmployeeReturnHistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                _EmployeeReturnHistory.Create(model);
                //TempData["Msg"] = 1;
                return Json(true);
            }
           // TempData["Msg"] = 4;
              return Json(false);
        }

    }
}