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
    public class AccountSalaryItemController : BaseController
    {

        AccountSalaryItemHandler _SalaryItemHandler = new AccountSalaryItemHandler();
        // GET: AccountSalaryItem
        public ActionResult Index()
        {
            if (companyId != 0)

            {
                 return PartialView(_SalaryItemHandler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }


        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]

        public ActionResult Create(AccountSalaryItemViewModel model)
        {
            model.Salary_itemTypeID = (int)model.Salary_itemTypeID; 
           
            if (ModelState.IsValid)
            {
                if (companyId!=0)
                {
                    model.CompanyID = companyId;
                    model.SchoolID = schoolId;
                }
            
                _SalaryItemHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _SalaryItemHandler.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _SalaryItemHandler.GetAllByCompany(companyId, schoolId));

        }



        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            try
            {
                _SalaryItemHandler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _SalaryItemHandler.GetAllByCompany(companyId, schoolId));
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _SalaryItemHandler.GetAllByCompany(companyId, schoolId));
            }
        }


        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", _SalaryItemHandler.GetById(id));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountSalaryItemViewModel model)
        {
           if (ModelState.IsValid)
            {
                _SalaryItemHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _SalaryItemHandler.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _SalaryItemHandler.GetAllByCompany(companyId, schoolId));


        }




    }
}