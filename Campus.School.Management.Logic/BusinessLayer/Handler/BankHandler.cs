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
    public class BankHandler : IRepository<BankViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();
        private AccountTreeHandler AccountTree = new AccountTreeHandler();
        public List<BankViewModel> GetAll()
        {
            return context.Banks.Where(x=>x.IsDeleted==false) .Select(x => new BankViewModel {
                BankID = x.BankID,
                BankNameAr = x.BankNameAr,
                BankNameEn = x.BankNameEn,
                BankAccountNameAr = x.BankAccountNameAr,
                BankAccountNameEn = x.BankAccountNameEn,
                OpeningBalance = x.AccountTree.OpenBalance,
                BranchName = x.BranchName,
                Description = x.Description,
                AccountNumber = x.AccountNumber
            }).ToList();
        }

        public List<BankViewModel> GetAllByCompanyID(int CompanyId)
        {
            return context.Banks
                            .Where(x => x.IsDeleted == false && x.CompanyID== CompanyId)
                            .Select(x => new BankViewModel
                            { BankID = x.BankID,
                              BankNameAr = x.BankNameAr,
                              BankNameEn = x.BankNameEn,
                              BankAccountNameAr=x.BankAccountNameAr,
                              BankAccountNameEn=x.BankAccountNameEn,
                                OpeningBalance = x.AccountTree.OpenBalance,
                                BranchName =x.BranchName,
                              Description=x.Description,
                                AccountNumber = x.AccountNumber,
                                AccountTreeID = (int)x.AccountTreeID
                            }
                            ).OrderByDescending(x=>x.BankID).ToList();
        }
        public BankViewModel GetById(int ID)
        {
            return context.Banks.Where(x => x.BankID == ID&& x.IsDeleted==false)
           .Select (x => new BankViewModel {
               BankID = x.BankID,
               BankNameAr = x.BankNameAr,
               BankNameEn = x.BankNameEn,
               BankAccountNameAr = x.BankAccountNameAr,
               BankAccountNameEn = x.BankAccountNameEn,
               OpeningBalance = x.AccountTree.OpenBalance,
               BranchName = x.BranchName,
               Description = x.Description,
               AccountNumber=x.AccountNumber,
               AccountTreeID=(int)x.AccountTreeID
           })
           .FirstOrDefault();
        }

        public void Create(BankViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            Bank _bank = new Bank
            {
                BankNameAr =model.BankNameAr,
                BankNameEn=model.BankNameEn,
                BankID = model.BankID,       
                BankAccountNameAr = model.BankAccountNameAr,
                BankAccountNameEn = model.BankAccountNameEn,
                OpeningBalance = model.OpeningBalance,
                BranchName = model.BranchName,
                Description = model.Description,
                AccountNumber=model.AccountNumber,
                ModifiedDate =DateTime.Now,
                CreatedDate =DateTime.Now,
                CreatedbyID = _dbUser.ID,
                DeletedDate =DateTime.Now,
                CompanyID=model.CompanyID,
                SchoolID=_dbUser.SchoolID
               
            };
           

            AccountTreeViewModel accountTree = new AccountTreeViewModel
            {
                AccountTreeParentID = 14,
                AccountNameEN = string.Format("{0} ({1})",model.BankNameEn ,model.BranchName),
                AccountNameAR = string.Format("{0} ({1})", model.BankNameAr, model.BranchName),
                OpenBalance=model.OpeningBalance,
                Description=model.Description
            };

            int AccountTreeID = 0;
            AccountTree.Create(accountTree,out AccountTreeID);

            _bank.AccountTreeID = AccountTreeID;

            context.Banks.Add(_bank);

            context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Bank = context.Banks.Where(x => x.BankID == ID&&x.IsDeleted==false).FirstOrDefault();
            if (_Bank != null)
            {
                if (context.BankTransactions.Where(c => c.IsDeleted == false && c.SchoolID == _dbUser.SchoolID && c.BankAccountId == ID).Count() == 0)
                {
                    _Bank.DeletedbyID = _dbUser.ID;
                    _Bank.IsDeleted = true;
                    _Bank.DeletedDate = DateTime.Now;
                    context.SaveChanges();
                    return true;

                }
                  
            }

             return false;
        }

        public BankViewModel Update(BankViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Bank = context.Banks.Where(x => x.BankID == model.BankID).FirstOrDefault();
            _Bank.BankNameAr = model.BankNameAr;
            _Bank.BankNameEn = model.BankNameEn;
            _Bank. BankAccountNameAr = model.BankAccountNameAr;
            _Bank.BankAccountNameEn = model.BankAccountNameEn;
            _Bank.OpeningBalance = model.OpeningBalance;
            _Bank.BranchName = model.BranchName;
            _Bank.Description = model.Description;
            _Bank.AccountNumber = model.AccountNumber;
            _Bank.ModifiedDate = DateTime.Now;
            _Bank.ModifiedbyID = _dbUser.ID;

            AccountTreeViewModel accountTree = new AccountTreeViewModel
            {
                AccountTreeID = model.AccountTreeID,
                AccountNameEN = string.Format("{0} ({1})", model.BankNameEn, model.BranchName),
                AccountNameAR = string.Format("{0} ({1})", model.BankNameAr, model.BranchName),
                OpenBalance = model.OpeningBalance,
                Description = model.Description
            };

            AccountTree.Update(accountTree);

            context.SaveChanges();
            return model;
        }

        public BankViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
