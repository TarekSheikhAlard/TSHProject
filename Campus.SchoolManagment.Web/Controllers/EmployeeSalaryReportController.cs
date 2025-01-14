using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using Campus.SchoolManagment.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Campus.SchoolManagment.Web.Controllers
{
    public class EmployeeSalaryReportController : BaseController
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        // GET: EmployeeSalaryReport
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult GetReport(EmployeeSalaryReportInputs inputs) {

            string query = string.Format(@"select * from Fn_EMP_SALARY_REPORT('{0}','{1}','{2}')", inputs.month, inputs.year, companyId);

            var results = Context.Database.SqlQuery<EmployeeSalaryReportViewModel>(query).ToList();
            return PartialView("_List", results);

        }

        [Filters.FileDownload]
        public ActionResult ExportPdf(string month ,string year)
        {
            string query = string.Format(@"select * from Fn_EMP_SALARY_REPORT('{0}','{1}','{2}')", month, year, companyId);
            var results = Context.Database.SqlQuery<EmployeeSalaryReportViewModel>(query).ToList();

          
            //return PartialView("_DailyJournalDetailList", list._AccountDailyJournalDetail);
            string html = RenderViewToString(this.ControllerContext, "_List_Pdf", results);

            //return Content(html);

            return new PDFResult(html, "Employee Salary Report", string.Format("Employee Salary Report Of Month:{0} and Year:{1}",month,year));

        }

    }
}