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
    public class PhysicalYearHandler : IRepository<PhysicalYearViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public List<PhysicalYearViewModel> GetAll()
        {
            return Context.PhysicalYears.Where(c => c.IsDeleted == false).Select(c => new PhysicalYearViewModel
            {
                PhysicalYearID = c.PhysicalYearID,
                PhysicalYearName = c.PhysicalYearName,
                IsCurrent = c.IsCurrent,
                FromDate = c.FromDate,
                EndDate = c.EndDate

            }).ToList();

        }

        public List<PhysicalYearViewModel> GetAllByCompany(int companyId, int schoolId)
        {
            return Context.PhysicalYears.Where(c => c.IsDeleted == false && c.CompanyID == companyId && c.SchoolID == schoolId).Select(c => new PhysicalYearViewModel
            {
                PhysicalYearID = c.PhysicalYearID,
                PhysicalYearName = c.PhysicalYearName,
                IsCurrent = c.IsCurrent,
                FromDate = c.FromDate,
                EndDate = c.EndDate

            }).ToList();

        }

        public PhysicalYearViewModel GetById(int ID)
        {
            return Context.PhysicalYears.Where(c => c.PhysicalYearID == ID && c.IsDeleted == false).Select(c => new PhysicalYearViewModel
            {
                PhysicalYearID = c.PhysicalYearID,
                PhysicalYearName = c.PhysicalYearName,
                IsCurrent = c.IsCurrent,
                FromDate = c.FromDate,
                EndDate = c.EndDate

            }).FirstOrDefault();
        }

        public PhysicalYearViewModel GetCurrent()
        {
            return Context.PhysicalYears.Where(c => c.IsCurrent == true && c.IsDeleted == false).Select(c => new PhysicalYearViewModel
            {
                PhysicalYearID = c.PhysicalYearID,
                PhysicalYearName = c.PhysicalYearName,
                IsCurrent = c.IsCurrent,
                FromDate = c.FromDate,
                EndDate = c.EndDate

            }).FirstOrDefault();
        }

        public void Create(PhysicalYearViewModel model)
        {
            try
            {
                int _last = GetAll().Select(x => new { x.PhysicalYearID }).LastOrDefault().PhysicalYearID;
                bool delete = Delete(_last);
            }
            catch (Exception)
            {
            }

            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            PhysicalYear _PhysicalYear = new PhysicalYear()
            {
                PhysicalYearID = model.PhysicalYearID,
                PhysicalYearName = model.PhysicalYearName,
                IsCurrent = true,
                CompanyID=model.CompanyID,
                SchoolID=model.SchoolID,
                FromDate = model.FromDate,
                EndDate = model.EndDate,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,

            };
            Context.PhysicalYears.Add(_PhysicalYear);
            Context.SaveChanges();
        }

        /// <summary>
        /// update IsCurrent = false when create new one
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool Delete(int ID)
        {
            var _PhysicalYear = Context.PhysicalYears.Where(c => c.PhysicalYearID == ID).FirstOrDefault();
            if (_PhysicalYear != null)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
                _PhysicalYear.DeletedbyID = _dbUser.ID;
                _PhysicalYear.IsCurrent = false;
                _PhysicalYear.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public PhysicalYearViewModel Update(PhysicalYearViewModel model)
        {
            var _PhysicalYear = Context.PhysicalYears.Where(c => c.PhysicalYearID == model.PhysicalYearID).FirstOrDefault();
            _PhysicalYear.PhysicalYearID = model.PhysicalYearID;
            _PhysicalYear.PhysicalYearName = model.PhysicalYearName;
            _PhysicalYear.IsCurrent = true;
            _PhysicalYear.FromDate = model.FromDate;
            _PhysicalYear.EndDate = model.EndDate;
            _PhysicalYear.ModifiedDate = DateTime.Now;
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            _PhysicalYear.ModifiedbyID = _dbUser.ID;

            Context.SaveChanges();
            return model;
        }

        public PhysicalYearViewModel Create()
        {
            PhysicalYearViewModel model = new PhysicalYearViewModel();

            return model;
        }
    }
}
