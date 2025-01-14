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
    public class UserPermissionHandler : IRepository<UserPermissionViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public UserPermissionViewModel Update(UserPermissionViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _permission = Context.UserPermissions.Where(c => c.ID == model.ID).FirstOrDefault();
            if (_permission != null)
            {
                _permission.RoleID = model.RoleID;
                _permission.PageID = model.PageID;
                _permission.ViewAct = model.ViewAct;
                _permission.CreateAct = model.CreateAct;
                _permission.EditAct = model.EditAct;
                _permission.DeleteAct = model.DeleteAct;
                _permission.ModifiedbyID = model.ModifiedbyID;               
                _permission.ModifiedDate = DateTime.Now;
                _permission.ModifiedbyID = _dbUser.ID;


                Context.SaveChanges();
            }
            return model;

        }

        public List<UserPermissionViewModel> GetAll()
        {
            return Context.UserPermissions.Where(x=> x.IsDeleted == false).Select(c => new UserPermissionViewModel
            {
                ID = c.ID,
                RoleID = c.RoleID,
                RoleName = c.UserGroup.groupNameEn,
                PageID = c.PageID,
                PageName = c.MenuPage.PageName,
                ViewAct = c.ViewAct,
                CreateAct = c.CreateAct,
                EditAct = c.EditAct,
                DeleteAct = c.DeleteAct,
             

            }).ToList();

        }

        public UserPermissionViewModel GetById(int ID)
        {
            return Context.UserPermissions.Where(c => c.ID == ID && c.IsDeleted == false).Select(c => new UserPermissionViewModel
            {
                ID = c.ID,
                RoleID = c.RoleID,
                RoleName = c.UserGroup.groupNameEn,
                PageID = c.PageID,
                PageName = c.MenuPage.PageName,
                ViewAct = c.ViewAct,
                CreateAct = c.CreateAct,
                EditAct = c.EditAct,
                DeleteAct = c.DeleteAct,
            }).FirstOrDefault();
        }

        public List<UserPermissionViewModel> GetByRoleId(string ID)
        {
            var query = from menuPage in Context.MenuPages
                        join userPermission in Context.UserPermissions
                        on new { key1 = menuPage.ID, key2 = ID }
                        equals new { key1 = userPermission.PageID, key2 = userPermission.RoleID }
                        into permission
                        from e in permission.DefaultIfEmpty()
                        select new
                        {
                            PageID = menuPage.ID,
                            menuPage.PageName,
                            RoleID = e.RoleID ?? ID,
                            ViewAct = e.ViewAct == null ? false : e.ViewAct,
                            CreateAct = e.CreateAct == null ? false : e.CreateAct,
                            EditAct = e.EditAct == null ? false : e.EditAct,
                            DeleteAct = e.DeleteAct == null ? false : e.DeleteAct
                        };

            return query.Select(c => new UserPermissionViewModel
            {
                PageID = c.PageID,
                PageName = c.PageName,
                RoleID = c.RoleID,
                ViewAct = c.ViewAct,
                CreateAct = c.CreateAct,
                EditAct = c.EditAct,
                DeleteAct = c.DeleteAct

            }).Where(x=>x.PageID!=29).ToList();  //29 is company 
        }


        public PageNoPermission[] GetNoAccessPagesByRoleId(string ID)
        {
            var query = from menuPage in Context.MenuPages
                        join userPermission in Context.UserPermissions
                        on new { key1 = menuPage.ID, key2 = ID }
                        equals new { key1 = userPermission.PageID, key2 = userPermission.RoleID }
                        into permission
                        from e in permission.DefaultIfEmpty()
                        select new
                        {       
                            e.PageID ,
                            ViewAct = e.ViewAct == null ? false : e.ViewAct, 
                        };

            return query.Where(c=>c.ViewAct==false).Select(c => new PageNoPermission
            {
                PageID =c.PageID
            }).ToArray();
        }


        public class PageNoPermission {

            public int PageID { get; set; }
        }



        /// <summary>
        /// get permission by userid and pageid
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public UserPermissionViewModel GetByRolePageId(string RoleID,int pageID)
        {
            return Context.UserPermissions.Where(c => c.PageID == pageID && c.RoleID == RoleID && c.IsDeleted == false).Select(c => new UserPermissionViewModel
            {
                ID = c.ID,
                RoleID = c.RoleID,
                RoleName = c.UserGroup.groupNameEn,
                PageID = c.PageID,
                PageName = c.MenuPage.PageName,
                ViewAct=c.ViewAct,
                CreateAct = c.CreateAct,
                EditAct = c.EditAct,
                DeleteAct = c.DeleteAct,
            }).FirstOrDefault();
        }

        public void Create(UserPermissionViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            UserPermission _permission = new UserPermission
            {
                ID = model.ID,
                RoleID = model.RoleID,
                PageID = model.PageID,
                ViewAct = model.ViewAct,
                CreateAct = model.CreateAct,
                EditAct = model.EditAct,
                DeleteAct = model.DeleteAct,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,

            };
            Context.UserPermissions.Add(_permission);
            Context.SaveChanges();
        }

       

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _permission = Context.UserPermissions.Where(c => c.ID == ID).FirstOrDefault();
            if (_permission != null)
            {
                _permission.DeletedbyID = _dbUser.ID;
                _permission.IsDeleted = true;
                _permission.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }

        public UserPermissionViewModel Create()
        {
            UserPermissionViewModel model = new UserPermissionViewModel();
       
            return model;
        } 
    }
}