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
    public class TransportSupervisorHandler : IRepository<TransportSupervisorViewModel>
    {
        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public void Create(TransportSupervisorViewModel model)
        {
            TransportSupervisor transportSupervisor = new TransportSupervisor
            {
                SupervisorNameAr = model.SupervisorNameAr,
                SupervisorNameEn = model.SupervisorNameEn,
                EmployeeId = model.EmployeeId,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                DeletedDate = DateTime.Now,
                CompanyID = _dbUser.CompanyID,
                SchoolID = _dbUser.SchoolID
            };
            context.TransportSupervisors.Add(transportSupervisor);
            context.SaveChanges();
        }

        public TransportSupervisorViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            var supervisor = context.TransportSupervisors.Where(x => x.Id == ID && x.IsDeleted == false).FirstOrDefault();
            if (supervisor != null)
            {
                supervisor.DeletedbyID = _dbUser.ID;
                supervisor.IsDeleted = true;
                supervisor.DeletedDate = DateTime.Now;
                context.SaveChanges();
                return true;


            }
            return false;
        }

        public List<TransportSupervisorViewModel> GetAll()
        {
            return context.TransportSupervisors.Where(x => x.IsDeleted == false && x.CompanyID == _dbUser.CompanyID)
             .Select(x => new TransportSupervisorViewModel
             {
                 Id = x.Id,
                 SupervisorNameAr = x.SupervisorNameAr,
                 SupervisorNameEn = x.SupervisorNameEn,
                 EmployeeId = x.EmployeeId

             }).ToList();
        }

        public TransportSupervisorViewModel GetById(int ID)
        {
            return context.TransportSupervisors.Where(x => x.Id == ID && x.IsDeleted == false && x.CompanyID == _dbUser.CompanyID)
                 .Select(x => new TransportSupervisorViewModel
                 {
                     Id = x.Id,
                     SupervisorNameEn = x.SupervisorNameEn,
                     SupervisorNameAr = x.SupervisorNameAr,
                     EmployeeId = x.EmployeeId


                 }).FirstOrDefault();
        }


        public TransportSupervisorViewModel Update(TransportSupervisorViewModel model)
        {
            var supervisor = context.TransportSupervisors.Where(x => x.Id == model.Id && x.IsDeleted == false).FirstOrDefault();
            if (supervisor != null)
            {
                supervisor.SupervisorNameAr = model.SupervisorNameAr;
                supervisor.SupervisorNameEn = model.SupervisorNameEn;
                supervisor.EmployeeId = model.EmployeeId;
                supervisor.ModifiedbyID = _dbUser.ID;
                supervisor.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                return model;
            }
            return model;
        }
    }
}
