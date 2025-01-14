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
    public class BankCurrencyController : Controller
    {
        private BankCurrencyHandler _BankCurrencyHandler = new BankCurrencyHandler();
        private int companyId = 0;

        public BankCurrencyController()
        {

            if (System.Web.HttpContext.Current.Session["CompanyID"] != null)
            {
                companyId = int.Parse(System.Web.HttpContext.Current.Session["CompanyID"].ToString());

            }
        }

        public ActionResult Index()
        {
          
            var model = _BankCurrencyHandler.GetAllByCompany(companyId);
             return PartialView(model);
        }


        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(BankCurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                int companyId = 0;

                if (Session["CompanyID"] != null)
                {
                    companyId = int.Parse(Session["CompanyID"].ToString());

                }
                model.CompanyID = companyId;
                _BankCurrencyHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _BankCurrencyHandler.GetAllByCompany(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _BankCurrencyHandler.GetAllByCompany(companyId));
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _BankCurrencyHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _BankCurrencyHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _BankCurrencyHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankCurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BankCurrencyHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _BankCurrencyHandler.GetAllByCompany(companyId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _BankCurrencyHandler.GetAllByCompany(companyId));
        }


        public JsonResult currencyfactor(int BankCurrencyID)
        {

         double factor=_BankCurrencyHandler.GetById(BankCurrencyID).Factor.Value;

            return Json(factor, JsonRequestBehavior.AllowGet);

        }


    }
}