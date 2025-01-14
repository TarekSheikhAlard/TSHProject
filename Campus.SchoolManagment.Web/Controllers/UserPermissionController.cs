
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Campus.SchoolManagment.Web.Controllers
{
   // [Filters.Compress]
    public class UserPermissionController : BaseController
    {
        private SchoolUserHandler _UserHandler = new SchoolUserHandler();
        private MenuPageHandler _MenuPageHandler = new MenuPageHandler();
        AspNetRolesHandler rolesHandler = new AspNetRolesHandler();
        UserPermissionHandler _Handler = new UserPermissionHandler();
        // GET: AccountSalaryItem
        public ActionResult Index()
        {
            //  return PartialView(_Handler.GetAll());

            //ViewBag.UserID = new SelectList(_UserHandler.GetAll(), "ID", "Name");
            //ViewBag.Page = _MenuPageHandler.GetAll();

            ViewBag.RoleID = new SelectList(rolesHandler.GetAllByCompanyId(companyId), "ID", "RoleName");

            //UserPermissionViewModel model = new UserPermissionViewModel();
            //model.RoleID = "49d32224-5815-4201-a8f5-70faf6f4f470";
            //model.UserPermission = _Handler.GetByRoleId(model.RoleID);


           // return PartialView("_List",model);
            return PartialView();
        }

        public object test() {

            return JsonConvert.SerializeObject(_Handler.GetByRoleId("49d32224-5815-4201-a8f5-70faf6f4f470"));
        }


        public ActionResult Create(string id)
        {
            var model = _Handler.GetByRoleId(id);
            
            return PartialView("_Create",model);
        }
        
        [HttpPost]
        public ActionResult Create(List<UserPermissionViewModel> List,string roleId)
        {
            try
            {
             
                UserPermissionViewModel model = new UserPermissionViewModel();
              
                foreach (UserPermissionViewModel item in List)
                {
                   
                    UserPermissionViewModel perm = _Handler.GetByRolePageId(item.RoleID,item.PageID);
                   
                 
                    if (perm != null)
                    {
                        item.ID = perm.ID;
                        _Handler.Update(item);
                    }
                    else
                    {
                        _Handler.Create(item);
                    }

                  
                }
                TempData["Msg"] = 1;
                return PartialView("_Create", _Handler.GetByRoleId(roleId));

            }
            catch (Exception ex)
            {
                TempData["Msg"] = 4;
                return PartialView("_Create", _Handler.GetByRoleId(roleId));
            }




        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            try
            {
                _Handler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _Handler.GetAll());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _Handler.GetAll());
            }
        }


        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", _Handler.GetById(id));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserPermissionViewModel model)
        {
           if (ModelState.IsValid)
            {
                //model.ModifiedbyID = _UserHandler.GetByMemberShipId(User.Identity.GetUserId());
                _Handler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _Handler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _Handler.GetAll());


        }
        
    }
}