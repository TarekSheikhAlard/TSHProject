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
   public class DepartmentHandler:IRepository<DepartmentViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public List<DepartmentViewModel> GetAll()
        {
            return Context.AdmDepartments.Where(x=>x.IsDeleted==false).Select(x => new DepartmentViewModel
            { DepartmentID = x.DepartmentID, DepartmentEn = x.DepartmentEn, DepartmentAr = x.DepartmentAr }).ToList();

        }
        public List<DepartmentViewModel> GetAllByCompany(int CompanyId, int SchoolId)
        {
            return Context.AdmDepartments.Where(x => x.IsDeleted == false &&
                                                     x.CompanyID==CompanyId &&   
                                                     x.SchoolID==SchoolId)
                                         .Select(x => new DepartmentViewModel
                                           { DepartmentID = x.DepartmentID,
                                             DepartmentEn = x.DepartmentEn,
                                             DepartmentAr = x.DepartmentAr
                                         }).ToList();

        }
        public DepartmentViewModel GetById(int ID)
        {
            return Context.AdmDepartments.Where(x => x.DepartmentID == ID&&x.IsDeleted==false)
           .Select(x => new DepartmentViewModel { DepartmentID = x.DepartmentID, DepartmentAr = x.DepartmentAr, DepartmentEn = x.DepartmentEn }).FirstOrDefault();
        }


        public void Create(DepartmentViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AdmDepartment _Department = new AdmDepartment
            {
                DepartmentEn =model.DepartmentEn,
                DepartmentAr =model.DepartmentAr,
                CreatedbyID = _dbUser.ID,
                CreatedDate =DateTime.Now,
                ModifiedDate =DateTime.Now,
                DeletedDate =DateTime.Now,
                SchoolID=model.ScholID,
                CompanyID=model.CompanyID
            };
            Context.AdmDepartments.Add(_Department);
            Context.SaveChanges(); 
        }


        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Department = Context.AdmDepartments.Where(x => x.DepartmentID == ID&&x.IsDeleted==false).FirstOrDefault();
            if (_Department != null)
            {
                _Department.DeletedbyID = _dbUser.ID;
                _Department.IsDeleted = true;
                _Department.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }
        
        public DepartmentViewModel Update(DepartmentViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Department = Context.AdmDepartments.Where(x => x.DepartmentID == model.DepartmentID).FirstOrDefault();
            _Department.DepartmentAr = model.DepartmentAr;
            _Department.DepartmentEn = model.DepartmentEn;
            _Department.ModifiedbyID = _dbUser.ID;
            _Department.ModifiedDate = DateTime.Now;
            Context.SaveChanges();
            return model;
        }

        public DepartmentViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
