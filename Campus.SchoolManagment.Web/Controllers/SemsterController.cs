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

    public class SemsterController : Controller
    {
        SemsterHandler _SemsterHandler = new SemsterHandler();
        AcademicYearHandler _AcademicYearHandler = new AcademicYearHandler();
        StudyYearHandler _StudyYearHandler = new StudyYearHandler();

        public ActionResult Index()
        {
             return PartialView(_SemsterHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Create()
        {
        
            ViewBag.YearID = new SelectList(_StudyYearHandler.GetAll(), "YearID", "YearNameE");
            return PartialView("_Create", _SemsterHandler.Create());
        }



        [HttpPost]
        public ActionResult Create(SemsterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _SemsterHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _SemsterHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _SemsterHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _SemsterHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _SemsterHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {
          
            return PartialView("_Edit", _SemsterHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SemsterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _SemsterHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _SemsterHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _SemsterHandler.GetAll());
        }


    
        [HttpPost]
        public JsonResult CheckSemester(int SemID, int? YearID, bool check)
        {
            if (YearID != null && check==true )
            {
                if (_SemsterHandler.GetAll().Any(x => x.SemID == SemID && x.YearID == YearID))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }






    }
}