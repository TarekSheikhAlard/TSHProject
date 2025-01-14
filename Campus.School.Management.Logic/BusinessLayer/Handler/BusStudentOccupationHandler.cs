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
    public class BusStudentOccupationHandler : IRepository<BusStudentOccupationViewModel>

    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        
        public void Create(BusStudentOccupationViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            BusStudentOccupation _BusStudentOccupation = new BusStudentOccupation()
            {
                Ref_Id=model.Ref_Id,
                BusID =model.BusID,
                EviningTrip=model.EviningTrip, MorningTrip=model.MorningTrip,
                CreatedbyID = _dbUser.ID,
                CreatedDate =DateTime.Now,
                ModifiedDate =DateTime.Now, DeletedDate=DateTime.Now, SchoolID=_dbUser.SchoolID
            };
            Context.BusStudentOccupations.Add(_BusStudentOccupation);
            Context.SaveChanges();
        }

        public void Upsert(BusStudentOccupationViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var busStudent = Context.BusStudentOccupations.Where(x => x.Ref_Id == model.Ref_Id).FirstOrDefault();
            if (busStudent != null) {
                busStudent.BusID = model.BusID;
                busStudent.MorningTrip = model.MorningTrip;
                busStudent.EviningTrip = model.EviningTrip;
                busStudent.ModifiedbyID = _dbUser.ID;
                busStudent.ModifiedDate = DateTime.Now;
                busStudent.IsDeleted = false;
            }
            else {

                BusStudentOccupation _BusStudentOccupation = new BusStudentOccupation()
                {
                    Ref_Id = model.Ref_Id,
                    BusID = model.BusID,
                    EviningTrip = model.EviningTrip,
                    MorningTrip = model.MorningTrip,
                    CreatedbyID = _dbUser.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DeletedDate = DateTime.Now,
                    SchoolID = _dbUser.SchoolID
                };
                Context.BusStudentOccupations.Add(_BusStudentOccupation);
            }
            Context.SaveChanges();
        }


        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _BusStudentOccupations = Context.BusStudentOccupations.Where(c => c.ID == ID).FirstOrDefault();
            if (_BusStudentOccupations != null)
            {
                _BusStudentOccupations.DeletedbyID = _dbUser.ID;
                _BusStudentOccupations.IsDeleted = true;
                _BusStudentOccupations.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<BusStudentOccupationViewModel> GetAll()
        {
            return Context.BusStudentOccupations.Where(x => x.IsDeleted == false)
            .Select(x => new BusStudentOccupationViewModel { ID = x.ID,
                Ref_Id = x.Ref_Id,
             BusID = x.BusID, EviningTrip = x.EviningTrip.Value,
                MorningTrip = x.MorningTrip.Value }).ToList();
        }

        public BusStudentOccupationViewModel GetById(int ID)
        {
            return Context.BusStudentOccupations.Where(x => x.IsDeleted == false&&x.ID==ID)
            .Select(x => new BusStudentOccupationViewModel { ID = x.ID,
                Ref_Id = x.Ref_Id,
            BusID = x.BusID, EviningTrip = x.EviningTrip.Value, MorningTrip = x.MorningTrip.Value }).FirstOrDefault();
        }

        public BusStudentOccupationViewModel Update(BusStudentOccupationViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _BusStudentOccupations = Context.BusStudentOccupations.Where(c => c.ID ==model.ID).FirstOrDefault();

            _BusStudentOccupations.BusID = model.BusID;
            _BusStudentOccupations.MorningTrip = model.MorningTrip;
            _BusStudentOccupations.EviningTrip = model.EviningTrip;
            _BusStudentOccupations.ModifiedbyID = _dbUser.ID;
            _BusStudentOccupations.ModifiedDate = DateTime.Now;
            Context.SaveChanges();
            return model;
        }



        public BusStudentOccupationViewModel Create()
        {
            throw new NotImplementedException();

        }
    }
}
