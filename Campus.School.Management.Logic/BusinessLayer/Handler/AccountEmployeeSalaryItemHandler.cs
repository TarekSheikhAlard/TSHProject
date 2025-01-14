using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Campus.School.Management.Logic.BusinessLayer.Common;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class AccountEmployeeSalaryItemHandler : IRepository<AccountEmployeeSalaryItemViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public List<AccountEmployeeSalaryItemViewModel> GetAll()
        {
            var culture = Utility.getCultureCookie(false).ToLower(); 

            return Context.AccountEmployeeSalaryItems.Include("AdmEmployee").Include("AccountSalaryItem").Where(x => x.IsDeleted == false).AsEnumerable().Select(c => new AccountEmployeeSalaryItemViewModel
            {
                ID = c.ID,
                Employee_ID = c.Employee_ID,
                EmployeeName = c.AdmEmployee.NameA,
                SalaryItemID = c.SalaryItemID,
                SalaryItemTypeID = c.SalaryItemTypeID,
                Amount = c.Amount,
                Notes = c.Notes,
                OperationDate = c.OperationDate,
                AccountSalaryItemName = culture=="ar" ?c.AccountSalaryItem.Salary_itemNameAr: c.AccountSalaryItem.Salary_itemNameEn,
                AccountSalaryItem = c.AccountSalaryItem,
                AccountSalaryItemTypeName = Enum.GetName(typeof(Utility.SalaryItemType), c.SalaryItemTypeID),
                 BounsType=c.BounsType


            }).ToList();

            
        }

        public List<AccountEmployeeSalaryItemViewModel> GetAllByCompany(int companyId,int schoolId)
        {
            var culture = Utility.getCultureCookie(false).ToLower();

            return Context.AccountEmployeeSalaryItems.Include("AdmEmployee").Include("AccountSalaryItem").Where(x => x.IsDeleted == false &&x.CompanyID==companyId&&x.SchoolID==schoolId).AsEnumerable().Select(c => new AccountEmployeeSalaryItemViewModel
            {
                ID = c.ID,
                Employee_ID = c.Employee_ID,
                EmployeeName = c.AdmEmployee.NameA,
                SalaryItemID = c.SalaryItemID,
                SalaryItemTypeID = c.SalaryItemTypeID,
                Amount = c.Amount,
                Notes = c.Notes,
                OperationDate = c.OperationDate,
                AccountSalaryItemName = culture == "ar" ? c.AccountSalaryItem.Salary_itemNameAr : c.AccountSalaryItem.Salary_itemNameEn,
                AccountSalaryItem = c.AccountSalaryItem,
                AccountSalaryItemTypeName = Enum.GetName(typeof(Utility.SalaryItemType), c.SalaryItemTypeID),
                BounsType = c.BounsType


            }).ToList();


        }
        public AccountEmployeeSalaryItemViewModel GetById(int ID)
        {
            var culture = Utility.getCultureCookie(false).ToLower();
            return   Context.AccountEmployeeSalaryItems.Where(c => c.ID == ID && c.IsDeleted == false).AsEnumerable().Select(c => new AccountEmployeeSalaryItemViewModel {

             ID = c.ID,
             Employee_ID = c.Employee_ID,
             EmployeeName = c.AdmEmployee.NameA,
             SalaryItemID =c.SalaryItemID,
             SalaryItemTypeID = c.SalaryItemTypeID,
             Amount = c.Amount,
             Notes = c.Notes,
             OperationDate = c.OperationDate,
             AccountSalaryItemName = culture == "ar" ? c.AccountSalaryItem.Salary_itemNameAr : c.AccountSalaryItem.Salary_itemNameEn,
             AccountSalaryItem = c.AccountSalaryItem,
             AccountSalaryItemTypeName=Enum.GetName(typeof(Utility.SalaryItemType),c.SalaryItemTypeID),
             BounsType=c.BounsType


         }).FirstOrDefault();
        }
        public GenericVModel GetTotalDeduct(DateTime DateFrom, DateTime DateTo)
        {
            
            int _Type = (int)Utility.SalaryItemType.خصومات;
            var list = Context.AccountEmployeeSalaryItems.Where(x => x.SalaryItemTypeID == _Type && x.OperationDate >= DateFrom && x.OperationDate <= DateTo).ToList();

            var _totalDeduct = list.Select(t => t.Amount ).Sum();

            GenericVModel _listDeduct = new GenericVModel()
            {

                ItemName = "Other Income",//TotalDeduct
                Value = _totalDeduct,
                TypeID = 1,
                Type = "Total Income",
            };
            return _listDeduct;
        }
        public void Create(AccountEmployeeSalaryItemViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccountEmployeeSalaryItem _AccountEmployeeSalaryItem = new AccountEmployeeSalaryItem()
            {
                Employee_ID = model.Employee_ID,
                SalaryItemID = model.SalaryItemID,
                SalaryItemTypeID = model.SalaryItemTypeID,
                Amount = model.Amount,
                Notes = model.Notes,
                OperationDate = model.OperationDate.Value,
                CreatedbyID=_dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                BounsType=model.BounsType,
                CompanyID=model.CompanyID,
                SchoolID=model.SchoolID
            };
            Context.AccountEmployeeSalaryItems.Add(_AccountEmployeeSalaryItem);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            var _AccountEmployeeSalaryItem = Context.AccountEmployeeSalaryItems.Where(c => c.ID == ID).FirstOrDefault();
            if (_AccountEmployeeSalaryItem != null)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
                _AccountEmployeeSalaryItem.DeletedbyID = _dbUser.ID;
                _AccountEmployeeSalaryItem.IsDeleted = true;
                _AccountEmployeeSalaryItem.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public AccountEmployeeSalaryItemViewModel Update(AccountEmployeeSalaryItemViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountEmployeeSalaryItem = Context.AccountEmployeeSalaryItems.Where(c => c.ID == model.ID).FirstOrDefault();
            _AccountEmployeeSalaryItem.Employee_ID = model.Employee_ID;
          _AccountEmployeeSalaryItem.SalaryItemID = model.SalaryItemID;
            _AccountEmployeeSalaryItem.SalaryItemTypeID = model.SalaryItemTypeID;
            _AccountEmployeeSalaryItem.Amount = model.Amount;
            _AccountEmployeeSalaryItem.Notes = model.Notes;
            _AccountEmployeeSalaryItem.OperationDate = model.OperationDate.Value;
            _AccountEmployeeSalaryItem.ModifiedDate = DateTime.Now;
            _AccountEmployeeSalaryItem.ModifiedbyID = _dbUser.ID;
            _AccountEmployeeSalaryItem.BounsType = model.BounsType;
            Context.SaveChanges();
            return model;
        }

        public AccountEmployeeSalaryItemViewModel Create()
        {
            AccountEmployeeSalaryItemViewModel model = new AccountEmployeeSalaryItemViewModel ();
           
            return model;
        }

       
    }
}
