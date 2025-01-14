using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class EmployeeBenfitsOtherHandler : IRepository<EmployeeBenefitsOthersViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public void Create(EmployeeBenefitsOthersViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            EmployeeBenfitsOther _EmployeeBenfitsOther = new EmployeeBenfitsOther()
            {
                Name_AR = model.Name_AR,
                Name_En = model.Name_En,
                AccountTreeID = model.AccountTreeID,
                Description = model.Description,
                Type = model.Type,
                Formula = model.Formula,
                CreatedbyID = _dbUser.CreatedbyID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID,
                IsDefault = model.IsDefault,
                Percentage = model.Percentage
              

            };
            Context.EmployeeBenfitsOthers.Add(_EmployeeBenfitsOther);
            Context.SaveChanges();


        }

        public EmployeeBenefitsOthersViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            var item = Context.EmployeeBenfitsOthers.Where(x => x.Id == ID && x.SchoolID == _dbUser.SchoolID).SingleOrDefault();


            if (item != null)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                item.DeletedbyID = _dbUser.DeletedbyID;
                return true;
            }


            return false;
        }

        public List<EmployeeBenefitsOthersViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
        public EmployeeBenefitsOthersViewModel GetById(int ID)
        {
            return Context.EmployeeBenfitsOthers.Where(x =>  x.Id ==ID && x.AdmSchool.CompanyID == _dbUser.CompanyID && x.IsDeleted == false).Select(x => new EmployeeBenefitsOthersViewModel
            {
                Id = x.Id,
                Name_AR = x.Name_AR,
                Name_En = x.Name_En,
                Type = x.Type,
                //Formula = x.Formula,
                AccountTreeID = x.AccountTreeID,
                AccountTreeName = x.AccountTree.AccountNameEN,
                IsDefault = x.IsDefault,
                Description = x.Description,
                Percentage=x.Percentage

            }).SingleOrDefault();
        }

        public List<EmployeeBenefitsOthersViewModel> GetAllByCompany(int Id, int Type)
        {
            return Context.EmployeeBenfitsOthers.Where(x => x.AdmSchool.CompanyID == _dbUser.CompanyID && x.Type == Type && x.IsDeleted==false).Select(x => new EmployeeBenefitsOthersViewModel
            {
                Id = x.Id,
                Name_AR = x.Name_AR,
                Name_En = x.Name_En,
                Type = x.Type,
                //Formula = x.Formula,
                AccountTreeID = x.AccountTreeID,
                AccountTreeName = x.AccountTree.AccountNameEN,
                IsDefault = x.IsDefault,
                Description = x.Description,
                Percentage = x.Percentage

            }).ToList();

        }

        public EmployeeBenefitsOthersViewModel Update(EmployeeBenefitsOthersViewModel model)
        {
            var item = Context.EmployeeBenfitsOthers.Where(x => x.Id == model.Id && x.SchoolID == _dbUser.SchoolID && x.IsDeleted==false).SingleOrDefault();

            item.Name_AR = model.Name_AR;
            item.Name_En = model.Name_En;
            item.Description = model.Description;
            item.Percentage = model.Percentage;
            item.IsDefault = model.IsDefault;
            item.ModifiedbyID = _dbUser.ModifiedbyID;
            item.ModifiedDate = DateTime.Now;
            Context.SaveChanges();
            return model;
        }
    }
}
