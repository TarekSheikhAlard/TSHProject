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
   public class BankBranchHandler : IRepository<BankBranchViewModel>
    {
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        
        public List<BankBranchViewModel> GetAll()
        {
            return Context.BankBranches.Where(x => x.IsDeleted == false && x.Bank.CompanyID==_dbUser.CompanyID).Select(x => new BankBranchViewModel
            {
                BankID = x.BankID,
                ID = x.ID,
                BranchAddress = x.BranchAddress,
                BranchFax = x.BranchFax,
                BranchMobile = x.BranchMobile,
                BranchNameEn = x.BranchName,
                BranchNameAr = x.BranchNameAr,
                BranchPhone = x.BranchPhone,
                BankNameEn = x.Bank.BankNameEn,
                AccountTreeID = x.AccountTreeID,
                AccountNameAR = "XYZ ACC",
                AccountchequePayables = x.AccountchequePayables,
                Accountchequesreceivables = x.Accountchequesreceivables.Where(y => y.BankBranchID == x.ID&&y.IsDeleted==false).ToList(),
                AccountCustomers = x.AccountCustomers.Where(y => y.BankBranchID == x.ID&&y.IsDeleted==false).ToList(),
                AccountSuppliers = x.AccountSuppliers.Where(y => y.BankBranchID == x.ID&&y.IsDeleted==false).ToList(),
                //BranchAccounts = x.BranchAccounts.Where(y => y.BranchID == x.ID&&y.IsDeleted==false).ToList()
            }).ToList();
        }

        public List<BankBranchViewModel> GetAllByCompany(int CompanyId)
        {
                return Context.BankBranches.Where(x => x.IsDeleted == false && x.CompanyID == CompanyId).Select(x => new BankBranchViewModel
                {
                    BankID = x.BankID,
                    ID = x.ID,
                    BranchAddress = x.BranchAddress,
                    BranchFax = x.BranchFax,
                    BranchMobile = x.BranchMobile,
                    BranchNameEn = x.BranchName,
                    BranchNameAr = x.BranchNameAr,
                    BranchPhone = x.BranchPhone,
                    BankNameEn = x.Bank.BankNameAr,
                    AccountTreeID = x.AccountTreeID,
                    AccountNameAR = "XYZ",//x.AccountTree.AccountNameAR,
                    AccountchequePayables = x.AccountchequePayables,
                    Accountchequesreceivables = x.Accountchequesreceivables.Where(y => y.BankBranchID == x.ID && y.IsDeleted == false).ToList(),
                    AccountCustomers = x.AccountCustomers.Where(y => y.BankBranchID == x.ID && y.IsDeleted == false).ToList(),
                    AccountSuppliers = x.AccountSuppliers.Where(y => y.BankBranchID == x.ID && y.IsDeleted == false).ToList(),
                    //BranchAccounts = x.BranchAccounts.Where(y => y.BranchID == x.ID && y.IsDeleted == false).ToList(),
                    CompanyID = x.CompanyID
                }).OrderByDescending(x => x.ID).ToList();

           

        }

        public BankBranchViewModel GetById(int ID)
        {
            return Context.BankBranches.Where(x => x.ID == ID&& x.IsDeleted==false).Select(x => new BankBranchViewModel
            {
                BankID = x.BankID,
                ID = x.ID,
                BranchAddress = x.BranchAddress,
                BranchFax = x.BranchFax,
                BranchMobile = x.BranchMobile,
                BranchNameEn = x.BranchName,
                BranchNameAr = x.BranchNameAr,
                BranchPhone = x.BranchPhone,
                BankNameEn = x.Bank.BankNameAr,
                AccountTreeID = x.AccountTreeID,
                AccountNameAR = "XYZ",//x.AccountTree.AccountNameAR
            }).FirstOrDefault();


        }

        public void Create(BankBranchViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            BankBranch _Bankbranch = new BankBranch
            {
                BankID=model.BankID,
                BranchAddress=model.BranchAddress,
                BranchFax=model.BranchFax,
                BranchMobile=model.BranchMobile,
                BranchName=model.BranchNameEn,
                BranchNameAr=model.BranchNameAr,
                BranchPhone=model.BranchPhone,
                AccountTreeID=model.AccountTreeID,
                CreatedbyID = _dbUser.ID,
                CreatedDate =DateTime.Now,
                ModifiedDate =DateTime.Now,
                DeletedDate =DateTime.Now, 
                CompanyID=model.CompanyID
            };
            Context.BankBranches.Add(_Bankbranch);
            Context.SaveChanges();

        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Bankbranch = Context.BankBranches.Where(x => x.ID == ID).FirstOrDefault();
            if (_Bankbranch != null)
            {
                _Bankbranch.DeletedbyID = _dbUser.ID;
                _Bankbranch.IsDeleted = true;
                _Bankbranch.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public BankBranchViewModel Update(BankBranchViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Bankbranch = Context.BankBranches.Where(x => x.ID == model.ID).FirstOrDefault();

            _Bankbranch.BankID = model.BankID;

            _Bankbranch.BranchAddress = model.BranchAddress;
            _Bankbranch.BranchFax = model.BranchFax;
            _Bankbranch.BranchMobile = model.BranchMobile;
            _Bankbranch.BranchName = model.BranchNameEn;
            _Bankbranch.BranchNameAr = model.BranchNameAr;
            _Bankbranch.BranchPhone = model.BranchPhone;
            _Bankbranch.AccountTreeID = model.AccountTreeID;
            _Bankbranch.ModifiedbyID = _dbUser.ID;
            _Bankbranch.ModifiedDate = DateTime.Now;
            Context.SaveChanges();
            return model;

        }

        public BankBranchViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
