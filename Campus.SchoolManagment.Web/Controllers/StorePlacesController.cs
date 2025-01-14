using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class StorePlacesController : BaseController
    {
        // GET: StorePlaces
        private StorePlacesHandler StorePlacesHandler = new StorePlacesHandler();

        public ActionResult Index()
        {
            if (companyId != 0)

            {
                return PartialView(StorePlacesHandler.GetAllCompanyId(companyId));
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
        
        public ActionResult Create(StorePlacesViewModel model)
        {
            if (ModelState.IsValid)
            {

                StorePlacesHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", StorePlacesHandler.GetAllCompanyId(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", StorePlacesHandler.GetAllCompanyId(companyId));
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            StorePlacesHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", StorePlacesHandler.GetAllCompanyId(companyId));
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", StorePlacesHandler.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StorePlacesViewModel model)
        {
            if (ModelState.IsValid)
            {
                StorePlacesHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", StorePlacesHandler.GetAllCompanyId(companyId));

            }
            TempData["Msg"] = 4;
            return PartialView("_List", StorePlacesHandler.GetAllCompanyId(companyId));
        }
    }
}