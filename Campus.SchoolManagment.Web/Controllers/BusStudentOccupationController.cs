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
    public class BusStudentOccupationController : BaseController
    {
        private BusStudentOccupationHandler _BusStudentOccupationHandler = new BusStudentOccupationHandler();
        private BusHandler _busHandler = new BusHandler();
        private StudentHandler _StudentHandler = new StudentHandler();

        public ActionResult Index()
        {
             return PartialView(_BusStudentOccupationHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.bus = new SelectList(_busHandler.GetAll(), "BusID", "BusNo");
            return PartialView("_Create");
        }




        [HttpPost]
        public JsonResult SearchStudentName(long? StudentAcdID)
        {

            string StudentName = "";

            var result = _StudentHandler.GetAll().Where(x => x.StudentAcdID.ToString().Equals(StudentAcdID.ToString())).FirstOrDefault();

            if (result != null)
            {
                StudentName = lang.ToLower()=="ar"?result.ArabicName:result.NameEn;
            }

            return Json(StudentName, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Create(BusStudentOccupationViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BusStudentOccupationHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _BusStudentOccupationHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _BusStudentOccupationHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _BusStudentOccupationHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _BusStudentOccupationHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {
            ViewBag.bus = new SelectList(_busHandler.GetAll(), "BusID", "BusNo");
            return PartialView("_Edit", _BusStudentOccupationHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusStudentOccupationViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BusStudentOccupationHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _BusStudentOccupationHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _BusStudentOccupationHandler.GetAll());
        }
    }
}