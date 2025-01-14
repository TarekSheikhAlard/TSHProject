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
    public class QurterHandler : IRepository<QurterViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public List<QurterViewModel> GetAll()
        {


            List<QurterViewModel> model = Context.AdmQurters.Where(x => x.IsDeleted == false).Select(x => new QurterViewModel
            {
                ID = x.ID,
                SID = x.SID,
                QID = x.QID,
                SemNameA = x.AdmSemster.SemNameA,
                SemNameE = x.AdmSemster.SemNameE,
                QStudyWeeks = x.QStudyWeeks,
                QStudyWeeksAbs = x.QStudyWeeksAbs,
                Grade = x.AdmSemster.AdmAcademicYear.AcadmicName,
                Year = x.AdmSemster.admStudyear.YearNameE,
                AdmSemster = x.AdmSemster,
                GradeID = x.AdmSemster.AdmAcademicYear.AcademicID,
                YearID = x.AdmSemster.admStudyear.YearID

            }).GroupBy(x => new { x.YearID,x.SemNameE,x.QID}).Select(x => x.FirstOrDefault()).ToList();

            return model;
        }
    
        public QurterViewModel GetById(int ID)
        {
            return Context.AdmQurters.Where(x => x.IsDeleted == false&&x.ID==ID).Select(x => new QurterViewModel
            {
                ID = x.ID,
                SID = x.SID,
                QID = x.QID,
                SemNameA = x.AdmSemster.SemNameA,
                SemNameE = x.AdmSemster.SemNameE,
                QStudyWeeks = x.QStudyWeeks,
                QStudyWeeksAbs = x.QStudyWeeksAbs,
                Grade = x.AdmSemster.AdmAcademicYear.AcadmicName,
                Year = x.AdmSemster.admStudyear.YearNameE,
                AdmSemster = x.AdmSemster,
                GradeID = x.AdmSemster.AdmAcademicYear.AcademicID,
                YearID = x.AdmSemster.admStudyear.YearID

            }).FirstOrDefault();
        }

        public void Create(QurterViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AdmQurter _Qurter = new AdmQurter
            {
                SID = model.SID,
                QID = model.QID,
                QStudyWeeks = model.QStudyWeeks,
                QStudyWeeksAbs = model.QStudyWeeksAbs,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now
            };
            Context.AdmQurters.Add(_Qurter);
            Context.SaveChanges();

        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Qurter = Context.AdmQurters.Where(x => x.ID == ID).FirstOrDefault();
            if (_Qurter != null)
            {

                _Qurter.IsDeleted = true;
                _Qurter.DeletedbyID = _dbUser.ID;
                _Qurter.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }


        public QurterViewModel Update(QurterViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();


            var Quter = Context.AdmQurters.Where(x => x.QID == model.QID && x.AdmSemster.admStudyear.YearID == model.YearID && x.AdmSemster.SemNameE == model.SemNameE);

            foreach (var item in Quter)
            {
                item.QStudyWeeks = model.QStudyWeeks;
                item.QStudyWeeksAbs = model.QStudyWeeksAbs;
                item.ModifiedbyID = _dbUser.ID;
                item.ModifiedDate = DateTime.Now;
            }


            Context.SaveChanges();


            return model;

        }


        public QurterViewModel Create()
        {
            throw new NotImplementedException();
        }


    }
}
