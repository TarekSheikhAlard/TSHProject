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
    public class StoresHandler : IRepository<StoreViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(StoreViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            Store store = new Store {
                StoreId=model.storeId,
                StoreNumber = model.StoreNumber,
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                PricingType = model.PricingType,
                FundAccount = model.FundAccount,
                StoreAccount = model.StoreAccount,
                CostAccount = model.CostAccount,
                SalesAccount = model.SalesAccount,
                CostCenter1 = model.CostCenter1,
                CostCenter2 = model.CostCenter2,
                BankName = model.BankName,
                BankAccountNumber = model.BankAccountNumber,
                Size = model.Size,
                Manager = model.Manager,
                Address = model.Address,
                City = model.City,
                tel = model.tel,
                Mobile = model.Mobile,
                Email = model.Email,
                Location = model.Location,
                OperationDate = model.OperationDate
            };
            
            Context.SaveChanges();

        }

        public StoreViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
           
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _store = Context.Stores.Where(x => x.StoreId == ID).FirstOrDefault();
            if (_store != null)
            {
                _store.DeletedbyID = _dbUser.ID;
                _store.IsDeleted = true;
                _store.DeletedDate = DateTime.Now;
                Context.SaveChanges(); 

                return true;
            }
            else return false;
        }

        public List<StoreViewModel> GetAll()
        {
            var list = Context.Stores.Where(x => x.IsDeleted == false).Select(x=>new StoreViewModel{
                storeId = x.StoreId,
                StoreNumber = x.StoreNumber,
                NameEn = x.NameEn,
                NameAr = x.NameAr,
                PricingType = x.PricingType,
                FundAccount = x.FundAccount,
                StoreAccount = x.StoreAccount,
                CostAccount = x.CostAccount,
                SalesAccount = x.SalesAccount,
                CostCenter1 = x.CostCenter1,
                CostCenter2 = x.CostCenter2,
                BankName = x.BankName,
                BankAccountNumber = x.BankAccountNumber,
                Size = x.Size,
                Manager = x.Manager,
                Address = x.Address,
                City = x.City,
                tel = x.tel,
                Mobile = x.Mobile,
                Email = x.Email,
                Location = x.Location

            }).ToList();

            return list;

     
        }
        public List<StoreViewModel> GetAllByCompany(int companyId ,int schoolId)
        {
            var list = Context.Stores.Where(x => x.IsDeleted == false && x.InventoryTree.SchoolID == schoolId && x.InventoryTree.AdmSchool.CompanyID ==companyId).Select(x => new StoreViewModel
            {
                storeId=x.StoreId,
                StoreNumber = x.StoreNumber,
                NameEn = x.NameEn,
                NameAr = x.NameAr,
                PricingType = x.PricingType,
                FundAccount = x.FundAccount,
                StoreAccount = x.StoreAccount,
                CostAccount = x.CostAccount,
                SalesAccount = x.SalesAccount,
                CostCenter1 = x.CostCenter1,
                CostCenter2 = x.CostCenter2,
                BankName = x.BankName,
                BankAccountNumber = x.BankAccountNumber,
                Size = x.Size,
                Manager = x.Manager,
                Address = x.Address,
                City = x.City,
                tel = x.tel,
                Mobile = x.Mobile,
                Email = x.Email,
                Location = x.Location

            }).ToList();

            return list;


        }

        public StoreViewModel GetById(int ID)
        {
            var store = Context.Stores.Where(x => x.IsDeleted == false && x.StoreId==ID).Select(x => new StoreViewModel
            {
                storeId = x.StoreId,
                StoreNumber = x.StoreNumber,
                NameEn = x.NameEn,
                NameAr = x.NameAr,
                PricingType = x.PricingType,
                FundAccount = x.FundAccount,
                StoreAccount = x.StoreAccount,
                CostAccount = x.CostAccount,
                SalesAccount = x.SalesAccount,
                CostCenter1 = x.CostCenter1,
                CostCenter2 = x.CostCenter2,
                BankName = x.BankName,
                BankAccountNumber = x.BankAccountNumber,
                Size = x.Size,
                Manager = x.Manager,
                Address = x.Address,
                City = x.City,
                tel = x.tel,
                Mobile = x.Mobile,
                Email = x.Email,
                Location = x.Location

            }).FirstOrDefault();
            return store;
        }

        public StoreViewModel Update(StoreViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _store = Context.Stores.Where(x => x.StoreId == model.storeId).FirstOrDefault();
            if (_store != null)
            {

                _store.StoreNumber = model.StoreNumber;
                _store. NameEn = model.NameEn;
                _store.NameAr = model.NameAr;
                _store.PricingType = model.PricingType;
                _store.FundAccount = model.FundAccount;
                _store.StoreAccount = model.StoreAccount;
                _store.CostAccount = model.CostAccount;
                _store.SalesAccount = model.SalesAccount;
                _store.CostCenter1 = model.CostCenter1;
                _store.CostCenter2 = model.CostCenter2;
                _store.BankName = model.BankName;
                _store.BankAccountNumber = model.BankAccountNumber;
                _store.Size = model.Size;
                _store.Manager = model.Manager;
                _store.Address = model.Address;
                _store.City = model.City;
                _store.tel = model.tel;
                _store.Mobile = model.Mobile;
                _store.Email = model.Email;
                _store.Location = model.Location;
               
                Context.SaveChanges();
                
            }

            return model;

        }
    }
}
