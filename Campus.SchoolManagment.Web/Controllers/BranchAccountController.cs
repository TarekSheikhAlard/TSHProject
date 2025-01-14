using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.SchoolManagment.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    public class BranchAccountController : BaseController
    {

        private BranchAccountHandler _BranchAccountHandler = new BranchAccountHandler();
        private BankHandler _BankHandler = new BankHandler();
        private BankBranchHandler _BankBranchHandler = new BankBranchHandler();
        private BankCurrencyHandler _BankCurrencyHandler = new BankCurrencyHandler();

        public ActionResult Index()
        {
            if (companyId != 0)

            {
                 return PartialView(_BranchAccountHandler.GetAllByCompany(companyId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }

        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Banks = new SelectList(_BankHandler.GetAll(), "BankID", "BankName"+lang);
            ViewBag.Currency = new SelectList(_BankCurrencyHandler.GetAll(), "BankCurrencyID", "Currency_Type");
            return PartialView("_Create");
        }


        [HttpPost]

        public JsonResult GetBranchs(int id)
        {
            var list =  _BankBranchHandler.GetAll().Where(x => x.BankID == id).Select(x => new{x.ID,x.BranchNameEn}).ToList();

            return Json (list,JsonRequestBehavior.AllowGet);

            
        }


        [HttpPost]
        public ActionResult Create(BranchAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BranchAccountHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _BranchAccountHandler.GetAll());
            }

            TempData["Msg"] = 4;
            return PartialView("_List", _BranchAccountHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _BranchAccountHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _BranchAccountHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {
            var Bank = _BranchAccountHandler.GetById(id).BankID;
            ViewBag.Banks = new SelectList(_BankHandler.GetAll(), "BankID", "BankName"+lang);
            ViewBag.Branch = new SelectList(_BankBranchHandler.GetAll().Where(x=>x.BankID== Bank), "ID","BranchName"+lang);
            ViewBag.Currency = new SelectList(_BankCurrencyHandler.GetAll(), "BankCurrencyID", "Currency_Type");
            return PartialView("_Edit", _BranchAccountHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BranchAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BranchAccountHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _BranchAccountHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _BranchAccountHandler.GetAll());



        }



        [Filters.FileDownload]
        public ActionResult ExportPdf()
        {


            var list = _BranchAccountHandler.GetAllByCompany(companyId).ToList();
            //return PartialView("_DailyJournalDetailList", list._AccountDailyJournalDetail);
            string html = RenderViewToString(this.ControllerContext, "_List_Pdf", list);

            //return Content(html);

            return new PDFResult(html, "Branch Account", "Branch Account - All Records");

        }



    }
}