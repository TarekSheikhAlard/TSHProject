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
    public class AccountCustomerHandler : IRepository<AccountCustomerViewModel>
    {

        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        private AccountTreeHandler AccountTree = new AccountTreeHandler();
        public List<AccountCustomerViewModel> GetAll()
        {
          
            return Context.AccountCustomers.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID)
           .Select(x => new AccountCustomerViewModel
           {
               ID = x.ID,
               AccountTreeID = x.AccountTreeID,
               Address = x.Address,
               CustomerName = x.CustomerName,
               CustomerNameAr = x.CustomerNameAr,
               AccountTree = x.AccountTree.AccountNameAR,
               Email = x.Email,
               Mobile = x.Mobile,
               Phone = x.Phone,
               Website = x.Website,
               SchoolID = x.SchoolID,
               Credit = x.Credit,
               Depeit = x.Depeit,
               FBalance = x.FBalance,
               Typeofactivity = x.Typeofactivity,
               Customer_Employee = x.Customer_Employee,
               PostCode = x.PostCode,
               IBAN1 = x.IBAN1,
               IBAN2 = x.IBAN2,
               IBAN3 = x.IBAN3,
               ReasonStop = x.ReasonStop,
               Fax = x.Fax,
               BankAccountNumber = x.BankAccountNumber,
               CustomerNumber = x.CustomerNumber,
               Commercialregister = x.Commercialregister,
               Customeractivity = x.Customeractivity,
               BankBranchID = x.BankBranchID,
               CityID = x.CityID,
               Emp_ID = x.Emp_ID,
               BankBranchName = x.BankBranch.BranchNameAr,
               CityName = x.AdmCity.CityAr,
               EmployeeName = x.AdmEmployee.NameA

           }).ToList();

        }

        public List<AccountCustomerViewModel> GetAllByCompany(int companyId, int schoolId)
        {
            return Context.AccountCustomers.Where(x => x.IsDeleted == false && x.SchoolID == schoolId)
           .Select(x => new AccountCustomerViewModel
           {
               ID = x.ID,
               AccountTreeID = x.AccountTreeID,
               Address = x.Address,
               CustomerName = x.CustomerName,
               CustomerNameAr = x.CustomerNameAr,
               AccountTree = x.AccountTree.AccountNameAR,
               Email = x.Email,
               Mobile = x.Mobile,
               Phone = x.Phone,
               Website = x.Website,
               SchoolID = x.SchoolID,
               Credit = x.Credit,
               Depeit = x.Depeit,
               FBalance = x.FBalance,
               Typeofactivity = x.Typeofactivity,
               Customer_Employee = x.Customer_Employee,
               PostCode = x.PostCode,
               IBAN1 = x.IBAN1,
               IBAN2 = x.IBAN2,
               IBAN3 = x.IBAN3,
               ReasonStop = x.ReasonStop,
               Fax = x.Fax,
               BankAccountNumber = x.BankAccountNumber,
               CustomerNumber = x.CustomerNumber,
               Commercialregister = x.Commercialregister,
               Customeractivity = x.Customeractivity,
               BankBranchID = x.BankBranchID,
               CityID = x.CityID,
               Emp_ID = x.Emp_ID,
               BankBranchName = x.BankBranch.BranchNameAr,
               CityName = x.AdmCity.CityAr,
               EmployeeName = x.AdmEmployee.NameA

           }).ToList();

        }

        public void Create(AccountCustomerViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccountCustomer _AccountCustomer = new AccountCustomer
            {
                AccountTreeID = model.AccountTreeID,
                Address = model.Address,
                CustomerName = model.CustomerName,
                CustomerNameAr = model.CustomerNameAr,
                Email = model.Email,
                Phone = model.Phone,
                Website = model.Website,
                Mobile = model.Mobile,
                Credit = model.Credit,
                Depeit = model.Depeit,
                FBalance = model.FBalance,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Typeofactivity = model.Typeofactivity,
                CityID = model.CityID,
                BankBranchID = model.BankBranchID,
                Customeractivity = model.Customeractivity,
                CustomerNumber = model.CustomerNumber,
                ReasonStop = model.ReasonStop,
                Commercialregister = model.Commercialregister,
                Emp_ID = model.Emp_ID,
                Fax = model.Fax,
                IBAN1 = model.IBAN1,
                IBAN2 = model.IBAN2,
                IBAN3 = model.IBAN3,
                PostCode = model.PostCode,
                Customer_Employee = model.Customer_Employee,
                BankAccountNumber = model.BankAccountNumber,
                SchoolID = model.SchoolID,
                CompanyID = model.CompanyID
            };

            AccountCustomerTransaction _CustomerTransaction = new AccountCustomerTransaction()
            {
                AccountTreeID = model.AccountTreeID,
                Balance = model.FBalance,
                Credit = model.Credit,
                Depit = model.Depeit,
                description = "Frist Balance",
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID,
                OperaionDate = DateTime.Now

            };
            AccountTreeViewModel accountTree = new AccountTreeViewModel
            {
                AccountTreeParentID = 15,
                AccountNameEN = string.Format("{0}", model.CustomerName),
                AccountNameAR = string.Format("{0}", model.CustomerName),
                OpenBalance = (decimal)model.FBalance,
                Description = "Client's Account"
            };
            int AccountTreeID = 0;
            AccountTree.Create(accountTree, out  AccountTreeID);

            _AccountCustomer.AccountCustomerTransactions.Add(_CustomerTransaction);
            Context.AccountCustomers.Add(_AccountCustomer);

            _AccountCustomer.AccountTreeID = AccountTreeID;





            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccountCustomer = Context.AccountCustomers.Where(c => c.ID == ID && c.IsDeleted == false).FirstOrDefault();
            if (_AccountCustomer != null)
            {
                _AccountCustomer.IsDeleted = true;
                _AccountCustomer.DeletedDate = DateTime.Now;
                _AccountCustomer.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public AccountCustomerViewModel GetById(int ID)
        {
            return Context.AccountCustomers.Where(x => x.IsDeleted == false && x.ID == ID)
          .Select(x => new AccountCustomerViewModel
          {
              ID = x.ID,
              AccountTreeID = x.AccountTreeID,
              Address = x.Address,
              CustomerName = x.CustomerName,
              CustomerNameAr = x.CustomerNameAr,
              AccountTree = x.AccountTree.AccountNameAR,
              Email = x.Email,
              Mobile = x.Mobile,
              Phone = x.Phone,
              Website = x.Website,
              SchoolID = x.SchoolID,
              Credit = x.Credit,
              Depeit = x.Depeit,
              FBalance = x.FBalance,
              Typeofactivity = x.Typeofactivity,
              Customer_Employee = x.Customer_Employee,
              PostCode = x.PostCode,
              IBAN1 = x.IBAN1,
              IBAN2 = x.IBAN2,
              IBAN3 = x.IBAN3,
              ReasonStop = x.ReasonStop,
              Fax = x.Fax,
              BankAccountNumber = x.BankAccountNumber,
              CustomerNumber = x.CustomerNumber,
              Commercialregister = x.Commercialregister,
              Customeractivity = x.Customeractivity,
              BankBranchID = x.BankBranchID,
              CityID = x.CityID,
              Emp_ID = x.Emp_ID,
              BankBranchName = x.BankBranch.BranchNameAr,
              CityName = x.AdmCity.CityAr,
              EmployeeName = x.AdmEmployee.NameA
          }).SingleOrDefault();

        }

        public AccountCustomerViewModel Update(AccountCustomerViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _AccountCustomer = Context.AccountCustomers.Where(x => x.ID == model.ID && x.IsDeleted == false).FirstOrDefault();

            _AccountCustomer.AccountTreeID = model.AccountTreeID;
            _AccountCustomer.Address = model.Address;
            _AccountCustomer.CustomerNameAr = model.CustomerNameAr;
            _AccountCustomer.CustomerName = model.CustomerName;
            _AccountCustomer.Email = model.Email;
            _AccountCustomer.Mobile = model.Mobile;
            _AccountCustomer.Website = model.Website;
            _AccountCustomer.Phone = model.Phone;
            _AccountCustomer.Credit = model.Credit;
            _AccountCustomer.Depeit = model.Depeit;
            _AccountCustomer.FBalance = model.FBalance;
            _AccountCustomer.ModifiedDate = DateTime.Now;
            _AccountCustomer.ModifiedbyID = _dbUser.ID;
            _AccountCustomer.IBAN1 = model.IBAN1;
            _AccountCustomer.IBAN2 = model.IBAN2;
            _AccountCustomer.IBAN3 = model.IBAN3;
            _AccountCustomer.Fax = model.Fax;
            _AccountCustomer.Emp_ID = model.Emp_ID;
            _AccountCustomer.CityID = model.CityID;
            _AccountCustomer.Commercialregister = model.Commercialregister;
            _AccountCustomer.Customeractivity = model.Customeractivity;
            _AccountCustomer.CustomerNumber = model.CustomerNumber;
            _AccountCustomer.ReasonStop = model.ReasonStop;
            _AccountCustomer.Typeofactivity = model.Typeofactivity;
            _AccountCustomer.PostCode = model.PostCode;
            _AccountCustomer.Customer_Employee = model.Customer_Employee;
            _AccountCustomer.BankAccountNumber = model.BankAccountNumber;

            _AccountCustomer.BankBranchID = model.BankBranchID;


            var _AccountCustomerTransactions = Context.AccountCustomerTransactions.Where(x => x.CustomerID == model.ID && x.description == "Frist Balance").FirstOrDefault();

            _AccountCustomerTransactions.Depit = model.Depeit;
            _AccountCustomerTransactions.Credit = model.Credit;
            _AccountCustomerTransactions.Balance = model.FBalance;
            _AccountCustomerTransactions.AccountTreeID = model.AccountTreeID;
            _AccountCustomerTransactions.OperaionDate = DateTime.Now;
            _AccountCustomerTransactions.ModifiedDate = DateTime.Now;
            _AccountCustomerTransactions.ModifiedbyID = _dbUser.ID;
            _AccountCustomerTransactions.SchoolID = _dbUser.SchoolID;
            Context.SaveChanges();
            return model;
        }

        public AccountCustomerViewModel Create()
        {
            AccountCustomerViewModel model = new ViewModel.AccountCustomerViewModel();
            model.Depeit = 0;
            model.Credit = 0;
            model.FBalance = 0;
            return model;
        }
    }
}
