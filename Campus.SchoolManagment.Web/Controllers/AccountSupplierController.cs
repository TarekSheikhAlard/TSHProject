using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    // [Filters.Compress]
    [Authorize]
    public class AccountSupplierController : BaseController
    {
      
        SupplierHandler SupplierHandler = new SupplierHandler();

        SupplierFormHandler formHandler = new SupplierFormHandler();

        AccountTreeHandler _TreeHandler = new AccountTreeHandler();


        BankBranchHandler _HandlerBankBranch = new BankBranchHandler();
        EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        CityHandler _CityHandler = new CityHandler();

        public ActionResult Index()
        {
            if (companyId != 0)

            {
                 return PartialView(SupplierHandler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.BranchList = new SelectList(_HandlerBankBranch.GetAllByCompany(companyId), "ID", "BranchName"+lang);
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAllByCompany(companyId, schoolId), "Employee_ID", "Name"+lang);
            ViewBag.CityList = new SelectList(_CityHandler.GetAllByCompanyID(companyId), "ID", "City"+lang);

            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                model.SchoolID = schoolId;
                SupplierHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", SupplierHandler.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", SupplierHandler.GetAllByCompany(companyId, schoolId));
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            SupplierHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", SupplierHandler.GetAllByCompany(companyId, schoolId));
        }



        public ActionResult Edit(int id)
        {
            ViewBag.BranchList = new SelectList(_HandlerBankBranch.GetAllByCompany(companyId), "ID", "BranchName"+lang);
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAllByCompany(companyId, schoolId), "Employee_ID", "Name"+lang);
            ViewBag.CityList = new SelectList(_CityHandler.GetAllByCompanyID(companyId), "ID", "City"+lang);


            return PartialView("_Edit", SupplierHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                SupplierHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", SupplierHandler.GetAllByCompany(companyId, schoolId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", SupplierHandler.GetAllByCompany(companyId, schoolId));
        }



        public ActionResult GetAccounts()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName"+lang);

            return PartialView("_AccountTree");
        }


        [HttpPost]

        public JsonResult GetBranchsAccount(int id)
        {
            dynamic list;
            if (lang.ToLower().Equals("ar"))
                 list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).Select(x => new { x.AccountTreeID, x.AccountNameAR }).ToList();
            else
                 list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).Select(x => new { x.AccountTreeID, x.AccountNameEN }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Supplier(int id) {

            var supplierDetails = SupplierHandler.GetById(id);

            ViewBag.SupplierId = supplierDetails.ID;
            ViewBag.SupplierName = supplierDetails.SupplierName;
            ViewBag.Email = supplierDetails.Email;
            ViewBag.Address = supplierDetails.Address;
            ViewBag.Phone = supplierDetails.Phone;
            ViewBag.Mobile = supplierDetails.Mobile;

            var model = formHandler.GetAllBySupplier(id);
            return PartialView("Supplier",model);
        }

        public ActionResult CreateQuotation(SupplierFormViewModel model)
        {
            model.Status = "P";
            model.Type = "Q";

            formHandler.Create(model);
            var formList = formHandler.GetAllBySupplier(model.SupplierId);
            return PartialView("Supplier", formList);
        }
        public JsonResult ConvertQuotation(int id)
        {
           
             var result=formHandler.ChangeType(id);

            return Json(result);
        }





    }
}