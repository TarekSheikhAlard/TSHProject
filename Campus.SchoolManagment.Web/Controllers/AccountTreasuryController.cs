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
    public class AccountTreasuryController : BaseController
    {
        private AccountTreasuryHandler _AccountTreasuryHandler = new AccountTreasuryHandler();

        EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();

        public ActionResult Index()
        {
            if (companyId != 0)

            {
                 return PartialView(_AccountTreasuryHandler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }


       [HttpGet]
        public ActionResult Create()
        {
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAllByCompany(companyId, schoolId), "Employee_ID", "Name"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeParentID==30), "AccountTreeID", "AccountName"+lang);


            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(AccountTreasuryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountTreasuryHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _AccountTreasuryHandler.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _AccountTreasuryHandler.GetAllByCompany(companyId, schoolId));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _AccountTreasuryHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _AccountTreasuryHandler.GetAllByCompany(companyId, schoolId));
        }


        public ActionResult Edit(int id)
        {
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAllByCompany(companyId, schoolId), "Employee_ID", "Name"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeParentID == 30), "AccountTreeID", "AccountName"+lang);
            return PartialView("_Edit", _AccountTreasuryHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountTreasuryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountTreasuryHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _AccountTreasuryHandler.GetAllByCompany(companyId, schoolId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _AccountTreasuryHandler.GetAllByCompany(companyId, schoolId));
        }









    }
}