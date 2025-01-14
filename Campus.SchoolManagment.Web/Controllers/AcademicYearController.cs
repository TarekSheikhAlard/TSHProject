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

namespace Campus.SchoolManagment.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Employee")]

    public class AcademicYearController : Controller
    {
        private AcademicYearHandler _handler = new AcademicYearHandler();
        private StageHandler _Stagehandler = new StageHandler();


        // GET: AcademicYear
        public ActionResult Index()
        {
             return PartialView(_handler.GetAll());
        }

        public ActionResult Create()
        {
            ViewBag.Stage_ID = new SelectList(_Stagehandler.GetAll(), "Stage_ID", "StageNameEn");
            var model = _handler.Create();

            return PartialView("_Create", model);
        }

        [HttpPost]
        public ActionResult Create(AcademicYearViewModel model)
        {
            if (ModelState.IsValid)
            {
                    _handler.Create(model);
                    TempData["Msg"] = 1;
                    return PartialView("_List", _handler.GetAll());
                
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _handler.GetAll());

        }

        public ActionResult Edit(int? ID)
        {
            ViewBag.Stage_ID = new SelectList(_Stagehandler.GetAll(), "Stage_ID", "StageNameEn");
            var model = _handler.GetById(ID.Value);

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(AcademicYearViewModel model)
        {
            if (ModelState.IsValid)
            {
                _handler.Update(model);
                    TempData["Msg"] = 2;
                    return PartialView("_List", _handler.GetAll());
                
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _handler.GetAll());

        }

        [HttpPost]
        public ActionResult List(int Stage_ID)
        {
            var list = _handler.GetAll().Where(c => c.Stage_ID == Stage_ID).ToList();
            return PartialView("_List", list);
        }

        // GET: AdmStages/Delete/5
        public ActionResult Delete(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return PartialView("_Delete", ID);
        }

        // POST: Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            try
            {
                _handler.Delete(ID);
                TempData["Msg"] = 3;
                return PartialView("_List", _handler.GetAll());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _handler.GetAll());
            }
        }
    }
}