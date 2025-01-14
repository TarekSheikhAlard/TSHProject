using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class AccountEmployeesSalaryHandler : IRepository<AccountEmployeesSalaryViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public AccountEmployeesSalaryViewModel Create()
        {
            throw new NotImplementedException();
        }

        public void Create(AccountEmployeesSalaryViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccountEmployeesSalary _AccountEmployeesSalary = new AccountEmployeesSalary()
            {
                BasicSalary = model.BasicSalary,
                AdditionalSalary = model.AdditionalSalary,
                BonusesSalary = model.BonusesSalary,
                AllowanceSalary = model.AllowanceSalary,
                extraSalary = model.extraSalary,
                TotalSalary = model.TotalSalary,
                Medicalinsurance = model.Medicalinsurance,
                TotalDedecated = model.TotalDedecated,
                Taxes = model.Taxes,
                Employee_ID = model.Employee_ID,
                JobID = Context.AdmEmployees.Where(x => x.Employee_ID == model.Employee_ID).Select(x => x.JobID).FirstOrDefault(),
                Socialinsurance = model.Socialinsurance,
                NetSalary = model.NetSalary,
                Dedecation = model.Dedecation,
                CreatedbyID = _dbUser.CreatedbyID,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID,
                accommodationallowanc = model.accommodationallowanc,
                accommodationallowancetype = model.accommodationallowancetype,
                AccountNumber = model.AccountNumber,
                Airline = model.Airline,
                workingHours = model.workingHours,
                AllowanceSalary3 = model.AllowanceSalary3,
                AllowanceSalary2 = model.AllowanceSalary2,
                BankBranchID = model.BankBranchID,
                valuedate = model.valuedate,
                Airlineclass = model.Airlineclass,
                Drivingallowance = model.Drivingallowance,
                conditionsworkallowance = model.conditionsworkallowance,
                Subsistenceallowance = model.Subsistenceallowance,
                Transitionallowance = model.Transitionallowance,
                 Dept_ID=model.Dept_ID,
                  flighttickets=model.flighttickets,
                   FTperMonth=model.FTperMonth,
                    leavebalance=model.leavebalance,
                     LBperMonth=model.LBperMonth,
                     CompanyID=model.CompanyID


            };
            Context.AccountEmployeesSalaries.Add(_AccountEmployeesSalary);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            var _AccountEmployeesSalary = Context.AccountEmployeesSalaries.Where(x => x.ID == ID && x.IsDeleted == false).FirstOrDefault();
            if (_AccountEmployeesSalary != null)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
                _AccountEmployeesSalary.DeletedbyID = _dbUser.ID;
                _AccountEmployeesSalary.IsDeleted = true;
                _AccountEmployeesSalary.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }

        public List<AccountEmployeesSalaryViewModel> GetAll()
        {
            return Context.AccountEmployeesSalaries.Where(x => x.IsDeleted == false)
            .Select(x => new AccountEmployeesSalaryViewModel
            {
                ID = x.ID,
                EmployeeName = x.AdmEmployee.NameE,
                AdditionalSalary = x.AdditionalSalary,
                AllowanceSalary = x.AllowanceSalary,
                BasicSalary = x.BasicSalary,
                BonusesSalary = x.BonusesSalary,
                Dedecation = x.Dedecation,
                Taxes = x.Taxes,
                Employee_ID = x.Employee_ID,
                extraSalary = x.extraSalary,
                JobID = x.JobID,
                JobName=x.AdmJob.JobNameEn,
                Medicalinsurance =x.Medicalinsurance,
                NetSalary =x.NetSalary,
                Socialinsurance =x.Socialinsurance,
                TotalDedecated =x.TotalDedecated,
                TotalSalary =x.TotalSalary,
                accommodationallowanc =x.accommodationallowanc,
                Transitionallowance =x.Transitionallowance,
                valuedate =x.valuedate,
                workingHours =x.workingHours,
                Subsistenceallowance =x.Subsistenceallowance,
                 Drivingallowance=x.Drivingallowance,
                  accommodationallowancetype=x.accommodationallowancetype,
                   conditionsworkallowance=x.conditionsworkallowance,
                AccountNumber =x.AccountNumber,
                Airline =x.Airline,
                Airlineclass =x.Airlineclass,
                AllowanceSalary2 =x.AllowanceSalary2,
                AllowanceSalary3 =x.AllowanceSalary3,
                BankBranchID =x.BankBranchID,Dept_Name=x.AdmDepartment.DepartmentAr,
                Dept_ID =x.Dept_ID, LBperMonth=x.LBperMonth, leavebalance=x.leavebalance, FTperMonth=x.FTperMonth, flighttickets=x.flighttickets
                                    
            }).ToList();

        }

        public List<AccountEmployeesSalaryViewModel> GetAllByCompany(int companyId,int schoolId)
        {
            return Context.AccountEmployeesSalaries.Where(x => x.IsDeleted == false&&x.CompanyID==companyId&&x.SchoolID==schoolId)
            .Select(x => new AccountEmployeesSalaryViewModel
            {
                ID = x.ID,
                EmployeeName = x.AdmEmployee.NameE,
                AdditionalSalary = x.AdditionalSalary,
                AllowanceSalary = x.AllowanceSalary,
                BasicSalary = x.BasicSalary,
                BonusesSalary = x.BonusesSalary,
                Dedecation = x.Dedecation,
                Taxes = x.Taxes,
                Employee_ID = x.Employee_ID,
                extraSalary = x.extraSalary,
                JobID = x.JobID,
                JobName = x.AdmJob.JobNameEn,
                Medicalinsurance = x.Medicalinsurance,
                NetSalary = x.NetSalary,
                Socialinsurance = x.Socialinsurance,
                TotalDedecated = x.TotalDedecated,
                TotalSalary = x.TotalSalary,
                accommodationallowanc = x.accommodationallowanc,
                Transitionallowance = x.Transitionallowance,
                valuedate = x.valuedate,
                workingHours = x.workingHours,
                Subsistenceallowance = x.Subsistenceallowance,
                Drivingallowance = x.Drivingallowance,
                accommodationallowancetype = x.accommodationallowancetype,
                conditionsworkallowance = x.conditionsworkallowance,
                AccountNumber = x.AccountNumber,
                Airline = x.Airline,
                Airlineclass = x.Airlineclass,
                AllowanceSalary2 = x.AllowanceSalary2,
                AllowanceSalary3 = x.AllowanceSalary3,
                BankBranchID = x.BankBranchID,
                BankName =x.BankBranch.BranchName,
                Dept_Name = x.AdmDepartment.DepartmentAr,
                Dept_ID = x.Dept_ID,
                LBperMonth = x.LBperMonth,
                leavebalance = x.leavebalance,
                FTperMonth = x.FTperMonth,
                flighttickets = x.flighttickets

            }).ToList();

        }

        public AccountEmployeesSalaryViewModel GetById(int ID)
        {
            return Context.AccountEmployeesSalaries.Where(x => x.IsDeleted == false && x.ID == ID)
          .Select(x => new AccountEmployeesSalaryViewModel
          {
              ID = x.ID,
              EmployeeName = x.AdmEmployee.NameE,
              AdditionalSalary = x.AdditionalSalary,
              AllowanceSalary = x.AllowanceSalary,
              BasicSalary = x.BasicSalary,
              BonusesSalary = x.BonusesSalary,
              Dedecation = x.Dedecation,
              Taxes = x.Taxes,
              Employee_ID = x.Employee_ID,
              extraSalary = x.extraSalary,
              JobID = x.JobID,
              JobName = x.AdmJob.JobNameEn,
              Medicalinsurance = x.Medicalinsurance,
              NetSalary = x.NetSalary,
              Socialinsurance = x.Socialinsurance,
              TotalDedecated = x.TotalDedecated,
              TotalSalary = x.TotalSalary,
              accommodationallowanc = x.accommodationallowanc,
              Transitionallowance = x.Transitionallowance,
              valuedate = x.valuedate,
              workingHours = x.workingHours,
              Subsistenceallowance = x.Subsistenceallowance,
              Drivingallowance = x.Drivingallowance,
              accommodationallowancetype = x.accommodationallowancetype,
              conditionsworkallowance = x.conditionsworkallowance,
              AccountNumber = x.AccountNumber,
              Airline = x.Airline,
              Airlineclass = x.Airlineclass,
              AllowanceSalary2 = x.AllowanceSalary2,
              AllowanceSalary3 = x.AllowanceSalary3,
              BankBranchID = x.BankBranchID,
               Dept_ID = x.Dept_ID,
              LBperMonth = x.LBperMonth,
              leavebalance = x.leavebalance,
              FTperMonth = x.FTperMonth,
              flighttickets = x.flighttickets,
              Dept_Name = x.AdmDepartment.DepartmentAr
          }).FirstOrDefault();
        }

        public AccountEmployeesSalaryViewModel Update(AccountEmployeesSalaryViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
         
            var _AccountEmployeesSalary = Context.AccountEmployeesSalaries.Where(x => x.ID ==model. ID && x.IsDeleted == false).FirstOrDefault();
            _AccountEmployeesSalary.TotalSalary = model.TotalSalary;
            _AccountEmployeesSalary.TotalDedecated = model.TotalDedecated;
            _AccountEmployeesSalary.Taxes = model.Taxes;
            _AccountEmployeesSalary.Socialinsurance = model.Socialinsurance;
            _AccountEmployeesSalary.NetSalary = model.NetSalary;
            _AccountEmployeesSalary.ModifiedDate = DateTime.Now;
            _AccountEmployeesSalary.Medicalinsurance = model.Medicalinsurance;
            _AccountEmployeesSalary.JobID = Context.AdmEmployees.Where(x => x.Employee_ID == model.Employee_ID).Select(x => x.JobID).FirstOrDefault();
            _AccountEmployeesSalary.extraSalary = model.extraSalary;
            _AccountEmployeesSalary.Employee_ID = model.Employee_ID;
            _AccountEmployeesSalary.Dedecation = model.Dedecation;
            _AccountEmployeesSalary.BonusesSalary = model.BonusesSalary;
            _AccountEmployeesSalary.BasicSalary = model.BasicSalary;
            _AccountEmployeesSalary.AllowanceSalary = model.AllowanceSalary;
            _AccountEmployeesSalary.AdditionalSalary = model.AdditionalSalary;
            _AccountEmployeesSalary.ModifiedbyID = _dbUser.ID;
            _AccountEmployeesSalary.ModifiedDate = DateTime.Now;
            _AccountEmployeesSalary.BankBranchID = model.BankBranchID;
            _AccountEmployeesSalary.accommodationallowanc = model.accommodationallowanc;
            _AccountEmployeesSalary.accommodationallowancetype = model.accommodationallowancetype;
            _AccountEmployeesSalary.AccountNumber = model.AccountNumber;
            _AccountEmployeesSalary.Airline = model.Airline;
            _AccountEmployeesSalary.Airlineclass = model.Airlineclass;
            _AccountEmployeesSalary.AllowanceSalary2 = model.AllowanceSalary2;
            _AccountEmployeesSalary.AllowanceSalary3 = model.AllowanceSalary3;
            _AccountEmployeesSalary.conditionsworkallowance = model.conditionsworkallowance;
         
            _AccountEmployeesSalary.Drivingallowance = model.Drivingallowance;
       
            _AccountEmployeesSalary.Subsistenceallowance = model.Subsistenceallowance;
            _AccountEmployeesSalary.Transitionallowance = model.Transitionallowance;
            _AccountEmployeesSalary.valuedate = model.valuedate;
            _AccountEmployeesSalary.workingHours = model.workingHours;
            _AccountEmployeesSalary.Dept_ID = model.Dept_ID;
            _AccountEmployeesSalary.flighttickets = model.flighttickets;
            _AccountEmployeesSalary.FTperMonth = model.FTperMonth;
            _AccountEmployeesSalary.leavebalance = model.leavebalance;
            _AccountEmployeesSalary.LBperMonth = model.LBperMonth;
            
           
            //----
            Context.SaveChanges();
            return model;

        }
    }
}
