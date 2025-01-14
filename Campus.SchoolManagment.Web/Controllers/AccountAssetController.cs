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
    //[Filters.Compress]
    [Authorize]
    public class AccountAssetController : BaseController
    {
        AccountAssetHandler _AccountAssetHandler = new AccountAssetHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();
        AssetTreeHandler _AssetTreeHandler = new AssetTreeHandler();
        DepartmentHandler _DepartmentHandler = new DepartmentHandler();

        public ActionResult Index()
        {
            if (companyId != 0)

            {
                 return PartialView(_AccountAssetHandler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }

        }

        [HttpGet]
        [Filters.Compress]
        public ActionResult Create()
        {           
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeParentID == 6 || x.AccountTreeParentID == 7), "AccountTreeID", "AccountName"+lang);
            ViewBag.DepartmentID = new SelectList(_DepartmentHandler.GetAll(), "DepartmentID", "Department" + lang);
            return PartialView("_Create");
        }

        [HttpPost]
        [Filters.Compress]
        public ActionResult Create(AccountAssetViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                model.SchoolID = schoolId;
                _AccountAssetHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _AccountAssetHandler.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _AccountAssetHandler.GetAllByCompany(companyId, schoolId));
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _AccountAssetHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _AccountAssetHandler.GetAllByCompany(companyId, schoolId));
        }

        [Authorize]
        [Filters.Compress]
        public ActionResult Edit(int id)
        {

            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeParentID == 6 || x.AccountTreeParentID == 7), "AccountTreeID", "AccountName" + lang);
            ViewBag.DepartmentID = new SelectList(_DepartmentHandler.GetAll(), "DepartmentID", "Department" + lang);
            return PartialView("_Edit", _AccountAssetHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Filters.Compress]
        public ActionResult Edit(AccountAssetViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountAssetHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _AccountAssetHandler.GetAllByCompany(companyId, schoolId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _AccountAssetHandler.GetAllByCompany(companyId, schoolId));
        }

        [Filters.Compress]
        public ActionResult GetAsset()
        {
            ViewBag.MainassetTree = new SelectList(_AssetTreeHandler.GetAll().Where(x => x.AssetTreeLevel == 1).OrderBy(x => x.AssetTreeID), "AssetTreeID", "AssetName"+lang);

            return PartialView("_AssetTree");
        }

        [HttpPost]
        [Filters.Compress]
        public JsonResult GetBranchsAsset(int id)
        {

            dynamic list;
            if (lang.ToLower().Equals("ar"))
                list = _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == id).Select(x => new { x.AssetTreeID, x.AssetNameAR }).ToList();
            else
                list = _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == id).Select(x => new { x.AssetTreeID, x.AssetNameEN }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        [Filters.Compress]
        public ActionResult GetAccounts()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName" + lang);

            return PartialView("_AccountTree");
        }


        [HttpPost]
        [Filters.Compress]
        public JsonResult GetBranchsAccount(int id)
        {


            dynamic list;
            if (lang.ToLower().Equals("ar"))
                list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).Select(x => new { x.AccountTreeID, x.AccountNameAR }).ToList();
            else
                list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).Select(x => new { x.AccountTreeID, x.AccountNameEN }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);


        }

        [Filters.FileDownload]
        public ActionResult ExportAll()
        {
            dynamic list = _AccountAssetHandler.GetAll().ToList();

            return new ExcelResult(list, string.Format("Assets-{0}", DateTime.Now), "Asset Report",
                new string[] {    
                "Name",
                "NameAr",
                "Amount",
                "Code",
                "AccountTreeID",
                "AccountTree",
                 "PurchaseDate",
                "depreciationRate",
                "depreciationType",
                "depreciationYears",
                "Notes",  
                "AccountTree"
                });


        }


        [Filters.FileDownload]
        public ActionResult ExportPdf()
        {


            var list = _AccountAssetHandler.GetAll().ToList();
            //return PartialView("_DailyJournalDetailList", list._AccountDailyJournalDetail);
            string html = RenderViewToString(this.ControllerContext, "_List_Pdf", list);

            //return Content(html);

            return new PDFResult(html, "Assets", "Assets - All Records");

        }

    }
}