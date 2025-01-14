﻿using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Employee")]

    public class StudyYearController : BaseController
    {
        StudyYearHandler _StudyYearHandler = new StudyYearHandler();

        public ActionResult Index()
        {
        //    var model = _StudyYearHandler.GetAll();
        //     return PartialView(model);

            if (companyId != 0)

            {
                 return PartialView(_StudyYearHandler.GetAllByCompany(companyId,schoolId));
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
        public ActionResult Create(StudyYearViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                model.SchoolID = schoolId;
                _StudyYearHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _StudyYearHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _StudyYearHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _StudyYearHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _StudyYearHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _StudyYearHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudyYearViewModel model)
        {
            if (ModelState.IsValid)
            {
                _StudyYearHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _StudyYearHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _StudyYearHandler.GetAll());
        }
    }
}