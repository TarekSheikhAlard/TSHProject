using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class AspNetRolesHandler : IRepository<AspNetRolesViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        //public bool CreateRole(AspNetRolesViewModel model)
        //{



        //}
        public void Create(AspNetRolesViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var groupdId = Guid.NewGuid().ToString();
            UserGroup userGroup = new UserGroup()
            {
                groupId = groupdId,
                groupNameEn =model.RoleName,
                groupNameAr = model.RoleName,
                DeletedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedbyID = _dbUser.ID,
                CompanyId =_dbUser.CompanyID,
                SchoolID = _dbUser.SchoolID
               
            };

            Context.UserGroups.Add(userGroup);

            foreach (int School in model.schoolId)
            {
                SchoolGroup schoolGroup = new SchoolGroup()
                {
                    SchoolId = School,
                    GroupdId = groupdId,
                    CreatedDate =DateTime.Now
                };
                Context.SchoolGroups.Add(schoolGroup);
            }

            Context.SaveChanges();
        }

        public AspNetRolesViewModel Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(string ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Role = Context.UserGroups.Where(x => x.groupId.ToString() == ID).FirstOrDefault();
            if (_Role != null)
            {
                Context.UserGroups.Remove(_Role);
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public List<AspNetRolesViewModel> GetAll()
        {
            return Context.UserGroups.Where(x=>x.groupNameEn!="SuperAdmin").Select(x => new AspNetRolesViewModel
            {
                ID = x.groupId.ToString(),
                RoleName=x.groupNameEn              
            }).ToList();
        }
        public List<AspNetRolesViewModel> GetAllByCompanyId(int companyId)
        {
            return Context.UserGroups.Where(x => x.groupNameEn != "SuperAdmin" && x.CompanyId==companyId &&x.SchoolID==_dbUser.SchoolID).Select(x => new AspNetRolesViewModel
            {
                ID = x.groupId.ToString(),
                RoleName = x.groupNameEn
            }).ToList();
        }

        public AspNetRolesViewModel GetById(string ID)
        {
            return Context.UserGroups.Where(x => x.groupId.ToString() == ID & x.groupNameEn != "SuperAdmin").Select(x => new AspNetRolesViewModel
            {
                ID = x.groupId.ToString(),
                RoleName = x.groupNameEn
            }).FirstOrDefault();
          
        }


        public int[] GetSchoolsByGroupId(string ID) {

            return Context.SchoolGroups.Where(x => x.GroupdId.ToString() == ID).Select(x => x.SchoolId).ToArray();

        }

        public AspNetRolesViewModel GetById(int ID)
        {
            throw new NotImplementedException();
        }

        public AspNetRolesViewModel Update(AspNetRolesViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var group = Context.UserGroups.Where(c => c.groupId.ToString() == model.ID).FirstOrDefault();
            group.groupNameEn = model.RoleName;
            group.ModifiedDate = DateTime.Now;
            group.ModifiedbyID = _dbUser.ID;
            Context.SaveChanges();
            return model;
        }
    }
}
