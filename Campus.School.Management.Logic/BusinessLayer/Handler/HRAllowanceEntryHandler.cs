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
    public class HRAllowanceEntryHandler : IRepository<HRAllowanceEntryViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();
        public void Create(HRAllowanceEntryViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            HRAllowanceEntry _hRAllowanceEntry = new HRAllowanceEntry
            {
                Allowance = model.Allowance,
                AllowanceType = model.AllowanceType,
                Employee_ID = model.Employee_ID,
                CREATED_DATE = DateTime.Now,
                CREATED_BY = _dbUser.ID.ToString(),
                IS_ACTIVE = true
               
               
                      
            };
            context.HRAllowanceEntries.Add(_hRAllowanceEntry);
            context.SaveChanges();      
        }

        public HRAllowanceEntryViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _hRAllowanceEntry = context.HRAllowanceEntries.Where(x => x.ID == ID && x.IS_ACTIVE == true).FirstOrDefault();
            if (_hRAllowanceEntry != null)
            {
                _hRAllowanceEntry.DELETED_BY = _dbUser.ID.ToString();
                _hRAllowanceEntry.IS_ACTIVE = false;
                _hRAllowanceEntry.DELETED_DATE = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            else return false;

           
        }

        public List<HRAllowanceEntryViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<HRAllowanceEntryViewModel> GetAllByEmployeeID(int ID)
        {
            return context.HRAllowanceEntries.Where(x => x.Employee_ID == ID && x.IS_ACTIVE == true).Select(x => new HRAllowanceEntryViewModel
            {
                ID = x.ID,
                Employee_ID = x.Employee_ID,
                Allowance = x.Allowance,
                AllowanceType = x.AllowanceType,
                CREATED_BY = x.CREATED_BY,
                CREATED_DATE = x.CREATED_DATE

            }).ToList();
            
        }

        public HRAllowanceEntryViewModel GetById(int ID)
        {
            return context.HRAllowanceEntries.Where(x => x.ID == ID).Select(x => new HRAllowanceEntryViewModel
            {
                ID = x.ID,
                Employee_ID = x.Employee_ID,
                Allowance = x.Allowance,
                AllowanceType = x.AllowanceType,
                CREATED_BY = x.CREATED_BY,
                CREATED_DATE = x.CREATED_DATE

            }).FirstOrDefault();
  
        }

        public HRAllowanceEntryViewModel Update(HRAllowanceEntryViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
