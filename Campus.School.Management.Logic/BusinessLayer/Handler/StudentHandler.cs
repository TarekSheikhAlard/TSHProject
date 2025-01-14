using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.BusinessLayer;
using Campus.School.Management.Logic.DataBaseLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class StudentHandler : IRepository<StudentViewModel>
    {
        
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
       
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public List<StudentViewModel> GetAll()
        {

            //var school = Context.AdmSchools.Where(x => x.SchoolID == _dbUser.SchoolID && x.IsDeleted == false)
            //           .Select(x => new
            //           {
            //               connection = x.ConnectionString,

            //           }).SingleOrDefault();

            //string connection = school.connection;

            //ExternalSchoolContext externalSchoolContext = new ExternalSchoolContext(connection);

            //Database.SqlQuery<AccStudentFeeViewModel>(query, id)

            return Context.AdmStudents.Select(c => new StudentViewModel
            {   
                ID = c.ID,
                StudentAcdID = c.StudAcdID,
                StudRefID=c.Ref_Id,
                YearName=c.StudentReference.admStudyear.YearNameE,
                //GradeID = c.GradeID,
              
                NameEn = c.NameFt +" "+c.NameM ,
                
                ArabicName = c.NameA,
                //ClassID=c.ClassID,
                //className =Context.AdmClassRooms.Where(x=>x.ClassID==c.ClassID).Select(x=>x.ClassName).FirstOrDefault(),
                
                BirthDate = c.BirthDate,
                BirthPlace = c.BirthPlace,
                Address = c.Address,
                Sex = c.Sex,

                NationalityID = c.IqamaNo,
                NationalityEn = c.Nationality,

                //NationalityAr = c.NationalityAr,
                IqamaNo = c.IqamaNo,
                IqamaStartDate = c.IqamaIsDate,
                IqamaEndDate = c.IqamaEdDate,

                PassportNo = c.PassNo,
                PassportStartDate = c.PassIsDate,
                PassEndDate = c.PassEdDate,

                Mobile = c.Mobile,
                Tel = c.Tel,
                Mail = c.Mail,
                ParentJob = c.ParenJob,
                ParentAddress = c.PAddress,
                ParentTell = c.PTell,

                Requirments = c.Requirments,
                Orignalfees = c.Origfees,
             
                LastEduShcool1 = c.LastEduShcool1,
                LastEduShcool2 = c.LastEduShcool2,
                LastEduCity1 = c.LastEduCity1,
                LastEduCity2 = c.LastEduCity2,
                LastEduDateFrom1 = c.LastEduDateFrom1,
                LastEduDateFrom2 = c.LastEduDateFrom2,

                LastEduFull1 = c.LastEduFull1,
                LastEduFull2 = c.LastEduFull2,
                PersonStm = c.PersonStm,
                Refrences = c.Refrences,
                ParentMail = c.PMail,
                Parentmobile = c.Pmobile,

                IqamaPlace = c.IqamaPlace,
                IsArchives = c.IsArchives.Value,
                PrevSchool = c.PrevSch,
                ParentEqmEndDate = c.ParEqmExpDate,
                AdmDate = c.AdmDate,

                GraduateDate = c.GraduateDate,
                Is_Update = c.Is_Update.Value,

                Is_Upload = c.Is_Upload.Value,
                PrevSchAr = c.PrevSch,
                Student_Img ="",

                StudentConfigCount = Context.AccStudentConfigs.Where(x => x.StudentReference.StudAcdID == c.StudentReference.StudAcdID && x.IsActive == true).Count()


            }).ToList();
        }

        public StudentViewModel GetById(int ID)
        {
            return Context.AdmStudents.Where(c => c.ID == ID)
                  .Select(c => new StudentViewModel
                  {
                      ID = c.ID,
                      StudentAcdID = c.StudAcdID,
                      YearName = c.StudentReference.admStudyear.YearNameE,
                      //GradeID = c.GradeID,

                      NameEn = c.NameFt + " " + c.NameM,

                      ArabicName = c.NameA,
                      //ClassID=c.ClassID,
                      //className =Context.AdmClassRooms.Where(x=>x.ClassID==c.ClassID).Select(x=>x.ClassName).FirstOrDefault(),

                      BirthDate = c.BirthDate,
                      BirthPlace = c.BirthPlace,
                      Address = c.Address,
                      Sex = c.Sex,

                      NationalityID = c.IqamaNo,
                      NationalityEn = c.Nationality,

                      //NationalityAr = c.NationalityAr,
                      IqamaNo = c.IqamaNo,
                      IqamaStartDate = c.IqamaIsDate,
                      IqamaEndDate = c.IqamaEdDate,

                      PassportNo = c.PassNo,
                      PassportStartDate = c.PassIsDate,
                      PassEndDate = c.PassEdDate,

                      Mobile = c.Mobile,
                      Tel = c.Tel,
                      Mail = c.Mail,
                      ParentJob = c.ParenJob,
                      ParentAddress = c.PAddress,
                      ParentTell = c.PTell,

                      Requirments = c.Requirments,
                      Orignalfees = c.Origfees,

                      LastEduShcool1 = c.LastEduShcool1,
                      LastEduShcool2 = c.LastEduShcool2,
                      LastEduCity1 = c.LastEduCity1,
                      LastEduCity2 = c.LastEduCity2,
                      LastEduDateFrom1 = c.LastEduDateFrom1,
                      LastEduDateFrom2 = c.LastEduDateFrom2,

                      LastEduFull1 = c.LastEduFull1,
                      LastEduFull2 = c.LastEduFull2,
                      PersonStm = c.PersonStm,
                      Refrences = c.Refrences,
                      ParentMail = c.PMail,
                      Parentmobile = c.Pmobile,

                      IqamaPlace = c.IqamaPlace,
                      IsArchives = c.IsArchives.Value,
                      PrevSchool = c.PrevSch,
                      ParentEqmEndDate = c.ParEqmExpDate,
                      AdmDate = c.AdmDate,

                      GraduateDate = c.GraduateDate,
                      Is_Update = c.Is_Update.Value,

                      Is_Upload = c.Is_Upload.Value,
                      PrevSchAr = c.PrevSch,
                      Student_Img = "",
                      StudentConfigCount = Context.AccStudentConfigs.Where(x => x.StudentReference.StudAcdID == c.StudentReference.StudAcdID && x.IsActive == true).Count()

                  }).FirstOrDefault();
        }


        public List<StudentViewModel> GetAllByGrades(int gradeId, string yearId)
        {


            var query =  Context.StudentReferences.Where(x=>x.YearID==yearId && x.GradeID==gradeId).Select(c => new StudentViewModel
            {
                ID = c.AdmStudent.ID,
                StudentAcdID = c.AdmStudent.StudAcdID,
                YearName = c.admStudyear.YearNameE,
                StudRefID = c.Ref_Id,
                //YearID = c.AdmStudent.StudentReference.YearID,
                GradeName =c.AdmGradesSchool.GradeSchoolNameA,
                NameEn = c.AdmStudent.NameFt + " " + c.AdmStudent.NameM,

                ArabicName = c.AdmStudent.NameA,
                //ClassID=c.AdmStudent.ClassID,
                //className =Context.AdmClassRooms.Where(x=>x.ClassID==c.AdmStudent.ClassID).Select(x=>x.ClassName).FirstOrDefault(),

                BirthDate = c.AdmStudent.BirthDate,
                BirthPlace = c.AdmStudent.BirthPlace,
                Address = c.AdmStudent.Address,
                Sex = c.AdmStudent.Sex,

                NationalityID = c.AdmStudent.IqamaNo,
                NationalityEn = c.AdmStudent.Nationality,

                //NationalityAr = c.AdmStudent.NationalityAr,
                IqamaNo = c.AdmStudent.IqamaNo,
                IqamaStartDate = c.AdmStudent.IqamaIsDate,
                IqamaEndDate = c.AdmStudent.IqamaEdDate,

                PassportNo = c.AdmStudent.PassNo,
                PassportStartDate = c.AdmStudent.PassIsDate,
                PassEndDate = c.AdmStudent.PassEdDate,

                Mobile = c.AdmStudent.Mobile,
                Tel = c.AdmStudent.Tel,
                Mail = c.AdmStudent.Mail,
                ParentJob = c.AdmStudent.ParenJob,
                ParentAddress = c.AdmStudent.PAddress,
                ParentTell = c.AdmStudent.PTell,

                Requirments = c.AdmStudent.Requirments,
                Orignalfees = c.AdmStudent.Origfees,

                LastEduShcool1 = c.AdmStudent.LastEduShcool1,
                LastEduShcool2 = c.AdmStudent.LastEduShcool2,
                LastEduCity1 = c.AdmStudent.LastEduCity1,
                LastEduCity2 = c.AdmStudent.LastEduCity2,
                LastEduDateFrom1 = c.AdmStudent.LastEduDateFrom1,
                LastEduDateFrom2 = c.AdmStudent.LastEduDateFrom2,

                LastEduFull1 = c.AdmStudent.LastEduFull1,
                LastEduFull2 = c.AdmStudent.LastEduFull2,
                PersonStm = c.AdmStudent.PersonStm,
                Refrences = c.AdmStudent.Refrences,
                ParentMail = c.AdmStudent.PMail,
                Parentmobile = c.AdmStudent.Pmobile,

                IqamaPlace = c.AdmStudent.IqamaPlace,
                IsArchives = c.AdmStudent.IsArchives.Value,
                PrevSchool = c.AdmStudent.PrevSch,
                ParentEqmEndDate = c.AdmStudent.ParEqmExpDate,
                AdmDate = c.AdmStudent.AdmDate,

                GraduateDate = c.AdmStudent.GraduateDate,
                Is_Update = c.AdmStudent.Is_Update.Value,

                Is_Upload = c.AdmStudent.Is_Upload.Value,
                PrevSchAr = c.AdmStudent.PrevSch,
                Student_Img = "",
                StudentConfigCount = Context.AccStudentConfigs.Where(x => x.StudentReference.StudAcdID == c.StudAcdID && x.IsActive == true).Count()

            });

            //var result = query;

            //var sql = ((System.Data.Entity.Core.Objects.ObjectQuery)query)
            //.ToTraceString();

            //var s = 0;



            return query.ToList() ;
            //string query = @"SELECT ID, S.StudAcdID, NameFt, NameM, NameFm, NameA, BirthDate, BirthPlace, BirthPlace_Ar, Address, Sex, Nationality, IqamaNo, IqamaIsDate, IqamaEdDate, PassNo, PassIsDate, PassEdDate, Mobile, Tel, Mail, ParenJob, PTell, PAddress, Requirments, Origfees, DisType, LastEduShcool1, LastEduShcool2, LastEduCity1, LastEduCity2, LastEduDateFrom1, LastEduDateFrom2, LastEduFull1, LastEduFull2, PersonStm, Refrences, Pmobile, PMail, NationalityA, IqamaPlace, IsArchives, PrevSch, PrevSch_Ar, ParEqmExpDate, AdmDate, GraduateDate, password, Is_Upload, Is_Update, R.Ref_Id,R.YearID,R.GradeID FROM AdmStudents S
            //                INNER JOIN StudentReference R
            //                ON S.StudAcdID=R.StudAcdID
            //                WHERE R.YearID='@p0' AND R.GradeID='@p1'";


            //var results = Context.Database.SqlQuery<StudentViewModel>(query,gradeId,yearId).ToList();



            //if (yearId == null)
            ////{
            //    return from students in  Context.AdmStudents
            //           join reference in Context.StudentReferences
            //           on students.StudAcdID equals reference.StudAcdID

            //return Context.AdmStudents.Join(Context.StudentReferences,
            //    s=>s.StudAcdID,
            //    r=>r.StudAcdID,
            //    (s,r) => new StudentViewModel() {
            //        ID = s.ID,
            //        StudentAcdID = s.StudAcdID,
            //        StudRefID = r.Ref_Id,
            //        GradeID = r.GradeID,
            //        YearName = r.admStudyear.YearNameE,
            //        NameEn = s.NameFt + " " + s.NameM,
            //        ArabicName = s.NameA,
            //        YearID =r.YearID,
            //        //ClassID=s.ClassID,
            //        //className =Context.AdmClassRooms.Where(x=>x.ClassID==s.ClassID).Select(x=>x.ClassName).FirstOrDefault(),
            //        BirthDate = s.BirthDate,
            //        BirthPlace = s.BirthPlace,
            //        Address = s.Address,
            //        Sex = s.Sex,
            //        NationalityID = s.IqamaNo,
            //        NationalityEn = s.Nationality,
            //        //NationalityAr = s.NationalityAr,
            //        IqamaNo = s.IqamaNo,
            //        IqamaStartDate = s.IqamaIsDate,
            //        IqamaEndDate = s.IqamaEdDate,
            //        PassportNo = s.PassNo,
            //        PassportStartDate = s.PassIsDate,
            //        PassEndDate = s.PassEdDate,
            //        Mobile = s.Mobile,
            //        Tel = s.Tel,
            //        Mail = s.Mail,
            //        ParentJob = s.ParenJob,
            //        ParentAddress = s.PAddress,
            //        ParentTell = s.PTell,
            //        Requirments = s.Requirments,
            //        Orignalfees = s.Origfees,
            //        LastEduShcool1 = s.LastEduShcool1,
            //        LastEduShcool2 = s.LastEduShcool2,
            //        LastEduCity1 = s.LastEduCity1,
            //        LastEduCity2 = s.LastEduCity2,
            //        LastEduDateFrom1 = s.LastEduDateFrom1,
            //        LastEduDateFrom2 = s.LastEduDateFrom2,
            //        LastEduFull1 = s.LastEduFull1,
            //        LastEduFull2 = s.LastEduFull2,
            //        PersonStm = s.PersonStm,
            //        Refrences = s.Refrences,
            //        ParentMail = s.PMail,
            //        Parentmobile = s.Pmobile,
            //        IqamaPlace = s.IqamaPlace,
            //        IsArchives = s.IsArchives.Value,
            //        PrevSchool = s.PrevSch,
            //        ParentEqmEndDate = s.ParEqmExpDate,
            //        AdmDate = s.AdmDate,
            //        GraduateDate = s.GraduateDate,
            //        Is_Update = s.Is_Update.Value,
            //        Is_Upload = s.Is_Upload.Value,
            //        PrevSchAr = s.PrevSch,
            //        Student_Img = "",
            //        StudentConfigCount = Context.AccStudentConfigs.Where(x => x.StudentReference.StudAcdID == s.StudentReference.StudAcdID && x.IsActive == true).Count()
            //    }).Where()
            //    .Select(st=>st).OrderBy(x => x.NameEn).ToList();
            //}
            //else {

            //    return Context.AdmStudents.Select(c => new StudentViewModel
            //    {
            //        ID = c.ID,
            //        StudentAcdID = c.StudAcdID,
            //        YearName = c.StudentReference.admStudyear.YearNameE,
            //        //YearID = c.StudentReference.YearID,

            //        NameEn = c.NameFt + " " + c.NameM,

            //        ArabicName = c.NameA,
            //        //ClassID=c.ClassID,
            //        //className =Context.AdmClassRooms.Where(x=>x.ClassID==c.ClassID).Select(x=>x.ClassName).FirstOrDefault(),

            //        BirthDate = c.BirthDate,
            //        BirthPlace = c.BirthPlace,
            //        Address = c.Address,
            //        Sex = c.Sex,

            //        NationalityID = c.IqamaNo,
            //        NationalityEn = c.Nationality,

            //        //NationalityAr = c.NationalityAr,
            //        IqamaNo = c.IqamaNo,
            //        IqamaStartDate = c.IqamaIsDate,
            //        IqamaEndDate = c.IqamaEdDate,

            //        PassportNo = c.PassNo,
            //        PassportStartDate = c.PassIsDate,
            //        PassEndDate = c.PassEdDate,

            //        Mobile = c.Mobile,
            //        Tel = c.Tel,
            //        Mail = c.Mail,
            //        ParentJob = c.ParenJob,
            //        ParentAddress = c.PAddress,
            //        ParentTell = c.PTell,

            //        Requirments = c.Requirments,
            //        Orignalfees = c.Origfees,

            //        LastEduShcool1 = c.LastEduShcool1,
            //        LastEduShcool2 = c.LastEduShcool2,
            //        LastEduCity1 = c.LastEduCity1,
            //        LastEduCity2 = c.LastEduCity2,
            //        LastEduDateFrom1 = c.LastEduDateFrom1,
            //        LastEduDateFrom2 = c.LastEduDateFrom2,

            //        LastEduFull1 = c.LastEduFull1,
            //        LastEduFull2 = c.LastEduFull2,
            //        PersonStm = c.PersonStm,
            //        Refrences = c.Refrences,
            //        ParentMail = c.PMail,
            //        Parentmobile = c.Pmobile,

            //        IqamaPlace = c.IqamaPlace,
            //        IsArchives = c.IsArchives.Value,
            //        PrevSchool = c.PrevSch,
            //        ParentEqmEndDate = c.ParEqmExpDate,
            //        AdmDate = c.AdmDate,

            //        GraduateDate = c.GraduateDate,
            //        Is_Update = c.Is_Update.Value,

            //        Is_Upload = c.Is_Upload.Value,
            //        PrevSchAr = c.PrevSch,
            //        Student_Img = "",
            //        StudentConfigCount = Context.AccStudentConfigs.Where(x => x.StudentReference.StudAcdID == c.StudentReference.StudAcdID && x.IsActive == true).Count()

            //    }).ToList();
            //}




        }


        public StudentViewModel Update(StudentViewModel model)
        {
            throw new NotImplementedException();
            //SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //var student = Context.AdmStudents.Where(c => c.ID == model.ID).FirstOrDefault();
            //if (student != null)
            //{
            //    student.ID = model.ID;
            //    student. StudentAcdID = model.StudentAcdID;
            //    student.YearID = model.YearID;
            //    student.GradeID = model.GradeID;
            //    student.NameEn = model.NameEn;
            //    student.ArabicName = model.ArabicName;


            //    student.BirthDate = model.BirthDate;
            //    student.BirthPlace = model.BirthPlace;
            //    student.Address = model.Address;
            //    student.Sex = model.Sex;
            //    student .NationalityID = model.NationalityID;
            //    student.IqamaNo = model.IqamaNo;
            //    student.IqamaStartDate = model.IqamaStartDate;
            //    student.IqamaEndDate = model.IqamaEndDate;
            //    student.PassportStartDate = model.PassportStartDate;

            //    student.PassportNo = model.PassportNo;
            //    student.PassEndDate = model.PassEndDate;

            //    student.Mobile = model.Mobile;
            //    student.Tel = model.Tel;
            //    student.Mail = model.Mail;
            //    student.ParentJob = model.ParentJob;
            //    student.ParentAddress = model.ParentAddress;
            //    student.ParentTell = model.ParentTell;

            //    student.Requirments = model.Requirments;
            //    student.Orignalfees = model.Orignalfees;
        

            //    student.LastEduShcool1 = model.LastEduShcool1;
            //    student.LastEduShcool2 = model.LastEduShcool2;
            //    student.LastEduCity1 = model.LastEduCity1;
            //    student.LastEduCity2 = model.LastEduCity2;
            //    student.LastEduDateFrom1 = model.LastEduDateFrom1;
            //    student.LastEduDateFrom2 = model.LastEduDateFrom2;

            //    student.LastEduFull1 = model.LastEduFull1;
            //    student.LastEduFull2 = model.LastEduFull2;
            //    student.PersonStm = model.PersonStm;
            //    student.Refrences = model.Refrences;
            //    student.ParentMail = model.ParentMail;
            //    student.Parentmobile = model.Parentmobile;

            //    //student.NationalityAr = model.NationalityAr;
            //    student.IqamaPlace = model.IqamaPlace;
            //    student.IsArchives = model.IsArchives;
            //    student.PrevSchool = model.PrevSchool;
            //    student.ParentEqmEndDate = model.ParentEqmEndDate;
            //    student.AdmDate = model.AdmDate;

            //    student.PrevSchool = model.PrevSchool;
            //    student.ParentEqmEndDate = model.ParentEqmEndDate;
            //    student.AdmDate = model.AdmDate;
            //    student.GraduateDate = model.GraduateDate;
            //    student.Is_Update = model.Is_Update;
            //    student.Student_Img = model.Student_Img;
            //    student.Is_Upload = model.Is_Upload;
            //    student.PrevSchAr = model.PrevSchAr;
            //    student.ModifiedbyID = _dbUser.ID;
            //    student.ModifiedDate = DateTime.Now;
            //    //student.ClassID = model.ClassID;
            //    Context.SaveChanges();
            //    //var _studentclass = Context.AdmStudent_Class.Where(x => x.IsDeleted == false && x.StudentID == model.ID).FirstOrDefault();
            //    //_studentclass.ClassID = model.ClassID;
            //    //_studentclass.ModifiedDate = DateTime.Now;
            //    //_studentclass.ModifiedbyID = _dbUser.ID;
            //    //Context.SaveChanges();
            //}
            //return model;
        }
        
        public void Create(StudentViewModel model)
        {
            throw new NotImplementedException();
            //SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //AdmStudent studentDetails = new AdmStudent
            //{
            //    StudentAcdID = model.StudentAcdID, YearID = model.YearID,GradeID=model.GradeID,

            //    ArabicName = model.ArabicName, NameEn = model.NameEn, BirthDate = model.BirthDate,

            //    BirthPlace = model.BirthPlace, Address = model.Address, NationalityID = model.NationalityID, Sex = model.Sex,

            //     IqamaNo = model.IqamaNo, IqamaStartDate = model.IqamaStartDate, IqamaEndDate = model.IqamaEndDate,

            //    PassportNo = model.PassportNo, PassportStartDate = model.PassportStartDate,

            //    PassEndDate = model.PassEndDate, Mobile = model.Mobile, Mail = model.Mail, Tel = model.Tel,

            //    ParentAddress = model.ParentAddress, Parentmobile = model.Parentmobile, ParentJob = model.ParentJob,

            //    Requirments = model.Requirments, Orignalfees = model.Orignalfees,

            //    LastEduShcool1 = model.LastEduShcool1, LastEduShcool2 = model.LastEduShcool2, LastEduCity1 = model.LastEduCity1,

            //    LastEduCity2 = model.LastEduCity2, LastEduDateFrom1 = model.LastEduDateFrom1, LastEduDateFrom2 = model.LastEduDateFrom2,
            //    LastEduFull1 = model.LastEduFull1, LastEduFull2 = model.LastEduFull2, PersonStm = model.PersonStm,
            //    Refrences = model.Refrences, ParentMail = model.ParentMail, 
            //    IqamaPlace = model.IqamaPlace, IsArchives = model.IsArchives, PrevSchool = model.PrevSchool,
            //    ParentEqmEndDate = model.IqamaEndDate, AdmDate = model.AdmDate, GraduateDate = model.GraduateDate,
            //    Student_Img= model.Student_Img,
            //    Is_Update = model.Is_Update, Is_Upload = model.Is_Upload,PrevSchAr= model.PrevSchAr ,ParentTell=model.ParentTell,
            //     IsDeleted=false, /*ClassID=model.ClassID,*/
            //    ModifiedDate =DateTime.Now,
            //    CreatedDate =DateTime.Now,
            //    DeletedDate =DateTime.Now,
            //    CreatedbyID = _dbUser.ID,
                
            //};
   
            //Context.AdmStudents.Add(studentDetails);
            //Context.SaveChanges();


        }
   


        public StudentViewModel CreateStudent(StudentViewModel model)
        {
            return model;            //SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //AdmStudent studentDetails = new AdmStudent
            //{
            //    StudentAcdID = model.StudentAcdID,
            //    YearID = model.YearID,
            //    GradeID = model.GradeID,

            //    ArabicName = model.ArabicName,
            //    NameEn = model.NameEn,
            //    BirthDate = model.BirthDate,

            //    BirthPlace = model.BirthPlace,
            //    Address = model.Address,
            //    NationalityID = model.NationalityID,
            //    Sex = model.Sex,

            //    IqamaNo = model.IqamaNo,
            //    IqamaStartDate = model.IqamaStartDate,
            //    IqamaEndDate = model.IqamaEndDate,

            //    PassportNo = model.PassportNo,
            //    PassportStartDate = model.PassportStartDate,

            //    PassEndDate = model.PassEndDate,
            //    Mobile = model.Mobile,
            //    Mail = model.Mail,
            //    Tel = model.Tel,

            //    ParentAddress = model.ParentAddress,
            //    Parentmobile = model.Parentmobile,
            //    ParentJob = model.ParentJob,

            //    Requirments = model.Requirments,
            //    Orignalfees = model.Orignalfees,

            //    LastEduShcool1 = model.LastEduShcool1,
            //    LastEduShcool2 = model.LastEduShcool2,
            //    LastEduCity1 = model.LastEduCity1,

            //    LastEduCity2 = model.LastEduCity2,
            //    LastEduDateFrom1 = model.LastEduDateFrom1,
            //    LastEduDateFrom2 = model.LastEduDateFrom2,
            //    LastEduFull1 = model.LastEduFull1,
            //    LastEduFull2 = model.LastEduFull2,
            //    PersonStm = model.PersonStm,
            //    Refrences = model.Refrences,
            //    ParentMail = model.ParentMail,
            //    IqamaPlace = model.IqamaPlace,
            //    IsArchives = model.IsArchives,
            //    PrevSchool = model.PrevSchool,
            //    ParentEqmEndDate = model.IqamaEndDate,
            //    AdmDate = model.AdmDate,
            //    GraduateDate = model.GraduateDate,
            //    Student_Img = model.Student_Img,
            //    Is_Update = model.Is_Update,
            //    Is_Upload = model.Is_Upload,
            //    PrevSchAr = model.PrevSchAr,
            //    ParentTell = model.ParentTell,
            //    IsDeleted = false,
            //    ModifiedDate = DateTime.Now,
            //    CreatedDate = DateTime.Now,
            //    DeletedDate = DateTime.Now,
            //    CreatedbyID = _dbUser.ID,
            //    ClassID = model.ClassID

            //};
            //AdmStudent_Class _AdmStudent_Class = new AdmStudent_Class()
            //{
            //    StudentAcdID = model.StudentAcdID,
            //    ClassID = model.ClassID,
            //    YearID = model.YearID,
            //    CreatedDate = DateTime.Now,
            //    DeletedDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now,
            //    CreatedbyID = _dbUser.ID

            //};
            //studentDetails.AdmStudent_Class.Add(_AdmStudent_Class);
            //Context.AdmStudents.Add(studentDetails);

            //Context.SaveChanges();
            //model.ID = studentDetails.ID;
            //return model;
        }





        public void RegisterStudent(StudentViewModel model)
        {

            var school = Context.AdmSchools.Where(x => x.SchoolID == _dbUser.SchoolID && x.IsDeleted == false)
                       .Select(x => new
                       {
                           connection = x.ConnectionString,

                       }).SingleOrDefault();

            string connection = school.connection;

            ExternalSchoolContext externalSchoolContext = new ExternalSchoolContext(connection);

            var StudAcdID = new SqlParameter
            {
                ParameterName = "@StudAcdID",
                DbType = System.Data.DbType.Int64,
                Direction = System.Data.ParameterDirection.Output
            };

            var YearID = new SqlParameter
            {
                ParameterName = "@YearID",
                DbType = System.Data.DbType.String,
                Size = 20,
                Direction = System.Data.ParameterDirection.Output
            };

            externalSchoolContext.Database.ExecuteSqlCommand("EXEC dbo.GetStudAcdID_YearID @StudAcdID OUTPUT,@YearID OUTPUT", StudAcdID, YearID);

            StudentReference studentReference = new StudentReference
            {
                SchoolID = _dbUser.SchoolID,
                StudAcdID = (long)StudAcdID.Value,
                YearID = (string)YearID.Value,
                GradeID = (int)model.GradeID,
                IsCurrent = true,
                CreatedDate =DateTime.Now     
            };

            Context.StudentReferences.Add(studentReference);
                
            Context.SaveChanges();

            string[] Name =  new string[3] { "", "", "" };
            for (var n = 0; n < model.NameEn.Split(' ').Length; n++) {
                Name[n] = model.NameEn.Split(' ')[n].ToString();
            }
          
            string query = @"INSERT INTO [dbo].[AdmStudents] 
                            (StudAcdID, NameFt, NameM, NameFm, NameA, BirthDate, BirthPlace, BirthPlace_Ar, [Address], Sex, Nationality, IqamaNo, IqamaIsDate, IqamaEdDate, PassNo, PassIsDate, PassEdDate, Mobile, Tel, Mail, ParenJob, PTell, PAddress, Requirments, LastEduShcool1, LastEduShcool2, LastEduCity1, LastEduCity2, LastEduDateFrom1, LastEduDateFrom2, LastEduFull1, LastEduFull2, PersonStm, Refrences, Pmobile, PMail, NationalityA, IqamaPlace, PrevSch, PrevSch_Ar, ParEqmExpDate, AdmDate, GraduateDate, Ref_Id,) 
                            values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,@p41,@p42,@p43,@p44)";


           externalSchoolContext.Database.ExecuteSqlCommand(query, studentReference.StudAcdID, Name[0], Name[1], Name[2], model.ArabicName, model.BirthDate,model.BirthPlace, model.BirthPlace, model.Address,model.Sex,model.NationalityID,model.IqamaNo,model.IqamaStartDate,model.IqamaEndDate,model.PassportNo,model.PassportStartDate,model.PassEndDate,model.Mobile,model.Tel,model.Mail,model.ParentJob,model.ParentTell,model.ParentAddress,model.Requirments,model.LastEduShcool1,model.LastEduShcool2,model.LastEduCity1,model.LastEduCity2,model.LastEduDateFrom1,model.LastEduDateFrom2,model.LastEduFull1,model.LastEduFull2,model.PersonStm,model.Refrences,model.Parentmobile,model.ParentMail,model.NationalityID,model.IqamaPlace,model.PrevSchool,model.PrevSchAr,model.ParentEqmEndDate,model.AdmDate,model.GraduateDate, studentReference.Ref_Id, studentReference.Ref_Id);

           var initialRegister= Context.AdmInitialStudRegistrations.Where(x => x.id == model.initId && x.IsDeleted == false && x.isRegistered == false).FirstOrDefault();

            initialRegister.isRegistered = true;

            Context.SaveChanges();

        }

        public class StudAcdID_YearID{
            public long StudAcdID { get; set; }
            public string YearID { get; set; }

        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
            //SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //var _Students = Context.AdmStudents.Where(c => c.ID == ID).FirstOrDefault();
            //if (_Students != null)
            //{
            //    _Students.DeletedbyID = _dbUser.ID;
            //    _Students.IsDeleted = true;
            //    _Students.DeletedDate = DateTime.Now;
            //    Context.SaveChanges();
            //    return true;
            //}
            //else return false;
        }
        public StudentViewModel Create()
        {
            throw new NotImplementedException();
        }

            public StudentViewModel Create(int id)
        {
            //throw new NotImplementedException();
            long _StudentAcdID;

            //var Entity = Context.AdmStudents.ToList().LastOrDefault();

            //if (Entity == null)
            //{
            //    _StudentAcdID = 1000;

            //}
            //else
            //{
            //    _StudentAcdID = Entity.StudentAcdID + 1;


            //}
            return Context.AdmInitialStudRegistrations.Where(x => x.id == id).Select(x => new StudentViewModel
            {
              GradeID = x.GradeID,
              NameEn = x.FullNameEn,
              ArabicName = x.FullNameAr,
              BirthDate = x.DOB.ToString(),
              NationalityID=x.Nationality.ToString(),
              Address =x.Address,
              Sex =x.Gender,
              Mobile=x.Mobile,
              Tel=x.Phone,
              LastEduShcool1 = x.LastSchool
            }).FirstOrDefault();

          //  StudentViewModel model = new StudentViewModel();


          //  model.StudentAcdID = 1000;
          ////  model.check = true;
          //  return model;
        }




    }
}
