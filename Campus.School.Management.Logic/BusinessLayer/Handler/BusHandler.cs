using System;
using System.Collections.Generic;
using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System.Linq;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class BusHandler : IRepository<BusViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        private BusTripHandler _BusTripHandler = new BusTripHandler();
        private BusDetailsHandler _BusDetailsHandler = new BusDetailsHandler();
        private EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public void Create(BusViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            //add bus
            Bus _Bus = new Bus
            {
                BusNo = model.BusNo, 
                BusType = model.BusType,
                ChassisNo = model.ChassisNo,
                IssuedFrom = model.IssuedFrom,
                LicenseEnd = model.LicenseEnd,
                LicenseNo = model.LicenseNo,
                LicenseStart = model.LicenseStart,
                Manufactur = model.Manufactur,
                Model = model.Model,
                Year = model.Year,
                PassengersNumber = model.PassengersNumber,
                CreatedbyID=_dbUser.CreatedbyID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                SchoolID=_dbUser.SchoolID,
                CompanyID=_dbUser.CompanyID
            };

            Context.Buses.Add(_Bus);

            Context.SaveChanges();



            //add bus details 
            foreach (var item in model._BusDetails)
            {
                item.BusID = _Bus.BusID;
                _BusDetailsHandler.Create(item);
            }
            //add bus trips
            if (model._BusTrips != null)
                foreach (var item in model._BusTrips)
                {
                    item.BusID = _Bus.BusID;
                    _BusTripHandler.Create(item);
                }

            Context.SaveChanges();

        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Bus = Context.Buses.Where(c => c.BusID == ID).FirstOrDefault();
            if (_Bus != null)
            {

                ////delete all bus details
                //Context.BusDetails.RemoveRange(_Bus.BusDetails);//.Remove(item);

                ////delete all bus Bus Trips
                //Context.BusTrips.RemoveRange(_Bus.BusTrips);//.Remove(item);

                ////delete bus
                //Context.Buses.Remove(_Bus);
                _Bus.DeletedbyID = _dbUser.ID;
                _Bus.IsDeleted = true;
                _Bus.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }

        public BusViewModel Create()
        {
            BusDetailsHandler _BusDetailsHandler = new BusDetailsHandler();
           
            //create bus
            BusViewModel model = new BusViewModel();
            var lastID = Context.Buses.Where(x=> x.SchoolID==_dbUser.SchoolID).OrderByDescending(c => c.BusID).Select(c => c.BusID).FirstOrDefault();
            if (lastID > 0) model.BusID = lastID + 1;
            else model.BusID = 1;

            //create bus details
            model._BusDetails = new List<BusDetailsViewModel>();

            //add supervisor
            var Supervisor = _BusDetailsHandler.Create();
            Supervisor.IsSuperVisor = true;
            Supervisor.BusID = model.BusID;
            Supervisor.Employee = new System.Web.Mvc.SelectList(_EmployeeHandler.GetAllByCompany(_dbUser.CompanyID,_dbUser.SchoolID), "Employee_ID", "Name"+Utility.getCultureCookie(false));

            model._BusDetails.Add(Supervisor);

            //add bus driver
            var driver = _BusDetailsHandler.Create();
            driver.BusID = model.BusID;
            driver.Employee = new System.Web.Mvc.SelectList(_EmployeeHandler.GetAllByCompany(_dbUser.CompanyID, _dbUser.SchoolID), "Employee_ID", "Name" + Utility.getCultureCookie(false));

            model._BusDetails.Add(driver);
            return model;
        }

        public List<BusViewModel> GetAll()
        {
            return Context.Buses.Where(x=> x.IsDeleted == false &&x.SchoolID==_dbUser.SchoolID).Select(c => new BusViewModel
            {
                BusID = c.BusID,
                BusNo = c.BusNo,
                Year = c.Year,
                Model = c.Model,
                Manufactur = c.Manufactur,
                BusType = c.BusType,
                ChassisNo = c.ChassisNo,
                IssuedFrom = c.IssuedFrom,
                LicenseEnd = c.LicenseEnd,
                LicenseNo = c.LicenseNo,
                LicenseStart = c.LicenseStart,
                PassengersNumber = c.PassengersNumber

            }).ToList();


        }

        public List<BusViewModel> GetAllExcluded()
        {
            return Context.Buses.Where(x => x.IsDeleted == false && x.SchoolID == _dbUser.SchoolID
       
            ).Select(c => new BusViewModel
            {
                BusID = c.BusID,
                BusNo = c.BusNo,
                Year = c.Year,
                Model = c.Model,
                Manufactur = c.Manufactur,
                BusType = c.BusType,
                ChassisNo = c.ChassisNo,
                IssuedFrom = c.IssuedFrom,
                LicenseEnd = c.LicenseEnd,
                LicenseNo = c.LicenseNo,
                LicenseStart = c.LicenseStart,
                PassengersNumber = c.PassengersNumber

            }).ToList();


        }


        public BusViewModel GetById(int ID)
        {
            var model = Context.Buses.Where(x => x.IsDeleted == false).Where(c => c.BusID == ID).Select(c => new BusViewModel
            {
                BusID = c.BusID,
                BusNo = c.BusNo,
                Year = c.Year,
                Model = c.Model,
                Manufactur = c.Manufactur,
                BusType = c.BusType,
                ChassisNo = c.ChassisNo,
                IssuedFrom = c.IssuedFrom,
                LicenseEnd = c.LicenseEnd,
                LicenseNo = c.LicenseNo,
                LicenseStart = c.LicenseStart,
                PassengersNumber = c.PassengersNumber
            }).FirstOrDefault();

            //select all bus trip
            var BusTrips = Context.BusTrips.Where(c => c.BusID == model.BusID)
                .Select(c => new BusTripViewModel
                {
                    BusID = c.BusID,
                    BusTripID = c.BusTripID,
                    DropTime = c.DropTime,
                    Notes = c.Notes,
                    PickUpTime = c.PickUpTime,
                    TripType = c.TripType,
                    BusStation = c.BusStation

                })
                .ToList();
            if (BusTrips != null)
            {
                model._BusTrips = new List<BusTripViewModel>();
                foreach (var item in BusTrips)
                    model._BusTrips.Add(item);
            }
            //select all bus details
            var BusDetails = Context.BusDetails.Where(c => c.BusID == model.BusID)
               .Select(c => new BusDetailsViewModel
               {
                   BusID = c.BusID,
                   BusDetailsID = c.BusDetailsID,
                   Employee_ID = c.Employee_ID,
                   IsSuperVisor = c.IsSuperVisor,
                   LicenseExpiryDate = c.LicenseExpiryDate

               })
               .ToList();
            if (BusDetails != null)
            {
                model._BusDetails = new List<BusDetailsViewModel>();
                foreach (var item in BusDetails)
                {
                    item.Employee = new System.Web.Mvc.SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "Name"+Utility.getCultureCookie(false), item.Employee_ID);
                    model._BusDetails.Add(item);
                }
            }
            return model;

        }

        public BusViewModel Update(BusViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Bus = Context.Buses.Where(c => c.BusID == model.BusID).FirstOrDefault();
            if (_Bus != null)
            {
                #region update bus data

                _Bus.BusNo = model.BusNo;
                _Bus.BusType = model.BusType;
                _Bus.ChassisNo = model.ChassisNo;
                _Bus.IssuedFrom = model.IssuedFrom;
                _Bus.LicenseEnd = model.LicenseEnd;
                _Bus.LicenseNo = model.LicenseNo;
                _Bus.LicenseStart = model.LicenseStart;
                _Bus.Manufactur = model.Manufactur;
                _Bus.Model = model.Model;
                _Bus.Year = model.Year;
                _Bus.PassengersNumber = model.PassengersNumber;
                _Bus.ModifiedDate = DateTime.Now;
                _Bus.ModifiedbyID = _dbUser.ID;
                #endregion

                #region bus trips

                var ModelTripListIDs = model._BusTrips.Select(c => c.BusTripID).ToList();
                var DataBaseBusTripList = Context.BusTrips.Where(c => c.BusID == model.BusID).ToList();
                var DataBaseBusTripIDs = Context.BusTrips.Where(c => c.BusID == model.BusID).Select(c => c.BusTripID).ToList();

                //update bus trips
                var busTripListToUpdate = model._BusTrips.Where(c => DataBaseBusTripIDs.Contains(c.BusTripID)).ToList();
                foreach (var item in busTripListToUpdate)
                    _BusTripHandler.Update(item);

                //Delete bus trips
                var DatabaseTripListToDelete = DataBaseBusTripList.Where(c => c.BusID == model.BusID && !ModelTripListIDs.Contains(c.BusTripID)).ToList();
                foreach (var item in DatabaseTripListToDelete)
                    _BusTripHandler.Delete(item.BusTripID);

                //add bus trip
                var DatabaseTripListToAdd = model._BusTrips.Where(c => !DataBaseBusTripIDs.Contains(c.BusTripID)).ToList();
                foreach (var item in DatabaseTripListToAdd)
                    _BusTripHandler.Create(item);

                #endregion

                #region bus Detais
                //var DataBaseBusDetailIDs = Context.BusDetails.Where(c => c.BusID == model.BusID).Select(c => c.BusDetailsID).ToList();

                ////update bus trips
                //var BusDetailListToUpdate = model._BusDetails.Where(c => DataBaseBusDetailIDs.Contains(c.BusDetailsID)).ToList();
                foreach (var item in model._BusDetails)
                    _BusDetailsHandler.Update(item);

                #endregion

                Context.SaveChanges();
            }

            return model;
        }
    }
}
