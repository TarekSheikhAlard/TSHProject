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
    public class StudyYearHandler : IRepository<StudyYearViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public List<StudyYearViewModel> GetAll()
        {
            return Context.admStudyears.Select(x => new StudyYearViewModel
            {
              
                YearID = x.YearID,
                YearNameEn = x.YearNameE,
                YearNameAr = x.YearNameA,
                YearStDate = x.YearStDate,
                YearEdDate = x.YearEdDate,
                //YearEdDateHijri = x.YearEdDateHijri,
                //YearStDateHijri = x.YearStDateHijri,
                CurrentYear = x.CurrentYear,
                CurrentQ = x.CurrentQ,
                CurrentSem = x.CurrentSem,
                CurrentWeek = x.CurrentWeek,
                Is_Update = x.Is_Update,
                Is_Upload = x.Is_Upload
            }).ToList();

        }

        public List<StudyYearViewModel> GetAllByCompany(int CompanyId, int SchoolId)
        {
            return Context.admStudyears
                                       .Select(x => new StudyYearViewModel
                                       {
                                           //IsDefault = x.IsDefault,
                                           YearID = x.YearID,
                                           YearNameEn = x.YearNameE,
                                           YearNameAr = x.YearNameA,
                                           YearStDate = x.YearStDate,
                                           YearEdDate = x.YearEdDate,
                                           //YearEdDateHijri = x.YearEdDateHijri,
                                           //YearStDateHijri = x.YearStDateHijri,
                                           CurrentYear = x.CurrentYear,
                                           CurrentQ = x.CurrentQ,
                                           CurrentSem = x.CurrentSem,
                                           CurrentWeek = x.CurrentWeek,
                                           Is_Update = x.Is_Update,
                                           Is_Upload = x.Is_Upload
                                       }).OrderByDescending(x => x.YearID).ToList();

        }

        public StudyYearViewModel GetById(int ID)
        {
            return Context.admStudyears.Select(x => new StudyYearViewModel
            {
                YearID = x.YearID,
                YearNameAr = x.YearNameA,
                YearNameEn = x.YearNameE,
                YearStDate = x.YearStDate,
                YearEdDate = x.YearEdDate,
                //YearEdDateHijri = x.YearEdDateHijri,
                //YearStDateHijri = x.YearStDateHijri,
                CurrentYear = x.CurrentYear,
                CurrentQ = x.CurrentQ,
                CurrentSem = x.CurrentSem,
                CurrentWeek = x.CurrentWeek,
                Is_Update = x.Is_Update,
                Is_Upload = x.Is_Upload
            }).FirstOrDefault();
        }

        public void Create(StudyYearViewModel model)
        {
            throw new NotImplementedException();
            //SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            //if (model.IsDefault == true)
            //{

            //    var yearToUpdate = Context.admStudyears.Where(s => s.YearID !=model.YearID);

            //    // update LastName for all Persons in personsToUpdate
            //    foreach (admStudyear s in yearToUpdate)
            //    {
            //        s.IsDefault = false;                    
            //    }
            //    Context.SaveChanges();
            //}
            //admStudyear _studyYear = new admStudyear
            //{
            //    YearNameA = model.YearNameAr,
            //    YearNameE = model.YearNameEn,
            //    YearStDate = model.YearStDate,
            //    YearEdDate = model.YearEdDate,
            //    YearEdDateHijri = model.YearEdDateHijri,
            //    YearStDateHijri = model.YearStDateHijri,
            //    CurrentYear = model.CurrentYear,
            //    CurrentQ = model.CurrentQ,
            //    CurrentSem = model.CurrentSem,
            //    CurrentWeek = model.CurrentWeek,
            //    Is_Update = model.Is_Update,
            //    Is_Upload = model.Is_Upload,
            //    CreatedbyID = _dbUser.ID,
            //    CreatedDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now,
            //    DeletedDate = DateTime.Now,
            //    CompanyID = model.CompanyID,
            //    SchoolID = model.SchoolID,
            //    IsDefault = model.IsDefault

            //};
            //Context.admStudyears.Add(_studyYear);
            //Context.SaveChanges();

        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
            //SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //var _studyYear = Context.admStudyears.Where(x => x.YearID == ID && x.IsDeleted == false).FirstOrDefault();
            //if (_studyYear != null)
            //{
            //    _studyYear.IsDeleted = true;
            //    _studyYear.DeletedbyID = _dbUser.ID;
            //    _studyYear.DeletedDate = DateTime.Now;
            //    Context.SaveChanges();
            //    return true;
            //}
            //else return false;
        }


        public StudyYearViewModel Update(StudyYearViewModel model)
        {
            throw new NotImplementedException();

            //if (model.IsDefault == true)
            //{

            //    var yearToUpdate = Context.admStudyears.Where(s => s.YearID != model.YearID);

            //    // update LastName for all Persons in personsToUpdate
            //    foreach (admStudyear s in yearToUpdate)
            //    {
            //        s.IsDefault = false;
            //    }
            //    Context.SaveChanges();
            //}
            //SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //var _studyYear = Context.admStudyears.Where(x => x.YearID == model.YearID).FirstOrDefault();
            //_studyYear.YearNameA = model.YearNameAr;
            //_studyYear.YearNameE = model.YearNameEn;
            //_studyYear.YearStDate = model.YearStDate;
            //_studyYear.YearEdDate = model.YearEdDate;
            //_studyYear.YearEdDateHijri = model.YearEdDateHijri;
            //_studyYear.YearStDateHijri = model.YearStDateHijri;
            //_studyYear.CurrentYear = model.CurrentYear;
            //_studyYear.CurrentQ = model.CurrentQ;
            //_studyYear.CurrentSem = model.CurrentSem;
            //_studyYear.CurrentWeek = model.CurrentWeek;
            //_studyYear.Is_Update = model.Is_Update;
            //_studyYear.Is_Upload = model.Is_Upload;
            //_studyYear.ModifiedbyID = _dbUser.ID;
            //_studyYear.ModifiedDate = DateTime.Now;
            //_studyYear.IsDefault = model.IsDefault;
            //Context.SaveChanges();


            //return model;

        }


        public StudyYearViewModel Create()
        {
            throw new NotImplementedException();
        }


    }
}
