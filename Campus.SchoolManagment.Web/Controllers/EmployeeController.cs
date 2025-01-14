using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.BusinessLayer;
using System.Threading;
using System.Globalization;
using System.Net;
using System.IO;
using Campus.SchoolManagment.Web.Models;
using Campus.School.Management.Logic.DataBaseLayer;
using System.Data.SqlClient;
using Campus.SchoolManagment.Web.Helper;
using static Campus.SchoolManagment.Web.Helper.SemanticControls;

namespace Campus.SchoolManagment.Web.Controllers
{
    //[Filters.Compress]
    public class EmployeeController : BaseController
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        private EmployeeHandler _handler = new EmployeeHandler();
        private JobsHandlar _Jobshandler = new JobsHandlar();
        private NationalityHandler _Nationalityhandler = new NationalityHandler();
        private DepartmentHandler _DepartmentHandler = new DepartmentHandler();
        private AccountEmployeesMonthlySalaryHandler _salaries = new AccountEmployeesMonthlySalaryHandler();

        AccountEmployeesSalaryHandler _EmployeesSalaryHandler = new AccountEmployeesSalaryHandler();
        AccountEmployeeSalaryItemHandler _EmployeeSalaryItem = new AccountEmployeeSalaryItemHandler();

        AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();
        EmployeeVacationHandler _EmployeeVacation = new EmployeeVacationHandler();
        EmployeeTerminationHandler _employeeTerminationHandler = new EmployeeTerminationHandler();
        HRAllowanceEntryHandler HRAllowanceEntry = new HRAllowanceEntryHandler();

        EmployeeBenfitsOtherHandler EmployeeBenfitsOtherHandler = new EmployeeBenfitsOtherHandler();

        //private SchoolUserController _UserController = new SchoolUserController();
        //private AccountController _AccountController = new AccountController();
        //ApplicationDbContext dbMembership = new ApplicationDbContext();

        // GET: School
        public ActionResult Index()
        {
            ViewBag.department = new SelectList(_DepartmentHandler.GetAllByCompany(companyId, schoolId), "DepartmentID", "Department" + lang);

            return PartialView(_handler.GetAllByCompany(companyId, schoolId));
        }

        public ActionResult EmployeeList(int ?id)
        {
            IEnumerable<EmployeeViewModel> model;
            if (id != null)
            {
                  model = _handler.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID == id);
            }
            else {
                model  = _handler.GetAllByCompany(companyId, schoolId);

            }
            return PartialView("_List", model);
        }

        public ActionResult Create()
        {
            ViewBag.department = new SelectList(_DepartmentHandler.GetAllByCompany(companyId, schoolId), "DepartmentID", "Department" + lang);
            ViewBag.Jobs = new SelectList(_Jobshandler.GetAll(), "JobID", "JobName" + lang);
            ViewBag.NationalityID = new SelectList(_Nationalityhandler.GetAllByCompanyID(companyId), "NationalityID", "Nationality" + lang);
            BenefitsAndOtherDropdowns();

            var model = (_handler.Create());

            return PartialView("_Create", model);
        }

        private void BenefitsAndOtherDropdowns()
        {
            ViewBag.Benefits = EmployeeBenfitsOtherHandler.GetAllByCompany(0, 1).Select(x => new DropdownList
            {
                name = x.Name_En,
                value = x.Id.ToString()
            }).ToList();

            ViewBag.Deduction = EmployeeBenfitsOtherHandler.GetAllByCompany(0, 2).Select(x => new DropdownList
            {
                name = x.Name_En,
                value = x.Id.ToString(),
                selected = x.IsDefault ? true : false
            }).ToList();

            ViewBag.Taxes = EmployeeBenfitsOtherHandler.GetAllByCompany(0, 3).Select(x => new DropdownList
            {
                name = x.Name_En,
                value = x.Id.ToString(),
                selected = x.IsDefault ? true : false

            }).ToList();

            ViewBag.Gov = EmployeeBenfitsOtherHandler.GetAllByCompany(0, 4).Select(x => new DropdownList
            {
                name = x.Name_En,
                value = x.Id.ToString(),
                selected = x.IsDefault ? true : false
            }).ToList();
        }



        public ActionResult EditPayrollSetup(int ID)
        {
            var model = _handler.GetById(ID);

            BenefitsAndOtherDropdowns();

            EmployeePayrollSetupViewModel viewModel = new EmployeePayrollSetupViewModel();
            viewModel.Employee_ID = model.Employee_ID;
            viewModel.BenefitID = string.Join(",", model._BenefitID);
            viewModel.DeductID = string.Join(",", model._DeductID);
            viewModel.TaxesID = string.Join(",", model._TaxesID);
            viewModel.GovID = string.Join(",", model._GovID);

            return PartialView("EditPayroll", viewModel);
        }
        [HttpPost]
        public ActionResult EditPayrollSetup(EmployeePayrollSetupViewModel model)
        {
            _handler.UpdatePayroll(model);

            return Content("");
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (TempData["photo"] != null)
                {
                    HttpPostedFileBase ImageFile = TempData["photo"] as HttpPostedFileBase;

                    string Name = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string vExtension = Path.GetExtension(ImageFile.FileName);

                    string ImageName = Name + "_" + Guid.NewGuid() + vExtension;
                    string physicalPath = Server.MapPath(Statistics.StudentImagesPath + ImageName);
                    ImageFile.SaveAs(physicalPath);
                    model.ImgUrl = ImageName;


                    model = _handler.CreateEmployee(model, companyId, schoolId);

                    // create User
                    //string userName =  model.NameE;
                    //SchoolUserViewModel _dbUser = _AccountController.Getcookie();
                    //SchoolUserViewModel _schoolUser = new SchoolUserViewModel()
                    //{
                    //    Name = userName,
                    //    Email = model.Mail,
                    //    Password = model.Password,
                    //    ConfirmPassword = model.ConfirmPassword,
                    //    SchoolID = _dbUser.SchoolID,
                    //    RoleID = dbMembership.Roles.Where(x => x.Name == "Employee").FirstOrDefault().Id,
                    //};
                    //int _userSaved = _UserController.CreateUser(_schoolUser);
                    //if (_userSaved == 1)
                    //{
                    TempData["Msg"] = 1;

                    return PartialView("_List", _handler.GetAllByCompany(companyId, schoolId).Where(x =>x.DeptID == model.DeptID));
                    //}
                    //else if (_userSaved == 5)
                    //{
                    //    _handler.Delete(model.Employee_ID);
                    //TempData["Msg"] = 5;
                    //    return PartialView("_List", _handler.GetAll());
                    //}
                    //else
                    //{
                    // delete employee if user not save
                    //    _handler.Delete(model.Employee_ID);
                    //    TempData["Msg"] = 4;
                    //    return PartialView("_List", _handler.GetAll());
                    //}
                }

            }
            TempData["Msg"] = 4;
            return PartialView("_List", _handler.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID == model.DeptID));

        }

        public ActionResult Edit(int ID)
        {
            var model = _handler.GetById(ID);

            model.BenefitID = string.Join(",", model._BenefitID);
            model.DeductID = string.Join(",", model._DeductID);
            model.TaxesID = string.Join(",", model._TaxesID);
            model.GovID = string.Join(",", model._GovID);

            // ViewBag.department = new SelectList(_DepartmentHandler.GetAll(), "DepartmentID", "Department" + lang);

            ViewBag.Jobs = new SelectList(_Jobshandler.GetAll().Where(x => x.DepartmentID == model.DeptID), "JobID", "JobName" + lang);
            ViewBag.NationalityID = new SelectList(_Nationalityhandler.GetAll(), "NationalityID", "Nationality" + lang, model.NationalityID);

            BenefitsAndOtherDropdowns();

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel model)
        {
            //if (ModelState.ContainsKey("Password"))
            //    ModelState["Password"].Errors.Clear();
            //if (ModelState.ContainsKey("ConfirmPassword"))
            //    ModelState["ConfirmPassword"].Errors.Clear();
            if (ModelState.IsValid)
            {
                if (TempData["photo"] != null)
                {
                    HttpPostedFileBase ImageFile = TempData["photo"] as HttpPostedFileBase;

                    string Name = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string vExtension = Path.GetExtension(ImageFile.FileName);

                    string ImageName = Name + "_" + Guid.NewGuid() + vExtension;

                    string physicalPath = Server.MapPath(Statistics.StudentImagesPath + ImageName);

                    ImageFile.SaveAs(physicalPath);

                    model.ImgUrl = ImageName;

                    _handler.Update(model);

                    TempData["Msg"] = 2;
                    return PartialView("_List", _handler.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID == model.DeptID));

                }
                else
                {
                    _handler.Update(model);

                    TempData["Msg"] = 2;

                    return PartialView("_List", _handler.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID == model.DeptID));

                }
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _handler.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID == model.DeptID));

        }


        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                TempData["photo"] = ImageFile;
            }

            return Json("sucess");
        }

        // GET: AdmStages/Delete/5
        public ActionResult Delete(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return PartialView("_Delete", ID);

        }

        // POST: Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            try
            {
                _handler.Delete(ID);
                TempData["Msg"] = 3;
                return PartialView("_List", _handler.GetAll());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _handler.GetAll());
            }
        }

        public ActionResult ChangeDepartment()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult ChangeDepartment(EmployeeViewModel model)
        {

            _handler.ChangeDepartment(model);

            return Json(true);

        }

        public ActionResult ChangeSponsor(int id)
        {
            var model = _handler.GetById(id);

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ChangeSponsor(EmployeeViewModel model)
        {
            _handler.ChangeSponsor(model);

            return Json(true);
        }

        public ActionResult PayHousing(int id)
        {
            var model = _salaries.GetHousingById(id);
            ViewBag.tableid = "Housing";

            if (model != null)
                return PartialView(model);
            else
                return null; //show error mesg on client if null 
        }

        [HttpPost]
        public ActionResult PayHousing(HRAllowanceEntryViewModel model)
        {
            model.AllowanceType = "Housing";
            model.Allowance = model.accommodationallowanc;

            HRAllowanceEntry.Create(model);

            var list = HRAllowanceEntry.GetAllByEmployeeID(model.Employee_ID).Where(x => x.AllowanceType == "Housing");

            return PartialView("HREntryList", list);

            //return PartialView(model);
        }

        public ActionResult GetHousingList(int empid)
        {
            var model = HRAllowanceEntry.GetAllByEmployeeID(empid).Where(x => x.AllowanceType == "Housing");

            ViewBag.tableid = "Housing";

            return PartialView("HREntryList", model);
        }

      


        //[HttpPost]
        //public ActionResult PayHousing(int id)
        //{
        //    var model = _salaries.GetHousingById(id);
        //    return PartialView(model);
        //}


        public ActionResult PayTickets(int id)
        {
            var model = _salaries.GetEmployeeTicketInfoById(id);
            ViewBag.tableid = "Ticket";

            if (model != null)
                return PartialView(model);
            else
                return null; //show error mesg on client if null 
        }
        public ActionResult GetTicketList(int empid)
        {
            var model = HRAllowanceEntry.GetAllByEmployeeID(empid).Where(x => x.AllowanceType == "Ticket");
            ViewBag.tableid = "Ticket";

            return PartialView("HREntryList", model);
        }


        [HttpPost]
        public ActionResult PayTickets(HRAllowanceEntryViewModel model)
        {
            model.AllowanceType = "Ticket";
            model.Allowance = model.TicketPrice;

            HRAllowanceEntry.Create(model);

            var list = HRAllowanceEntry.GetAllByEmployeeID(model.Employee_ID).Where(x => x.AllowanceType == "Ticket");
           
            return PartialView("HREntryList", list);

        }
        public ActionResult DeleteEntry(int id)
        {
            var entry = HRAllowanceEntry.GetById(id);
            ViewBag.tableid = entry.AllowanceType;
            return PartialView("DeleteEntry",id );

        }

        [HttpPost, ActionName("DeleteEntry")]
        public ActionResult DeleteEntryConfrim(int id)
        {
            HRAllowanceEntry.Delete(id);

            var entry = HRAllowanceEntry.GetById(id);

            var list = HRAllowanceEntry.GetAllByEmployeeID(entry.Employee_ID).Where(x => x.AllowanceType == entry.AllowanceType);

            ViewBag.tableid = entry.AllowanceType;

            return PartialView("HREntryList", list);

        }




        public ActionResult EmployeeVacation(int id)
        {   
            var HolidayTypelist = Context.HolidayTypes.Where(x => x.IsDeleted == false).Select(x => new EmployeeVacationViewModel
            {
                HolidayTypeId =x.HolidayID,
                HolidayNameEn=x.HolidayNameEn,
                HolidayNameAr=x.HolidayNameAr

            }).ToList();

            var deptid = Context.AdmEmployees.Where(x => x.Employee_ID == id).Select(x => x.AdmJob.DepartmentID);

            //var EmpList = Context.AdmEmployees.Where(x => x.AdmJob.DepartmentID == deptid);

            ViewBag.HolidayType = new SelectList(HolidayTypelist, "HolidayTypeId", "HolidayName" + lang);
            ViewBag.employee = new SelectList(_handler.GetAllByCompany(companyId, schoolId), "Employee_ID", "Name" + lang);
           // var model = _salaries.GetEmployeeTicketInfoById(id);

            return PartialView();
        }

        [HttpPost]
        public JsonResult EmployeeVacationAllowanceDetails(int Employee_ID, DateTime OperationDate)
        {

            var BasicSalaryInfo = _EmployeesSalaryHandler.GetAll().Where(x => x.Employee_ID == Employee_ID).FirstOrDefault();
            if (BasicSalaryInfo == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);

            }
            else
            {
                var DeductionForMonth = _EmployeeSalaryItem.GetAll().Where(x => x.Employee_ID == Employee_ID && x.SalaryItemTypeID == 1 && x.OperationDate.Value.Month == OperationDate.Month && x.OperationDate.Value.Year == OperationDate.Year);
                var BounsForMonth = _EmployeeSalaryItem.GetAll().Where(x => x.Employee_ID == Employee_ID && x.SalaryItemTypeID == 2 && x.OperationDate.Value.Month == OperationDate.Month && x.OperationDate.Value.Year == OperationDate.Year);
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
        [HttpPost]
        public ActionResult EmployeeVacation(EmployeeVacationViewModel model)
        {
            _EmployeeVacation.Create(model);
            return Json(true);
        }


        public ActionResult EmployeePromotion(int id)
        {
            string query = @" SELECT 
                                A.Employee_ID,A.NameA EmployeeName,
                                Jobs.JobNameEn CurrentJobName,
                                dept.DepartmentNameEN CurrentDeptName,
                                B.*
                                FROM AdmEmployees A
                                INNER JOIN AccountEmployeesSalaries B 
                                ON A.Employee_ID = B.Employee_ID
                                INNER JOIN AdmJobs Jobs 
                                ON Jobs.JobID=A.JobID
                                INNER JOIN DepartmentTree Dept
                                ON dept.DepartmentTreeID=jobs.DepartmentID
                                WHERE A.Employee_ID=@id";


            var model = Context.Database.SqlQuery<EmployeePromotionViewModal>(query, new SqlParameter("@id", id)).FirstOrDefault(); 
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult EmployeePromotion(EmployeePromotionViewModal model)
        {
            _handler.EmployeePromotion(model);

            var list = _handler.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID == model.DepartmentTreeID);

            return PartialView("_List", list);
        }

        public ActionResult EmployeeTermination()
        {       
            return PartialView();
        }

        [HttpPost]
        public JsonResult EmployeeTermination(EmployeeTerminationViewModel model)
        {
            _employeeTerminationHandler.Create(model);

            return Json(true);
        }
        [HttpGet]
        public ActionResult TerminationDetails(int id)
        {
           var model= _employeeTerminationHandler.GetById(id);

            return PartialView(model);
        }


        [HttpPost]
        public JsonResult GetJOBS(int id)
        {
            dynamic list;
                if (lang.ToLower().Equals("ar"))
                list = _Jobshandler.GetAll().Where(x => x.DepartmentID == id).Select(x => new { x.JobID, x.JobNameAr }).ToList();
                else
                list = _Jobshandler.GetAll().Where(x => x.DepartmentID == id).Select(x => new { x.JobID, x.JobNameEn }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }


        public ActionResult PettycashIndex()
        {
             return PartialView();
        }
        public ActionResult DaysOffIndex()
        {
             return PartialView();
        }




        [Filters.FileDownload]
        public ActionResult ExportAll(int id)
        {
            dynamic list = _handler.GetAllByCompany(companyId, schoolId).Where(x => x.DeptID ==id ).ToList();

            return new ExcelResult(list, string.Format("Employees-{0}", DateTime.Now), "Employee Details",
                new string[] {
                "Employee_ID",
                "NameAr",
                "NameEn",
                "DepartmentName",
                "jobName",
                "NationalityName",
                "National_ID",
                "WorkDate",
                "ContractDuration",
                "Maritalstatus",
                "Address",
                "Mail",
                "Mobile",
                "Tel",
                "BirthDate",
                "religion",
                "sex",
                "Educationallevel",
                "passportNumber",
                "visajob",
                "visaNumber",
                "IsTerminated"
                });
        }

        [Filters.FileDownload]
        public ActionResult ExportPdfDetail(int id)
        {

            var list = _handler.GetById(id);
            string html = RenderViewToString(this.ControllerContext, "EmployeeDetails_Pdf", list);
              
            return new PDFResult(html, "EmployeeDetails", "Employee Details  (ID : " + id.ToString()+")");

        }



    }
}