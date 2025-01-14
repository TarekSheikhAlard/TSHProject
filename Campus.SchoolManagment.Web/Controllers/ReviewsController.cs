using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using Campus.SchoolManagment.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Campus.SchoolManagment.Web.Helper.SemanticControls;

namespace Campus.SchoolManagment.Web.Controllers
{
   // [Filters.Compress]
   [Authorize]
    public class ReviewsController : BaseController
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
         AccountTreeHandler _TreeHandler = new AccountTreeHandler();
        // GET: DepreciationIndex
        public ActionResult DepreciationIndex()
        {
             return PartialView();
        }

        // GET: TrialBalanceIndex
        public ActionResult TrialBalanceIndex()
        {
            GenericVModel model = new GenericVModel();

           
            ViewBag.AccountTreeList = _TreeHandler.quickAccountTreeListFull().Select(x => new DropdownList
            {
                name = x.name,
                value = x.value.ToString(),
                text = x.text

            }).ToList();

            return PartialView(model);
            //return PartialView();
        }
        [HttpPost]
        public ActionResult GetTrialBalance(string from )
        {
            string query = @"select * from [dbo].[TrailBalanceReportByAccountIdInPeriod](@p0,@p1,@p2,@p2) ORDER BY AccountTreeCode";

            // ViewBag.AccountTreeName = _TreeHandler.GetById(AccountTree).AccountNameEN;
            ViewBag.DateFrom = from;
             var results = Context.Database.SqlQuery<LedgerTrailBalanceViewModel>(query,schoolId,0,from, from).ToList();


            return PartialView("_ListTrailBalance", results);
        }

        // GET: IncomeStatementIndex
        public ActionResult IncomeStatementIndex()
        {
             return PartialView();
        }
        public ActionResult GetInconeStatement(IncomeStatementViewModel model)
        {
            // string query = @"select * from TVF_IncomeStatement()";
            string query = @"SELECT R.AccountTreeID,R.AccountTreeLevel,r.AccountNameEN as AccountName,CreditTotal as Amount  
                            FROM (
                            select * from TrailBalanceReportByAccountIdInPeriod(@p0,11,@p1,@p2)
                            UNION ALL 
                            select * from TrailBalanceReportByAccountIdInPeriod(@p0,12,@p1,@p2) 
                            UNION ALL 
                            select * from TrailBalanceReportByAccountIdInPeriod(@p0,13,@p1,@p2)
                            )R
                            order by AccountTreeCode";

            var results = Context.Database.SqlQuery<IncomeStatementViewModel>(query,schoolId.ToString(),model.From.ToString(),model.From.ToString()).ToList();

            return PartialView("_ListIncomeStatement", results);
        }

        public ActionResult BalanceSheetIndex() {

            return PartialView();
        }


        public ActionResult GetBalanceSheet(BalanceSheetViewModel model)
        {
            // string query = @"select * from TVF_IncomeStatement()";
            string query = @"SELECT * FROM BalanceSheetReportByPeriod(@p0,@p1,@p2)
                            order by AccountTreeCode";

            var results = Context.Database.SqlQuery<BalanceSheetViewModel>(query, schoolId.ToString(), model.From.ToString(), model.From.ToString()).ToList();

            return PartialView("_ListBalanceSheet", results);
        }

        // GET: GeneralbudgetIndex
        public ActionResult GeneralbudgetIndex()
        {
             return PartialView();
        }
        public ActionResult ReportsIndex()
        {
             return PartialView();
        }

        public ActionResult GetAccounts2()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName" + lang);

            return PartialView("_AccountTree1");
        }

        public ActionResult GetAccounts1()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName" + lang);

            return PartialView("_AccountTree");
        }


        [Filters.FileDownload]
        public ActionResult ExportPdfTrailBalance(string from)
        {
            string query = @"select * from [dbo].[TrailBalanceReportByAccountIdInPeriod](@p0,@p1,@p2,@p2) ORDER BY AccountTreeCode";

            var results = Context.Database.SqlQuery<LedgerTrailBalanceViewModel>(query, schoolId, 0, from, from).ToList();



         

            string html = RenderViewToString(this.ControllerContext, "_ListTrailBalance_Pdf", results);

            return new PDFResult(html, "Trial Balance", "Trial Balance -" + from);

        }


        [Filters.FileDownload]
        public ActionResult ExportPdfBalanceSheet(BalanceSheetViewModel model)
        {
            string query = @"SELECT * FROM BalanceSheetReportByPeriod(@p0,@p1,@p2)
                            order by AccountTreeCode";

            var results = Context.Database.SqlQuery<BalanceSheetViewModel>(query, schoolId.ToString(), model.From.ToString(), model.From.ToString()).ToList();

            string html = RenderViewToString(this.ControllerContext, "_ListBalanceSheet_Pdf", results);

            return new PDFResult(html, "Balance Sheet - "+model.From, "Balance Sheet - " + model.From);

        }


        [Filters.FileDownload]
        public ActionResult ExportPdfIncomeStatement(IncomeStatementViewModel model)
        {
            //no need of to date 
            string query = @"SELECT R.AccountTreeID,R.AccountTreeLevel,r.AccountNameEN as AccountName,CreditTotal as Amount  
                            FROM (
                            select * from TrailBalanceReportByAccountIdInPeriod(@p0,11,@p1,@p2)
                            UNION ALL 
                            select * from TrailBalanceReportByAccountIdInPeriod(@p0,12,@p1,@p2) 
                            UNION ALL 
                            select * from TrailBalanceReportByAccountIdInPeriod(@p0,13,@p1,@p2)
                            )R
                            order by AccountTreeCode";

            var results = Context.Database.SqlQuery<IncomeStatementViewModel>(query, schoolId.ToString(), model.From.ToString(), model.From.ToString()).ToList();
    
            string html = RenderViewToString(this.ControllerContext, "_ListIncomeStatement_Pdf", results);

            return new PDFResult(html, "Income Statement -" + model.From, "Income Statement -" + model.From);

        }


        [Filters.FileDownload]
        public ActionResult ExportExcelTrailBalance(string from)
        {
            string query = @"select * from [dbo].[TrailBalanceReportByAccountIdInPeriod](@p0,@p1,@p2,@p2) ORDER BY AccountTreeCode";

            var results = Context.Database.SqlQuery<LedgerTrailBalanceViewModel>(query, schoolId, 0, from, from).ToList();


            return new ExcelResult(results, string.Format("TrailBalance-{0}", DateTime.Now), "Trial Balance -" + from, new string[] {
                "AccountTreeCode",
                "AccountNameEN",
                "DebitTotal",
                "CreditTotal",
                "DebitAmount",
                "CreditAmount",
               });

        }

        [Filters.FileDownload]
        public ActionResult ExportExcelBalanceSheet(BalanceSheetViewModel model)
        {
            string query = @"SELECT * FROM BalanceSheetReportByPeriod(@p0,@p1,@p2)
                            order by AccountTreeCode";

            var results = Context.Database.SqlQuery<BalanceSheetViewModel>(query, schoolId.ToString(), model.From.ToString(), model.From.ToString()).ToList();


            return new ExcelResult(results, string.Format("Balance Sheet-{0}", DateTime.Now), "Balance Sheet - " + model.From);

        }

        [Filters.FileDownload]
        public ActionResult ExportExcelIncomeStatement(IncomeStatementViewModel model)
        {
            //no need of to date 
            string query = @"SELECT R.AccountTreeID,R.AccountTreeLevel,r.AccountNameEN as AccountName,CreditTotal as Amount  
                            FROM (
                            select * from TrailBalanceReportByAccountIdInPeriod(@p0,11,@p1,@p2)
                            UNION ALL 
                            select * from TrailBalanceReportByAccountIdInPeriod(@p0,12,@p1,@p2) 
                            UNION ALL 
                            select * from TrailBalanceReportByAccountIdInPeriod(@p0,13,@p1,@p2)
                            )R
                            order by AccountTreeCode";

            var results = Context.Database.SqlQuery<IncomeStatementViewModel>(query, schoolId.ToString(), model.From.ToString(), model.From.ToString()).ToList();


            return new ExcelResult(results, string.Format("Income Statement-{0}", DateTime.Now), "Balance Sheet - " + model.From);

          
        }

    }
}