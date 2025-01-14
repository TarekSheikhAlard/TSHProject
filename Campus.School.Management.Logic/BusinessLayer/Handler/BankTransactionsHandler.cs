using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class BankTransactionsHandler : IRepository<BankTransactionsViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

     
        public void  Create(BankTransactionsViewModel model)
        {
            BankTransaction bankTransaction = new BankTransaction()
            {

                PayeeName = model.Payee,
                BankAccountId = model.BankAccountId,
                PayeeAccountId = model.PayeeAccountId,
                Description = model.Description,
                Tax = model.tax,
                Credit = model.Credit,
                Debit = model.Debit,
                TransactionDate = DateTime.Now,
                ReferenceNo = model.ReferenceNo,
                TransactionType = model.TransactionType,
                AttachedDocName=model.AttachedDocName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                SchoolID = _dbUser.SchoolID

            };
            model.Id = bankTransaction.Id;
            Context.BankTransactions.Add(bankTransaction);
            Context.SaveChanges();
         
        }

        public BankTransactionsViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<BankTransactionsViewModel> GetAll()
        {
            return Context.BankTransactions.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x => new BankTransactionsViewModel
            {
                Id = x.Id,
                TransactionDate = x.TransactionDate,
                Payee = x.PayeeName,
                ReferenceNo = x.ReferenceNo,
                Description = x.Description,
                TransactionName = x.TransactionType1.Name,
                PayeeAccountName = x.AccountTree1.AccountNameEN,
                BankAccountName = x.Bank.BankAccountNameEn,
                AttachedDocName = x.AttachedDocName,
                tax = x.Tax,
                Credit = x.Credit,
                Debit = x.Debit
            }).ToList();
        }
        public List<BankTransactionsViewModel> GetAllByBank(int id)
        {
            return Context.BankTransactions.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID && x.BankAccountId == id).Select(x => new BankTransactionsViewModel
            {
                Id = x.Id,
                TransactionDate = x.TransactionDate,
                Payee = x.PayeeName,
                ReferenceNo = x.ReferenceNo,
                Description = x.Description,
                BankAccountName = x.Bank.BankNameEn,
                TransactionName = x.TransactionType1.Name,
                PayeeAccountName = x.AccountTree1.AccountNameEN,
                AttachedDocName = x.AttachedDocName,
                tax = x.Tax,
                Credit = x.Credit,
                Debit = x.Debit
            }).ToList();
        }
        public List<BankTransactionsViewModel> GetAllByBankPeriod(int id, DateTime from, DateTime to)
        {
            from = from.Date;
            to = to.Date;

            return Context.BankTransactions.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID && x.BankAccountId == id && DbFunctions.TruncateTime(x.TransactionDate) >= from && DbFunctions.TruncateTime(x.TransactionDate) <= to).Select(x => new BankTransactionsViewModel
            {
                Id = x.Id,
                TransactionDate = x.TransactionDate,
                Payee = x.PayeeName,
                ReferenceNo = x.ReferenceNo,
                Description = x.Description,
                BankAccountName = x.Bank.BankNameEn,
                TransactionName = x.TransactionType1.Name,
                PayeeAccountName = x.AccountTree1.AccountNameEN,
                AttachedDocName = x.AttachedDocName,
                tax = x.Tax,
                Credit = x.Credit,
                Debit = x.Debit
            }).ToList();
        }


        public BankTransactionsViewModel GetById(int ID)
        {
            return Context.BankTransactions.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID && x.Id == ID).Select(x => new BankTransactionsViewModel
            {
                Id = x.Id,
                TransactionDate = x.TransactionDate,
                Payee = x.PayeeName,
                ReferenceNo = x.ReferenceNo,
                Description = x.Description,
                BankAccountName = x.Bank.BankNameEn,
                TransactionName = x.TransactionType1.Name,
                PayeeAccountName = x.AccountTree1.AccountNameEN,
                AttachedDocName = x.AttachedDocName,
                tax = x.Tax,
                Credit = x.Credit,
                Debit = x.Debit
            }).SingleOrDefault();
        }

        public BankTransactionsViewModel Update(BankTransactionsViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
