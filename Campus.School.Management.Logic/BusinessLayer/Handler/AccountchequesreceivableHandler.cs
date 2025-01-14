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
    public class AccountchequesreceivableHandler : IRepository<AccountchequesreceivableViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public AccountchequesreceivableViewModel Create()
        {
           
            AccountchequesreceivableViewModel model = new AccountchequesreceivableViewModel()
            {
             
            };

            DateTime DateFrom = DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", null);
            DateTime Dateto = DateFrom;
            TimeSpan ts = new TimeSpan(23, 59, 0);
            Dateto = Dateto.Date + ts;
            var InvoiceCode = Context.Accountchequesreceivables.Where(c => c.CreatedDate >= DateFrom && c.CreatedDate <= Dateto).OrderByDescending(c => c.ID).Count();
            if (InvoiceCode > 0) model.InvoiceCode = (InvoiceCode + 1).ToString() + "/" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            else model.InvoiceCode = "1/" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year; ;
            
            return model;

        }



        public void Create(AccountchequesreceivableViewModel model)
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
            _DailyJournal.Description = "استلام شيك" + model.InvoiceCode + "خاص" + model.Notes;

            _DailyJournal.Note = "received cheque" + model.InvoiceCode;
            _DailyJournal.Note1 = "استلام شيك " + model.InvoiceCode;
            _DailyJournal.SchoolID = _dbUser.SchoolID;
            _DailyJournal.CompanyID = _dbUser.CompanyID;
            if (model.isCollected == true)
            {
                _DailyJournal.IsPost = true;
            }
            else
            {
                _DailyJournal.IsPost = false;
            }
            _DailyJournal._AccountDailyJournalDetail = DailyJournalDetaillist;
            _AccountDailyJournalHandler.Create(_DailyJournal);

            var _dailyjornalid = Context.AccountDailyJournals.ToList().Last().DailyJournalID;




            Accountchequesreceivable _Accountchequesreceivable = new Accountchequesreceivable()
            {
                BankBranchID = model.BankBranchID,
                docementNumber = model.docementNumber,
                isCollected = model.isCollected,
                operationDate = model.operationDate,
                collectionDate = model.collectionDate,
                Notes = model.Notes,
                costcenter = model.costcenterID,
                CreatedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                fromAccount = model.fromAccount,
                Amount = model.Amount,
                AccountTreeID = model.AccountTreeID,
                InvoiceCode = model.InvoiceCode,
                CreatedbyID = _dbUser.ID, SchoolID=_dbUser.SchoolID,
                //BankCurrencyID = model.BankCurrencyID,
                Employee_ID = model.Employee_ID,
                ChequeDate =model.ChequeDate,
                DailyJournalID=_dailyjornalid

            };
            Context.Accountchequesreceivables.Add(_Accountchequesreceivable);
            Context.SaveChanges();
        }


    
        public List<AccountchequesreceivableViewModel> GetAll()
        {
            return Context.Accountchequesreceivables.Where(x => x.IsDeleted == false &&x.AccountDailyJournal.SchoolID ==_dbUser.SchoolID)
            .Select(x => new AccountchequesreceivableViewModel
            { AccountTreeID=x.AccountTreeID,
                 AccountNameEN = x.AccountTree.AccountNameAR,
                Amount=x.Amount, BankBranchID=x.BankBranchID,
                BankBranchName =x.BankBranch.BranchNameAr,
                collectionDate=x.collectionDate,
                costcenterID = x.costcenter,
                costcenterName =x.AccountCostCenter.CostCenterAR,
                docementNumber=x.docementNumber,
                fromAccount =x.fromAccount.Value,
                AccountNameEN1=x.AccountTree1.AccountNameAR,
                ID =x.ID,
                InvoiceCode =x.InvoiceCode,
                isCollected=x.isCollected.Value, Notes=x.Notes,
                operationDate =x.operationDate,
                Currency_Type = x.BankCurrency.Currency_Type,
                BankCurrencyID = x.BankCurrencyID,
                Employee_ID = x.Employee_ID,
                NameA = x.AdmEmployee.NameA,
                ChequeDate=x.ChequeDate,
                 AccountDailyJournal = x.AccountDailyJournal
            }
            ).ToList();
        }


        public AccountchequesreceivableViewModel GetById(int ID)
        {
            return Context.Accountchequesreceivables.Where(x => x.IsDeleted == false&& x.ID==ID && x.AccountDailyJournal.SchoolID == _dbUser.SchoolID)
          .Select(x => new AccountchequesreceivableViewModel
          {
             costcenterID = x.costcenter,
              AccountTreeID = x.AccountTreeID,
              AccountNameEN = x.AccountTree.AccountNameEN,
              Amount = x.Amount,
              BankBranchID = x.BankBranchID,
              BankBranchName = x.BankBranch.BranchName,
              collectionDate = x.collectionDate,
              costcenterName = x.AccountCostCenter.CostCenter,
              docementNumber = x.docementNumber,
              fromAccount = x.fromAccount.Value,
              AccountNameEN1 = x.AccountTree1.AccountNameEN,
              ID = x.ID,
              InvoiceCode = x.InvoiceCode,
              isCollected = x.isCollected.Value,
              Notes = x.Notes,
              operationDate = x.operationDate,
              Currency_Type = x.BankCurrency.Currency_Type,
              BankCurrencyID = x.BankCurrencyID,
              Employee_ID = x.Employee_ID,
              NameA = x.AdmEmployee.NameA,
              ChequeDate=x.ChequeDate,
              AccountDailyJournal = x.AccountDailyJournal
          }
          ).FirstOrDefault();
        }

        public AccountchequesreceivableViewModel Update(AccountchequesreceivableViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _Accountchequesreceivable = Context.Accountchequesreceivables.Where(x => x.ID == model.ID && x.AccountDailyJournal.SchoolID == _dbUser.SchoolID).FirstOrDefault();

            _Accountchequesreceivable.Notes = model.Notes;
            _Accountchequesreceivable.AccountTreeID = model.AccountTreeID;
            _Accountchequesreceivable.Amount = model.Amount;
            _Accountchequesreceivable.BankBranchID = model.BankBranchID;
            _Accountchequesreceivable.collectionDate = model.collectionDate;
            _Accountchequesreceivable.costcenter = model.costcenterID;
            _Accountchequesreceivable.docementNumber = model.docementNumber;
            _Accountchequesreceivable.InvoiceCode = model.InvoiceCode;
            _Accountchequesreceivable.ModifiedDate = DateTime.Now;
            _Accountchequesreceivable.isCollected = model.isCollected;
            _Accountchequesreceivable.operationDate = model.operationDate;
            _Accountchequesreceivable.ModifiedbyID = _dbUser.ID;
            _Accountchequesreceivable.Employee_ID = model.Employee_ID;
            //_Accountchequesreceivable.BankCurrencyID = model.BankCurrencyID;
            _Accountchequesreceivable.ChequeDate = model.ChequeDate;
            Context.SaveChanges();

            return model;
        }




        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _Accountchequesreceivable = Context.Accountchequesreceivables.Where(x => x.ID == ID && x.AccountDailyJournal.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_Accountchequesreceivable != null)
            {
                _Accountchequesreceivable.IsDeleted = true;
                _Accountchequesreceivable.DeletedDate = DateTime.Now;
                _Accountchequesreceivable.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

    }
}
