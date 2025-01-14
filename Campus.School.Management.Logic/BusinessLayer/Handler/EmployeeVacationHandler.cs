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
    public class EmployeeVacationHandler : IRepository<EmployeeVacationViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(EmployeeVacationViewModel model)
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

                //var treasuryaccount = Context.AccountTreasuries.Where(x => x.ID == model.).Select(x => x.AccountTreeID).FirstOrDefault();

                List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {
                    new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=9020,
                    AccountTreeID = 18,
                    Debit =(decimal)model.NetSalary
                },

                 new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=9020,
                    AccountTreeID = 40,
                    Debit =(decimal)model.NetSalary
                },
                  new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=9020,
                     AccountTreeID =33,
                     Credit =(decimal)model.NetSalary
                 },
                 new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=9020,
                     AccountTreeID =10071,
                     Credit =(decimal)model.NetSalary
                 }
                };

                var _DailyJournal = _AccountDailyJournalHandler.Create();


                _DailyJournal.BankCurrencyID = currencyId;
                _DailyJournal.DailyJournalDate = DateTime.Now;

                _DailyJournal.Description = "Vacation Allowances";
                _DailyJournal.Note = "payable note";
                _DailyJournal.IsPost = false;

                _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
                _AccountDailyJournalHandler.Create(_DailyJournal);
                _DailyJournal.SchoolID = _dbUser.SchoolID;
                _DailyJournal.SchoolID = _dbUser.CompanyID;

                _dailyjornalid = Context.AccountDailyJournals.ToList().Last().DailyJournalID;
                //------------------------------------------------End of Registration ------------------------------------------------


                //___________________________________________ Cash receipt _________________________________________
                AccountPayableCashHandler _AccountPayableCashHandler = new AccountPayableCashHandler();
                var _AccountPayableCashModel = _AccountPayableCashHandler.Create();
                AccountPayableCash _AccountPayableCash = new AccountPayableCash()
                {

                    Amount = model.NetSalary,
                    InvoiceCode = _AccountPayableCashModel.InvoiceCode,
                    AccountTreeID = 33,
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

                    CompanyID =_dbUser.CompanyID

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
             
                TotalDedecated = model.TotalDedecated,
               
                Employee_ID = model.Employee_ID,
              
                NetSalary = model.NetSalary,
                Dedecation = model.Dedecation,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
             
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
                
                
            };

            Context.AccountEmployeesMonthlySalaries.Add(_AccountEmployeesMonthlySalary);
            Context.SaveChanges();

            var _EmpSalaryId = Context.AccountEmployeesMonthlySalaries.ToList().Last().ID;

            //-------------------Vacation Entry--------------------//

            EmployeeVacation employeeVacation = new EmployeeVacation
            {
                Employee_ID = model.Employee_ID,
                HolidayTypeId = model.HolidayTypeId,
                DateOfVacation = model.DateOfVacation,
                LastWorkingDate = model.LastWorkingDate,
                EmployeeSalaryId = _EmpSalaryId,
                ContactOnVacation = model.ContactOnVacation,
                EmployeeInCharge = model.EmployeeInCharge,
                CreatedbyID =_dbUser.CreatedbyID,
                CreatedDate=DateTime.Now
               
                
            };
            Context.EmployeeVacations.Add(employeeVacation);
            Context.SaveChanges();

        }

        public EmployeeVacationViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeVacationViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public EmployeeVacationViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public EmployeeVacationViewModel Update(EmployeeVacationViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
