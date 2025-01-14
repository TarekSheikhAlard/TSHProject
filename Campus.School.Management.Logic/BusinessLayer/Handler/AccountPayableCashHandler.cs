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
    public class AccountPayableCashHandler : IRepository<AccountPayableCashViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public AccountPayableCashViewModel Create()
        {

            AccountPayableCashViewModel model = new AccountPayableCashViewModel();

            DateTime DateFrom = DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", null);
            DateTime Dateto = DateFrom;
            TimeSpan ts = new TimeSpan(23, 59, 0);
            Dateto = Dateto.Date + ts;
            var InvoiceCode = Context.AccountPayableCashes.Where(c => c.CreatedDate >= DateFrom && c.CreatedDate <= Dateto).OrderByDescending(c => c.ID).Count();
            if (InvoiceCode > 0) model.InvoiceCode = (InvoiceCode + 1).ToString() + "/" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            else model.InvoiceCode = "1/" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year; ;


            return model;

        }

        public void Create(AccountPayableCashViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();

            var treasuryaccount = Context.AccountTreasuries.Where(x => x.ID == model.TreasuryID && x.SchoolID == _dbUser.SchoolID).Select(x => x.AccountTreeID).FirstOrDefault();

            List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {

                new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=model.CostCenterID,AccountTreeID=(int) model.AccountTreeID,Debit=(decimal)model.Amount
                },
                 new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=model.CostCenterID, AccountTreeID=(int)treasuryaccount,Credit=(decimal)model.Amount
                 }
            };




            var _DailyJournal = _AccountDailyJournalHandler.Create();


            //_DailyJournal.BankCurrencyID = model.BankCurrencyID;
            _DailyJournal.DailyJournalDate = model.OperationDate;
            _DailyJournal.DocumentNumber = model.docementNumber;
            _DailyJournal.Description = "سند دفع رقم" + model.InvoiceCode + "خاص" + model.Description;

            _DailyJournal.Note = "payed receit" + model.InvoiceCode;
            _DailyJournal.Note1 = "سند دفع رقم" + model.InvoiceCode;

            _DailyJournal.IsPost = true;

            _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;

            _DailyJournal.SchoolID = _dbUser.SchoolID;
            _DailyJournal.CompanyID = _dbUser.CompanyID;

            _AccountDailyJournalHandler.Create(_DailyJournal);

            var _dailyjornalid = Context.AccountDailyJournals.ToList().Last().DailyJournalID;


            AccountPayableCash _AccountPayableCash = new AccountPayableCash()
            {

                Amount = model.Amount,
                InvoiceCode = model.InvoiceCode,
                AccountTreeID = model.AccountTreeID,
                CostCenterID = model.CostCenterID,
                docementNumber = model.docementNumber,
                TreasuryID = model.TreasuryID,
                Description = model.Description,
                OperationDate = model.OperationDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID=_dbUser.ID,
                SchoolID =_dbUser.SchoolID,
                //BankCurrencyID = model.BankCurrencyID,
                Employee_ID = model.Employee_ID,
                DailyJournalID = _dailyjornalid,
                RecipientName=model.RecipientName,
                RecipientTel=model.RecipientTel


            };
            Context.AccountPayableCashes.Add(_AccountPayableCash);
            Context.SaveChanges();



        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountPayableCash = Context.AccountPayableCashes.Where(x => x.ID == ID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_AccountPayableCash != null)
            {
                _AccountPayableCash.IsDeleted = true;
                _AccountPayableCash.DeletedDate = DateTime.Now;
                _AccountPayableCash.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<AccountPayableCashViewModel> GetAll()
        {
            return Context.AccountPayableCashes.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x => new AccountPayableCashViewModel
            {
                ID = x.ID,
                AccountTreeID = x.AccountTreeID,
                AccountNameEN = x.AccountTree.AccountNameEN,
                Amount = x.Amount,
                CostCenterID = x.CostCenterID,
                costcenterName = x.AccountCostCenter.CostCenter,
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
                AccountDailyJournal = x.AccountDailyJournal,
                RecipientName =x.RecipientName,
                RecipientTel =x.RecipientTel

            }).ToList();
        }

        public AccountPayableCashViewModel GetById(int ID)
        {
            return Context.AccountPayableCashes.Where(x => x.IsDeleted == false && x.ID == ID && x.SchoolID == _dbUser.SchoolID).Select(x => new AccountPayableCashViewModel
            {
                ID = x.ID,
                AccountTreeID = x.AccountTreeID,
                AccountNameEN = x.AccountTree.AccountNameEN,
                Amount = x.Amount,
                CostCenterID = x.CostCenterID,
                costcenterName = x.AccountCostCenter.CostCenter,
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
                AccountDailyJournal = x.AccountDailyJournal,
                RecipientName = x.RecipientName,
                RecipientTel = x.RecipientTel

            }).FirstOrDefault();
        }
        public AccountPayableCashViewModel Update(AccountPayableCashViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountPayableCash = Context.AccountPayableCashes.Where(x => x.ID == model.ID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            _AccountPayableCash.AccountTreeID = model.AccountTreeID;
            _AccountPayableCash.CostCenterID = model.CostCenterID;
            _AccountPayableCash.TreasuryID = model.TreasuryID;
            _AccountPayableCash.InvoiceCode = model.InvoiceCode;
            _AccountPayableCash.OperationDate = model.OperationDate;
            _AccountPayableCash.docementNumber = model.docementNumber;
            _AccountPayableCash.Description = model.Description;
            _AccountPayableCash.ModifiedDate = DateTime.Now;
            _AccountPayableCash.Amount = model.Amount;
            _AccountPayableCash.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;
        }
    }
}
