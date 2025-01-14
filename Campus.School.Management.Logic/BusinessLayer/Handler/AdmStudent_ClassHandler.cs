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
    public class AdmStudent_ClassHandler : IRepository<AdmStudent_ClassViewModel>

    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public AdmStudent_ClassViewModel Create()
        {
            throw new NotImplementedException();
        }

        public void Create(AdmStudent_ClassViewModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<AdmStudent_ClassViewModel> GetAll()
        {
            return Context.AdmStudent_Class.Where(x => x.IsDeleted == false)
            .Select(x => new AdmStudent_ClassViewModel {
            ClassID = x.ClassID,
            ClassName = x.AdmClassRoom.ClassName,
            StudentAcdID = x.StudentAcdID,
            YearID = x.YearID,
            YearName = x.admStudyear.YearNameE,
            ID = x.ID,
            StudentID = x.StudentID,
            StudentName = x.AdmStudent.NameEn
          }).ToList();
        }

        public AdmStudent_ClassViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public AdmStudent_ClassViewModel Update(AdmStudent_ClassViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
