using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Text;
using System.Threading.Tasks;
using Campus.SchoolManagment.Models;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class SchoolUserHandler : IRepository<SchoolUserViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        private ApplicationDbContext dbMembership = new ApplicationDbContext();

        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        public SchoolUserViewModel Update(SchoolUserViewModel model)
        {


            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _SchoolUser = Context.SchoolUsers.Where(c => c.ID == model.ID).FirstOrDefault();
            if (_SchoolUser != null)
            {
                _SchoolUser.Name = model.Name;
                _SchoolUser.RoleID = model.RoleID;
                // _SchoolUser.AspUserID = model.AspUserID;
                //_SchoolUser.SchoolID = model.SchoolID;
                _SchoolUser.ModifiedbyID = _dbUser.ID;
                _SchoolUser.ModifiedDate = DateTime.Now;
                _SchoolUser.ModifiedbyID = model.ModifiedbyID;

                Context.SaveChanges();
            }

            return model;

        }

        public List<SchoolUserViewModel> GetAll()
        {

            var Schooluser = Context.SchoolUsers.Include("AdmSchool").Where(x => x.IsDeleted == false).ToList();
            string[] roleUser = Schooluser.Select(c => c.RoleID).ToArray();
            string[] AspUser = Schooluser.Select(c => c.AspUserID).ToArray();
            if (Schooluser.Count > 0)
            {
                var Role = from u in dbMembership.Roles.Where(x => roleUser.Contains(x.Id) && x.Name != "SuperAdmin").ToList()
                           select new
                           {
                               u.Id,
                               u.Name,

                           };
                var user = from u in dbMembership.Users.Where(x => AspUser.Contains(x.Id)).ToList()

                           select new
                           {
                               u.Id,
                               u.Email,
                           };



                var _UserRole = from u in Role
                                join c in Schooluser
                                on u.Id equals c.RoleID

                                select new
                                {
                                    roleid = u.Id,
                                    roleName = u.Name,
                                    ID = c.ID,
                                    c.AspUserID,
                                    Name = c.Name,
                                    SchoolID = c.SchoolID,
                                    SchoolName = c.AdmSchool.SchoolNameEn,
                                    RoleID = c.RoleID,

                                };

                var _list = from u in _UserRole
                            join c in user
                            on u.AspUserID equals c.Id

                            select new
                            {

                                u.ID,
                                u.Name,
                                u.SchoolID,
                                u.SchoolName,
                                u.RoleID,
                                u.roleName,
                                c.Email,
                                u.AspUserID

                            };
                return _list.Select(c => new SchoolUserViewModel
                {
                    ID = c.ID,
                    Name = c.Name,
                    SchoolID = c.SchoolID,
                    SchoolName = c.SchoolName,
                    RoleID = c.RoleID,
                    RoleName = c.roleName,
                    Email = c.Email,
                    AspUserID = c.AspUserID

                }).ToList();
            }
            else
            {
                return new List<SchoolUserViewModel>();
            }




        }

        public List<SchoolUserViewModel> GetAllByCompany(int companyId)
        {
            var lang = Utility.getCultureCookie(false).ToLower();

            var Schooluser = Context.SchoolUsers.Include("AdmSchool").Where(x => x.IsDeleted == false && x.CompanyID == companyId &&x.SchoolID== _dbUser.SchoolID).ToList();
            string[] roleUser = Schooluser.Select(c => c.RoleID).ToArray();
            string[] AspUser = Schooluser.Select(c => c.AspUserID).ToArray();
            if (Schooluser.Count > 0)
            {
                var Role = from u in Context.UserGroups.Where(x => roleUser.Contains(x.groupId.ToString())).ToList()
                           select new
                           {
                               u.groupId,
                               u.groupNameEn,

                           };
                var user = from u in dbMembership.Users.Where(x => AspUser.Contains(x.Id)).ToList()

                           select new
                           {
                               u.Id,
                               u.Email,
                           };



                var _UserRole = from u in Role
                                join c in Schooluser
                                on u.groupId.ToString() equals c.RoleID

                                select new
                                {
                                    roleid = u.groupId,
                                    roleName = u.groupNameEn,
                                    ID = c.ID,
                                    c.AspUserID,
                                    Name = c.Name,
                                    SchoolID = c.SchoolID,
                                    SchoolName = lang == "ar" ? c.AdmSchool.SchoolNameAr : c.AdmSchool.SchoolNameEn,
                                    RoleID = c.RoleID,

                                };

                var _list = from u in _UserRole
                            join c in user
                            on u.AspUserID equals c.Id

                            select new
                            {

                                u.ID,
                                u.Name,
                                u.SchoolID,
                                u.SchoolName,
                                u.RoleID,
                                u.roleName,
                                c.Email,
                                u.AspUserID

                            };
                return _list.Select(c => new SchoolUserViewModel
                {
                    ID = c.ID,
                    Name = c.Name,
                    SchoolID = c.SchoolID,
                    SchoolName = c.SchoolName,
                    RoleID = c.RoleID,
                    RoleName = c.roleName,
                    Email = c.Email,
                    AspUserID = c.AspUserID

                }).ToList();
            }
            else
            {
                return new List<SchoolUserViewModel>();
            }




        }

        public SchoolUserViewModel GetById(int ID)
        {
            var _List = Context.SchoolUsers.Where(x => x.IsDeleted == false && x.ID == ID);

            var Schooluser = _List.FirstOrDefault();

            if (Schooluser != null)
            {
                string Role = Context.UserGroups.Where(x => x.groupId.ToString() == Schooluser.RoleID).FirstOrDefault().ToString();

               
                string userEmail = dbMembership.Users.Where(x => x.Id == Schooluser.AspUserID).FirstOrDefault().Email;

                return _List.Select(c => new SchoolUserViewModel
                {
                    ID = c.ID,
                    AspUserID = c.AspUserID,
                    Name = c.Name,
                    SchoolID = c.SchoolID,
                    SchoolName = c.AdmSchool.SchoolNameEn,
                    RoleID = c.RoleID,
                    RoleName = Role,
                    Email = userEmail,
                    CompanyID = c.CompanyID,
                    Url = c.AdmSchool.Url
                
                    // UserPermissionList = c.UserPermissions,                     

                }).FirstOrDefault();
            }
            else
            {
                return new SchoolUserViewModel();
            }
        }



        public int GetByMemberShipId(string ID)
        {
            return Context.SchoolUsers.Where(x => x.IsDeleted == false && x.AspUserID == ID).FirstOrDefault().ID;
        }
        public string GetRoleByMemberShipId(string ID)
        {
            return Context.SchoolUsers.Where(x => x.IsDeleted == false && x.AspUserID == ID).FirstOrDefault().RoleID;
        }
        public long checkUserSchoolAccess(string user, string schoolId)
        {

            string Role = Context.SchoolUsers.Where(x => x.AspUserID == user).Select(x => x.RoleID).SingleOrDefault();


            long id = Context.SchoolGroups.Where(x => x.GroupdId.ToString() == Role && x.SchoolId.ToString() == schoolId).Select(x => x.Id).SingleOrDefault();


            if (id==0)
            {

                return id;
            }
            else
            {
                return id;

            }

        }

        public SchoolUser GetUserByMemberShipId(string ID)
        {
            return Context.SchoolUsers.Where(x => x.IsDeleted == false && x.AspUserID == ID).FirstOrDefault();
        }

        public SchoolUserViewModel Create()
        {
            SchoolUserViewModel model = new SchoolUserViewModel();

            return model;
        }

        public void Create(SchoolUserViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            SchoolUser _SchoolUser = new SchoolUser
            {
                Name = model.Name,
                SchoolID = model.SchoolID,
                RoleID = model.RoleID,
                AspUserID = model.AspUserID,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                CompanyID = _dbUser.CompanyID


            };
            Context.SchoolUsers.Add(_SchoolUser);
            Context.SaveChanges();
        }



        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _SchoolUser = Context.SchoolUsers.Where(c => c.ID == ID).FirstOrDefault();
            if (_SchoolUser != null)
            {
                _SchoolUser.DeletedbyID = _dbUser.ID;
                _SchoolUser.AspUserID = null;
                _SchoolUser.IsDeleted = true;
                _SchoolUser.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }
    }
}
