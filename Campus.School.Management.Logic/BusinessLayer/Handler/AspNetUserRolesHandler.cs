using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using Campus.SchoolManagment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class AspNetUserRolesHandler : IRepository<AspNetUserRolesViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        public void Create(AspNetUserRolesViewModel model)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));

            var currentUser = UserManager.FindByName(model.Username);

            var roleresult = UserManager.AddToRole(currentUser.Id, model.groupName);
            
        }

        public AspNetUserRolesViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<AspNetUserRolesViewModel> GetAll()
        {


           
            throw new NotImplementedException();
        }

        public List<AspNetUserRolesViewModel> GetAllByCompanyandSchool(int companyId,int schoolId)
        {
          


            throw new NotImplementedException();
        }

        public AspNetUserRolesViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public AspNetUserRolesViewModel Update(AspNetUserRolesViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
