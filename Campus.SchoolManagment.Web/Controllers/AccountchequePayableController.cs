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
    public class AccountchequePayableController : BaseController
    {
        AccountchequePayableHandler _chequePayableHandler = new AccountchequePayableHandler();
        AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();
        BankBranchHandler _HandlerBankBranch = new BankBranchHandler();
        BankCurrencyHandler _bankCurrency = new BankCurrencyHandler();
        EmployeeHandler _EmployeeHandler = new EmployeeHandler();

        public ActionResult Index()
        {
             return PartialView(_chequePayableHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.BranchList = new SelectList(_HandlerBankBranch.GetAll(), "ID", "BranchName"+lang);
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter"+lang);
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "Name"+lang);
            //ViewBag.Currency = new SelectList(_bankCurrency.GetAll(), "BankCurrencyID", "Currency_Type");

            return PartialView("_Create", _chequePayableHandler.Create());
        }



        [HttpPost]
        public ActionResult Create(AccountchequePayableViewModel model)
        {
            if (ModelState.IsValid)
            {
                _chequePayableHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _chequePayableHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _chequePayableHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _chequePayableHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _chequePayableHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {

            ViewBag.BranchList = new SelectList(_HandlerBankBranch.GetAll(), "ID", "BranchName"+lang);
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter"+lang);
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "Name"+lang);
            ViewBag.Currency = new SelectList(_bankCurrency.GetAll(), "BankCurrencyID", "Currency_Type");
            return PartialView("_Edit", _chequePayableHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountchequePayableViewModel model)
        {
            if (ModelState.IsValid)
            {
                _chequePayableHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _chequePayableHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _chequePayableHandler.GetAll());
        }



        public ActionResult GetAccounts2()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName"+lang);

            return PartialView("_AccountTree");
        }

        public ActionResult GetAccounts1()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName"+lang);

            return PartialView("_AccountTree1");
        }

        [HttpPost]

        public JsonResult GetBranchsAccount(int id)
        {
            var list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).Select(x => new { x.AccountTreeID, x.AccountNameAR }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }

    }
}