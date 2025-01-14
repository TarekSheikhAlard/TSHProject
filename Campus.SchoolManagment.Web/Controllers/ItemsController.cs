using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class ItemsController : BaseController
    {
        ItemsHandler ItemsHandler = new ItemsHandler();
        SupplierHandler SupplierHandler = new SupplierHandler();
        MaterialManufactureHandler MaterialManufactureHandler = new MaterialManufactureHandler();
        // GET: Items
        public ActionResult Index()
        {
          
            return View(ItemsHandler.GetAll());
        }

        public ActionResult Items()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {

            ViewBag.Supplier = new SelectList(SupplierHandler.GetAllByCompany(companyId,schoolId), "ID", "SupplierName");
            ViewBag.material = new SelectList(MaterialManufactureHandler.GetAllCompanyId(companyId), "Id", "MaterialName"+lang);

            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                // model.CompanyID = companyId;
                ItemsHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", ItemsHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", ItemsHandler.GetAll());
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            ItemsHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", ItemsHandler.GetAll());
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Supplier = new SelectList(SupplierHandler.GetAllByCompany(companyId, schoolId), "ID", "SupplierName");
            ViewBag.material = new SelectList(MaterialManufactureHandler.GetAllCompanyId(companyId), "Id", "MaterialName" + lang);

            return PartialView("_Edit", ItemsHandler.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                ItemsHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", ItemsHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", ItemsHandler.GetAll());
        }




        public object GetAllItems() {

            return Newtonsoft.Json.JsonConvert.SerializeObject(ItemsHandler.GetAll().Select(x => new {      
               value = x.ItemId,
              name = x.nameEn
            }));

        }

    }
}