using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{ 
   public  class AccountCostCenterHandler : IRepository<AccountCostCenterViewModel>
     {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public void Create(AccountCostCenterViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AccountCostCenter _AccountCostCenters = new AccountCostCenter()
            {
                Comment =model.Comment,
                CostCenter =model.CostCenterEn,
                CreatedDate =DateTime.Now,
                ModifiedDate =DateTime.Now,
                DeletedDate =DateTime.Now,
                CreatedbyID=_dbUser.ID,
                SchoolID =_dbUser.SchoolID,
                CostCenterAR=model.CostCenterAR,
                Code=model.Code,
                CompanyID = _dbUser.CompanyID,

            };
            Context.AccountCostCenters.Add(_AccountCostCenters);
            Context.SaveChanges();

        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountCostCenters = Context.AccountCostCenters.Where(x => x.ID == ID).FirstOrDefault();
            if (_AccountCostCenters != null)
            {
                _AccountCostCenters.IsDeleted = true;
                _AccountCostCenters.DeletedDate = DateTime.Now;
                _AccountCostCenters.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<AccountCostCenterViewModel> GetAll()
        {
            return Context.AccountCostCenters.Where(x => x.IsDeleted == false).
            Select(x => new AccountCostCenterViewModel { ID = x.ID, CostCenterEn = x.CostCenter, Comment = x.Comment , CostCenterAR=x.CostCenterAR, Code=x.Code}).ToList();
        }

        public List<AccountCostCenterViewModel> GetAllByCompany(int companyId,int schoolId)
        {
            return Context.AccountCostCenters.Where(x => x.IsDeleted == false&&x.SchoolID==schoolId&&x.CompanyID==companyId).
            Select(x => new AccountCostCenterViewModel { ID = x.ID, CostCenterEn = x.CostCenter, Comment = x.Comment, CostCenterAR = x.CostCenterAR, Code = x.Code }).ToList();
        }

        public AccountCostCenterViewModel GetById(int ID)
        {
            return Context.AccountCostCenters.Where(x => x.IsDeleted == false &&x.ID==ID).
            Select(x => new AccountCostCenterViewModel { ID = x.ID, CostCenterEn = x.CostCenter, Comment = x.Comment, Code=x.Code, CostCenterAR=x.CostCenterAR }).SingleOrDefault();
        }

        public AccountCostCenterViewModel Update(AccountCostCenterViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountCostCenters = Context.AccountCostCenters.Where(x => x.ID ==model.ID).FirstOrDefault();

            _AccountCostCenters.Comment = model.Comment;
            _AccountCostCenters.CostCenter = model.CostCenterEn;
            _AccountCostCenters.ModifiedDate = DateTime.Now;
            _AccountCostCenters.ModifiedbyID = _dbUser.ID;
            _AccountCostCenters.CostCenterAR = model.CostCenterAR;
            _AccountCostCenters.Code = model.Code;
            Context.SaveChanges();
            return model;
        }


        public AccountCostCenterViewModel Create()
        {
            throw new NotImplementedException();
        }

    }
}
