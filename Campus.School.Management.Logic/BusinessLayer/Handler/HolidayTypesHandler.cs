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
  
    public class HolidayTypesHandler : IRepository<HolidayTypesViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public List<HolidayTypesViewModel> GetAll()
        {
            return context.HolidayTypes.Where(x => x.IsDeleted == false && x.CompanyID == _dbUser.CompanyID).Select(x => new HolidayTypesViewModel { HolidayID = x.HolidayID, HolidayNameAr = x.HolidayNameAr, HolidayNameEn = x.HolidayNameEn,Days = x.Days,Rate = x.Rate
            }).ToList();
        }
        public List<HolidayTypesViewModel> GetAllByCompanyID(int CompanyId)
        {
            return context.HolidayTypes
                            .Where(x => x.IsDeleted == false && x.CompanyID == CompanyId)
                            .Select(x => new HolidayTypesViewModel
                            {
                                HolidayID = x.HolidayID,
                                HolidayNameAr = x.HolidayNameAr,
                                HolidayNameEn = x.HolidayNameEn,
                                Days = x.Days,
                                Rate =x.Rate
                            }
                            ).OrderByDescending(x => x.HolidayID).ToList();
        }
        public HolidayTypesViewModel GetById(int ID)
        {
            return context.HolidayTypes.Where(x => x.HolidayID == ID && x.IsDeleted == false)
           .Select(x => new HolidayTypesViewModel { HolidayID = x.HolidayID, HolidayNameAr = x.HolidayNameAr, HolidayNameEn = x.HolidayNameEn, Days = x.Days,Rate = x.Rate,Notes=x.Notes
           })
           .FirstOrDefault();
        }

        public void Create(HolidayTypesViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            HolidayType _Holiday = new HolidayType
            {
                HolidayNameAr = model.HolidayNameAr,
                HolidayNameEn = model.HolidayNameEn,   
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                Days= model.Days,
                Rate=model.Rate,
                Notes=model.Notes,         
                CompanyID = _dbUser.CompanyID
            };
            context.HolidayTypes.Add(_Holiday);
            context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Holiday = context.HolidayTypes.Where(x => x.HolidayID == ID && x.IsDeleted == false).FirstOrDefault();
            if (_Holiday != null)
            {
                _Holiday.DeletedbyID = _dbUser.ID;
                _Holiday.IsDeleted = true;
                _Holiday.DeletedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        public HolidayTypesViewModel Update(HolidayTypesViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Holiday = context.HolidayTypes.Where(x => x.HolidayID == model.HolidayID).FirstOrDefault();
            _Holiday.HolidayNameAr = model.HolidayNameAr;
            _Holiday.HolidayNameEn = model.HolidayNameEn;
            _Holiday.ModifiedDate = DateTime.Now;
            _Holiday.Days = model.Days;
            _Holiday.Rate = model.Rate;
            _Holiday.Notes = model.Notes;
            _Holiday.ModifiedbyID = _dbUser.ID;

            context.SaveChanges();
            return model;
        }

        public HolidayTypesViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
