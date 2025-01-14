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

    public class StageController : Controller
    {
        private StageHandler _handler = new StageHandler();


        // GET: Stage
        public ActionResult Index()
        {
             return PartialView(_handler.GetAll());
        }

        public ActionResult Create(int? ID)
        {
            var model = (ID == null) ? (_handler.Create()) : _handler.GetById(ID.Value);

            return PartialView("_Create",model);
        }

        [HttpPost]
        public ActionResult Create(StageViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsNew)
                {
                    _handler.Create(model);
                    TempData["Msg"] = 1;
                    return PartialView("_List", _handler.GetAll());
                }
                else {
                    _handler.Update(model);
                    TempData["Msg"] = 2;
                    return PartialView("_List", _handler.GetAll());
                }
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _handler.GetAll());

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