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
    [Authorize]
    public class AccountCustomerController : BaseController
    {
        AccountCustomerHandler _AccountCustomerHandler = new AccountCustomerHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();
        BankBranchHandler _HandlerBankBranch = new BankBranchHandler();
        EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        CityHandler _CityHandler = new CityHandler();
        public ActionResult Index()
        {
            if (companyId != 0)

            {
                 return PartialView(_AccountCustomerHandler.GetAllByCompany(companyId, schoolId));
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

            return PartialView("_Create",_AccountCustomerHandler.Create());
        }



        [HttpPost]
        public ActionResult Create(AccountCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                model.SchoolID = schoolId;
                _AccountCustomerHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _AccountCustomerHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _AccountCustomerHandler.GetAll());
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _AccountCustomerHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _AccountCustomerHandler.GetAllByCompany(companyId, schoolId)); ;
        }



        public ActionResult Edit(int id)
        {


            ViewBag.BranchList = new SelectList(_HandlerBankBranch.GetAll(), "ID", "BranchName"+lang);
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "Name"+lang);
            ViewBag.CityList = new SelectList(_CityHandler.GetAll(), "ID", "City"+lang);
            return PartialView("_Edit", _AccountCustomerHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountCustomerHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _AccountCustomerHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _AccountCustomerHandler.GetAll());
        }



        public ActionResult GetAccounts()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName"+lang);

            return PartialView("_AccountTree");
        }


        [HttpPost]

        public JsonResult GetBranchsAccount(int id)
        {


            var list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).Select(x => new { x.AccountTreeID, x.AccountNameAR }).ToList();



            return Json(list, JsonRequestBehavior.AllowGet);


        }

    }
}