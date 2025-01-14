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
    public class AccountStudentFeesListHandler : IRepository<AccountStudentFeesListViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();



        public List<AccountStudentFeesListViewModel> GetAll()
        {
            return Context.AccountStudentFeesLists.Where(x => x.IsDeleted == false)
            .Select(x => new AccountStudentFeesListViewModel
            {
                StudyFees = x.StudyFees,
                BooksFees = x.BooksFees,
                AcademicYearID = x.AcademicYearID,
                ID = x.ID,
                AcademicYearName = x.AdmAcademicYear.AcadmicName
            }).ToList();
        }

        public void Create(AccountStudentFeesListViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccountStudentFeesList _AccountStudentFeesLists = new AccountStudentFeesList()
            {
                AcademicYearID = model.AcademicYearID,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                BooksFees=model.BooksFees, 
                StudyFees=model.StudyFees,
                
             };
            Context.AccountStudentFeesLists.Add(_AccountStudentFeesLists);
            Context.SaveChanges();





        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccountStudentFeesLists = Context.AccountStudentFeesLists.Where(x => x.IsDeleted == false && x.ID == ID).FirstOrDefault();


            if (_AccountStudentFeesLists != null)
            {
                _AccountStudentFeesLists.IsDeleted = true;
                _AccountStudentFeesLists.DeletedbyID = _dbUser.ID;
                _AccountStudentFeesLists.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

    

        public AccountStudentFeesListViewModel GetById(int ID)
        {
            return Context.AccountStudentFeesLists.Where(x => x.IsDeleted == false && x.ID == ID)
            .Select(x => new AccountStudentFeesListViewModel
            {
                StudyFees = x.StudyFees,
                BooksFees = x.BooksFees,
                AcademicYearID = x.AcademicYearID,
                ID = x.ID,
                AcademicYearName = x.AdmAcademicYear.AcadmicName
            }).FirstOrDefault();
        }

        public AccountStudentFeesListViewModel Update(AccountStudentFeesListViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccountStudentFeesLists = Context.AccountStudentFeesLists.Where(x => x.IsDeleted == false && x.ID ==model. ID).FirstOrDefault();
            _AccountStudentFeesLists.BooksFees = model.BooksFees;
            _AccountStudentFeesLists.StudyFees = model.StudyFees;
            _AccountStudentFeesLists.ModifiedDate = DateTime.Now;
            _AccountStudentFeesLists.ModifiedbyID = _dbUser.ID;
            _AccountStudentFeesLists.AcademicYearID = model.AcademicYearID;
            
            Context.SaveChanges();
            return model;
        }


        public AccountStudentFeesListViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
