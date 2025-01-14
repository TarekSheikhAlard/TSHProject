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
    public class AcademicYearHandler : IRepository<AcademicYearViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public AcademicYearViewModel Update(AcademicYearViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AcadimicYear = Context.AdmAcademicYears.Where(c => c.AcademicID == model.AcademicID).FirstOrDefault();
            if (_AcadimicYear != null)
            {
                _AcadimicYear.Stage_ID = model.Stage_ID;
                _AcadimicYear.AcadmicName = model.AcadmicName;
                _AcadimicYear.ModifiedDate = DateTime.Now;
                _AcadimicYear.ModifiedbyID = _dbUser.ID;
                Context.SaveChanges();
            }
            return model;

        }

        public List<AcademicYearViewModel> GetAll()
        {
            return Context.AdmAcademicYears.Where(c=> c.IsDeleted == false).Select(c => new AcademicYearViewModel
            {
                AcademicID = c.AcademicID,
                AcadmicName = c.AcadmicName,
                Stage_ID = c.Stage_ID,
                StageName = c.AdmStage.StageNameEn,
                IsNew = false
            }).ToList();

        }

        public AcademicYearViewModel GetById(int ID)
        {
            return Context.AdmAcademicYears.Where(c => c.AcademicID == ID && c.IsDeleted == false).Select(c => new AcademicYearViewModel
            {
                AcademicID = c.AcademicID,
                Stage_ID = c.Stage_ID,
                AcadmicName = c.AcadmicName,
                IsNew = false,
                StageName=c.AdmStage.StageNameEn
            }).FirstOrDefault();
        }

        public AcademicYearViewModel Create()
        {
            AcademicYearViewModel model = new AcademicYearViewModel();
            model.IsNew = true;
            return model;
        }

        public void Create(AcademicYearViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AdmAcademicYear _AcadimicYear = new AdmAcademicYear
            {
                AcadmicName = model.AcadmicName,
                Stage_ID = model.Stage_ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CreatedbyID=_dbUser.ID,
               
            };
            Context.AdmAcademicYears.Add(_AcadimicYear);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AcadimicYear = Context.AdmAcademicYears.Where(c => c.AcademicID == ID).FirstOrDefault();
            if (_AcadimicYear != null)
            {
                _AcadimicYear.IsDeleted = true;
                _AcadimicYear.DeletedDate = DateTime.Now;
                _AcadimicYear.DeletedbyID = _dbUser.ID;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }




    }
}