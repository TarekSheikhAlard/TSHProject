using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Authorize]
    public class AccountCostCenterController : BaseController
    {
        private AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();

        public ActionResult Index()
        {
            if (companyId != 0)

            {
                 return PartialView(_CostCenterHandler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }

        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(AccountCostCenterViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                model.SchoolID = schoolId;
                _CostCenterHandler.Create(model);
                TempData["Msg"] = 1;

                return PartialView("_List", _CostCenterHandler.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _CostCenterHandler.GetAllByCompany(companyId, schoolId));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _CostCenterHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _CostCenterHandler.GetAllByCompany(companyId, schoolId));
        }

        public ActionResult Edit(int id)
        {
         
            return PartialView("_Edit", _CostCenterHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountCostCenterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _CostCenterHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _CostCenterHandler.GetAllByCompany(companyId, schoolId));
               
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _CostCenterHandler.GetAllByCompany(companyId, schoolId));
        }


        public object CostCenterSelectList() {

            var obj = new
            {
                data = _CostCenterHandler.GetAllByCompany(companyId, schoolId)
                .Select(x => new {
                    name = lang.ToLower() == "ar" ? x.CostCenterAR:x.CostCenterEn,
                    value=x.ID
                })
            };
            return JsonConvert.SerializeObject(obj);
        }







    }
}