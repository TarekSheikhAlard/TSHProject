using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class TransportBusesHandler : IRepository<TransportBusesViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public void Create(TransportBusesViewModel model)
        {
            try
            {
                TransportBus transportBus = new TransportBus
                {
                    BusNameAr = model.BusNameAr,
                    BusNameEn = model.BusNameEn,
                    BusNumber = model.BusNumber,
                    Driver = model.Driver,
                    Supervisor = model.Supervisor,
                    TotalSeats = model.TotalSeats,
                    AvaliableSeats = model.TotalSeats,
                    ManufacturerName = model.ManufacturerName,
                    Model = model.Model,
                    PlateNumber = model.PlateNumber,
                    LicenseNumber = model.LicenseNumber,
                    LicenseExpiryDate = model.LicenseExpiryDate,
                    FarestPoint = model.FarestPoint,
                    FarestPointCost = model.FarestPointCost,
                    FarestPointDiscount = model.FarestPointDiscount,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    CreatedbyID = _dbUser.ID,
                    DeletedDate = DateTime.Now,
                    CompanyID = _dbUser.CompanyID,
                    SchoolID = _dbUser.SchoolID
                };

                context.TransportBuses.Add(transportBus);
                context.SaveChanges();
            }
            catch (Exception ex) {
                string s;

            }
        }

        public TransportBusesViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            var Bus = context.TransportBuses.Where(x => x.Id == ID && x.IsDeleted == false &&x.SchoolID == _dbUser.SchoolID).FirstOrDefault();
            if (Bus != null)
            {
                Bus.DeletedbyID = _dbUser.ID;
                Bus.IsDeleted = true;
                Bus.DeletedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<TransportBusesViewModel> GetAll()
        {
            return context.TransportBuses.Where(x =>  x.IsDeleted == false && x.CompanyID == _dbUser.CompanyID &&x.SchoolID ==_dbUser.SchoolID)
                .Select(x => new TransportBusesViewModel
                {
                    Id =x.Id,
                    BusNameAr = x.BusNameAr,
                    BusNameEn = x.BusNameEn,
                    BusNumber = x.BusNumber,
                    LicenseExpiryDate = x.LicenseExpiryDate,
                    LicenseNumber = x.LicenseNumber,
                    PlateNumber = x.PlateNumber,
                    ManufacturerName = x.ManufacturerName,
                    Model = x.Model,
                    Driver = x.Driver,
                    DriverName = x.TransportDriver.DriverNameEn,
                    SupervisorName=x.TransportSupervisor.SupervisorNameEn,
                    Supervisor = x.Supervisor,
                    TotalSeats = x.TotalSeats,
                    FarestPoint = x.FarestPoint,
                    FarestPointCost = x.FarestPointCost,
                    FarestPointDiscount = x.FarestPointDiscount

                }).ToList();
        }

        public TransportBusesViewModel GetById(int ID)
        {
            return context.TransportBuses.Where(x => x.Id == ID && x.IsDeleted == false && x.CompanyID == _dbUser.CompanyID && x.SchoolID == _dbUser.SchoolID)
                  .Select(x => new TransportBusesViewModel
                  {
                      Id = x.Id,
                      BusNameAr = x.BusNameAr,
                      BusNameEn = x.BusNameEn,
                      BusNumber = x.BusNumber,
                      LicenseExpiryDate = x.LicenseExpiryDate,
                      LicenseNumber = x.LicenseNumber,
                      PlateNumber =x.PlateNumber,
                      ManufacturerName=x.ManufacturerName,
                      Model =x.Model,
                      Driver = x.Driver,
                      DriverName = x.TransportDriver.DriverNameEn,
                      SupervisorName = x.TransportSupervisor.SupervisorNameEn,
                      Supervisor =x.Supervisor,
                      TotalSeats = x.TotalSeats,
                      FarestPoint =x.FarestPoint,
                      FarestPointCost=x.FarestPointCost,
                      FarestPointDiscount=x.FarestPointDiscount

                  }).FirstOrDefault();
        }

        public TransportBusesViewModel Update(TransportBusesViewModel model)
        {
            var Bus = context.TransportBuses.Where(x => x.Id == model.Id && x.IsDeleted == false).FirstOrDefault();
            if (Bus != null)
            {

                Bus.BusNameAr = model.BusNameAr;
                Bus.BusNameEn = model.BusNameEn;
                Bus.BusNumber = model.BusNumber;
                Bus.Driver = model.Driver;
                Bus.Supervisor = model.Supervisor;
                Bus.TotalSeats = model.TotalSeats;
                Bus.PlateNumber = model.PlateNumber;
                Bus.ManufacturerName = model.ManufacturerName;
                Bus.Model = model.Model;
                Bus.LicenseNumber = model.LicenseNumber;
                Bus.LicenseExpiryDate = model.LicenseExpiryDate;
                Bus.FarestPoint = model.FarestPoint;
                Bus.FarestPointCost = model.FarestPointCost;
                Bus.FarestPointDiscount = model.FarestPointDiscount;
                Bus.ModifiedbyID = _dbUser.ID;    
                Bus.ModifiedDate = DateTime.Now;
                context.SaveChanges();
              
            }
            return model;
        }
    }
}
