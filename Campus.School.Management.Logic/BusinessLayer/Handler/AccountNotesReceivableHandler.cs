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
   public  class AccountNotesReceivableHandler : IRepository<AccountNotesReceivableViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

      

        public List<AccountNotesReceivableViewModel> GetAll()
        {
            return Context.AccountNotesReceivables.Where(x => x.IsDeleted == false).
                     Select(x => new AccountNotesReceivableViewModel
                     {
                         ID = x.ID,
                         AccountTreeCode = x.AccountTreeCode,
                         Amount = x.Amount,
                         IsRelay = x.IsRelay,
                         docementNumber = x.docementNumber,
                         InvoiceCode = x.InvoiceCode,
                         description = x.description,
                         InvoiceDate = x.InvoiceDate,
                         TreasuryID = x.TreasuryID,
                         TreasuryName = x.AccountTreasury.TreasuryName,
                         AccountTreeID = x.AccountTreeID,
                         AccountNameEN = x.AccountTree.AccountNameEN,
                         costcenterID = x.costcenterID,
                         costcenterName = x.AccountCostCenter.CostCenter
                     }).ToList();
        }

        public AccountNotesReceivableViewModel GetById(int ID)
        {
            return Context.AccountNotesReceivables.Where(x => x.IsDeleted == false&&x.ID==ID ).
                      Select(x => new AccountNotesReceivableViewModel
                      {
                          ID = x.ID,
                          AccountTreeCode = x.AccountTreeCode,
                          Amount = x.Amount,
                          IsRelay = x.IsRelay,
                          docementNumber = x.docementNumber,
                          InvoiceCode = x.InvoiceCode,
                          description = x.description,
                          InvoiceDate = x.InvoiceDate,
                          TreasuryID = x.TreasuryID,
                          TreasuryName = x.AccountTreasury.TreasuryName,
                          AccountTreeID = x.AccountTreeID,
                          AccountNameEN = x.AccountTree.AccountNameEN,
                          costcenterID = x.costcenterID,
                          costcenterName = x.AccountCostCenter.CostCenter
                      }).FirstOrDefault();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountNotesReceivables = Context.AccountNotesReceivables.Where(x => x.ID == ID&& x.IsDeleted==false).FirstOrDefault();
            if (_AccountNotesReceivables != null)
            {
                _AccountNotesReceivables.IsDeleted = true;
                _AccountNotesReceivables.DeletedDate = DateTime.Now;
                _AccountNotesReceivables.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

     



        public AccountNotesReceivableViewModel Create()
        {
            int _InvoiceCode;
            var Entity = Context.AccountNotesReceivables.ToList().LastOrDefault();

            if (Entity == null)
            {
                _InvoiceCode = 1;
            }
            else
            {
                _InvoiceCode = Entity.InvoiceCode.Value + 1;

            }

            AccountNotesReceivableViewModel model = new AccountNotesReceivableViewModel() 
            {
                InvoiceCode = _InvoiceCode
            };
          

            return model;
        }

        public void Create(AccountNotesReceivableViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AccountNotesReceivable _AccountNotesReceivable = new AccountNotesReceivable()
            { Amount=model.Amount, InvoiceCode=model.InvoiceCode, AccountTreeID=model.AccountTreeID,
              TreasuryID =model.TreasuryID, costcenterID=model.costcenterID, InvoiceDate=model.InvoiceDate,
              description=model.description, AccountTreeCode=model.AccountTreeCode,docementNumber=model.docementNumber,
              ModifiedDate=DateTime.Now, DeletedDate=DateTime.Now,CreatedDate=DateTime.Now, CreatedbyID=_dbUser.ID, SchoolID=_dbUser.SchoolID

            };
            Context.AccountNotesReceivables.Add(_AccountNotesReceivable);
            Context.SaveChanges();


        }

        public AccountNotesReceivableViewModel Update(AccountNotesReceivableViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountNotesReceivables = Context.AccountNotesReceivables.Where(x => x.ID ==model.ID && x.IsDeleted == false).FirstOrDefault();

            _AccountNotesReceivables.Amount = model.Amount;
            _AccountNotesReceivables.TreasuryID = model.TreasuryID;
            _AccountNotesReceivables.AccountTreeID = model.AccountTreeID;
            _AccountNotesReceivables.costcenterID = model.costcenterID;
            _AccountNotesReceivables.docementNumber = model.docementNumber;
            _AccountNotesReceivables.description = model.description;
            _AccountNotesReceivables.AccountTreeCode = model.AccountTreeCode;
            _AccountNotesReceivables.IsRelay = model.IsRelay;
            _AccountNotesReceivables.InvoiceDate = model.InvoiceDate;
            _AccountNotesReceivables.InvoiceCode = model.InvoiceCode;
            _AccountNotesReceivables.ModifiedDate = DateTime.Now;
            _AccountNotesReceivables.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;
        }



    }
}
