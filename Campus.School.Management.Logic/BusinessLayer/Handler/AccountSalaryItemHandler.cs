using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using Campus.School.Management.Logic.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Campus.School.Management.Logic.BusinessLayer.Common;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class AccountSalaryItemHandler : IRepository<AccountSalaryItemViewModel>
    {

        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public AccountSalaryItemViewModel Create()
        {
            throw new NotImplementedException();
        }

        public void Create(AccountSalaryItemViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccountSalaryItem _AccountSalaryItems = new AccountSalaryItem()
            { 
                Salary_itemNameEn =model.Salary_itemNameEn,
                Salary_itemNameAr = model.Salary_itemNameAr,
                Salary_itemTypeID =model.Salary_itemTypeID, 
                CreatedbyID  = _dbUser.ID,
                CreatedDate  = DateTime.Now,
                DeletedDate  = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CompanyID=model.CompanyID,
                SchoolID=model.SchoolID
            };

            Context.AccountSalaryItems.Add(_AccountSalaryItems);
            Context.SaveChanges();
        }



        public List<AccountSalaryItemViewModel> GetAll()
        {
            

            return Context.AccountSalaryItems.Where(x => x.IsDeleted == false).AsEnumerable()
           .Select(x => new AccountSalaryItemViewModel
           {
               ID = x.ID,
               Salary_itemNameEn = x.Salary_itemNameEn,
               Salary_itemNameAr = x.Salary_itemNameAr,
               Salary_itemTypeID = x.Salary_itemTypeID,
               Salary_itemTypeName = Enum.GetName(typeof(Utility.SalaryItemType), x.Salary_itemTypeID.Value)
           }).OrderBy(x => x.Salary_itemTypeID).ToList();

        }

        public List<AccountSalaryItemViewModel> GetAllByCompany(int companyId,int schoolId)
        {
            return Context.AccountSalaryItems.Where(x => x.IsDeleted == false &&x.CompanyID== companyId &&x.SchoolID==schoolId)
                                             .AsEnumerable()
           .Select(x => new AccountSalaryItemViewModel
           {
               ID = x.ID,
               Salary_itemNameEn = x.Salary_itemNameEn,
               Salary_itemNameAr = x.Salary_itemNameAr,
               Salary_itemTypeID = x.Salary_itemTypeID,
               Salary_itemTypeName = Enum.GetName(typeof(Utility.SalaryItemType), x.Salary_itemTypeID.Value)
           }).OrderBy(x => x.Salary_itemTypeID).ToList();

        }

        public AccountSalaryItemViewModel GetById(int ID)
        {
            return Context.AccountSalaryItems.Where(x => x.IsDeleted == false && x.ID==ID).AsEnumerable()
          .Select(x => new AccountSalaryItemViewModel
          {
              ID = x.ID,
              Salary_itemNameEn = x.Salary_itemNameEn,
              Salary_itemNameAr = x.Salary_itemNameAr,
              Salary_itemTypeID = x.Salary_itemTypeID,
              Salary_itemTypeName = Enum.GetName(typeof(Utility.SalaryItemType), x.Salary_itemTypeID)
          }).FirstOrDefault();
        }
        
        public bool Delete(int ID)
        {
            var _AccountSalaryItems = Context.AccountSalaryItems.Where(x => x.ID == ID && x.IsDeleted == false).FirstOrDefault();
            if (_AccountSalaryItems != null)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
                _AccountSalaryItems.DeletedbyID = _dbUser.ID;
                _AccountSalaryItems.IsDeleted = true;
                _AccountSalaryItems.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public AccountSalaryItemViewModel Update(AccountSalaryItemViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccountSalaryItems = Context.AccountSalaryItems.Where(x => x.ID == model.ID && x.IsDeleted == false).FirstOrDefault();

            _AccountSalaryItems.Salary_itemNameEn = model.Salary_itemNameEn;
             _AccountSalaryItems.Salary_itemNameAr = model.Salary_itemNameAr;
            _AccountSalaryItems.Salary_itemTypeID = model.Salary_itemTypeID;
            _AccountSalaryItems.ModifiedbyID = _dbUser.ID;
            _AccountSalaryItems.ModifiedDate = DateTime.Now;

            Context.SaveChanges();
            return model;
        }
    }
}
