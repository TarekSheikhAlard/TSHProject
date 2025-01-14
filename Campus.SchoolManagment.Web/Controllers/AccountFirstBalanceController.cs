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
    public class AccountFirstBalanceController : BaseController
    {
        AccountFirstBalanceHandler _AccountFirstBalanceHandler = new AccountFirstBalanceHandler();
        AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();


        public ActionResult Index()
        {
           //  return PartialView(_AccountFirstBalanceHandler.GetAll());

            if (companyId != 0)

            {
                 return PartialView(_AccountFirstBalanceHandler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAllByCompany(companyId, schoolId), "ID", "CostCenter"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4), "AccountTreeID", "AccountName"+lang);
            return PartialView("_Create", _AccountFirstBalanceHandler.Create());
        }



        [HttpPost]
        public ActionResult Create(AccountFirstBalanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                model.SchoolID = schoolId;
                _AccountFirstBalanceHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _AccountFirstBalanceHandler.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _AccountFirstBalanceHandler.GetAllByCompany(companyId, schoolId));
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _AccountFirstBalanceHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _AccountFirstBalanceHandler.GetAllByCompany(companyId, schoolId));
        }



        public ActionResult Edit(int id)
        {
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAllByCompany(companyId, schoolId), "ID", "CostCenter"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4), "AccountTreeID", "AccountName"+lang);

            return PartialView("_Edit", _AccountFirstBalanceHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountFirstBalanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountFirstBalanceHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _AccountFirstBalanceHandler.GetAllByCompany(companyId, schoolId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _AccountFirstBalanceHandler.GetAllByCompany(companyId, schoolId));
        }




    }
}