using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
   
    public class UnitsMeasurementsController : BaseController
    {
        private UnitsMeasurementsHandler UnitsMeasurementsHandler = new UnitsMeasurementsHandler();

        // GET: UnitsMeasurements
        public ActionResult Index()
        {
            if (companyId != 0)

            {
                return PartialView(UnitsMeasurementsHandler.GetAllCompanyId(companyId));
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
      
        public ActionResult Create(UnitsMeasurementsViewModel model)
        {
            if (ModelState.IsValid)
            {

                UnitsMeasurementsHandler.Create(model);

                  TempData["Msg"] = 1;
                return PartialView("_List", UnitsMeasurementsHandler.GetAllCompanyId(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", UnitsMeasurementsHandler.GetAllCompanyId(companyId));
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            UnitsMeasurementsHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", UnitsMeasurementsHandler.GetAllCompanyId(companyId));
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
     
            return PartialView("_Edit", UnitsMeasurementsHandler.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UnitsMeasurementsViewModel model)
        {
            if (ModelState.IsValid)
            {
                UnitsMeasurementsHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", UnitsMeasurementsHandler.GetAllCompanyId(companyId));

            }
            TempData["Msg"] = 4;
            return PartialView("_List", UnitsMeasurementsHandler.GetAllCompanyId(companyId));
        }


    }
}