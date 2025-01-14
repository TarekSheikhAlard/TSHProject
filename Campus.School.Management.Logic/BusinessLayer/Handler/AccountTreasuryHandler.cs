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
     public class AccountTreasuryHandler : IRepository<AccountTreasuryViewModel>

    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

     

        public void Create(AccountTreasuryViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AccountTreasury _AccountTreasury = new AccountTreasury()
            { Comment = model.Comment,
                TreasuryName = model.TreasuryNameEn,
                CreatedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                SchoolID=model.SchoolID,
                CompanyID = model.CompanyID,
                AccountTreeID = model.AccountTreeID,
                Employee_ID =model.Employee_ID, 
               TreasuryNameAR=model.TreasuryNameAR
               


            };
            Context.AccountTreasuries.Add(_AccountTreasury);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccountTreasury = Context.AccountTreasuries.Where(x => x.ID == ID).FirstOrDefault();
            if (_AccountTreasury != null)
            {
                _AccountTreasury.IsDeleted = true;
                _AccountTreasury.DeletedDate = DateTime.Now;
                _AccountTreasury.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<AccountTreasuryViewModel> GetAll()
        {
            return Context.AccountTreasuries.Where(x => x.IsDeleted == false).
             Select(x => new AccountTreasuryViewModel { ID = x.ID, TreasuryNameEn=x.TreasuryName,Comment=x.Comment, Employee_ID=x.Employee_ID, NameA=x.AdmEmployee.NameA, AccountTreeID=x.AccountTreeID, TreasuryNameAR=x.TreasuryNameAR, AccountNameAR=x.AccountTree.AccountNameAR }).ToList();
        }

        public List<AccountTreasuryViewModel> GetAllByCompany(int companyId,int schoolId)
        {
            return Context.AccountTreasuries.Where(x => x.IsDeleted == false&&x.CompanyID==companyId &&x.SchoolID==schoolId).
             Select(x => new AccountTreasuryViewModel { ID = x.ID, TreasuryNameEn = x.TreasuryName, Comment = x.Comment, Employee_ID = x.Employee_ID, NameA = x.AdmEmployee.NameA, AccountTreeID = x.AccountTreeID, TreasuryNameAR = x.TreasuryNameAR, AccountNameAR = x.AccountTree.AccountNameAR }).ToList();
        }

        public AccountTreasuryViewModel GetById(int ID)
        {
            return Context.AccountTreasuries.Where(x => x.IsDeleted == false&& ID==x.ID).
              Select(x => new AccountTreasuryViewModel { ID = x.ID, TreasuryNameEn = x.TreasuryName, Comment = x.Comment, Employee_ID = x.Employee_ID, NameA = x.AdmEmployee.NameA, AccountTreeID = x.AccountTreeID, TreasuryNameAR = x.TreasuryNameAR, AccountNameAR = x.AccountTree.AccountNameAR }).FirstOrDefault();
        }

        public AccountTreasuryViewModel Update(AccountTreasuryViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccountTreasury = Context.AccountTreasuries.Where(x => x.ID ==model.ID).FirstOrDefault();
            _AccountTreasury.TreasuryName = model.TreasuryNameEn;
            _AccountTreasury.Comment = model.Comment;
            _AccountTreasury.AccountTreeID = model.AccountTreeID;
            _AccountTreasury.Employee_ID = model.Employee_ID;
            _AccountTreasury.TreasuryNameAR = model.TreasuryNameAR;
            _AccountTreasury.ModifiedDate = DateTime.Now;
            _AccountTreasury.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;

        }



        public AccountTreasuryViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
