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
    [Authorize]
    public class AccountIncomeStatementController : Controller
    {
     //   private AccStudentConfigurationHandler _Handler = new AccStudentConfigurationHandler ();
        private AccountEmployeeSalaryItemHandler _EmpSalaryItemHandler = new AccountEmployeeSalaryItemHandler ();
        private AccountEmployeesMonthlySalaryHandler _EmpMonthHandler = new AccountEmployeesMonthlySalaryHandler ();
        private BusExpensesHandler _BusExpensesHandler = new BusExpensesHandler ();
        private AccountDailyJournalDetailHandler _DailyJournalDetailHandler = new AccountDailyJournalDetailHandler ();

        // GET: AccountEmployeeSalaryItem
        public ActionResult Index()
        {
             return PartialView();
        }
        [HttpPost]
        public ActionResult List(string From, string To)
        {
            DateTime DateFrom = DateTime.ParseExact(From, "MM/dd/yyyy", null);
            DateTime DateTo = DateTime.ParseExact(To, "MM/dd/yyyy", null);
            TimeSpan ts = new TimeSpan(23, 59, 0);
            DateTo = DateTo.Date + ts;

            List<GenericVModel> list = new List<GenericVModel>();
            //Total income ==1
            // Total Student fees
            //list.Add(_Handler.GetIncome(DateFrom, DateTo));


            ////OtherIncome ==> salary deduct
            //list.Add(_EmpSalaryItemHandler.GetTotalDeduct(DateFrom, DateTo));

            ////Expenses = 2
            ////Total Salaries 
            //list.Add(_EmpMonthHandler.GetTotalSalary(DateFrom, DateTo));
            ////Deduct ==> discount
            //list.Add(_Handler.GetDeduct(DateFrom, DateTo));
            ////Total Bus Expenses  
            //list.Add(_BusExpensesHandler.GetTotalExpenses(DateFrom, DateTo));

            ////Total General Expenses 
            //list.Add(_DailyJournalDetailHandler.GetTotalExpenses(DateFrom, DateTo));

            return PartialView("_List", list);

        }
        
    }
}