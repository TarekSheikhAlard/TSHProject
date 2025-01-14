using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Campus.SchoolManagment.Web.Controllers
{
    //[Filters.Compress]
    public class EmployeeLoansController : BaseController
    {
        private EmployeeLoansHandler _employeeLoansHandler = new EmployeeLoansHandler();

        public EmployeeLoansController()
        {
            // int companyId = 0;

            if (System.Web.HttpContext.Current.Session["CompanyID"] != null)
            {
                companyId = int.Parse(System.Web.HttpContext.Current.Session["CompanyID"].ToString());

            }
        }

        // GET: EmployeeLoans

      

        public ActionResult Index(int id)
        {
            ViewBag.empid = id;
            return PartialView();

           
        }

        public ActionResult GetList(int empid)
        {   
            return PartialView("_List",_employeeLoansHandler.GetAllByEmployeeId(empid));
        }

        [HttpGet]
        public ActionResult Create(int empid)
        {
            EmployeeLoansViewModel model = new EmployeeLoansViewModel();
            model.Employee_ID = empid;
            return PartialView("_Create",model);
        }

        [HttpPost]
        public ActionResult Create(EmployeeLoansViewModel model)
        {
            if (ModelState.IsValid)
            {
                _employeeLoansHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _employeeLoansHandler.GetAllByEmployeeId(model.Employee_ID));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _employeeLoansHandler.GetAllByEmployeeId(model.Employee_ID));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _employeeLoansHandler.Delete(id);
            var x = _employeeLoansHandler.GetEmployeeIdFromRecId(id);
            TempData["Msg"] = 3;


            return PartialView("_List", _employeeLoansHandler.GetAllByEmployeeId(x.Employee_ID));
        }

        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _employeeLoansHandler.GetById(id));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeLoansViewModel model)
        {
            if (ModelState.IsValid)
            {
                _employeeLoansHandler.Update(model);
                TempData["Msg"] = 2;

                return PartialView("_List", _employeeLoansHandler.GetAllByEmployeeId(model.Employee_ID));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _employeeLoansHandler.GetAllByEmployeeId(model.Employee_ID));
        }

    }
}