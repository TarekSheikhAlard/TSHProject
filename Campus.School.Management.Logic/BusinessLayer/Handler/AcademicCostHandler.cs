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
    public class AcademicCostHandler : IRepository<AcademicCostViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public List<AcademicCostViewModel> GetAll()
        {
            return Context.AccAcademicCosts.Select(x => new AcademicCostViewModel
            { ID = x.ID,
              Academic_YearID = x.Academic_YearID,
              Year_Cost = x.Year_Cost,
             AcadmicName = x.AdmAcademicYear.AcadmicName }).ToList();

        }

        public AcademicCostViewModel GetById(int ID)
        {
            return Context.AccAcademicCosts.Where(x => x.ID == ID).Select(x => new AcademicCostViewModel
            {
                ID = x.ID,
                Academic_YearID = x.Academic_YearID,
                Year_Cost = x.Year_Cost,
                AcadmicName = x.AdmAcademicYear.AcadmicName
            }).SingleOrDefault();
        }

        public void Create(AcademicCostViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccAcademicCost _AccAcademicCost = new AccAcademicCost
            {
                Academic_YearID = model.Academic_YearID,
                Year_Cost = model.Year_Cost
            };

             Context.AccAcademicCosts.Add(_AccAcademicCost);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccAcademicCost = Context.AccAcademicCosts.Where(c => c.ID == ID).FirstOrDefault();
            if (_AccAcademicCost != null)
            {
                Context.AccAcademicCosts.Remove(_AccAcademicCost);
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public AcademicCostViewModel Update(AcademicCostViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccAcademicCost = Context.AccAcademicCosts.Where(c => c.ID == model.ID).FirstOrDefault();
            if (_AccAcademicCost != null)
            {   _AccAcademicCost.Academic_YearID = model.Academic_YearID;
                _AccAcademicCost.Year_Cost = model.Year_Cost;
                Context.SaveChanges();
                
            }
            return model;
        }

        public AcademicCostViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
