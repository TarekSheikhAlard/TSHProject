using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Employee")]

    public class QurterController : Controller
    {
        QurterHandler _QurterHandler = new QurterHandler();
        AcademicYearHandler _AcademicYearHandler = new AcademicYearHandler();
        SemsterHandler _SemsterHandler = new SemsterHandler();
        StudyYearHandler studyyearhandler = new StudyYearHandler();

        public ActionResult Index()
        {
            ViewBag.Year = new SelectList(studyyearhandler.GetAll(), "YearID", "YearNameE");

             return PartialView(_QurterHandler.GetAll());
        }


        public ActionResult QuterList(int id)
        {
            var model = _QurterHandler.GetAll().Where(x => x.YearID == id);

            return PartialView("_List",model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SID = new SelectList(_SemsterHandler.GetAll(), "SID", "SemNameE");
            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(QurterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _QurterHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _QurterHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _QurterHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _QurterHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _QurterHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {
            //ViewBag.SID = new SelectList(_SemsterHandler.GetAll(), "SID", "SemNameE");
            return PartialView("_Edit", _QurterHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QurterViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                _QurterHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _QurterHandler.GetAll().Where(x=>x.YearID==model.YearID));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _QurterHandler.GetAll());
        }




        
    }
}