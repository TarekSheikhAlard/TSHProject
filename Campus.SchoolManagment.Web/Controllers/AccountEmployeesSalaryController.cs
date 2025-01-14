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
    [Authorize]
    public class AccountEmployeesSalaryController : BaseController
    {
        EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        JobsHandlar _JobsHandlar = new JobsHandlar();
        AccountEmployeesSalaryHandler _EmployeesSalaryHandler = new AccountEmployeesSalaryHandler();
        BankBranchHandler _HandlerBankBranch = new BankBranchHandler();
        DepartmentHandler _DepartmentHandler = new DepartmentHandler();

        public ActionResult Index()
        {
           
            if (companyId != 0)

            {
                 return PartialView(_EmployeesSalaryHandler.GetAllByCompany(companyId, schoolId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Department = new SelectList(_DepartmentHandler.GetAllByCompany(companyId, schoolId), "DepartmentID", "Department"+lang);
            //ViewBag.employee = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "NameE");
            ViewBag.BranchList = new SelectList(_HandlerBankBranch.GetAllByCompany(companyId), "ID", "BranchName"+lang);

            return PartialView("_Create");
        }

        [HttpPost]
        public JsonResult SearchJobName(int Employee_ID)
        {
            int _jobID = _EmployeeHandler.GetById(Employee_ID).jobID;
            var JobNameEn = _JobsHandlar.GetById(_jobID).JobNameEn;

            return Json(JobNameEn, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetEmployeeList(int id)
        {
            dynamic list;
                if (lang.ToLower().Equals("ar"))
                list = _EmployeeHandler.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID == id).Select(x => new { Employee_ID = x.Employee_ID, NameA = x.NameAr }).ToList();
                else
                list = _EmployeeHandler.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID == id).Select(x => new { Employee_ID = x.Employee_ID, NameE = x.NameEn }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Create(AccountEmployeesSalaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                _EmployeesSalaryHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _EmployeesSalaryHandler.GetAllByCompany(companyId, schoolId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _EmployeesSalaryHandler.GetAllByCompany(companyId, schoolId));
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _EmployeesSalaryHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _EmployeesSalaryHandler.GetAllByCompany(companyId, schoolId));
        }


        public ActionResult Edit(int id)

        {

            ViewBag.Department = new SelectList(_DepartmentHandler.GetAllByCompany(companyId, schoolId), "DepartmentID", "Department"+lang);

            ViewBag.BranchList = new SelectList(_HandlerBankBranch.GetAllByCompany(companyId), "ID", "BranchName"+lang);

            return PartialView("_Edit", _EmployeesSalaryHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountEmployeesSalaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _EmployeesSalaryHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _EmployeesSalaryHandler.GetAllByCompany(companyId, schoolId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _EmployeesSalaryHandler.GetAllByCompany(companyId, schoolId));
        }



        [Filters.FileDownload]
        public ActionResult ExportAll()
        {
            dynamic list = _EmployeesSalaryHandler.GetAllByCompany(companyId, schoolId).ToList();

            return new ExcelResult(list, string.Format("Employees Salary-{0}", DateTime.Now), "Employees Salary Details",
                new string[] {
                "Employee_ID",
                "EmployeeName",
                "AccountNumber",
                "BankName",
                "BasicSalary",
                "BonusesSalary",
                "Dedecation",
                "Taxes",
                "extraSalary",
                "accommodationallowanc",
                "Transitionallowance",
                "Subsistenceallowance",
                "Drivingallowance",
                "accommodationallowancetype",
                "conditionsworkallowance",
                "Medicalinsurance",
                "NetSalary",
                "TotalDedecated",
                "TotalSalary",     
                "flighttickets"
                });
        }


        [Filters.FileDownload]
        public ActionResult ExportPdfAll()
        {

            var list = _EmployeesSalaryHandler.GetAllByCompany(companyId, schoolId).ToList();
            string html = RenderViewToString(this.ControllerContext, "List_Pdf", list);

            return new PDFResult(html, "Employee Salary Details", "All Employees Salary Details");

        }

        [Filters.FileDownload]
        public ActionResult ExportPdfDetail(int id)
        {

            var list = _EmployeesSalaryHandler.GetById(id);
            string html = RenderViewToString(this.ControllerContext, "Detail_Pdf", list);

            return new PDFResult(html, "Employee Salary Details", "Employee Salary Details  (ID : " + id.ToString() + ")");

        }

    }
}