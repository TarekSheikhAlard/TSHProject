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
    public class EmployeeReturnHistoryHandler:IRepository<EmployeeReturnHistoryViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public List<EmployeeReturnHistoryViewModel> GetAll()
        {
            return Context.EmployeeReturnHistories.Where(x => x.IsDeleted == false && x.AdmEmployee.SchoolID == _dbUser.SchoolID).Select(x => new EmployeeReturnHistoryViewModel
            {
                ID=x.ID,
                ReturnDate=x.ReturnDate,
                Notes=x.Notes,
                CreatedbyID=x.CreatedbyID,
                CreatedDate=x.CreatedDate
                   
            }).ToList();

        }

        public List<EmployeeReturnHistoryViewModel> GetAllById(int ID)
        {
            return Context.EmployeeReturnHistories.Where(x => x.EmployeeID == ID && x.AdmEmployee.SchoolID == _dbUser.SchoolID).Select(x => new EmployeeReturnHistoryViewModel
            {
                ID = x.ID,
                EmployeeID=x.EmployeeID,
                ReturnDate = x.ReturnDate,
                Notes = x.Notes,
                CreatedbyID = x.CreatedbyID,
                CreatedDate = x.CreatedDate
            }).ToList();
        }

        public void Create(EmployeeReturnHistoryViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            EmployeeReturnHistory _EmployeeReturnHistory = new EmployeeReturnHistory
            {
                EmployeeID = model.EmployeeID,
                ReturnDate = model.ReturnDate,
                Notes=model.Notes,
                CreatedbyID= _dbUser.CreatedbyID,
                CreatedDate=DateTime.Now
            };

            Context.EmployeeReturnHistories.Add(_EmployeeReturnHistory);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _EmployeeReturnHistory = Context.EmployeeReturnHistories.Where(c => c.ID == ID && c.AdmEmployee.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_EmployeeReturnHistory != null)
            {
                _EmployeeReturnHistory.DeletedbyID = _dbUser.ID;
                _EmployeeReturnHistory.IsDeleted = true;
                _EmployeeReturnHistory.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public EmployeeReturnHistoryViewModel Update(EmployeeReturnHistoryViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _EmployeeReturnHistory = Context.EmployeeReturnHistories.Where(c => c.ID == model.ID && c.AdmEmployee.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (_EmployeeReturnHistory != null)
            {

                _EmployeeReturnHistory.ReturnDate = model.ReturnDate;
                _EmployeeReturnHistory.Notes=model.Notes;
                Context.SaveChanges();

            }
            return model;
        }

        public EmployeeReturnHistoryViewModel Create()
        {
            throw new NotImplementedException();
        }

        public EmployeeReturnHistoryViewModel GetById(int ID)
        {
            return Context.EmployeeReturnHistories.Where(x => x.ID == ID && x.AdmEmployee.SchoolID == _dbUser.SchoolID).Select(x => new EmployeeReturnHistoryViewModel
            {
                ID = x.ID,
                EmployeeID=x.EmployeeID,
                ReturnDate = x.ReturnDate,
                Notes = x.Notes,
                CreatedbyID = x.CreatedbyID,
                CreatedDate = x.CreatedDate
            }).FirstOrDefault();
            
        }
    }
}
