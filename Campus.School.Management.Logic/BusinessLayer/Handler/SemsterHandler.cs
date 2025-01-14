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
    public class SemsterHandler : IRepository<SemsterViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public List<SemsterViewModel> GetAll()
        {

            List<SemsterViewModel> model = Context.AdmSemsters.Where(x => x.IsDeleted == false).Select(x => new SemsterViewModel
            {
                SID = x.SID,
                YearID = x.YearID,
                Year = x.admStudyear.YearNameE,
                GradeID = x.GradeID,
                Grade = x.AdmAcademicYear.AcadmicName,
                SemID = x.SemID,
                SemNameA = x.SemNameA,
                SemNameE = x.SemNameE,
                SemStDate = x.SemStDate,
                SemEdDate = x.SemEdDate,
                QuartersCount = x.QuartersCount,
                Is_Update = x.Is_Update,
                Is_Upload = x.Is_Upload

            }).GroupBy(x => new { x.YearID, x.SemID }).Select(x => x.FirstOrDefault()).ToList();

            return model;

    }


        public SemsterViewModel GetById(int ID)
        {
            return Context.AdmSemsters.Where(x => x.IsDeleted == false && x.SID == ID).Select(x => new SemsterViewModel
            {
                SID = x.SID,
                YearID = x.YearID,
                Year = x.admStudyear.YearNameE,
                GradeID = x.GradeID,
                Grade = x.AdmAcademicYear.AcadmicName,
                SemID = x.SemID,
                SemNameA = x.SemNameA,
                SemNameE = x.SemNameE,
                SemStDate = x.SemStDate,
                SemEdDate = x.SemEdDate,
                QuartersCount = x.QuartersCount,
                Is_Update = x.Is_Update,
                Is_Upload = x.Is_Upload,
                check = false
                
            }).FirstOrDefault();
        }

        public void Create(SemsterViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            if (model.SemID == 1)
            {
                model.SemNameE = "frist Semester";
                model.SemNameA = "الترم الاول";
            }
            else
            {
                model.SemNameE = "Second Semester";
                model.SemNameA = "الترم الثانى";
            }
        

            var Grade_ID_list = Context.AdmAcademicYears.Where(x=>x.IsDeleted==false).Select(x => x.AcademicID).ToList();
           

            foreach( var item in Grade_ID_list)
            {

                AdmSemster _Semster = new AdmSemster
                {
                    YearID = model.YearID,
                    GradeID = item,
                    SemID = model.SemID,
                    SemNameA = model.SemNameA,
                    SemNameE = model.SemNameE,
                    SemStDate = model.SemStDate,
                    SemEdDate = model.SemEdDate,
                    QuartersCount = model.QuartersCount,
                    Is_Update = model.Is_Update,
                    Is_Upload = model.Is_Upload,
                    CreatedbyID = _dbUser.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DeletedDate = DateTime.Now

                };
                Context.AdmSemsters.Add(_Semster);
            
            }

            Context.SaveChanges();

      List<int> SID_list  =Context.AdmSemsters.Where(x => x.YearID == model.YearID && x.SemID == model.SemID).Select(x => x.SID).ToList();

        
            foreach (var item in SID_list)
            {
                for (int i = 1; i <= model.QuartersCount; i++)
                {
                    AdmQurter _Qurter = new AdmQurter
                    {
                        SID = item,
                        QID =i,
                        QStudyWeeks = (i<3)? 6:0,
                        CreatedbyID=_dbUser.ID,
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now,
                        DeletedDate=DateTime.Now
                    
                    };
                    Context.AdmQurters.Add(_Qurter);

                }
                Context.SaveChanges();

            }
           

        }


        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _Semster = Context.AdmSemsters.Where(x => x.SID == ID).FirstOrDefault();
            if (_Semster != null)
            {
                _Semster.IsDeleted = true;
                _Semster.DeletedbyID = _dbUser.ID;
                _Semster.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }


        public SemsterViewModel Update(SemsterViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var _Semster = Context.AdmSemsters.Where(x => x.SemID == model.SemID && x.YearID == model.YearID);

            var SID = _Semster.Select(x => x.SID);

           
            if (_Semster.FirstOrDefault().QuartersCount != model.QuartersCount)
            {

                // delete all Quter associate with semetsers edit 
                foreach (var items in SID)
                {
                  List<AdmQurter> Quter = Context.AdmQurters.Where(x => x.SID == items).ToList();

                    foreach (var item in Quter)
                    {
                        Context.AdmQurters.Remove(item);
                    }
                    
                }
                //-------------------------------------------------------------------------------
                // create Quters for semster edit
                foreach (var item1 in SID)
                {
                    for (int i = 1; i <= model.QuartersCount; i++)
                    {
                        AdmQurter _Qurter = new AdmQurter
                        {
                            SID = item1,
                            QID = i,
                            QStudyWeeks = (i < 3) ? 6 : 0,
                            CreatedbyID = _dbUser.ID,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            DeletedDate = DateTime.Now

                        };
                        Context.AdmQurters.Add(_Qurter);
                    }
                }
            }

      
                    // edit all semseter with same year and semcode 
                    foreach (var item in _Semster)
                    {
                        //item.YearID = item.YearID;
                        //item.GradeID = item.GradeID;
                        //item.SemID = item.SemID;
                        //item.SemNameA = item.SemNameA;
                        //item.SemNameE = item.SemNameE;
                        item.SemStDate = model.SemStDate;
                        item.SemEdDate = model.SemEdDate;
                        item.QuartersCount = model.QuartersCount;
                        //item.Is_Update = item.Is_Update;
                        //item.Is_Upload = item.Is_Upload;
                        item.ModifiedbyID = _dbUser.ID;
                        item.ModifiedDate = DateTime.Now;

                    }
                    Context.SaveChanges();

                    return model;

             
            }

        public SemsterViewModel Create()
        {
            SemsterViewModel model = new ViewModel.SemsterViewModel();
            model.check = true;
            return model;
        }


    }
}
