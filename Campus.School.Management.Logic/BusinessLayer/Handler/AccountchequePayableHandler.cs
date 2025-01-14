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
    public class AccountchequePayableHandler : IRepository<AccountchequePayableViewModel>
    {

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public AccountchequePayableViewModel Create()
        {

            AccountchequePayableViewModel model = new AccountchequePayableViewModel();
            DateTime DateFrom = DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", null);
            DateTime Dateto = DateFrom;
            TimeSpan ts = new TimeSpan(23, 59, 0);
            Dateto = Dateto.Date + ts;
            var InvoiceCode = Context.AccountchequePayables.Where(c => c.CreatedDate >= DateFrom && c.CreatedDate <= Dateto && c.SchoolID ==_dbUser.SchoolID).OrderByDescending(c => c.ID).Count();
            if (InvoiceCode > 0) model.InvoiceCode = (InvoiceCode + 1).ToString() + "/" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            else model.InvoiceCode = "1/" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year; ;
            return model;

        }

        public void Create(AccountchequePayableViewModel model)
        {

            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AccountDailyJournalHandler _AccountDailyJournalHandler = new AccountDailyJournalHandler();

            List<AccountDailyJournalDetailViewModel> DailyJournalDetaillist = new List<AccountDailyJournalDetailViewModel>()
            {

                new AccountDailyJournalDetailViewModel
                {
                    CostCenterID=model.costcenterID,AccountTreeID= model.fromAccount,Credit=(decimal)model.Amount
                },
                 new AccountDailyJournalDetailViewModel
                 {
                     CostCenterID=model.costcenterID, AccountTreeID=(int)model.AccountTreeID,Debit=(decimal)model.Amount
                 }
            };

            var _DailyJournal = _AccountDailyJournalHandler.Create();


            //_DailyJournal.BankCurrencyID = model.BankCurrencyID;
            _DailyJournal.DailyJournalDate = model.operationDate;
            _DailyJournal.DocumentNumber = model.docementNumber;
            _DailyJournal.Description = "اصدار شيك" + model.InvoiceCode + "خاص" + model.Notes;

            _DailyJournal.Note = "payable cheque" + model.InvoiceCode;
            _DailyJournal.Note1 = "اصدار شيك " + model.InvoiceCode;
       
                _DailyJournal.IsPost = true;
       
            _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
            _DailyJournal.SchoolID = _dbUser.SchoolID;
            _DailyJournal.CompanyID = _dbUser.CompanyID;


            _AccountDailyJournalHandler.Create(_DailyJournal);
          

            var _dailyjornalid = Context.AccountDailyJournals.ToList().Last().DailyJournalID;


            AccountchequePayable _AccountchequePayable = new AccountchequePayable()
           {
                BankBranchID = model.BankBranchID,
                docementNumber = model.docementNumber,
                operationDate = model.operationDate,
             
                Notes = model.Notes,
                costcenterID = model.costcenterID,
                CreatedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                fromAccount = model.fromAccount,
                Amount = model.Amount,
                AccountTreeID = model.AccountTreeID,
                InvoiceCode = model.InvoiceCode,
                CreatedbyID=_dbUser.ID,
                SchoolID=_dbUser.SchoolID,
               // BankCurrencyID = model.BankCurrencyID,
                Employee_ID = model.Employee_ID,
                chequeNumber =model.chequeNumber,
                RecipientName=model.RecipientName,
                RecipientTel=model.RecipientTel,
                Deliverydate=model.Deliverydate,
                DateOfIssuance=model.DateOfIssuance,
                DailyJournalID = _dailyjornalid,

            };
            Context.AccountchequePayables.Add(_AccountchequePayable);
            Context.SaveChanges();
      
        }

        public List<AccountchequePayableViewModel> GetAll()
        {
            return Context.AccountchequePayables.Where(x => x.IsDeleted == false &&x.AccountDailyJournal.SchoolID == _dbUser.SchoolID)
            .Select(x => new AccountchequePayableViewModel
            {
                AccountTreeID = x.AccountTreeID,
                AccountNameEN = x.AccountTree.AccountNameAR ,
                AccountNameEN1=x.AccountTree1.AccountNameAR,
                Amount = x.Amount,
                BankBranchID = x.BankBranchID,
                BankBranchName = x.BankBranch.BranchName,
                costcenterID = x.costcenterID,
                costcenterName = x.AccountCostCenter.CostCenter,
                docementNumber = x.docementNumber,
                fromAccount = x.fromAccount.Value,
                ID = x.ID,
                InvoiceCode = x.InvoiceCode,
                 Notes = x.Notes,
                operationDate = x.operationDate,
                Currency_Type = x.BankCurrency.Currency_Type,
                BankCurrencyID = x.BankCurrencyID,
                Employee_ID = x.Employee_ID,
                NameA = x.AdmEmployee.NameA,
                RecipientName=x.RecipientName,
                chequeNumber=x.chequeNumber,
                Deliverydate=x.Deliverydate,
                RecipientTel=x.RecipientTel,
                DateOfIssuance=x.DateOfIssuance,
               AccountDailyJournal=x.AccountDailyJournal

            }
            ).ToList();
        }

        public AccountchequePayableViewModel GetById(int ID)
        {
            return Context.AccountchequePayables.Where(x => x.IsDeleted == false && x.ID == ID && x.AccountDailyJournal.SchoolID == _dbUser.SchoolID)
                     .Select(x => new AccountchequePayableViewModel
                     {
                         costcenterID = x.costcenterID,
                         AccountTreeID = x.AccountTreeID,
                         AccountNameEN = x.AccountTree.AccountNameEN,
                         AccountNameEN1 = x.AccountTree1.AccountNameAR,
                         Amount = x.Amount,
                         BankBranchID = x.BankBranchID,
                         BankBranchName = x.BankBranch.BranchName,
                       
                         costcenterName = x.AccountCostCenter.CostCenter,
                         docementNumber = x.docementNumber,
                         fromAccount = x.fromAccount.Value,
                         ID = x.ID,
                         InvoiceCode = x.InvoiceCode,
                         Notes = x.Notes,
                         operationDate = x.operationDate,
                         Currency_Type = x.BankCurrency.Currency_Type,
                         BankCurrencyID = x.BankCurrencyID,
                         Employee_ID = x.Employee_ID,
                         NameA = x.AdmEmployee.NameA,
                         RecipientName = x.RecipientName,
                         chequeNumber = x.chequeNumber,
                         Deliverydate = x.Deliverydate,
                         RecipientTel = x.RecipientTel,
                         DateOfIssuance = x.DateOfIssuance,
                         AccountDailyJournal = x.AccountDailyJournal
                     }
                     ).FirstOrDefault();
        }

        public AccountchequePayableViewModel Update(AccountchequePayableViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountchequePayable = Context.AccountchequePayables.Where(x => x.ID == model.ID && x.AccountDailyJournal.SchoolID == _dbUser.SchoolID).FirstOrDefault();

            _AccountchequePayable.Notes = model.Notes;
            _AccountchequePayable.AccountTreeID = model.AccountTreeID;
            _AccountchequePayable.Amount = model.Amount;
            _AccountchequePayable.BankBranchID = model.BankBranchID;
      
            _AccountchequePayable.costcenterID = model.costcenterID;
            _AccountchequePayable.docementNumber = model.docementNumber;
            _AccountchequePayable.InvoiceCode = model.InvoiceCode;
            _AccountchequePayable.ModifiedDate = DateTime.Now;
            _AccountchequePayable.ModifiedbyID = _dbUser.ID;
            _AccountchequePayable.operationDate = model.operationDate;
            Context.SaveChanges();

            return model;
        }
        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccountchequePayable = Context.AccountchequePayables.Where(x => x.ID == ID && x.AccountDailyJournal.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_AccountchequePayable != null)
            {
                _AccountchequePayable.IsDeleted = true;
                _AccountchequePayable.DeletedDate = DateTime.Now;
                _AccountchequePayable.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }


    }
}
