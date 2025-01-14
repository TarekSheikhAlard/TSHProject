using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class MaterialManufactureController : BaseController
    {
       
        private MaterialManufactureHandler MaterialManufactureHandler = new MaterialManufactureHandler();

        public ActionResult Index()
        {
            if (companyId != 0)

            {
                return PartialView(MaterialManufactureHandler.GetAllCompanyId(companyId));
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
        
        public ActionResult Create(MaterialManufactureViewModel model)
        {
            if (ModelState.IsValid)
            {

                MaterialManufactureHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", MaterialManufactureHandler.GetAllCompanyId(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", MaterialManufactureHandler.GetAllCompanyId(companyId));
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            MaterialManufactureHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", MaterialManufactureHandler.GetAllCompanyId(companyId));
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", MaterialManufactureHandler.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MaterialManufactureViewModel model)
        {
            if (ModelState.IsValid)
            {
                MaterialManufactureHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", MaterialManufactureHandler.GetAllCompanyId(companyId));

            }
            TempData["Msg"] = 4;
            return PartialView("_List", MaterialManufactureHandler.GetAllCompanyId(companyId));
        }
    }
}