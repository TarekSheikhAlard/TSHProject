using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using Campus.SchoolManagment.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using static Campus.SchoolManagment.Web.Helper.SemanticControls;
using Campus.School.Management.Logic.BusinessLayer;

namespace Campus.SchoolManagment.Web.Controllers
{
   
    [Authorize]
    public class BankTransactionsController : BaseController
    {
        BankHandler Bank = new BankHandler();
        BankTransactionsHandler _bankTransactions = new BankTransactionsHandler();
        SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        // GET: BankTransactions
        public ActionResult Index()
        {
            ViewBag.BankList = Bank.GetAllByCompanyID(companyId).Select(x => new DropdownList
            {
                name = x.BankNameEn,
                value = x.BankID.ToString()
            }).ToList();
            
            ViewBag.TransactionTypeList = Context.TransactionTypes.Where(x => x.IsDeleted == false).Select(x => new DropdownList
            {
                name = x.Name,
                value = x.Id.ToString()
            }).ToList();
            ViewBag.AttachedDocumentPath = string.Format("{0}{1}/{2}/transactions/", Statistics.JournalAttachedDocumentPath, companyId, schoolId);

            return PartialView();
        }

        public ActionResult TransactionList()
        {
            List<BankTransactionsViewModel> model = new List<BankTransactionsViewModel>();

            BankTransactionsViewModel bankTransactions = new BankTransactionsViewModel();

            model.Add(bankTransactions);

            ViewBag.TransactionTypeList = Context.TransactionTypes.Where(x => x.IsDeleted == false).Select(x => new DropdownList
            {
                name = x.Name,
                value = x.Id.ToString()
            }).ToList();


            return PartialView(model);

        }

        public object SaveTransaction(List<BankTransactionsViewModel> items, int bankId)
        {

            if (ModelState.IsValid)
            {
            
                foreach (var item in items)
                {
                    if (item.Payee != null)
                    { item.BankAccountId = bankId;
                        _bankTransactions.Create(item);
                    }
                
                 
                }
                TempData["Msg"] = 1;

               
                List<BankTransactionsViewModel> model = new List<BankTransactionsViewModel>();

                BankTransactionsViewModel bankTransactions = new BankTransactionsViewModel();

                model.Add(bankTransactions);

                ViewBag.TransactionTypeList = Context.TransactionTypes.Where(x => x.IsDeleted == false).Select(x => new DropdownList
                {
                    name = x.Name,
                    value = x.Id.ToString()
                }).ToList();

             
                return PartialView("TransactionList", model);
            }
            return PartialView("TransactionList");
        }
        public ActionResult GetTransactionHistory(int id, DateTime from, DateTime to)
        {

            var model = _bankTransactions.GetAllByBankPeriod(id, from, to);


            return PartialView("_TransactionHistoryList",model);
        }

        [Filters.FileDownload]
        public ActionResult ExportPdfById(int id)
        {

            var model = _bankTransactions.GetById(id);

            List<BankTransactionsViewModel> List = new List<BankTransactionsViewModel>();

            List.Add(model);

            string html = RenderViewToString(this.ControllerContext, "_TransactionHistory_Pdf", List);
           // ViewBag.Pdf = "true";
          //  return PartialView("_TransactionHistory_Pdf", model);
            return new PDFResult(html, "Bank Transaction Details",string.Format( "Transaction Details of {0} - Reference No : {1}", model.BankAccountName,model.ReferenceNo));
        }


        [Filters.FileDownload]
        public ActionResult ExportPdfByPeriod(int id, DateTime from, DateTime to)
        {

          
            var model = _bankTransactions.GetAllByBankPeriod(id, from, to);

            string html = RenderViewToString(this.ControllerContext, "_TransactionHistory_Pdf", model);
            // ViewBag.Pdf = "true";
            //  return PartialView("_TransactionHistory_Pdf", model);
            return new PDFResult(html, "Bank Transaction Details", string.Format("Transaction Details of {0}, From : {1} - To : {2}", model.FirstOrDefault().BankAccountName,from.ToShortDateString(),to.ToShortDateString()));
        }




        public object AccountList(string type)
        {
            List<quickAccountTreeListModel> list;
            //   type = "account";
            switch (type.ToLower())
            {
                case "account":
                    list = Context.Database.SqlQuery<quickAccountTreeListModel>("select * from [AccountTreeDropdownListFilter](@p0,@p1)", schoolId, 14).ToList();
                    break;
                case "customer":
                    list = Context.Database.SqlQuery<quickAccountTreeListModel>("select * from [AccountTreeDropdownListFilter](@p0,@p1)", schoolId, 15).ToList();
                    break;
                case "supplier":
                    list = Context.Database.SqlQuery<quickAccountTreeListModel>("select * from [AccountTreeDropdownListFilter](@p0,@p1)", schoolId, 21).ToList();
                    break;
                default:
                    list = Context.Database.SqlQuery<quickAccountTreeListModel>("select * from [AccountTreeDropdownListFilter](@p0,@p1)", schoolId, 14).ToList();
                    break;
            }


            return JsonConvert.SerializeObject(list);
        }
        public class quickAccountTreeListModel
        {

            public int value { get; set; }
            public string name { get; set; }
            public string text { get; set; }
        }

    }
}