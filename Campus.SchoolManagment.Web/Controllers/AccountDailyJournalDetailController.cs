using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    [Authorize]
    public class AccountDailyJournalDetailController : Controller
    {
       private AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();
        private AccountDailyJournalDetailHandler _AccountDailyJournalDetailHandler = new  AccountDailyJournalDetailHandler();
        private AccountStatementTypeHandler _AccountStatementTypeHandler = new AccountStatementTypeHandler();
        private AccountTreeHandler _AccountTreeHandler = new AccountTreeHandler();

        // GET: AccountDailyJournal
        public ActionResult Index()
        {
             return PartialView();
        }
        [HttpPost]
        public ActionResult List(string From, string To)
        {
            var list = _AccountDailyJournalDetailHandler.GetTrialBalance(From,To).ToList();
        
            return PartialView("_List", list);

        }

       
    }
}