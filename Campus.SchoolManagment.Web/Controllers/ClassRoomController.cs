using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class ClassRoomController : Controller
    {
        ClassRoomHandler _ClassRoomHandler = new ClassRoomHandler();
        AcademicYearHandler _AcademicYearHandler = new AcademicYearHandler();
        EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        public ActionResult Index()
        {
             return PartialView(_ClassRoomHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AcademicYear = new SelectList(_AcademicYearHandler.GetAll(), "AcademicID", "AcadmicName");
            ViewBag.ClassLeader = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "NameE");
            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(ClassRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                _ClassRoomHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _ClassRoomHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _ClassRoomHandler.GetAll());
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _ClassRoomHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _ClassRoomHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {
            ViewBag.AcademicYear = new SelectList(_AcademicYearHandler.GetAll(), "AcademicID", "AcadmicName");
            ViewBag.ClassLeader = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "NameE");
            return PartialView("_Edit", _ClassRoomHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                _ClassRoomHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _ClassRoomHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _ClassRoomHandler.GetAll());
        }
    }
}