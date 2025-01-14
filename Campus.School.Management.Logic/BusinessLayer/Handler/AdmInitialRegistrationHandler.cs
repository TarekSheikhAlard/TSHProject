using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class AdmInitialRegistrationHandler : IRepository<AdmInitialStudRegistrationViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        string lang = Utility.getCultureCookie(false).ToLower();
        public void Create(AdmInitialStudRegistrationViewModel model)
        {
            AdmInitialStudRegistration admInitialStudRegistration = new AdmInitialStudRegistration()
            {
                GradeID = model.GradeID,
                FullNameAr = model.FullNameAr,
                FullNameEn = model.FullNameEn,
                Nationality = model.Nationality,
                Gender = model.Gender,
                Mobile = model.Mobile,
                Phone = model.Phone,
                DOB = model.DOB,
                LastSchool = model.LastSchool,
                Address = model.Address,
                BirthPlace = model.BirthPlace,
                CreatedbyID = _dbUser.CreatedbyID,
                CreatedDate = DateTime.Now,
                SchoolID = _dbUser.SchoolID,
                isRegistered = model.isRegistered
                

            };
            context.AdmInitialStudRegistrations.Add(admInitialStudRegistration);
            context.SaveChanges();
        }

        public AdmInitialStudRegistrationViewModel Create()
        {
            throw new NotImplementedException();
        }

        public int DeleteReturnGradeID(int ID)
        {

            var InitialRegistration = context.AdmInitialStudRegistrations.Where(x => x.id == ID).FirstOrDefault();

            var gradeID = InitialRegistration.GradeID;
            InitialRegistration.IsDeleted = true;
            InitialRegistration.DeletedbyID = _dbUser.ID;
            InitialRegistration.DeletedDate = DateTime.Now;

            context.SaveChanges();
            return gradeID;

        }

     

        public List<AdmInitialStudRegistrationViewModel> GetAll()
        {
            return context.AdmInitialStudRegistrations.Where(x=>x.IsDeleted==false && x.SchoolID==_dbUser.SchoolID).Select(x => new AdmInitialStudRegistrationViewModel
            {
                id = x.id,
                GradeID = x.GradeID,
                GradeName = lang == "ar" ? x.AdmGradesSchool.GradeSchoolNameA : x.AdmGradesSchool.GradeSchoolNameE,
                NationalityName = lang == "ar" ? x.Nationality1.NationalityAr : x.Nationality1.NationalityAr,
                FullNameAr = x.FullNameAr,
                FullNameEn = x.FullNameEn,
                DOB = x.DOB,
                BirthPlace = x.BirthPlace,
                Nationality = x.Nationality,
                Address = x.Address,
                Gender = x.Gender,
                Phone = x.Phone,
                Mobile = x.Mobile,
                LastSchool = x.LastSchool,
                Result = x.Result,
                CreatedbyID = x.CreatedbyID,
                  isRegistered = x.isRegistered

            }).ToList();
        }
        public List<AdmInitialStudRegistrationViewModel> GetAllByGrades(int ID)
        {
          
            return context.AdmInitialStudRegistrations.Where(x =>x.GradeID ==ID && x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID).Select(x => new AdmInitialStudRegistrationViewModel
            {
                id = x.id,
                GradeID = x.GradeID,
                GradeName = lang =="ar"?x.AdmGradesSchool.GradeSchoolNameA: x.AdmGradesSchool.GradeSchoolNameE,
                NationalityName = lang == "ar" ? x.Nationality1.NationalityAr : x.Nationality1.NationalityAr,
                FullNameAr = x.FullNameAr,
                FullNameEn = x.FullNameEn,
                DOB = x.DOB,
                BirthPlace = x.BirthPlace,
                Nationality = x.Nationality,
                Address = x.Address,
                Gender = x.Gender,
                Phone = x.Phone,
                Mobile = x.Mobile,
                LastSchool = x.LastSchool,
                Result=x.Result,
                CreatedbyID = x.CreatedbyID,
                isRegistered = x.isRegistered

            }).ToList();
        }

        public AdmInitialStudRegistrationViewModel GetById(int ID)
        {
            return context.AdmInitialStudRegistrations.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID&&x.id==ID).Select(x => new AdmInitialStudRegistrationViewModel
            {
                id = x.id,
                GradeID = x.GradeID,
                GradeName = lang == "ar" ? x.AdmGradesSchool.GradeSchoolNameA : x.AdmGradesSchool.GradeSchoolNameE,
                NationalityName = lang == "ar" ? x.Nationality1.NationalityAr : x.Nationality1.NationalityAr,
                FullNameAr = x.FullNameAr,
                FullNameEn = x.FullNameEn,
                DOB = x.DOB,
                BirthPlace = x.BirthPlace,
                Nationality = x.Nationality,
                Address = x.Address,
                Gender = x.Gender,
                Phone = x.Phone,
                Mobile = x.Mobile,
                LastSchool = x.LastSchool,
                Result = x.Result,
                CreatedbyID = x.CreatedbyID,
                isRegistered = x.isRegistered

            }).FirstOrDefault();
        }

        public AdmInitialStudRegistrationViewModel Update(AdmInitialStudRegistrationViewModel model)
        {
            var InitialRegistration = context.AdmInitialStudRegistrations.Where(x => x.id == model.id).FirstOrDefault();

            InitialRegistration.GradeID = model.GradeID;
            InitialRegistration.FullNameAr = model.FullNameAr;
            InitialRegistration.FullNameEn = model.FullNameEn;
            InitialRegistration.DOB = model.DOB;
            InitialRegistration.BirthPlace = model.BirthPlace;
            InitialRegistration.Gender = model.Gender;
            InitialRegistration.Nationality = model.Nationality;
            InitialRegistration.LastSchool = model.LastSchool;
            InitialRegistration.Phone = model.Phone;
            InitialRegistration.Mobile = model.Mobile;
            InitialRegistration.Address = model.Address;
            InitialRegistration.ModifiedbyID = _dbUser.ID;
            InitialRegistration.ModifiedDate = DateTime.Now;

            context.SaveChanges();
            return model;
        }
        public void SaveResult(int id,string result)
        {
            var InitialRegistration = context.AdmInitialStudRegistrations.Where(x => x.id == id).FirstOrDefault();
            InitialRegistration.Result = result;

            context.SaveChanges();
           
        }


        bool IRepository<AdmInitialStudRegistrationViewModel>.Delete(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
