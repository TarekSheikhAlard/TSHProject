using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.SchoolManagment.Web.Helper;

namespace Campus.SchoolManagment.Web.Controllers
{
    //[Filters.Compress]
 
    public class BankController : BaseController
    {

        private BankHandler _BankHandler = new BankHandler();
        private AccountTreeHandler _AccountTree = new AccountTreeHandler();

        private int companyId = 0;

        public BankController()
        {
           // int companyId = 0;

            if (System.Web.HttpContext.Current.Session["CompanyID"] != null)
            {
                companyId = int.Parse( System.Web.HttpContext.Current.Session["CompanyID"].ToString());

            }
        }
        
        // GET: Bank
        public ActionResult Index()
        {
            ViewBag.lang = lang.ToLower();
             return PartialView(_BankHandler.GetAllByCompanyID(companyId));
        }


        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(BankViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                _BankHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _BankHandler.GetAllByCompanyID(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _BankHandler.GetAllByCompanyID(companyId));
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var result = _BankHandler.Delete(id);
            if (result)
                TempData["Msg"] = 3;
            else
                TempData["Msg"] = 5;

            return PartialView("_List", _BankHandler.GetAllByCompanyID(companyId));
        }


        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _BankHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BankHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _BankHandler.GetAllByCompanyID(companyId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _BankHandler.GetAllByCompanyID(companyId));
        }

        public JsonResult GetBankBalance(int id) {
            var balance = _BankHandler.GetById(id).OpeningBalance;
            return Json(balance);
        }
        [Filters.FileDownload]
        public ActionResult ExportPdf()
        {

            var list = _BankHandler.GetAllByCompanyID(companyId);
            string html = RenderViewToString(this.ControllerContext, "Banks_Pdf", list);

            return new PDFResult(html, "Banks", "Banks");

        }

        [Filters.FileDownload]
        public ActionResult ExportExcel()
        {
            dynamic list = _BankHandler.GetAllByCompanyID(companyId);

            return new ExcelResult(list, string.Format("Banks-{0}", DateTime.Now), "Bank Details",
                new string[] {
                "AccountNumber",
                "BankNameEn",
                "BankAccountNameEn",
                "BranchName",
                "OpeningBalance",
                "Description"
                });
        }
    }
}