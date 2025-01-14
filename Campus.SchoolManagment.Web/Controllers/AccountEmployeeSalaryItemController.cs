using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.SchoolManagment.Web.Helper;

namespace Campus.SchoolManagment.Web.Controllers
{
    // [Filters.Compress]
    [Authorize]
    public class AccountEmployeeSalaryItemController : BaseController
    {
       private AccountEmployeeSalaryItemHandler _Handler = new AccountEmployeeSalaryItemHandler();
        private AccountSalaryItemHandler _SalaryItemHandler = new AccountSalaryItemHandler ();
        private EmployeeHandler _EmpHandler = new EmployeeHandler ();

        // GET: AccountEmployeeSalaryItem
        public ActionResult Index()
        {
            //var list =_Handler.GetAll().Where(x => x.IsDeleted == false).ToList();
            // return PartialView(list);

            if (companyId != 0)

            {
                 return PartialView(_Handler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }

        [HttpPost]  
        public JsonResult GetChild(int id)
        {

            var list = _SalaryItemHandler.GetAllByCompany(companyId, schoolId).Where(x => x.Salary_itemTypeID == id).Select(x => new
            {
                Id = x.ID,
                Salary_itemName = lang.ToLower() == "ar" ? x.Salary_itemNameAr : x.Salary_itemNameEn,
            }).ToList();
            
            return new JsonResult() { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 
        }

        public ActionResult Create()
        {
            ViewBag.Employee_ID = new SelectList(_EmpHandler.GetAllByCompany(companyId, schoolId), "Employee_ID", "Name"+lang);  
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(AccountEmployeeSalaryItemViewModel model)
        {
            //var list = _Handler.GetAll().Where(x => x.IsDeleted == false).ToList();
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                model.SchoolID = schoolId;
                _Handler.Create(model);

                TempData["Msg"] = 1;
                return PartialView("_List", _Handler.GetAllByCompany(companyId, schoolId));
            }
            ViewBag.Employee_ID = new SelectList(_EmpHandler.GetAllByCompany(companyId, schoolId), "Employee_ID", "Name"+lang);
            TempData["Msg"] = 4;
            return PartialView("_List", _Handler.GetAllByCompany(companyId, schoolId));
        }


        //public ActionResult Delete(int id)
        //{
        //     return PartialView(_Handler.GetById(id));

        //}

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }
        
        [HttpPost ,ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                _Handler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _Handler.GetAllByCompany(companyId, schoolId));
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _Handler.GetAllByCompany(companyId, schoolId));
            }
        }

        public ActionResult Edit(int id)
        {
            AccountEmployeeSalaryItemViewModel _item = _Handler.GetById(id);

            ViewBag.Employee_ID = new SelectList(_EmpHandler.GetAllByCompany(companyId, schoolId), "Employee_ID", "Name"+lang,_item.Employee_ID);
           
            ViewBag.SalaryItemID = new SelectList(_SalaryItemHandler.GetAllByCompany(companyId, schoolId).Where(x => x.Salary_itemTypeID == _item.SalaryItemTypeID), "ID", "Salary_itemName"+lang,_item.SalaryItemID);

            return PartialView("_Edit", _item);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountEmployeeSalaryItemViewModel model)
        {
            //var list = _Handler.GetAll().Where(x => x.IsDeleted == false).ToList();
            if (ModelState.IsValid)
            {
                _Handler.Update(model);
               
                TempData["Msg"] = 2;
                return PartialView("_List", _Handler.GetAllByCompany(companyId, schoolId));
            }
            ViewBag.Employee_ID = new SelectList(_EmpHandler.GetAll(), "Employee_ID", "Name"+lang,model.Employee_ID);
            
            ViewBag.SalaryItemID = new SelectList(_SalaryItemHandler.GetAllByCompany(companyId, schoolId).Where(x=> x.Salary_itemTypeID == model.SalaryItemTypeID), "ID", "Salary_itemName"+lang,model.SalaryItemID);
            TempData["Msg"] = 4;
            return PartialView("_List", _Handler.GetAllByCompany(companyId, schoolId));
            //  return PartialView(model); 
        }


        [HttpPost]
        public ActionResult Search(string name)
        {
            var list = _Handler.GetAllByCompany(companyId, schoolId).Where(x => x.AccountSalaryItemName.Contains(name));
            return PartialView("_Search", list);
        }



    }
}