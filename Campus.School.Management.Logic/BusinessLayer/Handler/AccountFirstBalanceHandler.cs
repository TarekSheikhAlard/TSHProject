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
  public  class AccountFirstBalanceHandler : IRepository<AccountFirstBalanceViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public AccountFirstBalanceViewModel Create()
        {
         

            AccountFirstBalanceViewModel model = new AccountFirstBalanceViewModel()
            {
             PhysicalYearID =  Context.PhysicalYears.Where(x => x.IsCurrent == true).FirstOrDefault().PhysicalYearID

            };

            return model;
        }

        public void Create(AccountFirstBalanceViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AccountFirstBalance _AccountFirstBalance = new DataBaseLayer.AccountFirstBalance()
            {
                 AccountTreeID=model.AccountTreeID,
                 CostCenterID=model.CostCenterID,
                 PhysicalYearID=model.PhysicalYearID,
                 ActionType=model.ActionType,
                 Amount=model.Amount,
                 OperationDate =model.OperationDate,
                 ModifiedDate=DateTime.Now,
                 CreatedDate=DateTime.Now,
                 DeletedDate=DateTime.Now ,
                 CreatedbyID =_dbUser.ID,
                 SchoolID =model.SchoolID,
                 CompanyID=model.CompanyID              

            };
            Context.AccountFirstBalances.Add(_AccountFirstBalance);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountFirstBalance = Context.AccountFirstBalances.Where(x => x.ID == ID && x.IsDeleted == false).FirstOrDefault();
            if (_AccountFirstBalance != null)
            {
                _AccountFirstBalance.IsDeleted = true;
                _AccountFirstBalance.DeletedDate = DateTime.Now;
                _AccountFirstBalance.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }

        public List<AccountFirstBalanceViewModel> GetAll()
        {
            return Context.AccountFirstBalances.Where(x => x.IsDeleted == false)
            .Select(x => new AccountFirstBalanceViewModel
            { AccountTreeID =x.AccountTreeID,
                AccountNameEN = x.AccountTree.AccountNameEN,
             ActionType =x.ActionType, Amount=x.Amount, CostCenterID=x.CostCenterID,
             costcenterName =x.AccountCostCenter.CostCenter, OperationDate =x.OperationDate,
             ID =x.ID, PhysicalYearID=x.PhysicalYearID

            }).ToList();
        }

        public List<AccountFirstBalanceViewModel> GetAllByCompany(int companyId,int schoolId)
        {
            return Context.AccountFirstBalances.Where(x => x.IsDeleted == false&&x.CompanyID==companyId&&x.SchoolID==schoolId)
            .Select(x => new AccountFirstBalanceViewModel
            {
                AccountTreeID = x.AccountTreeID,
                AccountNameEN = x.AccountTree.AccountNameEN,
                ActionType = x.ActionType,
                Amount = x.Amount,
                CostCenterID = x.CostCenterID,
                costcenterName = x.AccountCostCenter.CostCenter,
                OperationDate = x.OperationDate,
                ID = x.ID,
                PhysicalYearID = x.PhysicalYearID

            }).ToList();
        }


        public AccountFirstBalanceViewModel GetById(int ID)
        {
            return Context.AccountFirstBalances.Where(x => x.IsDeleted == false&&x.ID==ID )
             .Select(x => new AccountFirstBalanceViewModel
             {
                 AccountTreeID = x.AccountTreeID,
                 AccountNameEN = x.AccountTree.AccountNameEN,
                 ActionType = x.ActionType,
                 Amount = x.Amount,
                 CostCenterID = x.CostCenterID,
                 costcenterName = x.AccountCostCenter.CostCenter,
                 OperationDate = x.OperationDate,
                 ID = x.ID,
                 PhysicalYearID = x.PhysicalYearID

             }).FirstOrDefault();
        }

        public AccountFirstBalanceViewModel Update(AccountFirstBalanceViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountFirstBalance = Context.AccountFirstBalances.Where(x => x.ID ==model.ID && x.IsDeleted == false).FirstOrDefault();
            _AccountFirstBalance.AccountTreeID = model.AccountTreeID;
            _AccountFirstBalance.CostCenterID = model.CostCenterID;
            _AccountFirstBalance.ActionType = model.ActionType;
            _AccountFirstBalance.Amount = model.Amount;
            _AccountFirstBalance.OperationDate = model.OperationDate;
            _AccountFirstBalance.ModifiedDate = DateTime.Now;
            _AccountFirstBalance.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();

            return model;
        }
    }
}
