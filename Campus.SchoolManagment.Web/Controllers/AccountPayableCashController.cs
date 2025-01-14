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
    public class AccountPayableCashController : BaseController
    {

        AccountPayableCashHandler _PayableCashHandler = new AccountPayableCashHandler();
        AccountTreasuryHandler _TreasuryHandler = new AccountTreasuryHandler();
        AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();
        BankCurrencyHandler _bankCurrency = new BankCurrencyHandler();
        EmployeeHandler _EmployeeHandler = new EmployeeHandler();

        public ActionResult Index()
        {
             return PartialView(_PayableCashHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Currency = new SelectList(_bankCurrency.GetAll(), "BankCurrencyID", "Currency_Type");
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "Name"+lang);
            ViewBag.Treasury = new SelectList(_TreasuryHandler.GetAll(), "ID", "TreasuryName"+lang);
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4), "AccountTreeID", "AccountName"+lang);
            return PartialView("_Create", _PayableCashHandler.Create());
        }



        [HttpPost]
        public ActionResult Create(AccountPayableCashViewModel model)
        {
            if (ModelState.IsValid)
            {
                _PayableCashHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _PayableCashHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _PayableCashHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _PayableCashHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _PayableCashHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {
            ViewBag.Currency = new SelectList(_bankCurrency.GetAll(), "BankCurrencyID", "Currency_Type");
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "Name"+lang);
            ViewBag.Treasury = new SelectList(_TreasuryHandler.GetAll(), "ID", "TreasuryName"+lang);
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4), "AccountTreeID", "AccountName"+lang);

            return PartialView("_Edit", _PayableCashHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountPayableCashViewModel model)
        {
            if (ModelState.IsValid)
            {
                _PayableCashHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _PayableCashHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _PayableCashHandler.GetAll());

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