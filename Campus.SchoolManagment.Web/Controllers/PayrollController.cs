using Campus.School.Management.Logic.BusinessLayer;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using Campus.SchoolManagment.Web.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Campus.SchoolManagment.Web.Helper.SemanticControls;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class PayrollController : BaseController
    {

        BankHandler Bank = new BankHandler();
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        // GET: Payroll
        public ActionResult Index()
        {
            ViewBag.BankList = Bank.GetAllByCompanyID(companyId).Select(x => new DropdownList
            {
                name = x.BankNameEn,
                value = x.AccountTreeID.ToString()
            }).ToList();

            ViewBag.SalaryMonths = Context.HRSalaryMonths.Select(x => new DropdownList
            {
                name = x.NameEn,
                value = x.Id.ToString()
            }).ToList();

            ViewBag.DepartmentList = Context.Database.SqlQuery<DropdownList>(@"Select * from DepartmentList(@p0,0)", schoolId).Select(x => new DropdownList
            {
                name = x.name,
                value = x.value.ToString(),
                text = x.text
            }).ToList();


            return PartialView();
        }

        public ActionResult GetPayroll(int? deptId, int month,int year, int? empId )
        {
            List<EmployeePayrollViewModel> list = new List<EmployeePayrollViewModel>();

            if (deptId != null)
            {
                list = GetPayrollByDepartment((int)deptId, month,year);
            }
            else if (empId != null)
            {
                list = GetPayrollByEmpId((int)empId, month, year);
            }
            else
            {
                list = GetPayrollByDepartment(11, month, year);

            }


            return PartialView("_List", list);
        }

        public ActionResult HistoryPayroll()
        {
            List<EmployeePayrollHistoryViewModel> results = GetPayrollHistory();

            return PartialView("history", results);
        }

        private List<EmployeePayrollHistoryViewModel> GetPayrollHistory()
        {
            string query = @"SELECT * FROM [EmployeePayrollHistory]()";

            // ViewBag.AccountTreeName = _TreeHandler.GetById(AccountTree).AccountNameEN;
            var results = Context.Database.SqlQuery<EmployeePayrollHistoryViewModel>(query).ToList();
            return results;
        }

        public ActionResult CreatePayroll(List<EmployeePayrollViewModel> model, string operDate, int BankAccId)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            List<EmployeeAddedBenefitsOthersViewModel> empBenefit = new List<EmployeeAddedBenefitsOthersViewModel>();
            string empId = string.Empty;
            decimal BaseAmount = 0, OTAmount = 0, AbsenceAmount = 0, Amount = 0, TaxAmount = 0, GrossPay = 0;
            bool flag = false ;
            int? costCenter;
            DateTime operDt = DateTime.ParseExact(operDate, "yyyy/mm/dd", null);



            HRPayroll hRPayroll = new HRPayroll()
            {
                SalaryMonthId = model.FirstOrDefault().SalaryMonth,
                PayrollYear = DateTime.Now.Year.ToString(),
                OperationDate = operDt,
                CompanyID = _dbUser.CompanyID,
                SchoolID = _dbUser.SchoolID,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now
            };

            Context.HRPayrolls.Add(hRPayroll);

            foreach (var item in model)
            {
                GrossPay = 0;

                empId = item.Employee_ID.ToString();

                costCenter = Context.AccountCostCenters.Where(x => x.CostCenter == empId).Select(c => c.ID).SingleOrDefault();

                HRPayrollEmployee hRPayrollEmployee = new HRPayrollEmployee()
                {
                    Payroll_Id = hRPayroll.Id,
                    EmployeeId = item.Employee_ID,
                    RegularHours = item.RegularHours,
                    OTHours = item.OTHours,
                    DeductionHours = item.DeductionHours,
                    Bonus = item.Bonus,
                    SalaryMonthId = item.SalaryMonth,
                    CreatedbyID = _dbUser.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DeletedDate = DateTime.Now,
                    CompanyID=_dbUser.CompanyID,
                    SchoolID=_dbUser.SchoolID
                };

                Context.HRPayrollEmployees.Add(hRPayrollEmployee);

                empBenefit = Context.EmployeeAddedBenefitsOthers.Where(x => x.EmployeeId == item.Employee_ID).Select(c => new EmployeeAddedBenefitsOthersViewModel
                {
                    Id = c.Id,
                    EmployeeBenefitsOtherId = c.EmployeeBenefitsOtherId,
                    BenefitPercent = c.EmployeeBenfitsOther.Percentage,
                    Allowancetype = c.EmployeeBenfitsOther.Type,
                    AccountTreeID = c.EmployeeBenfitsOther.AccountTreeID,
                    BenefitsOtherNameEn = c.EmployeeBenfitsOther.Name_En


                }).ToList();

                foreach (var benefit in empBenefit)
                {
                    flag = false;
                    AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();
                    List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>();
                    JournalTypesHandler journalTypes = new JournalTypesHandler();   


                    if (benefit.Allowancetype == 1 && hRPayrollEmployee.RegularHours != 0)
                    {
                        
                        Amount = ((hRPayrollEmployee.RegularHours * item.BasePay) / 100) * benefit.BenefitPercent;

                        if (benefit.BenefitsOtherNameEn.ToLower().Contains("over time"))
                        {
                            OTAmount = (((decimal)hRPayrollEmployee.OTHours * item.BasePay) / 100) * benefit.BenefitPercent;
                            Amount = OTAmount;
                        }
                        
                         DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
                        {
                            new AccountDailyJournalDetailViewModel
                            {
                                CostCenterID=costCenter,
                                AccountTreeID = BankAccId,
                                Credit =Amount
                            },

                             new AccountDailyJournalDetailViewModel
                            {
                                CostCenterID=costCenter,
                                AccountTreeID = benefit.AccountTreeID,
                                Debit =Amount
                            }
                             
                        };
                        
                           // GrossPay = GrossPay + Amount;

                        var _DailyJournal = _AccountDailyJournalHandler.Create();


                        _DailyJournal.DailyJournalDate = operDt;
                        _DailyJournal.Credit = Amount;
                        _DailyJournal.Debit = Amount;
                        _DailyJournal.Description = benefit.BenefitsOtherNameEn+" benefit of Month No :" + item.SalaryMonth;
                        _DailyJournal.Note = "payable note";
                        _DailyJournal.IsPost = false;
                        _DailyJournal.JournalType = 10; //journalTypes.GetAllByCompany(_dbUser.CompanyID).Where(x => x.NameEn == "Employee Salary").Select(c => c.ID).FirstOrDefault();
                        _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
                        _AccountDailyJournalHandler.Create(_DailyJournal);
                        flag = true;
                    }
                    else if (benefit.Allowancetype == 2 && hRPayrollEmployee.DeductionHours != 0)
                    {
                        Amount = (((decimal)hRPayrollEmployee.DeductionHours * item.BasePay) / 100) * benefit.BenefitPercent;

                        DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
                        {
                            new AccountDailyJournalDetailViewModel
                            {
                                CostCenterID=costCenter,
                                AccountTreeID = BankAccId,
                                Debit =Amount
                                
                            },

                             new AccountDailyJournalDetailViewModel
                            {
                                CostCenterID=costCenter,
                                AccountTreeID = benefit.AccountTreeID,
                                Credit =Amount
                            }

                        };

                        var _DailyJournal = _AccountDailyJournalHandler.Create();


                        _DailyJournal.DailyJournalDate = operDt;
                        _DailyJournal.Credit = Amount;
                        _DailyJournal.Debit = Amount;
                        _DailyJournal.Description = benefit.BenefitsOtherNameEn+ " deduction of Month No :" + item.SalaryMonth;
                        _DailyJournal.Note = "payable note";
                        _DailyJournal.IsPost = false;
                        _DailyJournal.JournalType = 10; //journalTypes.GetAllByCompany(_dbUser.CompanyID).Where(x => x.NameEn == "Employee Salary").Select(c => c.ID).FirstOrDefault();
                        _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
                        _AccountDailyJournalHandler.Create(_DailyJournal);

                        flag = true;
                       // GrossPay = Amount - GrossPay;
                    }
                    else if (benefit.Allowancetype == 3 )
                    {
                        Amount = (((decimal)hRPayrollEmployee.DeductionHours * item.BasePay) / 100) * benefit.BenefitPercent;

                        DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
                        {
                            new AccountDailyJournalDetailViewModel
                            {
                                CostCenterID=costCenter,
                                AccountTreeID = BankAccId,
                                Debit =Amount

                            },

                             new AccountDailyJournalDetailViewModel
                            {
                                CostCenterID=costCenter,
                                AccountTreeID = benefit.AccountTreeID,
                                Credit =Amount
                            }

                        };

                        var _DailyJournal = _AccountDailyJournalHandler.Create();


                        _DailyJournal.DailyJournalDate = operDt;
                        _DailyJournal.Credit = Amount;
                        _DailyJournal.Debit = Amount;
                        _DailyJournal.Description = benefit.BenefitsOtherNameEn + " deduction of Month No :" + item.SalaryMonth;
                        _DailyJournal.Note = "payable note";
                        _DailyJournal.IsPost = false;
                        _DailyJournal.JournalType = 10; //journalTypes.GetAllByCompany(_dbUser.CompanyID).Where(x => x.NameEn == "Employee Salary").Select(c => c.ID).FirstOrDefault();
                        _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
                        _AccountDailyJournalHandler.Create(_DailyJournal);
                        flag = true;
                        // GrossPay = Amount - GrossPay;
                    }

                    else
                    {

                        flag = false;
                    }

                    if (flag == true ) {

                        var _dailyjornalid = Context.AccountDailyJournals.ToList().Last().DailyJournalID;

                        AccountPayableCashHandler _AccountPayableCashHandler = new AccountPayableCashHandler();
                        var _AccountPayableCashModel = _AccountPayableCashHandler.Create();
                        AccountPayableCash _AccountPayableCash = new AccountPayableCash()
                        {

                            Amount = Amount,
                            InvoiceCode = _AccountPayableCashModel.InvoiceCode,
                            AccountTreeID = benefit.AccountTreeID,
                            CostCenterID = costCenter,
                            //docementNumber = model.docementNumber,
                            //TreasuryID = model.TreasuryID,
                            // Description = model.Description,
                            OperationDate = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            DeletedDate = DateTime.Now,
                            CreatedbyID = _dbUser.ID,
                            SchoolID = _dbUser.SchoolID,

                            Employee_ID = item.Employee_ID,
                            DailyJournalID = _dailyjornalid,



                        };

                        Context.AccountPayableCashes.Add(_AccountPayableCash);


                        HRPayrollEmployeeBenfitsOther hRPayrollEmployeeBenfitsOther = new HRPayrollEmployeeBenfitsOther()
                        {
                            PayrollEmployeesId = hRPayrollEmployee.Id,
                            AddedBenefitsOthersId = benefit.Id,
                            Percentage = benefit.BenefitPercent,
                            DailyJournalID = _dailyjornalid,
                            Amount = Amount,
                            CreatedbyID = _dbUser.ID,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            DeletedDate = DateTime.Now,
                            CompanyID = _dbUser.CompanyID,
                            SchoolID = _dbUser.SchoolID
                         
                        };
                        Context.HRPayrollEmployeeBenfitsOthers.Add(hRPayrollEmployeeBenfitsOther);



                        Context.SaveChanges();
                    }


                }


            };
            return Content("");
        }


        private List<EmployeePayrollViewModel> GetPayrollByDepartment(int deptId, int month,int year)
        {
            string query = @"select * from [EmployeePayrollByMonth](@p0,@p1,@p2) order by employee_ID";

            // ViewBag.AccountTreeName = _TreeHandler.GetById(AccountTree).AccountNameEN;
            var results = Context.Database.SqlQuery<EmployeePayrollViewModel>(query, deptId, month,year.ToString()).ToList();

            return results;
        }
        private List<EmployeePayrollViewModel> GetPayrollByEmpId(int empId, int month,int year)
        {
            string query = @"select * from [EmployeePayrollById](@p0,@p1)";

      
            var results = Context.Database.SqlQuery<EmployeePayrollViewModel>(query, empId, month, year).ToList();

            return results;
        }


        private RptCurrEarningFullViewModel GetCurrEarningReportByDepartment(int deptId, string fromDt, string toDt)
        {
            RptCurrEarningFullViewModel list = new RptCurrEarningFullViewModel();    

            string query = @"select * from [RptEmployeePayrollTotalSalary](@p0,@p1,@p2) order by EmployeeId";

            // ViewBag.AccountTreeName = _TreeHandler.GetById(AccountTree).AccountNameEN;
            list.Earnings = Context.Database.SqlQuery<RptCurrEarningViewModel>(query, deptId, fromDt, toDt).ToList();

        
           query = @"select * from [RptEmployeePayrollDetail](@p0,@p1,@p2) order by EmployeeId";

            // ViewBag.AccountTreeName = _TreeHandler.GetById(AccountTree).AccountNameEN;
            list.Earningsdetails = Context.Database.SqlQuery<RptCurrEarningViewModel>(query, deptId, fromDt, toDt).ToList();

        

            return list;
        }
        private RptCurrEarningFullViewModel GetCurrEarningReportByEmpId(int empId, string fromDt, string toDt)
        {
            RptCurrEarningFullViewModel list = new RptCurrEarningFullViewModel();
            string query = @"select * from [RptEmployeePayrollTotalSalaryById](@p0,@p1,@p2) order by EmployeeId";

            list.Earnings = Context.Database.SqlQuery<RptCurrEarningViewModel>(query, empId, fromDt, toDt).ToList();


             query = @"select * from [RptEmployeePayrollDetailByEmpId](@p0,@p1,@p2) order by EmployeeId";

            list.Earningsdetails = Context.Database.SqlQuery<RptCurrEarningViewModel>(query, empId, fromDt, toDt).ToList();

            return list;
        }




        public ActionResult Report()
        {
            return View();
        }


        [Filters.FileDownload]
        public ActionResult ExportExcel()
        {
            List<EmployeePayrollHistoryViewModel> results = GetPayrollHistory();

            return new ExcelResult(results, string.Format("Payroll History-{0}", DateTime.Now), "Report");


        }


        [Filters.FileDownload]
        public ActionResult ExportPdf()
        {


            List<EmployeePayrollHistoryViewModel> results = GetPayrollHistory();

            //return PartialView("_DailyJournalDetailList", list._AccountDailyJournalDetail);
            string html = RenderViewToString(this.ControllerContext, "History_Pdf", results);

            //return Content(html);

            return new PDFResult(html, "Payroll History", "Payroll history for all employees");

        }


        public ActionResult CurrEarnings() {

            ViewBag.SalaryMonths = Context.HRSalaryMonths.Select(x => new DropdownList
            {
                name = x.NameEn,
                value = x.Id.ToString()
            }).ToList();

            ViewBag.DepartmentList = Context.Database.SqlQuery<DropdownList>(@"Select * from DepartmentList(@p0,0)", schoolId).Select(x => new DropdownList
            {
                name = x.name,
                value = x.value.ToString(),
                text = x.text
            }).ToList();

            return PartialView();
        }


        public object GetCurrEarnings(int? deptId, string fromDt,string toDt, int? empId)
        {
            RptCurrEarningFullViewModel list = new RptCurrEarningFullViewModel() ;

            if (deptId != null)
            {
                list = GetCurrEarningReportByDepartment((int)deptId, fromDt, toDt);
                 
            }
            else if (empId != null)
            {
                list = GetCurrEarningReportByEmpId((int)empId, fromDt, toDt);
            }
            else
            {
                list = GetCurrEarningReportByDepartment(11, fromDt, toDt);

            }

            return PartialView("_RptCurrEarnings", list);
        }

        [Filters.FileDownload]
        public ActionResult ExportCurrEarnings(int? deptId, string fromDt, string toDt, int? empId)
        {
            RptCurrEarningFullViewModel list = new RptCurrEarningFullViewModel();

            if (deptId != null)
            {
                list = GetCurrEarningReportByDepartment((int)deptId, fromDt, toDt);

            }
            else if (empId != null)
            {
                list = GetCurrEarningReportByEmpId((int)empId, fromDt, toDt);
            }
            else
            {
                list = GetCurrEarningReportByDepartment(11, fromDt, toDt);

            }

          



            string html = RenderViewToString(this.ControllerContext, "_RptCurrEarnings_Pdf", list);

            return new PDFResult(html, "Current Earnings", "Current Earnings (" + fromDt +" - " + toDt +")");

        }




    }
}