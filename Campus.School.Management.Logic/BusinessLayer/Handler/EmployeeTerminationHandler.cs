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
   public class EmployeeTerminationHandler :IRepository<EmployeeTerminationViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(EmployeeTerminationViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            EmployeeTermination _employeeTermination = new EmployeeTermination
            {
                 Employee_ID = model.Employee_ID,
                 Salary =model.Salary,
                 HousingAllowance =model.HousingAllowance,
                 EndOfService =model.EndOfService,
                 LastWorkingDate=model.LastWorkingDate,
                 Reason =model.Reason,
                 TelNo =model.TelNo,
                 CreatedbyID =_dbUser.CreatedbyID,
                 CreatedDate =DateTime.Now         
            };

            var _employee = Context.AdmEmployees.Where(x => x.Employee_ID == model.Employee_ID && x.IsDeleted ==false).FirstOrDefault();
            _employee.IsTerminated = true;

            Context.EmployeeTerminations.Add(_employeeTermination);

            Context.SaveChanges();
        }

        public EmployeeTerminationViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _EmployeeTerminations = Context.EmployeeTerminations.Where(x => x.IsDeleted == false).FirstOrDefault();
            if (_EmployeeTerminations != null)
            {
                _EmployeeTerminations.DeletedbyID = _dbUser.ID;
                _EmployeeTerminations.IsDeleted = true;
                _EmployeeTerminations.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }

        public List<EmployeeTerminationViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public EmployeeTerminationViewModel GetById(int ID)
        {
           return Context.EmployeeTerminations.Where(x => x.Employee_ID == ID)
                  .Select(x => new EmployeeTerminationViewModel
                  {
                      ID = x.ID,
                      Employee_ID = x.Employee_ID,
                      Salary = x.Salary,
                      EndOfService = x.EndOfService,
                      HousingAllowance = x.HousingAllowance,
                      LastWorkingDate = x.LastWorkingDate,
                      Reason = x.Reason,
                      TelNo = x.TelNo

                  }).FirstOrDefault();
        }

        public EmployeeTerminationViewModel Update(EmployeeTerminationViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
