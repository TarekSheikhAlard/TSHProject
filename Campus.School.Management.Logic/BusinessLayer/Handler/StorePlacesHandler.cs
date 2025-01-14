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
    public class StorePlacesHandler : IRepository<StorePlacesViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(StorePlacesViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
           StorePlace storePlace = new StorePlace
            {
                PlaceNameEn = model.PlaceNameEn,
                PlaceNameAr = model.PlaceNameAr,
                PlaceNumber = model.PlaceNumber,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                DeletedDate = DateTime.Now,
                CompanyID = _dbUser.CompanyID,
                SchoolID = _dbUser.SchoolID,
            };
            Context.StorePlaces.Add(storePlace);
            Context.SaveChanges();

        }

        public StorePlacesViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var storeplace = Context.StorePlaces.Where(x => x.Id == ID && x.IsDeleted == false).FirstOrDefault();

            if (storeplace != null)
            {
                storeplace.DeletedbyID = _dbUser.ID;
                storeplace.IsDeleted = true;
                storeplace.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<StorePlacesViewModel> GetAll()
        {
            return Context.StorePlaces.Where(x => x.IsDeleted == false).Select(x => new StorePlacesViewModel
            {

                Id = x.Id,
                PlaceNameEn = x.PlaceNameEn,
                PlaceNameAr = x.PlaceNameAr,
                PlaceNumber = x.PlaceNumber


            }).ToList();
        }
        public List<StorePlacesViewModel> GetAllCompanyId(int companyId)
        {
            return Context.StorePlaces.Where(x => x.CompanyID == companyId && x.IsDeleted == false).Select(x => new StorePlacesViewModel
            {

                Id = x.Id,
                PlaceNameEn = x.PlaceNameEn,
                PlaceNameAr = x.PlaceNameAr,
                PlaceNumber = x.PlaceNumber

            }).ToList();
        }

        public StorePlacesViewModel GetById(int ID)
        {
            return Context.StorePlaces.Where(x => x.Id == ID && x.IsDeleted == false).Select(x => new StorePlacesViewModel
            {

                Id = x.Id,
                PlaceNameEn = x.PlaceNameEn,
                PlaceNameAr = x.PlaceNameAr,
                PlaceNumber = x.PlaceNumber


            }).FirstOrDefault();
        }

        public StorePlacesViewModel Update(StorePlacesViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var Units = Context.StorePlaces.Where(x => x.Id == model.Id && x.IsDeleted == false).FirstOrDefault();

            Units.PlaceNumber = model.PlaceNumber;
            Units.PlaceNameAr = model.PlaceNameAr;
            Units.PlaceNameEn = model.PlaceNameEn;
            Context.SaveChanges();
            return model;

        }
    }
}
