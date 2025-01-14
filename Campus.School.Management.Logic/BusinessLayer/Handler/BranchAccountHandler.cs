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
    public class BranchAccountHandler : IRepository<BranchAccountViewModel>

    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(BranchAccountViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            BranchAccount _BranchAccount = new BranchAccount
            {   Account_Number = model.Account_Number,
                BankID = model.BankID,
                BranchID = model.BranchID,
                BankCurrencyID=model.BankCurrencyID,
                CreatedbyID=_dbUser.ID,
                CreatedDate =DateTime.Now ,
              ModifiedDate =DateTime.Now ,
                DeletedDate =DateTime.Now
              
            };

              Context.BranchAccounts.Add(_BranchAccount);
              Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _BranchAccount = Context.BranchAccounts.Where(c => c.AccountID == ID).FirstOrDefault();
            if (_BranchAccount != null)
            {
                _BranchAccount.DeletedbyID = _dbUser.ID;
                _BranchAccount.IsDeleted =true;
                _BranchAccount.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<BranchAccountViewModel> GetAll()
        {
            return Context.BranchAccounts.Where(x=>x.IsDeleted==false).Select(x => new BranchAccountViewModel
            {   Account_Number = x.Account_Number,
                BankID = x.BankID,
                BankNameEn=x.BankBranch.Bank.BankNameEn,
                BranchID = x.BranchID,
                BranchName=x.BankBranch.BranchName,
                AccountID = x.AccountID,
                BankCurrencyID =x.BankCurrencyID,
                Currency_Type = x.BankCurrency.Currency_Type,

            }).ToList();
        }

        public List<BranchAccountViewModel> GetAllByCompany(int CompanyId)
        {
            return Context.BranchAccounts.Where(x => x.IsDeleted == false&&x.CompanyID==CompanyId).Select(x => new BranchAccountViewModel
            {
                Account_Number = x.Account_Number,
                BankID = x.BankID,
                BankNameEn = x.BankBranch.Bank.BankNameEn,
                BranchID = x.BranchID,
                BranchName = x.BankBranch.BranchName,
                AccountID = x.AccountID,
                BankCurrencyID = x.BankCurrencyID,
                Currency_Type = x.BankCurrency.Currency_Type,
                CompanyID=x.CompanyID

            }).ToList();
        }

        public BranchAccountViewModel GetById(int ID)
        {
            return Context.BranchAccounts.Where(x => x.AccountID == ID && x.IsDeleted==false).Select(x => new BranchAccountViewModel
            {
                Account_Number = x.Account_Number,
                BankID = x.BankID,
                BankNameEn = x.BankBranch.Bank.BankNameEn,
                BranchID = x.BranchID,
                BranchName = x.BankBranch.BranchName,
                AccountID = x.AccountID,
                BankCurrencyID = x.BankCurrencyID,
                Currency_Type = x.BankCurrency.Currency_Type


            }).FirstOrDefault();
        }

        public BranchAccountViewModel Update(BranchAccountViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _BranchAccount = Context.BranchAccounts.Where(c => c.AccountID == model.AccountID).FirstOrDefault();

            _BranchAccount.BankCurrencyID = model.BankCurrencyID;
            _BranchAccount.Account_Number = model.Account_Number;
            _BranchAccount.BankID = model.BankID;
            _BranchAccount.BranchID = model.BranchID;
            _BranchAccount.ModifiedbyID = _dbUser.ID;
            _BranchAccount.ModifiedDate = DateTime.Now;
            
            Context.SaveChanges();
            return model;

        }
     

        public BranchAccountViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
