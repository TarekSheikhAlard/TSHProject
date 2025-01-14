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
    public class TransportDriverHandler : IRepository<TransportDriverViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public void Create(TransportDriverViewModel model)
        {
            TransportDriver transportDriver = new TransportDriver
            {
                DriverNameEn = model.DriverNameEn,
                DriverNameAr =model.DriverNameAr,
                LicenseNumber=model.LicenseNumber,
                LicenseExpiryDate =model.LicenseExpiryDate,
                EmployeeId=model.EmployeeId,
                ModifiedDate= DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                DeletedDate = DateTime.Now,
                CompanyID = _dbUser.CompanyID,
                SchoolID = _dbUser.SchoolID
            };

            context.TransportDrivers.Add(transportDriver);
            context.SaveChanges();

        }

        public TransportDriverViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            var driver = context.TransportDrivers.Where(x => x.Id == ID && x.IsDeleted == false).FirstOrDefault();

            if (driver != null) {

                driver.DeletedbyID = _dbUser.ID;
                driver.IsDeleted = true;
                driver.DeletedDate = DateTime.Now;
                context.SaveChanges();
                return true;

            }

            return false;
          
        }

        public List<TransportDriverViewModel> GetAll()
        {
            return context.TransportDrivers.Where(x => x.IsDeleted == false && x.CompanyID == _dbUser.CompanyID)
        .Select(x => new TransportDriverViewModel
        {
            Id = x.Id,
            DriverNameAr = x.DriverNameAr,
            DriverNameEn = x.DriverNameEn,
            LicenseNumber = x.LicenseNumber,
            LicenseExpiryDate =x.LicenseExpiryDate,
            EmployeeId = x.EmployeeId


        }).ToList();

        }
        public List<TransportDriverViewModel> GetAllFreeDrivers()
        {
            var bookedDrivers = context.TransportBuses.Select(c => c.Driver);

            return context.TransportDrivers.Where(x => !bookedDrivers.Contains(x.Id) && x.IsDeleted == false && x.CompanyID == _dbUser.CompanyID)
        .Select(x => new TransportDriverViewModel
        {
            Id = x.Id,
            DriverNameAr = x.DriverNameAr,
            DriverNameEn = x.DriverNameEn,
            LicenseNumber = x.LicenseNumber,
            LicenseExpiryDate = x.LicenseExpiryDate,
            EmployeeId = x.EmployeeId


        }).ToList();

        }

        public TransportDriverViewModel GetById(int ID)
        {
            return context.TransportDrivers.Where(x => x.Id == ID && x.IsDeleted == false && x.CompanyID == _dbUser.CompanyID)
          .Select(x => new TransportDriverViewModel
          {
              Id = x.Id,
              DriverNameAr = x.DriverNameAr,
              DriverNameEn =x.DriverNameEn,
              LicenseNumber =x.LicenseNumber,
              LicenseExpiryDate =x.LicenseExpiryDate,
              EmployeeId =x.EmployeeId
             

          }).FirstOrDefault();


        }

        public TransportDriverViewModel Update(TransportDriverViewModel model)
        {
            var driver = context.TransportDrivers.Where(x => x.Id == model.Id && x.IsDeleted == false).FirstOrDefault();
            if (driver != null)
            {
                driver.DriverNameAr = model.DriverNameAr;
                driver.DriverNameEn = model.DriverNameEn;
                driver.LicenseNumber = model.LicenseNumber;
                driver.LicenseExpiryDate = model.LicenseExpiryDate;
                driver.EmployeeId = model.EmployeeId;
                driver.ModifiedbyID = _dbUser.ID;
                driver.ModifiedDate = DateTime.Now;

                context.SaveChanges();

            }
            return model; 
        }
    }
}
