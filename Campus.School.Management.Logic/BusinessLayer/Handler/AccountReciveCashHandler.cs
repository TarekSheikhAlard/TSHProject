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
     public  class AccountReciveCashHandler : IRepository<AccountReciveCashViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public AccountReciveCashViewModel Create()
        {
           

            AccountReciveCashViewModel model = new AccountReciveCashViewModel();

            DateTime DateFrom = DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", null);
            DateTime Dateto = DateFrom;
            TimeSpan ts = new TimeSpan(23, 59, 0);
            Dateto = Dateto.Date + ts;
            var InvoiceCode = Context.AccountReciveCashes.Where(c => c.CreatedDate >= DateFrom && c.CreatedDate <= Dateto).OrderByDescending(c => c.ID).Count();
            if (InvoiceCode > 0) model.InvoiceCode = (InvoiceCode + 1).ToString() + "/" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            else model.InvoiceCode = "1/" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year; ;

            return model;

       
        }

        public void Create(AccountReciveCashViewModel model)
        {
         SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

         AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();

            var treasuryaccount = Context.AccountTreasuries.Where(x => x.ID == model.TreasuryID && x.SchoolID == _dbUser.SchoolID).Select(x => x.AccountTreeID).FirstOrDefault();

         List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {

                new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=model.CostCenterID,AccountTreeID= model.AccountTreeID,Credit=(decimal)model.Amount  
                },
                 new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=model.CostCenterID, AccountTreeID=(int)treasuryaccount,Debit=(decimal)model.Amount
                 }
            };




         var _DailyJournal = _AccountDailyJournalHandler.Create();

           
            //_DailyJournal.BankCurrencyID = model.BankCurrencyID;
            _DailyJournal.DailyJournalDate = model.OperationDate;
            _DailyJournal.DocumentNumber = model.docementNumber;
            _DailyJournal.Description = "سند قبض رقم" + model.InvoiceCode + "خاص" + model.Description;
            _DailyJournal.Note = "received receit" + model.InvoiceCode;
            _DailyJournal.Note1 = "سند قبض رقم" + model.InvoiceCode;
            _DailyJournal.IsPost = true;
            
            _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;

            _DailyJournal.SchoolID = _dbUser.SchoolID;
            _DailyJournal.CompanyID = _dbUser.CompanyID;

            _AccountDailyJournalHandler.Create(_DailyJournal);

            var _dailyjornalid = Context.AccountDailyJournals.ToList().Last().DailyJournalID;

            //int _dailyjornalid = element.DailyJournalID;

            AccountReciveCash _AccountReciveCash = new AccountReciveCash()
            {

                 Amount=model.Amount, InvoiceCode=model.InvoiceCode,
                 AccountTreeID=model.AccountTreeID, CostCenterID=model.CostCenterID,
                 docementNumber=model.docementNumber,TreasuryID=model.TreasuryID,
                 Description=model.Description, OperationDate =model.OperationDate,
                CreatedDate=DateTime.Now,
                ModifiedDate =DateTime.Now ,
                DeletedDate =DateTime .Now,
                CreatedbyID =_dbUser.ID,
                SchoolID =_dbUser.SchoolID,
                //BankCurrencyID = model.BankCurrencyID,
                Employee_ID = model.Employee_ID,
                DailyJournalID=_dailyjornalid
         
            };
            Context.AccountReciveCashes.Add(_AccountReciveCash);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountReciveCash = Context.AccountReciveCashes.Where(x => x.ID == ID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_AccountReciveCash != null)
            {
                _AccountReciveCash.IsDeleted = true;
                _AccountReciveCash.DeletedDate = DateTime.Now;
                _AccountReciveCash.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<AccountReciveCashViewModel> GetAll()
        {
            return Context.AccountReciveCashes.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x => new AccountReciveCashViewModel
            {
                ID = x.ID,
                AccountTreeID =(int) x.AccountTreeID,
                AccountNameEN = x.AccountTree.AccountNameAR,
                Amount = x.Amount,
                CostCenterID = x.CostCenterID,
                costcenterName = x.AccountCostCenter.CostCenterAR,
                Description = x.Description,
                docementNumber = x.docementNumber,
                InvoiceCode = x.InvoiceCode,
                OperationDate = x.OperationDate,
                TreasuryID = x.TreasuryID,
                TreasuryName = x.AccountTreasury.TreasuryName,
                Currency_Type=x.BankCurrency.Currency_Type,
                BankCurrencyID=x.BankCurrencyID,
                Employee_ID=x.Employee_ID,
                NameA=x.AdmEmployee.NameA,
                AccountDailyJournal =x.AccountDailyJournal
                
            }).ToList();
        }

        public AccountReciveCashViewModel GetById(int ID)
        {
            return Context.AccountReciveCashes.Where(x => x.IsDeleted == false && x.ID == ID && x.SchoolID == _dbUser.SchoolID).Select(x => new AccountReciveCashViewModel
            {
                ID = x.ID,
                AccountTreeID =(int) x.AccountTreeID,
                AccountNameEN = x.AccountTree.AccountNameAR,
                Amount = x.Amount,
                CostCenterID = x.CostCenterID,
                costcenterName = x.AccountCostCenter.CostCenterAR,
                Description = x.Description,
                docementNumber = x.docementNumber,
                InvoiceCode = x.InvoiceCode,
                OperationDate = x.OperationDate,
                TreasuryID = x.TreasuryID,
                TreasuryName = x.AccountTreasury.TreasuryName,
                Currency_Type = x.BankCurrency.Currency_Type,
                BankCurrencyID = x.BankCurrencyID,
                Employee_ID = x.Employee_ID,
                NameA = x.AdmEmployee.NameA,
                AccountDailyJournal = x.AccountDailyJournal
               
            }).FirstOrDefault();
        }

        public AccountReciveCashViewModel Update(AccountReciveCashViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountReciveCash = Context.AccountReciveCashes.Where(x => x.ID ==model. ID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            _AccountReciveCash.AccountTreeID = model.AccountTreeID;
            _AccountReciveCash.CostCenterID = model.CostCenterID;
            _AccountReciveCash.TreasuryID = model.TreasuryID;
            _AccountReciveCash.InvoiceCode = model.InvoiceCode;
            _AccountReciveCash.OperationDate = model.OperationDate;
            _AccountReciveCash.docementNumber = model.docementNumber;
            _AccountReciveCash.Description = model.Description;
            _AccountReciveCash.ModifiedDate = DateTime.Now;
            _AccountReciveCash.Amount = model.Amount;
            _AccountReciveCash.ModifiedbyID = _dbUser.ID;
            _AccountReciveCash.Employee_ID = model.Employee_ID;
            //_AccountReciveCash.BankCurrencyID = model.BankCurrencyID;

            Context.SaveChanges();
            return model;
        }
    }
}
