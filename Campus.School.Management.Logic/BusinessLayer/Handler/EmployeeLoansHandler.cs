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
    public class EmployeeLoansHandler : IRepository<EmployeeLoansViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public void Create(EmployeeLoansViewModel model)
        {
            
            EmployeeLoan   employeeLoan = new EmployeeLoan
            {
                Employee_ID = model.Employee_ID,
                TotalAmount=model.TotalAmount,
                MonthlyDeductionAmt=model.MonthlyDeductionAmt,
                StartOfDeduction =model.StartOfDeduction,
                CreatedbyID = _dbUser.CreatedbyID,
                CreatedDate = DateTime.Now,
                
                
            };
            context.EmployeeLoans.Add(employeeLoan);
            context.SaveChanges();

       
        }

        public EmployeeLoansViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var  employeeLoan = context.EmployeeLoans.Where(x => x.ID == ID && x.IsDeleted == false).FirstOrDefault();
            if (employeeLoan != null)
            {
                employeeLoan.DeletedbyID = _dbUser.ID;
                employeeLoan.IsDeleted = true;
                employeeLoan.DeletedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            else return false;

            
        }

        public List<EmployeeLoansViewModel> GetAll()
        {
            var culture = Utility.getCultureCookie(false).ToLower();

            return context.EmployeeLoans.Where(x => x.IsDeleted == false && x.AdmEmployee.SchoolID ==_dbUser.SchoolID)
                            .Select(x => new EmployeeLoansViewModel
                            {
                                ID = x.ID,
                                EmployeeName = culture == "ar" ? x.AdmEmployee.NameA : x.AdmEmployee.NameE,
                                TotalAmount =x.TotalAmount,
                                MonthlyDeductionAmt =x.MonthlyDeductionAmt,
                                StartOfDeduction=x.StartOfDeduction,
                                Balance =x.Balance
                              
                            }
                            ).ToList();

        
        }
        public List<EmployeeLoansViewModel> GetAllByEmployeeId(int id )
        {
            var culture = Utility.getCultureCookie(false).ToLower();

            return context.EmployeeLoans.Where(x => x.IsDeleted == false && x.Employee_ID == id && x.AdmEmployee.SchoolID == _dbUser.SchoolID)
                            .Select(x => new EmployeeLoansViewModel
                            {
                                ID = x.ID,
                                EmployeeName = culture == "ar" ? x.AdmEmployee.NameA : x.AdmEmployee.NameE,
                                TotalAmount = x.TotalAmount,
                                MonthlyDeductionAmt = x.MonthlyDeductionAmt,
                                StartOfDeduction = x.StartOfDeduction,
                                Balance = x.Balance

                            }
                            ).ToList();

           
        }

        public EmployeeLoansViewModel GetEmployeeIdFromRecId(int id)
        {

            return context.EmployeeLoans
                             .Where(x =>  x.ID == id && x.AdmEmployee.SchoolID == _dbUser.SchoolID)
                             .Select(x => new EmployeeLoansViewModel
                             {

                                 Employee_ID = x.Employee_ID
                             }
                             ).FirstOrDefault();

        }

        public EmployeeLoansViewModel GetById(int ID)
        {
            var culture = Utility.getCultureCookie(false).ToLower();

            return context.EmployeeLoans.Where(x => x.ID==ID && x.IsDeleted == false && x.AdmEmployee.SchoolID == _dbUser.SchoolID)
                            .Select(x => new EmployeeLoansViewModel
                            {
                                ID = x.ID,
                                Employee_ID=x.Employee_ID,
                                EmployeeName = culture == "ar" ? x.AdmEmployee.NameA : x.AdmEmployee.NameE,
                                TotalAmount = x.TotalAmount,
                                MonthlyDeductionAmt = x.MonthlyDeductionAmt,
                                StartOfDeduction = x.StartOfDeduction,
                                Balance = x.Balance

                            }
                            ).FirstOrDefault();
           
        }

        public EmployeeLoansViewModel Update(EmployeeLoansViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _employeeLoan = context.EmployeeLoans.Where(x => x.ID == model.ID && x.AdmEmployee.SchoolID == _dbUser.SchoolID).FirstOrDefault();

            _employeeLoan.TotalAmount = model.TotalAmount;
            _employeeLoan.StartOfDeduction = model.StartOfDeduction;
            _employeeLoan.MonthlyDeductionAmt = model.MonthlyDeductionAmt;
            _employeeLoan.ModifiedDate = DateTime.Now;
            _employeeLoan.ModifiedbyID = _dbUser.ID;

            context.SaveChanges();
            return model;
          
        }
    }
}
