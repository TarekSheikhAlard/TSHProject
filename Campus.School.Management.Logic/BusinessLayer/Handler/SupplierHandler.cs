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
 public class SupplierHandler : IRepository<SupplierViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public List<SupplierViewModel> GetAll()
        {
 
            return Context.AccountSuppliers.Where(x => x.IsDeleted == false && x.SchoolID==_dbUser.SchoolID&&x.CompanyID==_dbUser.CompanyID)
           .Select(x => new SupplierViewModel
           {
               ID = x.ID,
               AccountTreeID = x.AccountTreeID,
               Address = x.Address,
               SupplierName = x.SupplierName,
               SupplierNameAr = x.SupplierNameAr,

               Email = x.Email,
               Mobile = x.Mobile,
               Phone = x.Phone,
               Website = x.Website,
               SchoolID = x.SchoolID,
               FBalance = x.FBalance,
               Typeofactivity = x.Typeofactivity,
               Supplier_Employee = x.Supplier_Employee,
               Supplieractivity = x.Supplieractivity,
               ReasonStop = x.ReasonStop,
               PostCode = x.PostCode,
               BankAccountNumber = x.BankAccountNumber,
               IBAN1 = x.IBAN1,
               CityID = x.CityID,
               Commercialregister = x.Commercialregister,
               Fax = x.Fax,
               Credit = x.Credit,
               Depeit = x.Depeit,
               IBAN2 = x.IBAN2,
               Emp_ID = x.Emp_ID,
               IBAN3 = x.IBAN3,
               SupplierNumber = x.SupplierNumber,
               BankBranchID = x.BankBranchID,
              AccountTree =x.AccountTree.AccountNameAR,
               BankBranchName=x.BankBranch.BranchNameAr,
               CityName =x.AdmCity.CityAr,
               EmployeeName =x.AdmEmployee.NameA
           }).ToList();

        }



        public List<SupplierViewModel> GetAllByCompany(int companyId,int schoolId)
        {

            return Context.AccountSuppliers.Where(x => x.IsDeleted == false&&x.CompanyID==companyId&&x.SchoolID==schoolId)
           .Select(x => new SupplierViewModel
           {
               ID = x.ID,
               AccountTreeID = x.AccountTreeID,
               Address = x.Address,
               SupplierName = x.SupplierName,
               SupplierNameAr = x.SupplierNameAr,
               Email = x.Email,
               Mobile = x.Mobile,
               Phone = x.Phone,
               Website = x.Website,
               SchoolID = x.SchoolID,
               FBalance = x.FBalance,
               Typeofactivity = x.Typeofactivity,
               Supplier_Employee = x.Supplier_Employee,
               Supplieractivity = x.Supplieractivity,
               ReasonStop = x.ReasonStop,
               PostCode = x.PostCode,
               BankAccountNumber = x.BankAccountNumber,
               IBAN1 = x.IBAN1,
               CityID = x.CityID,
               Commercialregister = x.Commercialregister,
               Fax = x.Fax,
               Credit = x.Credit,
               Depeit = x.Depeit,
               IBAN2 = x.IBAN2,
               Emp_ID = x.Emp_ID,
               IBAN3 = x.IBAN3,
               SupplierNumber = x.SupplierNumber,
               BankBranchID = x.BankBranchID,
               AccountTree = x.AccountTree.AccountNameAR,
               BankBranchName = x.BankBranch.BranchNameAr,
               CityName = x.AdmCity.CityAr,
               EmployeeName = x.AdmEmployee.NameA
           }).ToList();

        }

        public SupplierViewModel Create()
        {
            SupplierViewModel model = new SupplierViewModel();
            model.Depeit = 0;
            model.Credit = 0;
            model.FBalance = 0;
            return model;
        }

     

        public void Create(SupplierViewModel model)
        {
           SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
           AccountSupplier supplier = new AccountSupplier
           {
                AccountTreeID = model.AccountTreeID,
                Address = model.Address,
                SupplierName = model.SupplierName,
                SupplierNameAr = model.SupplierNameAr,
                Email = model.Email,
                Phone = model.Phone,
                Website = model.Website,
                Mobile = model.Mobile,
                SchoolID = model.SchoolID,
                CompanyID = model.CompanyID,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Fax=model.Fax,
                BankAccountNumber=model.BankAccountNumber,
                Commercialregister=model.Commercialregister,
                SupplierNumber=model.SupplierNumber,
                BankBranchID=model.BankBranchID,
                Emp_ID=model.Emp_ID,
                Credit=model.Credit,
                CityID=model.CityID, 
                Depeit=model.Depeit,
               IBAN1=model.IBAN1,
               IBAN2=model.IBAN2,
               IBAN3=model.IBAN3,
               PostCode =model.PostCode,
               ReasonStop=model.ReasonStop,
               Supplieractivity=model.Supplieractivity,
               Supplier_Employee =model.Supplier_Employee, 
              Typeofactivity=model.Typeofactivity, 
               FBalance=model.FBalance
                    
            };
            Context.AccountSuppliers.Add(supplier);
            Context.SaveChanges();
        }


        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var supplier = Context.AccountSuppliers.Where(c => c.ID == ID && c.IsDeleted == false).FirstOrDefault();
            if (supplier != null)
            {
                supplier.IsDeleted = true;
                supplier.DeletedDate = DateTime.Now;
                supplier.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

   
        public SupplierViewModel GetById(int ID)
        {
            return Context.AccountSuppliers.Where(x => x.IsDeleted == false && x.ID == ID)
          .Select(x => new SupplierViewModel
          {
              ID = x.ID,
              AccountTreeID = x.AccountTreeID,
              Address = x.Address,
              SupplierName = x.SupplierName,
              SupplierNameAr = x.SupplierNameAr,

              Email = x.Email,
              Mobile = x.Mobile,
              Phone = x.Phone,
              Website = x.Website,
              SchoolID = x.SchoolID,
              FBalance = x.FBalance,
              Typeofactivity = x.Typeofactivity,
              Supplier_Employee = x.Supplier_Employee,
              Supplieractivity = x.Supplieractivity,
              ReasonStop = x.ReasonStop,
              PostCode = x.PostCode,
              BankAccountNumber = x.BankAccountNumber,
              IBAN1 = x.IBAN1,
              CityID = x.CityID,
              Commercialregister = x.Commercialregister,
              Fax = x.Fax,
              Credit = x.Credit,
              Depeit = x.Depeit,
              IBAN2 = x.IBAN2,
              Emp_ID = x.Emp_ID,
              IBAN3 = x.IBAN3,
              SupplierNumber = x.SupplierNumber,
              BankBranchID = x.BankBranchID,
              AccountTree = x.AccountTree.AccountNameAR,
              BankBranchName = x.BankBranch.BranchNameAr,
              CityName = x.AdmCity.CityAr,
              EmployeeName = x.AdmEmployee.NameA

          }).SingleOrDefault();


        }


        public SupplierViewModel Update(SupplierViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var supplier = Context.AccountSuppliers.Where(x => x.ID == model.ID && x.IsDeleted == false).FirstOrDefault();

            supplier.AccountTreeID = model.AccountTreeID;
            supplier.Address = model.Address;
            supplier.Typeofactivity = model.Typeofactivity;
            supplier.SupplierName = model.SupplierName;
            supplier.SupplierNameAr = model.SupplierNameAr;
            supplier.Email = model.Email;
            supplier.Mobile = model.Mobile;
            supplier.Website = model.Website;
            supplier.Phone = model.Phone;
            supplier.CityID = model.CityID;
            supplier.Commercialregister = model.Commercialregister;
            supplier.BankBranchID = model.BankBranchID;
            supplier.Credit = model.Credit;
            supplier.Depeit = model.Depeit;
            supplier.Emp_ID = model.Emp_ID;
            supplier.Fax = model.Fax;
            supplier.Supplieractivity = model.Supplieractivity;
            supplier.Supplier_Employee = model.Supplier_Employee;
            supplier.ReasonStop = model.ReasonStop;
            supplier.BankAccountNumber = model.BankAccountNumber;
            supplier.IBAN1 = model.IBAN1;
            supplier.IBAN2 = model.IBAN2;
            supplier.IBAN3 = model.IBAN3;
            supplier.PostCode = model.PostCode;
            supplier.FBalance = model.FBalance;
            supplier.SupplierNumber = model.SupplierNumber;
            supplier.ModifiedDate = DateTime.Now;
            supplier.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;
        }
    }
}
