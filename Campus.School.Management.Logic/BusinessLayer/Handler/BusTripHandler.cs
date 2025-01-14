using System;
using System.Collections.Generic;
using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System.Linq;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class BusTripHandler : IRepository<BusTripViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();


        public BusTripViewModel Create()
        {
            BusTripViewModel model = new BusTripViewModel();

            return model;
        }

        public void Create(BusTripViewModel model)
        {
            BusTrip _BusTrip = new BusTrip();
            _BusTrip.BusID = model.BusID;
            _BusTrip.BusStation = model.BusStation;
            _BusTrip.DropTime = model.DropTime;
            _BusTrip.PickUpTime = model.PickUpTime;
            _BusTrip.Notes = model.Notes;
            _BusTrip.TripType = model.TripType;
            _BusTrip.SchoolID = _dbUser.SchoolID;
            Context.BusTrips.Add(_BusTrip);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            var item = Context.BusTrips.Where(c => c.BusTripID == ID).FirstOrDefault();
            if (item != null) Context.BusTrips.Remove(item);
            Context.SaveChanges();
            return true;
        }

        public List<BusTripViewModel> GetAll()
        {
            return Context.BusTrips.Where(x=>x.SchoolID==_dbUser.SchoolID).Select(c => new BusTripViewModel
            {
                BusID = c.BusID,
                BusStation = c.BusStation,
                DropTime = c.DropTime,
                Notes = c.Notes,
                PickUpTime = c.PickUpTime,
                TripType = c.TripType,
                BusTripID = c.BusTripID,
                BusNo=c.Bus.BusNo

            }).ToList();
        }

        public BusTripViewModel GetById(int ID)
        {
            return Context.BusTrips.Where(c => c.BusTripID == ID)
                    .Select(c => new BusTripViewModel
                    {
                        BusID = c.BusID,
                        BusTripID = c.BusTripID,
                        DropTime = c.DropTime,
                        Notes = c.Notes,
                        PickUpTime = c.PickUpTime,
                        TripType = c.TripType
                    }).FirstOrDefault();
        }

        public BusTripViewModel Update(BusTripViewModel model)
        {
            var _BusTrip = Context.BusTrips.Where(c => c.BusTripID == model.BusTripID).FirstOrDefault();
            if (_BusTrip != null)
            {
                _BusTrip.BusID = model.BusID;
                _BusTrip.BusStation = model.BusStation;
                _BusTrip.DropTime = model.DropTime;
                _BusTrip.PickUpTime = model.PickUpTime;
                _BusTrip.Notes = model.Notes;
                _BusTrip.TripType = model.TripType;
                Context.SaveChanges();
            }
            return model;
        }
    }
}
