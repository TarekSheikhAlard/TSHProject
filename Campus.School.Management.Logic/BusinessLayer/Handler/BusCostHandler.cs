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
  public  class BusCostHandler : IRepository<BusCostViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public List<BusCostViewModel> GetAll()
        {
            return Context.AccBusCosts.Where(x => x.IsDeleted == false && x.SchoolID==_dbUser.SchoolID).Select(x => new BusCostViewModel
            {
                ID = x.ID,
                BusID = x.BusID.Value,
                BusNo = x.BusNo,
                DoubleTripCost = x.DoubleTripCost,
                SingleTripCost = x.SingleTripCost,
                Station = x.Station

            }).ToList();
        }
        
        public BusCostViewModel GetById(int ID)
        {
            return Context.AccBusCosts.Where(x => x.IsDeleted == false && x.ID==ID).Select(x => new BusCostViewModel
           {
                ID = x.ID,
                BusID = x.BusID.Value,
                BusNo = x.BusNo,
                DoubleTripCost = x.DoubleTripCost,
                SingleTripCost = x.SingleTripCost,
                Station = x.Station

               }).SingleOrDefault();

        }
        public BusCostViewModel GetByBusId(int ID)
        {
            return Context.AccBusCosts.Where(x => x.IsDeleted == false && x.BusID == ID).Select(x => new BusCostViewModel
            {
                ID = x.ID,
                BusID = x.BusID.Value,
                BusNo = x.BusNo,
                DoubleTripCost = x.DoubleTripCost,
                SingleTripCost = x.SingleTripCost,
                Station = x.Station

            }).SingleOrDefault();

        }

        public BusCostViewModel Update(BusCostViewModel model)
        {
          
            var _buscost = Context.AccBusCosts.Where(x => x.ID == model.ID).FirstOrDefault();
            _buscost.BusID = model.BusID;
            _buscost.BusNo = model.BusNo;
            _buscost.ModifiedDate = DateTime.Now;
            _buscost.Station = model.Station;
            _buscost.SingleTripCost = model.SingleTripCost;
            _buscost.DoubleTripCost = model.DoubleTripCost;
            _buscost.ModifiedbyID = _dbUser.ID;
            _buscost.ModifiedDate = DateTime.Now;
            Context.SaveChanges();
            return model;
        }
        
        public void Create(BusCostViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AccBusCost _AccBusCost = new AccBusCost()
            {
                 BusID=model.BusID,
                 BusNo =Context.Buses.Where(x=>x.BusID==model.BusID).FirstOrDefault().BusNo,
                 DoubleTripCost =model.DoubleTripCost,
                SingleTripCost =model.SingleTripCost,
                Station =model.Station,
                CompanyID=_dbUser.CompanyID,
                SchoolID=_dbUser.SchoolID,
                 ModifiedDate =DateTime.Now,
                CreatedDate =DateTime.Now,
                DeletedDate =DateTime.Now,
                CreatedbyID = _dbUser.ID
            };



            Context.AccBusCosts.Add(_AccBusCost);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccBusCost = Context.AccBusCosts.Where(x => x.ID == ID).FirstOrDefault();
            if (_AccBusCost != null)
            {
                _AccBusCost.DeletedbyID = _dbUser.ID;
                _AccBusCost.IsDeleted = true;
                _AccBusCost.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }
        
        public BusCostViewModel Create()
        {
            throw new NotImplementedException();
        }



    }
}
