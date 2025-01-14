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
    public class JobsHandlar : IRepository<JobsViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public List<JobsViewModel> GetAll()
        {
            var lang = Utility.getCultureCookie(false).ToLower();

            return Context.AdmJobs.Where(x => x.IsDeleted == false).Select(x => new JobsViewModel
            { JobID = x.JobID, JobNameAr = x.JobNameAr, JobNameEn = x.JobNameEn, DepartmentID = x.DepartmentID, DepartmentName = lang == "ar" ? x.DepartmentTree.DepartmentNameAR : x.DepartmentTree.DepartmentNameEN, }).OrderBy(x => x.DepartmentID).ToList();
        }

        public List<JobsViewModel> GetAllByCompany(int companyId, int schoolId)
        {
            var lang = Utility.getCultureCookie(false).ToLower();
            
            return Context.AdmJobs.Where(x => x.IsDeleted == false
                                         && x.CompanyID == companyId
                                         && x.SchoolID == schoolId)
                                  .Select(x => new JobsViewModel
                                  {
                                      JobID = x.JobID,
                                      JobNameAr = x.JobNameAr,
                                      JobNameEn = x.JobNameEn,
                                      DepartmentID = x.DepartmentID,
                                      DepartmentName =  lang=="ar"? x.DepartmentTree.DepartmentNameAR: x.DepartmentTree.DepartmentNameEN,
                                  }).OrderByDescending(x => x.DepartmentID).ToList();
        }


        public void Create(JobsViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AdmJob _job = new AdmJob
            {
                JobNameAr = model.JobNameAr,
                JobNameEn = model.JobNameEn,
                DepartmentID = model.DepartmentID,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CompanyID = model.CompanyId,
                SchoolID = model.SchoolId
            };

            Context.AdmJobs.Add(_job);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _job = Context.AdmJobs.Where(x => x.JobID == ID && x.IsDeleted == false).FirstOrDefault();
            if (_job != null)
            {
                _job.DeletedbyID = _dbUser.ID;
                _job.IsDeleted = true;
                _job.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }



        public JobsViewModel GetById(int ID)
        {
            var lang = Utility.getCultureCookie(false).ToLower();

            return Context.AdmJobs.Where(x => x.JobID == ID && x.IsDeleted == false).Select(x => new JobsViewModel
            { JobID = x.JobID, JobNameAr = x.JobNameAr, JobNameEn = x.JobNameEn,DepartmentName= lang == "ar" ? x.DepartmentTree.DepartmentNameAR : x.DepartmentTree.DepartmentNameEN }).FirstOrDefault();
        }

        public JobsViewModel Update(JobsViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _job = Context.AdmJobs.Where(x => x.JobID == model.JobID).FirstOrDefault();
            _job.JobNameAr = model.JobNameAr;
            _job.JobNameEn = model.JobNameEn;
            _job.DepartmentID = model.DepartmentID;
            _job.ModifiedDate = DateTime.Now;
            _job.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;
        }

        public JobsViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
