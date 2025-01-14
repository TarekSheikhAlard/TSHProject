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
  public  class GradesSchoolHandler : IRepository<GradesSchoolViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public GradesSchoolViewModel Create()
        {
            throw new NotImplementedException();
        }

        public void Create(GradesSchoolViewModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _gradeschool = Context.AdmGradesSchools.Where(c => c.GradeOrig == ID  ).FirstOrDefault();
            if (_gradeschool != null)
            {
              
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<GradesSchoolViewModel> GetAll()
        {
            return Context.AdmGradesSchools.Select(x => new GradesSchoolViewModel {
                YearID = x.YearID,
                GradeSchoolNameA = x.GradeSchoolNameA,
                GradeSchoolNameE = x.GradeSchoolNameE,
                GradeOrig = x.GradeOrig,
                GradeID = x.GradeID,
                Is_Update = false,
                Is_Upload = false
                
            }).ToList();
        }

        public GradesSchoolViewModel GetById(int ID)
        {
            return Context.AdmGradesSchools.Where(x => x.GradeID == ID).Select(x => new GradesSchoolViewModel
            {
               // YearID = x.YearID,
                GradeSchoolNameA = x.GradeSchoolNameA,
                GradeSchoolNameE = x.GradeSchoolNameE,
                GradeOrig = x.GradeOrig,
                GradeID = x.GradeID,
                Is_Update = false,
                Is_Upload = false

            }).FirstOrDefault();
        }

        public GradesSchoolViewModel Update(GradesSchoolViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var gradeschool = Context.AdmGradesSchools.Where(c => c.GradeOrig == model.GradeOrig).FirstOrDefault();
            gradeschool.GradeOrig = model.GradeOrig;
            gradeschool.YearID = model.YearID;
            gradeschool.GradeSchoolNameA = model.GradeSchoolNameA;
            gradeschool.GradeSchoolNameE = model.GradeSchoolNameE;
            gradeschool.GradeID = model.GradeID;
            gradeschool.Is_Update = false;
            gradeschool.Is_Upload = false;
            Context.SaveChanges();
            return model;
        }
    }
}
