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
    public class JobsController : BaseController
    {
        private JobsHandlar _JobsHandlar = new JobsHandlar();
        private DepartmentHandler _DepartmentHandler = new DepartmentHandler();
        // GET: Jobs
        public ActionResult Index()
        {
            if (companyId != 0)

            {
                 return PartialView(_JobsHandlar.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
           
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Department = new SelectList(_DepartmentHandler.GetAll(), "DepartmentID", "DepartMentEn");
            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(JobsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyId = companyId;
                model.SchoolId = schoolId;
                _JobsHandlar.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _JobsHandlar.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _JobsHandlar.GetAllByCompany(companyId, schoolId));
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _JobsHandlar.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _JobsHandlar.GetAllByCompany(companyId, schoolId));
        }


        public ActionResult Edit(int id)
        {
            ViewBag.Department = new SelectList(_DepartmentHandler.GetAllByCompany(companyId, schoolId), "DepartmentID", "DepartMentEn");

            return PartialView("_Edit", _JobsHandlar.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobsViewModel model)
        {
            if (ModelState.IsValid)
            {
                _JobsHandlar.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _JobsHandlar.GetAllByCompany(companyId, schoolId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _JobsHandlar.GetAllByCompany(companyId, schoolId));
        }

    }
}