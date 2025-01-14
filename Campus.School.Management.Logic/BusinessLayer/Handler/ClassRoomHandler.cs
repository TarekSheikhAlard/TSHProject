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
    public class ClassRoomHandler : IRepository<ClassRoomViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();



        public List<ClassRoomViewModel> GetAll()
        {

            return Context.AdmClassRooms.Where(x => x.IsDeleted == false)
            .Select(x => new ClassRoomViewModel { ClassID = x.ClassID,
             AcademicYearID = x.AcademicYearID,
             ClassCode = x.ClassCode,
             ClassLeaderID = x.ClassLeader,
             ClassName = x.ClassName, ClassNameAr = x.ClassNameAr,
             StudentCount = x.StudentCount, AcademicYearName=x.AdmAcademicYear.AcadmicName, ClassLeaderName=x.AdmEmployee.NameE }).ToList();
        }
    

        public void Create(ClassRoomViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AdmClassRoom _ClassRoom = new AdmClassRoom()
            {   AcademicYearID = model.AcademicYearID,
                ClassLeader = model.ClassLeaderID,
                ClassName = model.ClassName,
                ClassNameAr = model.ClassNameAr,
                ClassCode = model.ClassCode,
                StudentCount = model.StudentCount,
                CreatedDate =DateTime.Now,
                CreatedbyID =_dbUser.ID,
                ModifiedDate =DateTime.Now,
                DeletedDate =DateTime.Now
            };
            Context.AdmClassRooms.Add(_ClassRoom);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _ClassRoom = Context.AdmClassRooms.Where(x => x.IsDeleted == false && x.ClassID == ID).FirstOrDefault();


            if (_ClassRoom != null)
            {
                _ClassRoom.IsDeleted = true;
                _ClassRoom.DeletedbyID = _dbUser.ID;
                _ClassRoom.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

      

        public ClassRoomViewModel GetById(int ID)
        {
            return Context.AdmClassRooms.Where(x => x.IsDeleted == false && x.ClassID == ID)
            .Select(x => new ClassRoomViewModel
            {
                ClassID = x.ClassID,
                AcademicYearID = x.AcademicYearID,
                ClassCode = x.ClassCode,
                ClassLeaderID = x.ClassLeader,
                ClassName = x.ClassName,
                ClassNameAr = x.ClassNameAr,
                StudentCount = x.StudentCount,
                 AcademicYearName = x.AdmAcademicYear.AcadmicName,
                ClassLeaderName = x.AdmEmployee.NameE
            }).FirstOrDefault();
        }

        public ClassRoomViewModel Update(ClassRoomViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _ClassRoom = Context.AdmClassRooms.Where(x => x.IsDeleted == false && x.ClassID == model.ClassID).FirstOrDefault();
            _ClassRoom.AcademicYearID = model.AcademicYearID;
            _ClassRoom.ClassCode = model.ClassCode;
            _ClassRoom.ClassLeader = model.ClassLeaderID;
            _ClassRoom.ClassName = model.ClassName;
            _ClassRoom.ClassNameAr = model.ClassNameAr;
            _ClassRoom.StudentCount = model.StudentCount;
            _ClassRoom.ModifiedDate = DateTime.Now;
            _ClassRoom.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;
        }


        public ClassRoomViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
