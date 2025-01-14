using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.SchoolManagment.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Campus.SchoolManagment.Web.Controllers
{
    //[Filters.Compress]
    public class StoreController : BaseController
    {
        StoresHandler _StoresHandler = new StoresHandler();
        AccountTreeHandler _AccountTreeHandler = new AccountTreeHandler();
        InventoryTreeHandler _InventoryTreeHandler = new InventoryTreeHandler();
        AccountTreeHandler _accountTreeHandler = new AccountTreeHandler();
        EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        AccountCostCenterHandler _AccountCostCenterHandler = new AccountCostCenterHandler();
     
        // GET: Store
        public ActionResult Index()
        {
            if (companyId != 0)
            {
               
                return PartialView(_StoresHandler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }

        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Employees = new SelectList(_EmployeeHandler.GetAllByCompany(companyId, schoolId), "Employee_ID", "Name" + lang);
            ViewBag.CostCenter = new SelectList(_AccountCostCenterHandler.GetAllByCompany(companyId, schoolId), "ID", "CostCenter" + lang);
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(StoreViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                _StoresHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _StoresHandler.GetAllByCompany(companyId,schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _StoresHandler.GetAllByCompany(companyId, schoolId));
        }



        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _StoresHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                _StoresHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _StoresHandler.GetAllByCompany(companyId,schoolId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _StoresHandler.GetAllByCompany(companyId,schoolId));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _StoresHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _StoresHandler.GetAllByCompany(companyId,schoolId));
        }

        [Filters.Compress]
        public ActionResult GetAccounts()
        {
            ViewBag.MainaccountTree = new SelectList(_accountTreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName" + lang);

            return PartialView("_AccountTree");
        }

    }
}