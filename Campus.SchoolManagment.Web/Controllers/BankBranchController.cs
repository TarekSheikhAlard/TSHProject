using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.SchoolManagment.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    public class BankBranchController : BaseController
    {

        private BankBranchHandler _BankBranchHandler = new BankBranchHandler();
        private BankHandler _BankHandler = new BankHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();

        // GET: BankBranch
        public ActionResult Index()
        {
            if (companyId!=0)
            {
                 return PartialView(_BankBranchHandler.GetAllByCompany(companyId));

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Bank = new SelectList(_BankHandler.GetAll(), "BankID", "BankName"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeParentID == 30), "AccountTreeID", "AccountName"+lang);

            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(BankBranchViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                _BankBranchHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _BankBranchHandler.GetAllByCompany(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _BankBranchHandler.GetAllByCompany(companyId));
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _BankBranchHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _BankBranchHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {
            ViewBag.Bank = new SelectList(_BankHandler.GetAll(), "BankID", "BankName"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeParentID == 30), "AccountTreeID", "AccountName"+lang);

            return PartialView("_Edit", _BankBranchHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankBranchViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BankBranchHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _BankBranchHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _BankBranchHandler.GetAll());
        }



        [Filters.FileDownload]
        public ActionResult ExportPdf()
        {
            var list = _BankBranchHandler.GetAllByCompany(companyId).ToList();
            //return PartialView("_DailyJournalDetailList", list._AccountDailyJournalDetail);
            string html = RenderViewToString(this.ControllerContext, "_List_Pdf", list);

            //return Content(html);

            return new PDFResult(html, "Banks Branch", "Banks Branches - All Records");

        }




    }
}