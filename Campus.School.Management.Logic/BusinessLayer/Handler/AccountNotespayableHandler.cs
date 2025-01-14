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
    public class AccountNotespayableHandler : IRepository<AccountNotespayableViewModel>
    {

        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public AccountNotespayableViewModel Create()
        {
            int _InvoiceCode;
            var Entity = Context.AccountNotespayables.ToList().LastOrDefault();

            if (Entity == null)
            {
                _InvoiceCode = 1;
            }
            else
            {
                _InvoiceCode = Entity.InvoiceCode.Value + 1;

            }

            AccountNotespayableViewModel model = new AccountNotespayableViewModel()
            {
                InvoiceCode = _InvoiceCode
            };


            return model;
        }

        public void Create(AccountNotespayableViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AccountNotespayable _AccountNotespayable = new AccountNotespayable()
            {
                Amount = model.Amount,
                InvoiceCode = model.InvoiceCode,
                AccountTreeID = model.AccountTreeID,
                TreasuryID = model.TreasuryID,
                costcenterID = model.costcenterID,
                InvoiceDate = model.InvoiceDate,
                description = model.description,
                AccountTreeCode = model.AccountTreeCode,
                docementNumber = model.docementNumber,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID, SchoolID=_dbUser.SchoolID
                
            };
            Context.AccountNotespayables.Add(_AccountNotespayable);
            Context.SaveChanges();
        }

        

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountNotespayable = Context.AccountNotespayables.Where(x => x.ID == ID && x.IsDeleted == false).FirstOrDefault();
            if (_AccountNotespayable != null)
            {
                _AccountNotespayable.IsDeleted = true;
                _AccountNotespayable.DeletedDate = DateTime.Now;
                _AccountNotespayable.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<AccountNotespayableViewModel> GetAll()
        {
            return Context.AccountNotespayables.Where(x => x.IsDeleted == false).
                     Select(x => new AccountNotespayableViewModel
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


        public AccountNotespayableViewModel GetById(int ID)
        {
            return Context.AccountNotespayables.Where(x => x.IsDeleted == false && x.ID == ID).
                                 Select(x => new AccountNotespayableViewModel
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

        public AccountNotespayableViewModel Update(AccountNotespayableViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountNotespayable = Context.AccountNotespayables.Where(x => x.ID == model.ID && x.IsDeleted == false).FirstOrDefault();

            _AccountNotespayable.Amount = model.Amount;
            _AccountNotespayable.TreasuryID = model.TreasuryID;
            _AccountNotespayable.AccountTreeID = model.AccountTreeID;
            _AccountNotespayable.costcenterID = model.costcenterID;
            _AccountNotespayable.docementNumber = model.docementNumber;
            _AccountNotespayable.description = model.description;
            _AccountNotespayable.AccountTreeCode = model.AccountTreeCode;
            _AccountNotespayable.IsRelay = model.IsRelay;
            _AccountNotespayable.InvoiceDate = model.InvoiceDate;
            _AccountNotespayable.InvoiceCode = model.InvoiceCode;
            _AccountNotespayable.ModifiedDate = DateTime.Now;
            _AccountNotespayable.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;
        }
    }
}
