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
    public class EmployeeAddedBenefitsOthersHandler : IRepository<EmployeeAddedBenefitsOthersViewModel>
    {

        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public void Create(EmployeeAddedBenefitsOthersViewModel model)
        {
            EmployeeAddedBenefitsOther employeeAddedBenefitsOther = new EmployeeAddedBenefitsOther
            {

                EmployeeId=model.EmployeeId,
                EmployeeBenefitsOtherId=model.EmployeeBenefitsOtherId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID = _dbUser.CreatedbyID

            };
        }

        public EmployeeAddedBenefitsOthersViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            var result = Context.EmployeeAddedBenefitsOthers.Where(x => x.Id == ID && x.IsDeleted == false).SingleOrDefault();

            if (result != null ) {
                result.DeletedbyID = _dbUser.ID;
                result.IsDeleted = true;
                result.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;

            }
            return false;
        }

        public List<EmployeeAddedBenefitsOthersViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public EmployeeAddedBenefitsOthersViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public EmployeeAddedBenefitsOthersViewModel Update(EmployeeAddedBenefitsOthersViewModel model)
        {
            var result = Context.EmployeeAddedBenefitsOthers.Where(x => x.Id == model.Id && x.IsDeleted == false).SingleOrDefault();
            if (result!=null) {

                result.IsDeleted = false;
                Context.SaveChanges();
            }
            return model;

        }
    }
}
