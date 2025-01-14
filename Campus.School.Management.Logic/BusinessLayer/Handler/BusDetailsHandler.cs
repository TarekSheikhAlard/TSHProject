using System;
using System.Collections.Generic;
using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System.Linq;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
  public  class BusDetailsHandler : IRepository<BusDetailsViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public BusDetailsViewModel Create()
        {
            BusDetailsViewModel model = new BusDetailsViewModel();
            
            return model;
        }

        public void Create(BusDetailsViewModel model)
        {
            BusDetail _BusDetails = new BusDetail();
            _BusDetails.BusID = model.BusID;
            _BusDetails.Employee_ID = model.Employee_ID;
            _BusDetails.IsSuperVisor = model.IsSuperVisor;
            _BusDetails.LicenseExpiryDate = model.LicenseExpiryDate;
           
            Context.BusDetails.Add(_BusDetails);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<BusDetailsViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public BusDetailsViewModel GetById(int ID)
        {
            return Context.BusDetails.Where(c => c.BusDetailsID == ID)
                  .Select(c => new BusDetailsViewModel
                  {
                      BusID = c.BusID,
                      BusDetailsID = c.BusDetailsID,
                      Employee_ID = c.Employee_ID,
                      IsSuperVisor = c.IsSuperVisor,
                      LicenseExpiryDate = c.LicenseExpiryDate
                  }).FirstOrDefault();
        }

        public BusDetailsViewModel Update(BusDetailsViewModel model)
        {
            BusDetail _BusDetails = Context.BusDetails.Where(c => c.BusDetailsID == model.BusDetailsID).FirstOrDefault();
            if (_BusDetails != null)
            {
                _BusDetails.BusID = model.BusID;
                _BusDetails.Employee_ID = model.Employee_ID;
                _BusDetails.IsSuperVisor = model.IsSuperVisor;
                _BusDetails.LicenseExpiryDate = model.LicenseExpiryDate;

                Context.SaveChanges();
            }
            return model;

        }
    }
}
