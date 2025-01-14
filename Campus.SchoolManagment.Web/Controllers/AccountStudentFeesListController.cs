using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class AccountStudentFeesListController : Controller
    {
        AccountStudentFeesListHandler _AccountStudentFeesListHandler = new AccountStudentFeesListHandler();
        //AcademicYearHandler _AcademicYearHandler = new AcademicYearHandler();

        public ActionResult Index()
        {
             return PartialView(_AccountStudentFeesListHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AcademicYear = new SelectList(_AcademicYearHandler.GetAll(), "AcademicID", "AcadmicName");
            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(AccountStudentFeesListViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountStudentFeesListHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _AccountStudentFeesListHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _AccountStudentFeesListHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _AccountStudentFeesListHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _AccountStudentFeesListHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {
            ViewBag.AcademicYear = new SelectList(_AcademicYearHandler.GetAll(), "AcademicID", "AcadmicName");
            return PartialView("_Edit", _AccountStudentFeesListHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountStudentFeesListViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountStudentFeesListHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _AccountStudentFeesListHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _AccountStudentFeesListHandler.GetAll());
        }

    }
}