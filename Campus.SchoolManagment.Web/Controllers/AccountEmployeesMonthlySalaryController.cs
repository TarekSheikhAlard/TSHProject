using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.SchoolManagment.Web.Helper;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    //[Filters.Compress]
    [Authorize]
    public class AccountEmployeesMonthlySalaryController : BaseController
    {
        AccountEmployeesSalaryHandler _EmployeesSalaryHandler = new AccountEmployeesSalaryHandler();
        AccountEmployeeSalaryItemHandler _EmployeeSalaryItem = new AccountEmployeeSalaryItemHandler();
        AccountEmployeesMonthlySalaryHandler _EmployeesMonthlySalaryHandler = new AccountEmployeesMonthlySalaryHandler();
        EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();

        private static DateTime DateTime = DateTime.Now;

        public ActionResult Index()
        {
             return PartialView(_EmployeesMonthlySalaryHandler.GetAllByCompany(companyId,schoolId));
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAllByCompany(companyId,schoolId), "ID", "CostCenter"+lang);

            ViewBag.employee = new SelectList(_EmployeeHandler.GetAllByCompany(companyId,schoolId), "Employee_ID", "Name"+lang);

            return PartialView("_Create");
        }



        public JsonResult EmployeeSalaryDetails(int Employee_ID, string monthYear)
        {
            int month = int.Parse(monthYear.Split('/')[0]);
            int year = int.Parse(monthYear.Split('/')[1]);

            var BasicSalaryInfo = _EmployeesSalaryHandler.GetAllByCompany(companyId,schoolId).Where(x => x.Employee_ID == Employee_ID).FirstOrDefault();
            if (BasicSalaryInfo == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);

            }
            else
            {
                var DeductionForMonth = _EmployeeSalaryItem.GetAllByCompany(companyId,schoolId).Where(x => x.Employee_ID == Employee_ID && x.SalaryItemTypeID == 1 && x.OperationDate.Value.Month == month && x.OperationDate.Value.Year == year);
                var BounsForMonth = _EmployeeSalaryItem.GetAllByCompany(companyId,schoolId).Where(x => x.Employee_ID == Employee_ID && x.SalaryItemTypeID == 2 && x.OperationDate.Value.Month == month && x.OperationDate.Value.Year == year);

                decimal totalDeductionMonthly = 0;
                decimal totalBounsMonthly = 0;

                foreach (var d in DeductionForMonth)
                {
                    totalDeductionMonthly += d.Amount;

                }
                foreach (var b in BounsForMonth)
                {
                    totalBounsMonthly += b.Amount;

                }

                decimal Deduction = BasicSalaryInfo.Dedecation.Value + totalDeductionMonthly;
                decimal Bouns = BasicSalaryInfo.BonusesSalary.Value + totalBounsMonthly;

                decimal totalDeduction = BasicSalaryInfo.TotalDedecated.Value + totalDeductionMonthly;
                decimal totalSalary = BasicSalaryInfo.TotalSalary.Value + totalBounsMonthly;
                decimal Netsalary = BasicSalaryInfo.NetSalary.Value + totalBounsMonthly - totalDeductionMonthly;

                BasicSalaryInfo.BonusesSalary = Bouns;
                BasicSalaryInfo.Dedecation = Deduction;
                BasicSalaryInfo.TotalDedecated = totalDeduction;
                BasicSalaryInfo.TotalSalary = totalSalary;
                BasicSalaryInfo.NetSalary = Netsalary;


                return Json(BasicSalaryInfo, JsonRequestBehavior.AllowGet);
            }
        }


        public object EmployeeSalariedMonths(int Employee_ID)
        {
            var months = _EmployeesMonthlySalaryHandler.GetAllByCompany(companyId, schoolId).Where(x => x.Employee_ID == Employee_ID).Select(x => x.SalaryOfMonthDt).ToList();
            return JsonConvert.SerializeObject(months,Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy/dd/MM" });
        }


        [HttpPost]
        public ActionResult Create(AccountEmployeesMonthlySalaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _EmployeesMonthlySalaryHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _EmployeesMonthlySalaryHandler.GetAllByCompany(companyId,schoolId));
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            TempData["Msg"] = 4;
            return PartialView("_List", _EmployeesMonthlySalaryHandler.GetAllByCompany(companyId,schoolId));
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _EmployeesMonthlySalaryHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _EmployeesMonthlySalaryHandler.GetAllByCompany(companyId,schoolId));
        }


        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _EmployeesMonthlySalaryHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountEmployeesMonthlySalaryViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                _EmployeesMonthlySalaryHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _EmployeesMonthlySalaryHandler.GetAllByCompany(companyId,schoolId));

            //}

            //TempData["Msg"] = 4;
            //return PartialView("_List", _EmployeesMonthlySalaryHandler.GetAll());
        }


        public ActionResult GetAccounts2()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeID == 4).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName"+lang);

            return PartialView("_AccountTree");
        }

        public ActionResult GetAccounts1()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeID == 10).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName"+lang);

            return PartialView("_AccountTree1");
        }



        public ActionResult GetAccounts3()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeID == 10).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName"+lang);

            return PartialView("_AccountTree2");
        }

        public ActionResult GetAccounts4()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeID == 7).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName"+lang);

            return PartialView("_AccountTree3");
        }



        [HttpPost]

        public JsonResult GetBranchsAccount(int id)
        {
            var list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).Select(x => new { x.AccountTreeID, x.AccountNameAR }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }



        [Filters.FileDownload]
        public ActionResult ExportAll()
        {
            dynamic list = _EmployeesMonthlySalaryHandler.GetAllByCompany(companyId, schoolId).ToList();

            return new ExcelResult(list, string.Format("Employees Monthly Salary-{0}", DateTime.Now), "Employees Monthly Salary Details",
             
                new string[] {
                "Employee_ID",
                "EmployeeName",
                "JobName",
                "AdditionalSalary",
                "AllowanceSalary",
                "BasicSalary",
                "BonusesSalary",
                "Dedecation",
                "Taxes",
                "extraSalary",   
                "NetSalary",
                "TotalDedecated",
                "TotalSalary",
                "OperationDate",
                "Subsistenceallowance",
                "Transitionallowance",
                "paymentsalariesway",
                "Drivingallowance",
                "conditionsworkallowance",
                "AllowanceSalary2",
                "AllowanceSalary3",
                "accommodationallowanc",
                "sumallowance",
                "IsPaid"
                });

        }

        [Filters.FileDownload]
        public ActionResult ExportPdfAll()
        {

            var list = _EmployeesMonthlySalaryHandler.GetAllByCompany(companyId, schoolId).ToList();
            string html = RenderViewToString(this.ControllerContext, "List_Pdf", list);

            return new PDFResult(html, "Employee Monthly Salaries Details", "All Employees Monthly  Salary Details");

        }

        [Filters.FileDownload]
        public ActionResult ExportPdfDetail(int id)
        {

            var list = _EmployeesMonthlySalaryHandler.GetById(id);
            string html = RenderViewToString(this.ControllerContext, "Detail_Pdf", list);

            return new PDFResult(html, "Employee Monthly Salary Details", "Employee Salary Detail of Date: " +list.OperationDate.Value.ToShortDateString());

        }


    }
}