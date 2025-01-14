using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.SchoolManagment.Web.Helper;
using static Campus.SchoolManagment.Web.Helper.SemanticControls;
using Campus.School.Management.Logic.BusinessLayer;



namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    [Authorize]
    public class AccountDailyJournalController : BaseController
    {
        private AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();
        private AccountDailyJournalDetailHandler _AccountDailyJournalDetailHandler = new AccountDailyJournalDetailHandler();
        private BankCurrencyHandler _bankhandler = new BankCurrencyHandler();
        private AccountTreeHandler _AccountTreeHandler = new AccountTreeHandler();
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        private AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();
        SchoolUserViewModel _dbUser = School.Management.Logic.BusinessLayer.Statistics.GetLogiccookie();
        AccountController _AccountController = new AccountController();
        UserPermissionHandler _permissionHandler = new UserPermissionHandler();
        JournalTypesHandler JournalTypes = new JournalTypesHandler();


        public ActionResult Index()
        {

            var list = _AccountDailyJournalHandler.GetAll().ToList();
            SchoolUserViewModel _dbUser = _AccountController.Getcookie();
            ViewBag.AttachedDocumentPath = string.Format("{0}{1}/{2}/journal/", Statistics.JournalAttachedDocumentPath, companyId, schoolId);
            ViewBag.JournalTypes = new SelectList(JournalTypes.GetAllByCompanyWithDefault(companyId), "ID", "Name" + lang);

            if (_dbUser != null)
            {
                // ViewBag.Permission = _permissionHandler.GetByRoleId(_dbUser.RoleID).Where(x => x.PageID == 5).FirstOrDefault();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return PartialView(list);
        }


        [HttpPost]
        public ActionResult GetDetail(int id)
        {
            var list = _AccountDailyJournalHandler.GetById(id);
            //return PartialView("_DailyJournalDetailList", list._AccountDailyJournalDetail);
            return PartialView("_DailyJournalDetailList", list);

        }
        [HttpPost]
        public ActionResult GetListByType(int id)
        {
            var list = _AccountDailyJournalHandler.GetAllByType(id);
            //return PartialView("_DailyJournalDetailList", list._AccountDailyJournalDetail);
            return PartialView("_List", list);

        }

        public ActionResult Create()
        {
            // ViewBag.Currency = new SelectList(_bankhandler.GetAll(), "BankCurrencyID", "Currency_Type");
            ViewBag.JournalType = new SelectList(JournalTypes.GetAllByCompany(companyId), "ID", "Name" + lang);

            ViewBag.JournalType1 = JournalTypes.GetAllByCompany(companyId).Select(x => new DropdownList
            {
                name = lang.ToLower() == "ar" ? x.NameAr : x.NameEn,
                value = x.ID.ToString()
            }).ToList();

            return PartialView("_Create", _AccountDailyJournalHandler.Create());
        }

        [HttpPost]
        public ActionResult Create(AccountDailyJournalViewModel model)
        {
            //model.DailyJournalDate = DateTime.Now;

            //if (ModelState.IsValid)
            //{
            model.SchoolID = schoolId;
            model.CompanyID = companyId;

            _AccountDailyJournalHandler.Create(model);
            TempData["Msg"] = 1;
            return PartialView("_List", _AccountDailyJournalHandler.GetAll());
           // }
           // TempData["Msg"] = 4;

//            return PartialView("_List", _AccountDailyJournalHandler.GetAll());

        }

        public JsonResult GetReferenceNo(string journalDate)
        {
            return Json(_AccountDailyJournalHandler.GetReferenceNo(journalDate), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddDetail(int DailyJournalID)
        {
            var detail = _AccountDailyJournalDetailHandler.Create();

            detail.DailyJournalID = DailyJournalID;
            //  ViewBag.AccountTree = new SelectList(_AccountTreeHandler.Getlevel3_4_5(), "AccountTreeID", "AccountName"+lang);

            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter" + lang);

            return PartialView("_DebitItm", detail);
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
                _AccountDailyJournalHandler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _AccountDailyJournalHandler.GetAll());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _AccountDailyJournalHandler.GetAll());
            }
        }


        public ActionResult Edit(int id)
        {
            AccountDailyJournalViewModel _AccountDailyJournal = _AccountDailyJournalHandler.GetById(id);
            //ViewBag.AccountTree = new SelectList(_AccountTreeHandler.Getlevel3_4_5(), "AccountTreeID", "AccountName"+lang);

            //ViewBag.Currency = new SelectList(_bankhandler.GetAll(), "BankCurrencyID", "Currency_Type");
            //ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter"+lang);

            ViewBag.JournalType1 = JournalTypes.GetAllByCompany(companyId).Select(x => new DropdownList
            {
                name = lang.ToLower() == "ar" ? x.NameAr : x.NameEn,
                value = x.ID.ToString()
            }).ToList();


            return PartialView("_Edit", _AccountDailyJournal);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountDailyJournalViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            _AccountDailyJournalHandler.Update(model);
            TempData["Msg"] = 1;
            return PartialView("_List", _AccountDailyJournalHandler.GetAll());
            //}
            //TempData["Msg"] = 4;
            //return PartialView("_List", _AccountDailyJournalHandler.GetAll());

        }



        //-----------ترحيل القيود ----------

        public ActionResult DailyJournalPost()
        {

            return PartialView();
        }




        [HttpPost]
        public ActionResult searchForpost(string From, string To)
        {
            //DateTime DateFrom = DateTime.ParseExact(From,"MM/dd/yyyy", null);
            //DateTime DateTo = DateTime.ParseExact(To,"MM/dd/yyyy", null);
            DateTime DateFrom = DateTime.ParseExact(From, "yyyy/MM/dd", null);
            DateTime DateTo = DateTime.ParseExact(To, "yyyy/MM/dd", null);
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;

            var list = _AccountDailyJournalHandler.GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo && x.IsPost == false);
            ViewBag.DateTo = DateTo;
            ViewBag.DateFrom = DateFrom;

            return PartialView("_ListForPost", list);
        }


        [HttpPost]
        public ActionResult Approvepost(List<int> ids, DateTime DateTo, DateTime DateFrom)
        {
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    var _daily = Context.AccountDailyJournals.Where(x => x.DailyJournalID == item).FirstOrDefault();
                    _daily.IsPost = true;
                    _daily.PostedbyID = _dbUser.ID;
                    _daily.PostedDate = DateTime.Now;

                }
                Context.SaveChanges();
                TempData["Msg"] = 1;
            }
            else
            {
                TempData["Msg"] = 4;
            }

            var list = _AccountDailyJournalHandler.GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo && x.IsPost == true);
            return PartialView("_ListForUNPost", list);
            //return RedirectToAction("DailyJournalPost");
        }

        //---------الرجوع فى الترحيل  ----------

        public ActionResult DailyJournalUNPost()
        {

            return PartialView();
        }
        [HttpPost]
        public ActionResult searchForUNpost(string From, string To)
        {
            DateTime DateFrom = DateTime.ParseExact(From, "yyyy/MM/dd", null);
            DateTime DateTo = DateTime.ParseExact(To, "yyyy/MM/dd", null);
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;

            var list = _AccountDailyJournalHandler.GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo && x.IsPost == true);
            ViewBag.DateTo = DateTo;
            ViewBag.DateFrom = DateFrom;

            return PartialView("_ListForUNPost", list);
        }

        [HttpPost]
        public ActionResult ApproveUNpost(List<int> ids, DateTime DateTo, DateTime DateFrom)
        {
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    var _daily = Context.AccountDailyJournals.Where(x => x.DailyJournalID == item).FirstOrDefault();
                    _daily.IsPost = false;
                    _daily.PostedbyID = _dbUser.ID;
                    _daily.PostedDate = DateTime.Now;

                }
                Context.SaveChanges();
                TempData["Msg"] = 1;
            }
            else
            {
                TempData["Msg"] = 4;
            }

            var list = _AccountDailyJournalHandler.GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo && x.IsPost == true);

            return PartialView("_ListForUNPost", list);

        }




        public ActionResult LedgerIndex()
        {
            GenericVModel model = new GenericVModel();
            ViewBag.AccountTreeList = _AccountTreeHandler.quickAccountTreeListFull().Select(x => new DropdownList
            {
                name = x.name,
                value = x.value.ToString(),
                text = x.text
            }).ToList();

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Ledger(int AccountTreeID, string From = "2019/08/15", string To = "2019/08/15")
        {
            //var list = _AccountDailyJournalDetailHandler.GetLedgerByAccountTree(AccountTreeID).ToList();

            string query = @" select * from [LedgerReportByAccountId](@p0,@p1,@p2,@p3) order by AccountTreeID,CreatedDate ";

            ViewBag.AccountTreeName = _AccountTreeHandler.GetById(AccountTreeID).AccountNameEN;
            ViewBag.From = From;
            ViewBag.To = To;
             var results = Context.Database.SqlQuery<SubLedgerReportViewModel>(query, schoolId, AccountTreeID, From, To).ToList();

            return PartialView("_LedgerList", results);

        }

        //----------------------------الاستاذ العام ------------------------------
        public ActionResult AccountProf()
        {
            ViewBag.AccountTree = new SelectList(_AccountTreeHandler.GetAll().Where(x => x.AccountTreeLevel == 2).OrderBy(x => x.AccountTreeParentID), "AccountTreeID", "AccountName" + lang);

            return PartialView();
        }


        [HttpPost]
        public ActionResult AccountProf(string From, string To, int? Accounttree)
        {

            return PartialView("_AccountProfList", _AccountDailyJournalHandler.accountprof(From, To, Accounttree));
        }
        //----------------------------------------------------------------------------


        //----------------------------الاستاذ المساعد  ------------------------------

        [HttpGet]
        public ActionResult subsidiaryledger()
        {

            return PartialView();
        }

        [HttpPost]
        public ActionResult subsidiaryledgerList(string From, string To, string AccountCode)
        {
            var child = _AccountTreeHandler.GetAll().Where(x => x.AccountTreeCode == AccountCode).FirstOrDefault();



            return PartialView("_subsidiaryledgerList", _AccountDailyJournalHandler.subsidiaryledger(From, To, child.AccountTreeID));
        }


        public ActionResult GetAccounts()
        {
            ViewBag.MainaccountTree = new SelectList(_AccountTreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeCode", "AccountName" + lang);

            return PartialView("_AccountTree");
        }


        [HttpPost]
        public JsonResult GetBranchsAccount(string id)
        {

            var child = _AccountTreeHandler.GetAll().Where(x => x.AccountTreeCode == id).FirstOrDefault();

            //  if (System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft)

            var list = _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == child.AccountTreeID).Select(x => new { x.AccountTreeCode, x.AccountNameAR }).ToList();



            return Json(list, JsonRequestBehavior.AllowGet);


        }

        //All Download Actions Below
        //----------------------------------------------------------------------------

        [Filters.FileDownload]
        public ActionResult ExportAll()
        {
            dynamic list = _AccountDailyJournalHandler.GetAll().ToList();

            return new ExcelResult(list, string.Format("Daily Journal-{0}", DateTime.Now), "Report",
                new string[] {
                "DailyJournalID",
                "SerialNo",
                "DocumentNumber",
                "Debit",
                "Credit",
                "DailyJournalDate",
                "Currency_Type",
                "Note",
                "Note1",
                "PhysicalYearName",
                "Description",
                "TransationRate",
                "IsPost"
                });


        }


        [Filters.FileDownload]
        public ActionResult ExportPdfDetail(int id)
        {

            var list = _AccountDailyJournalHandler.GetById(id);
            string html = RenderViewToString(this.ControllerContext, "_DailyJournalDetailList_Pdf", list);
            ViewBag.Pdf = "true";

            return new PDFResult(html, "DailyJournalDetail", "Daily Journal Details ID : " + id.ToString());

        }


        [Filters.FileDownload]
        public ActionResult ExportPdf()
        {


            var list = _AccountDailyJournalHandler.GetAll().ToList();
            //return PartialView("_DailyJournalDetailList", list._AccountDailyJournalDetail);
            string html = RenderViewToString(this.ControllerContext, "_List_Pdf", list);

            //return Content(html);

            return new PDFResult(html, "DailyJournalAll", "Daily Journall All Records");

        }


        [Filters.FileDownload]
        public ActionResult ExportExcelForpost(string From, string To)
        {
            DateTime DateFrom = DateTime.ParseExact(From, "yyyy/MM/dd", null);
            DateTime DateTo = DateTime.ParseExact(To, "yyyy/MM/dd", null);
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;

            var list = _AccountDailyJournalHandler.GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo && x.IsPost == false);
            ViewBag.DateTo = DateTo;
            ViewBag.DateFrom = DateFrom;

            return new ExcelResult(list, string.Format("Daily Journal Unposted- {0}", DateTime.Now), "Daily Journal Unposted",
               new string[] {
                "DailyJournalID",
                "SerialNo",
                "DocumentNumber",
                "Debit",
                "Credit",
                "DailyJournalDate",
                "Currency_Type",
                "Note",
                "Note1",
                "PhysicalYearName",
                "Description",
                "TransationRate",
                "IsPost"
               });
        }


        [Filters.FileDownload]
        public ActionResult ExportPdfForpost(string From, string To)
        {
            DateTime DateFrom = DateTime.ParseExact(From, "yyyy/MM/dd", null);
            DateTime DateTo = DateTime.ParseExact(To, "yyyy/MM/dd", null);
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;

            var list = _AccountDailyJournalHandler.GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo && x.IsPost == false);
            ViewBag.DateTo = DateTo;
            ViewBag.DateFrom = DateFrom;

            string html = RenderViewToString(this.ControllerContext, "_ListForPost_Pdf", list);

            return new PDFResult(html, "DailyJournal Unposted", string.Format("Daily Journal Unposted From: {0} To: {1}", DateFrom.ToShortDateString(), DateTo.ToShortDateString()));
        }


        [Filters.FileDownload]
        public ActionResult ExportExcelForUnpost(string From, string To)
        {
            DateTime DateFrom = DateTime.ParseExact(From, "yyyy/MM/dd", null);
            DateTime DateTo = DateTime.ParseExact(To, "yyyy/MM/dd", null);
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;

            var list = _AccountDailyJournalHandler.GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo && x.IsPost == true);
            ViewBag.DateTo = DateTo;
            ViewBag.DateFrom = DateFrom;

            return new ExcelResult(list, string.Format("Daily Journal Posted- {0}", DateTime.Now), "Daily Journal Posted",
               new string[] {
                "DailyJournalID",
                "SerialNo",
                "DocumentNumber",
                "Debit",
                "Credit",
                "DailyJournalDate",
                "Currency_Type",
                "Note",
                "Note1",
                "PhysicalYearName",
                "Description",
                "TransationRate",
                "IsPost"
               });
        }



        [Filters.FileDownload]
        public ActionResult ExportPdfForUnpost(string From, string To)
        {
            DateTime DateFrom = DateTime.ParseExact(From, "yyyy/MM/dd", null);
            DateTime DateTo = DateTime.ParseExact(To, "yyyy/MM/dd", null);
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;
            var list = _AccountDailyJournalHandler.GetAll().Where(x => x.DailyJournalDate >= DateFrom && x.DailyJournalDate <= DateTo && x.IsPost == true);

            ViewBag.DateTo = DateTo;
            ViewBag.DateFrom = DateFrom;

            string html = RenderViewToString(this.ControllerContext, "_ListForUNPost_Pdf", list);

            return new PDFResult(html, "DailyJournal Posted", string.Format("Daily Journal Posted From: {0} To: {1}", DateFrom.ToShortDateString(), DateTo.ToShortDateString()));
        }


        [Filters.FileDownload]
        public ActionResult ExportPdfLedgerReport(int AccountTreeID, string From = "2019/08/15", string To = "2019/08/15")
        {

            //var list = _AccountDailyJournalDetailHandler.GetLedgerByAccountTree(AccountTreeID).ToList();

            string query = @" select * from [LedgerReportByAccountId](@p0,@p1,@p2,@p3) order by  AccountTreeID,CreatedDate  ";

            ViewBag.AccountTreeName = _AccountTreeHandler.GetById(AccountTreeID).AccountNameEN;


            ViewBag.From = From;
            ViewBag.To = To;


            var results = Context.Database.SqlQuery<SubLedgerReportViewModel>(query, schoolId, AccountTreeID, From, To).ToList();

            //return PartialView("_DailyJournalDetailList", list._AccountDailyJournalDetail);
            string html = RenderViewToString(this.ControllerContext, "_LedgerList_Pdf", results);


            //return Content(html);

            return new PDFResult(html, "Ledger Report", "Ledger Report");

        }

        [Filters.FileDownload]
        public ActionResult ExportExcelLedgerReport(int AccountTreeID, string From = "2019/08/15", string To = "2019/08/15")
        {

            //var list = _AccountDailyJournalDetailHandler.GetLedgerByAccountTree(AccountTreeID).ToList();

            string query = @" select * from [LedgerReportByAccountId](@p0,@p1,@p2,@p3) order by AccountTreeID,CreatedDate ";

            ViewBag.AccountTreeName = _AccountTreeHandler.GetById(AccountTreeID).AccountNameEN;
            

            ViewBag.From = From;
            ViewBag.To = To;


            var results = Context.Database.SqlQuery<SubLedgerReportViewModel>(query, schoolId, AccountTreeID, From, To).ToList();

            return new ExcelResult(results, string.Format("Ledger Report- {0}", DateTime.Now), "Ledger Report");

        }

    }

}