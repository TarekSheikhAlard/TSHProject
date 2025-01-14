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
    public class CityHandler : IRepository<CityViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public List<CityViewModel> GetAll()
        {
            return Context.AdmCities.Where(x=>x.IsDeleted==false).Select(c => new CityViewModel
            {
                CityAr = c.CityAr,
                CityEn = c.CityEn,
                NationalityID=c.NationalityID,
                ID = c.ID,
                NationalityEn =c.Nationality.NationalityEn
                         
            }).ToList();
        }

        public List<CityViewModel> GetAllByCompanyID(int CompanyId)
        {
        
            return Context.AdmCities.Where(x => x.IsDeleted == false).Select(c => new CityViewModel
            {
                CityAr = c.CityAr,
                CityEn = c.CityEn,
                NationalityID = c.NationalityID,
                ID = c.ID,
                NationalityEn = c.Nationality.NationalityEn

            }).ToList();
        }
        public CityViewModel GetById(int ID)
        {
         return   Context.AdmCities.Where(c => c.ID == ID&&c.IsDeleted==false).Select(c => new CityViewModel { ID = c.ID, CityAr = c.CityAr, CityEn = c.CityEn,NationalityID=c.NationalityID , NationalityEn =c.Nationality.NationalityEn}).FirstOrDefault();
        }
        public void Create(CityViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AdmCity _city = new AdmCity()
            {
                CityAr = model.CityAr,
                CityEn=model.CityEn,
                NationalityID=model.NationalityID,
                DeletedDate =DateTime.Now,
                ModifiedDate =DateTime.Now,
                CreatedDate =DateTime.Now,
                CreatedbyID = _dbUser.ID,
                CompanyID=model.CompanyID
            };
            Context.AdmCities.Add(_city);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _city = Context.AdmCities.Where(c => c.ID == ID&& c.IsDeleted==false).FirstOrDefault();
            if (_city != null)
            {
                _city.DeletedbyID = _dbUser.ID;
                _city.IsDeleted = true;
                _city.ModifiedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public CityViewModel Update(CityViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _city = Context.AdmCities.Where(c => c.ID == model.ID).FirstOrDefault();
            _city.CityAr = model.CityAr;
            _city.CityEn = model.CityEn;
            _city.NationalityID = model.NationalityID;
            _city.ModifiedDate = DateTime.Now;
            _city.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;
        }

        public CityViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
