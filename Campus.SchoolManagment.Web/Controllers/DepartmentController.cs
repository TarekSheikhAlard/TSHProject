using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    public class DepartmentController : BaseController
    {
       

        private DepartmentHandler _DepartmentHandler = new DepartmentHandler();
        // GET: Department
        public ActionResult Index()
        {
            if (companyId != 0)

            {
                 return PartialView(_DepartmentHandler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
           
        }


        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("_Create");
        }
        
        [HttpPost]
        public ActionResult Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                model.ScholID = schoolId;
                _DepartmentHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _DepartmentHandler.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _DepartmentHandler.GetAllByCompany(companyId, schoolId));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _DepartmentHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _DepartmentHandler.GetAllByCompany(companyId, schoolId));
        }


        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _DepartmentHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _DepartmentHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _DepartmentHandler.GetAllByCompany(companyId, schoolId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _DepartmentHandler.GetAllByCompany(companyId, schoolId));
        }

    }
}