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
    public class UnitsMeasurementsHandler : IRepository<UnitsMeasurementsViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(UnitsMeasurementsViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            UnitsMeasurement unitsMeasurement = new UnitsMeasurement
            {
                UnitNameAr = model.UnitNameAr,
                UnitNameEn = model.UnitNameEn,
                UnitNumber = model.UnitNumber,
                SymbolAr = model.SymbolAr,
                SymbolEn = model.SymbolEn,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                DeletedDate = DateTime.Now,
                CompanyID = _dbUser.CompanyID,
                SchoolID=_dbUser.SchoolID,
            };
            Context.UnitsMeasurements.Add(unitsMeasurement);
            Context.SaveChanges();

        }

        public UnitsMeasurementsViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var Units = Context.UnitsMeasurements.Where(x => x.Id == ID && x.IsDeleted == false).FirstOrDefault();

            if (Units != null)
            {
                Units.DeletedbyID = _dbUser.ID;
                Units.IsDeleted = true;
                Units.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<UnitsMeasurementsViewModel> GetAll()
        {
            return Context.UnitsMeasurements.Where(x=> x.IsDeleted == false).Select(x=>new UnitsMeasurementsViewModel {

                Id = x.Id,
                UnitNameAr = x.UnitNameAr,
                UnitNameEn = x.UnitNameEn,
                UnitNumber = x.UnitNumber,
                SymbolAr = x.SymbolAr,
                SymbolEn = x.SymbolEn

            }).ToList();
        }
        public List<UnitsMeasurementsViewModel> GetAllCompanyId(int companyId)
        {
            return Context.UnitsMeasurements.Where(x => x.CompanyID== companyId&& x.IsDeleted == false).Select(x => new UnitsMeasurementsViewModel
            {

                Id = x.Id,
                UnitNameAr = x.UnitNameAr,
                UnitNameEn = x.UnitNameEn,
                UnitNumber = x.UnitNumber,
                SymbolAr = x.SymbolAr,
                SymbolEn = x.SymbolEn

            }).ToList();
        }

        public UnitsMeasurementsViewModel GetById(int ID)
        {
            return Context.UnitsMeasurements.Where(x =>x.Id==ID&& x.IsDeleted == false).Select(x => new UnitsMeasurementsViewModel
            {

                Id = x.Id,
                UnitNameAr = x.UnitNameAr,
                UnitNameEn = x.UnitNameEn,
                UnitNumber = x.UnitNumber,
                SymbolAr = x.SymbolAr,
                SymbolEn = x.SymbolEn

            }).FirstOrDefault();
        }

        public UnitsMeasurementsViewModel Update(UnitsMeasurementsViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var Units = Context.UnitsMeasurements.Where(x => x.Id == model.Id && x.IsDeleted == false).FirstOrDefault();

            Units.UnitNameAr = model.UnitNameAr;
            Units.UnitNameEn = model.UnitNameEn;
            Units.UnitNumber = model.UnitNumber;
            Units.SymbolAr = model.SymbolAr;
            Units.SymbolEn = model.SymbolEn;
            Context.SaveChanges();

            return model;

        }
    }
}
