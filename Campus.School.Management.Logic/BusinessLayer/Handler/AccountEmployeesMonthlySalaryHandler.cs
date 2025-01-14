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
    public class AccountEmployeesMonthlySalaryHandler : IRepository<AccountEmployeesMonthlySalaryViewModel>
    {

        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public AccountEmployeesMonthlySalaryViewModel Create()
        {
            throw new NotImplementedException();
        }

        public void Create(AccountEmployeesMonthlySalaryViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var currencyId = Context.BankCurrencies.Where(x => x.Isdefault == true && x.IsDeleted == false).Select(x => x.BankCurrencyID).FirstOrDefault();

            int? _dailyjornalid = 0;
            int? _AccountPayableCash_Id = 0;
            //int? _Accountchequespayables_Id = 0;


            if (model.paymentsalariesway == "BankStatment")
            {

            }
            else
            {
                //------------------------------------------------Enrollment registration ---------------------------------------
                AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();
                JournalTypesHandler journalTypes = new JournalTypesHandler();
                //var treasuryaccount = Context.AccountTreasuries.Where(x => x.ID == model.).Select(x => x.AccountTreeID).FirstOrDefault();

                List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {
                new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=model.costcenterID,
                    AccountTreeID = (int)model.AccountTreeID,
                    Debit =(decimal)model.NetSalary
                },

                 new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=model.costcenterID,
                    AccountTreeID = (int)model.AccountTreeID2,
                    Debit =(decimal)model.NetSalary
                },
                  new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=model.costcenterID,
                     AccountTreeID =model.fromAccount,
                     Credit =(decimal)model.NetSalary
                 },
                 new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=model.costcenterID,
                     AccountTreeID =model.fromAccount2,
                     Credit =(decimal)model.NetSalary
                 }
            };

                var _DailyJournal = _AccountDailyJournalHandler.Create();


                _DailyJournal.BankCurrencyID = currencyId;
                _DailyJournal.DailyJournalDate = model.OperationDate;

                _DailyJournal.Description = "Salary of "+model.SalaryOfMonth;
                _DailyJournal.Note = "payable note";
                _DailyJournal.IsPost = false;
                _DailyJournal.JournalType = journalTypes.GetAllByCompany(_dbUser.CompanyID).Where(x => x.NameEn == "Employee Salary").Select(c => c.ID).FirstOrDefault();
                _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
                _AccountDailyJournalHandler.Create(_DailyJournal);

                _dailyjornalid = Context.AccountDailyJournals.ToList().Last().DailyJournalID;
                //------------------------------------------------End of Registration ------------------------------------------------


                //___________________________________________ Cash receipt _________________________________________
                AccountPayableCashHandler _AccountPayableCashHandler = new AccountPayableCashHandler();
                var _AccountPayableCashModel = _AccountPayableCashHandler.Create();
                AccountPayableCash _AccountPayableCash = new AccountPayableCash()
                {

                    Amount = model.NetSalary,
                    InvoiceCode = _AccountPayableCashModel.InvoiceCode,
                    AccountTreeID = model.AccountTreeID,
                    CostCenterID = model.costcenterID,
                    //docementNumber = model.docementNumber,
                    //TreasuryID = model.TreasuryID,
                    // Description = model.Description,
                    OperationDate = model.OperationDate,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DeletedDate = DateTime.Now,
                    CreatedbyID = _dbUser.ID,
                    SchoolID = _dbUser.SchoolID,
                    BankCurrencyID = currencyId,
                    Employee_ID = model.Employee_ID,
                    DailyJournalID = _dailyjornalid,



                };
                Context.AccountPayableCashes.Add(_AccountPayableCash);
                Context.SaveChanges();
                _AccountPayableCash_Id = Context.AccountPayableCashes.ToList().Last().ID;
                //______________________________________________End of cash receipt ________________________________________


            }


            AccountEmployeesMonthlySalary _AccountEmployeesMonthlySalary = new AccountEmployeesMonthlySalary()
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
                Socialinsurance = model.Socialinsurance,
                NetSalary = model.NetSalary,
                Dedecation = model.Dedecation,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                OperationDate = model.OperationDate,
                JobID = model.JobID,
                SchoolID = _dbUser.SchoolID,
                accommodationallowanc = model.accommodationallowanc,
                AllowanceSalary2 = model.AllowanceSalary2,
                AllowanceSalary3 = model.AllowanceSalary3,
                conditionsworkallowance = model.conditionsworkallowance,
                Drivingallowance = model.Drivingallowance,
                paymentsalariesway = model.paymentsalariesway,
                Transitionallowance = model.Transitionallowance,
                Subsistenceallowance = model.Subsistenceallowance,
                DailyJournalID = _dailyjornalid,
                CashPayableID = _AccountPayableCash_Id,
                SalaryOfMonth = Convert.ToDateTime("01/"+model.SalaryOfMonth) 



            };

            Context.AccountEmployeesMonthlySalaries.Add(_AccountEmployeesMonthlySalary);
            Context.SaveChanges();


        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountEmployeesMonthlySalaries = Context.AccountEmployeesMonthlySalaries.Where(x => x.IsDeleted == false && x.ID == ID).FirstOrDefault();
            if (_AccountEmployeesMonthlySalaries != null)
            {
                _AccountEmployeesMonthlySalaries.IsDeleted = true;
                _AccountEmployeesMonthlySalaries.DeletedDate = DateTime.Now;
                _AccountEmployeesMonthlySalaries.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<AccountEmployeesMonthlySalaryViewModel> GetAll()
        {
            return Context.AccountEmployeesMonthlySalaries.Where(x => x.IsDeleted == false)
            .Select(x => new AccountEmployeesMonthlySalaryViewModel
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
                OperationDate = x.OperationDate,
                Subsistenceallowance = x.Subsistenceallowance,
                Transitionallowance = x.Transitionallowance,
                paymentsalariesway = x.paymentsalariesway,
                Drivingallowance = x.Drivingallowance,
                conditionsworkallowance = x.conditionsworkallowance,
                AllowanceSalary2 = x.AllowanceSalary2,
                AllowanceSalary3 = x.AllowanceSalary3,
                accommodationallowanc = x.accommodationallowanc,
                IsPaid = x.IsPaid,
                sumallowance = x.conditionsworkallowance + x.Transitionallowance + x.Drivingallowance + x.Subsistenceallowance + x.accommodationallowanc + x.AllowanceSalary + x.AllowanceSalary2 + x.AllowanceSalary3 + x.extraSalary,

            }).ToList();

        }


        public List<AccountEmployeesMonthlySalaryViewModel> GetAllByCompany(int companyId ,int schoolId)
        {
            return Context.AccountEmployeesMonthlySalaries.Where(x => x.IsDeleted == false && x.AdmEmployee.CompanyID == companyId && x.AdmEmployee.SchoolID == schoolId)
            .Select(x => new AccountEmployeesMonthlySalaryViewModel
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
                OperationDate = x.OperationDate,
                Subsistenceallowance = x.Subsistenceallowance,
                Transitionallowance = x.Transitionallowance,
                paymentsalariesway = x.paymentsalariesway,
                Drivingallowance = x.Drivingallowance,
                conditionsworkallowance = x.conditionsworkallowance,
                AllowanceSalary2 = x.AllowanceSalary2,
                AllowanceSalary3 = x.AllowanceSalary3,
                accommodationallowanc = x.accommodationallowanc,
                IsPaid = x.IsPaid,
                SalaryOfMonthDt = x.SalaryOfMonth,
                sumallowance = x.conditionsworkallowance + x.Transitionallowance + x.Drivingallowance + x.Subsistenceallowance + x.accommodationallowanc + x.AllowanceSalary + x.AllowanceSalary2 + x.AllowanceSalary3 + x.extraSalary,

            }).ToList();

        }

        public AccountEmployeesMonthlySalaryViewModel GetById(int ID)
        {


            return Context.AccountEmployeesMonthlySalaries.Where(x => x.IsDeleted == false && x.ID == ID)
           .Select(x => new AccountEmployeesMonthlySalaryViewModel
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
               OperationDate = x.OperationDate,
               Subsistenceallowance = x.Subsistenceallowance,
               Transitionallowance = x.Transitionallowance,
               paymentsalariesway = x.paymentsalariesway,
               Drivingallowance = x.Drivingallowance,
               conditionsworkallowance = x.conditionsworkallowance,
               AllowanceSalary2 = x.AllowanceSalary2,
               AllowanceSalary3 = x.AllowanceSalary3,
               accommodationallowanc = x.accommodationallowanc,

           }).FirstOrDefault();

        }

        public GenericVModel GetTotalSalary(DateTime DateFrom, DateTime DateTo)
        {

            var list = Context.AccountEmployeesMonthlySalaries.Where(x => x.OperationDate >= DateFrom && x.OperationDate <= DateTo).ToList();

            var _TotalSalary = decimal.Parse(list.Select(t => t.TotalSalary).Sum().ToString());

            GenericVModel _listDeduct = new GenericVModel()
            {

                ItemName = "Salaries Expenses",
                Value = _TotalSalary,
                TypeID = 2,
                Type = "Expenses",
            };
            return _listDeduct;
        }

        public AccountEmployeesMonthlySalaryViewModel Update(AccountEmployeesMonthlySalaryViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountEmployeesMonthlySalaries = Context.AccountEmployeesMonthlySalaries.Where(x => x.IsDeleted == false && x.ID == model.ID).FirstOrDefault();
            _AccountEmployeesMonthlySalaries.TotalSalary = model.TotalSalary;
            _AccountEmployeesMonthlySalaries.TotalDedecated = model.TotalDedecated;
            _AccountEmployeesMonthlySalaries.Taxes = model.Taxes;
            _AccountEmployeesMonthlySalaries.Socialinsurance = model.Socialinsurance;
            _AccountEmployeesMonthlySalaries.NetSalary = model.NetSalary;
            _AccountEmployeesMonthlySalaries.ModifiedDate = DateTime.Now;
            _AccountEmployeesMonthlySalaries.Medicalinsurance = model.Medicalinsurance;
            _AccountEmployeesMonthlySalaries.JobID = model.JobID;
            _AccountEmployeesMonthlySalaries.extraSalary = model.extraSalary;
            _AccountEmployeesMonthlySalaries.Employee_ID = model.Employee_ID;
            _AccountEmployeesMonthlySalaries.Dedecation = model.Dedecation;
            _AccountEmployeesMonthlySalaries.BonusesSalary = model.BonusesSalary;
            _AccountEmployeesMonthlySalaries.BasicSalary = model.BasicSalary;
            _AccountEmployeesMonthlySalaries.AllowanceSalary = model.AllowanceSalary;
            _AccountEmployeesMonthlySalaries.AdditionalSalary = model.AdditionalSalary;
            _AccountEmployeesMonthlySalaries.IsPaid = model.IsPaid;
            _AccountEmployeesMonthlySalaries.OperationDate = model.OperationDate;
            _AccountEmployeesMonthlySalaries.ModifiedbyID = _dbUser.ID;


            Context.SaveChanges();
            return model;

        }

        public AccountEmployeesMonthlySalaryViewModel GetHousingById(int ID)
        {
            return Context.AccountEmployeesMonthlySalaries.Where(x => x.IsDeleted == false && x.Employee_ID == ID)
              .Select(x => new AccountEmployeesMonthlySalaryViewModel
              {
                  ID = x.ID,
                  EmployeeName = x.AdmEmployee.NameE,
                  accommodationallowancetype = x.accommodationallowancetype,
                  Employee_ID = x.Employee_ID,
                  BankBranchID = x.BankBranchID,
                  accommodationallowanc = x.accommodationallowanc,

              }).FirstOrDefault();

        }

        public AccountEmployeesMonthlySalaryViewModel GetEmployeeTicketInfoById(int ID)
        {
            return Context.AccountEmployeesMonthlySalaries.Where(x => x.IsDeleted == false && x.Employee_ID == ID)
              .Select(x => new AccountEmployeesMonthlySalaryViewModel
              {
                  ID = x.ID,
                  Employee_ID = x.Employee_ID,
                  EmployeeName = x.AdmEmployee.NameE,
                  flighttickets = x.flighttickets
              }).FirstOrDefault();

        }


        public void PayTicket(AccountEmployeesMonthlySalaryViewModel model)
        {

            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var currencyId = Context.BankCurrencies.Where(x => x.Isdefault == true && x.IsDeleted == false).Select(x => x.BankCurrencyID).FirstOrDefault();

            int? _dailyjornalid = 0;
            int? _AccountPayableCash_Id = 0;
            //int? _Accountchequespayables_Id = 0;

            //------------------------------------------------Enrollment registration ---------------------------------------
            AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();

            //var treasuryaccount = Context.AccountTreasuries.Where(x => x.ID == model.).Select(x => x.AccountTreeID).FirstOrDefault();

            List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {
                new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=9020,
                    AccountTreeID = 25,
                    Debit =model.TicketPrice
                },

                 new AccountDailyJournalDetailViewModel
                 {
                    CostCenterID=9020,
                    AccountTreeID = 10061,
                    Debit =model.TicketPrice
                 }
            };


            var _DailyJournal = _AccountDailyJournalHandler.Create();


            _DailyJournal.BankCurrencyID = currencyId;
            _DailyJournal.DailyJournalDate = DateTime.Now;

            _DailyJournal.Description = "Ticket Payment For employee id :" + model.Employee_ID;
            _DailyJournal.Note = " note";
            _DailyJournal.IsPost = false;

            _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
            _AccountDailyJournalHandler.Create(_DailyJournal);

            _dailyjornalid = Context.AccountDailyJournals.ToList().Last().DailyJournalID;
            //------------------------------------------------End of Registration ------------------------------------------------


            //___________________________________________ Cash receipt _________________________________________
            AccountPayableCashHandler _AccountPayableCashHandler = new AccountPayableCashHandler();
            var _AccountPayableCashModel = _AccountPayableCashHandler.Create();
            AccountPayableCash _AccountPayableCash = new AccountPayableCash()
            {

                Amount = model.TicketPrice,
                InvoiceCode = _AccountPayableCashModel.InvoiceCode,
                AccountTreeID = 10061,
                CostCenterID = 9020,
                //docementNumber = model.docementNumber,
                //TreasuryID = model.TreasuryID,
                // Description = model.Description,
                OperationDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                SchoolID = _dbUser.SchoolID,
                BankCurrencyID = currencyId,
                Employee_ID = model.Employee_ID,
                DailyJournalID = _dailyjornalid,

            };
            Context.AccountPayableCashes.Add(_AccountPayableCash);
            Context.SaveChanges();
            _AccountPayableCash_Id = Context.AccountPayableCashes.ToList().Last().ID;
            //______________________________________________End of cash receipt ________________________________________


        }

    }
}
