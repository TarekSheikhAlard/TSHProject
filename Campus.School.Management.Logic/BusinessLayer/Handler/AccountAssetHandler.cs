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
    public class AccountAssetHandler : IRepository<AccountAssetViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public List<AccountAssetViewModel> GetAll()
        {
             
            return Context.AccountAssets.Where(x => x.IsDeleted == false && x.SchoolID ==_dbUser.SchoolID )
           .Select(x => new AccountAssetViewModel {
            AccountTreeID = x.AccountTreeID,
            Amount = x.Amount,
            Code = x.Code,
            depreciationRate = x.depreciationRate,
            depreciationType = x.depreciationType,
            depreciationYears = x.depreciationYears,
            ID = x.ID,
            Name = x.Name,
            NameAr = x.NameAr,
            Notes = x.Notes,
            PurchaseDate = x.PurchaseDate,
            AccountTree =x.AccountTree.AccountNameAR

           }).ToList();

        }


        public List<AccountAssetViewModel> GetAllByCompany(int companyId,int schoolId)
        {
            return Context.AccountAssets.Where(x => x.IsDeleted == false&&x.CompanyID== companyId&&x.SchoolID==schoolId)
           .Select(x => new AccountAssetViewModel
           {
               AccountTreeID = x.AccountTreeID,
               Amount = x.Amount,
               Code = x.Code,
               depreciationRate = x.depreciationRate,
               depreciationType = x.depreciationType,
               depreciationYears = x.depreciationYears,
               ID = x.ID,
               Name = x.Name,
               NameAr = x.NameAr,
               Notes = x.Notes,
               PurchaseDate = x.PurchaseDate,
               AccountTree = x.AccountTree.AccountNameAR
           }).ToList();

        }



        public AccountAssetViewModel GetById(int ID)
        {
            var lang = Utility.getCultureCookie(false).ToLower();
            return Context.AccountAssets.Where(x => x.IsDeleted == false && x.ID == ID && x.SchoolID == _dbUser.SchoolID)
            .Select(x => new AccountAssetViewModel
            {
                AccountTreeID = x.AccountTreeID,
                Amount = x.Amount,
                Code = x.Code,
                depreciationRate = x.depreciationRate,
                depreciationType = x.depreciationType,
                depreciationYears = x.depreciationYears,
                ID = x.ID,
                Name = x.Name,
                NameAr = x.NameAr,
                Notes = x.Notes,
                PurchaseDate = x.PurchaseDate,
                AccountTree = x.AccountTree.AccountNameAR,
                AssetTreeID = x.AssetTreeID,
                AssetNameEN = lang == "ar" ? x.AssetTree.AssetNameAR : x.AssetTree.AssetNameEN,
                AccountExpenseID = x.AccountTreeIDExpense,
                AccountExpenseName = lang == "ar" ? x.AccountTree1.AccountNameAR : x.AccountTree1.AccountNameEN,
                AccountAccumulatedID =x.AccountTreeIDExpense,
                AccountAccumulatedName = lang == "ar" ? x.AccountTree1.AccountNameAR : x.AccountTree1.AccountNameEN
               // DepartmentID =x.DeptID,
                //DepartmentName= lang == "ar" ? x.AdmDepartment.DepartmentAr : x.AdmDepartment.DepartmentEn

            }).FirstOrDefault();
        }

        public void Create(AccountAssetViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccountAsset _AccountAsset = new AccountAsset()
            {
                AccountTreeID = model.AccountTreeID,
                Amount = model.Amount,
                Code = model.Code,
                Name = model.Name,
                NameAr = model.NameAr,
                Notes = model.Notes,
                depreciationRate = model.depreciationRate,
                depreciationType = model.depreciationType,
                depreciationYears = model.depreciationYears,
                PurchaseDate = model.PurchaseDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID = _dbUser.CreatedbyID,
                SchoolID = model.SchoolID,
                CompanyID = model.CompanyID,
                AssetTreeID = model.AssetTreeID,
                AccountTreeIDExpense = model.AccountExpenseID,
                AccountTreeIDAccumulated = model.AccountAccumulatedID
               // DeptID=model.DepartmentID
            };

            Context.AccountAssets.Add(_AccountAsset);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccountAsset = Context.AccountAssets.Where(x => x.ID == ID && x.IsDeleted== false && x.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_AccountAsset != null)
            {
                _AccountAsset.IsDeleted = true;
                _AccountAsset.DeletedDate = DateTime.Now;
                _AccountAsset.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }



        public AccountAssetViewModel Update(AccountAssetViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccountAsset = Context.AccountAssets.Where(x => x.ID == model.ID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).FirstOrDefault();

            _AccountAsset.AccountTreeID = model.AccountTreeID;
            _AccountAsset.Amount = model.Amount;
            _AccountAsset.Code = model.Code;
            _AccountAsset.depreciationRate = model.depreciationRate;
            _AccountAsset.depreciationType = model.depreciationType;
            _AccountAsset.depreciationYears = model.depreciationYears;
            _AccountAsset.Name = model.Name;
            _AccountAsset.NameAr = model.NameAr;
            _AccountAsset.Notes = model.Notes;
            _AccountAsset.PurchaseDate = model.PurchaseDate;
            _AccountAsset.ModifiedDate = DateTime.Now;
            _AccountAsset.ModifiedbyID = _dbUser.ID;
            _AccountAsset.AssetTreeID = model.AssetTreeID;
            _AccountAsset.AccountTreeIDExpense = model.AccountExpenseID;
            _AccountAsset.AccountTreeIDAccumulated = model.AccountAccumulatedID;
           // _AccountAsset.DeptID = model.DepartmentID;

            Context.SaveChanges();
            return model;
        }




        public AccountAssetViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
