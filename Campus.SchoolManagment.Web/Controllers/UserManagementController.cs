using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Campus.SchoolManagment.Models;

namespace Campus.SchoolManagment.Web.Controllers
{
   [Filters.Compress]
    //[Authorize(Roles ="SuperAdmin")]
    public class UserManagementController : BaseController
    {
        private AspNetRolesHandler rolesHandler = new AspNetRolesHandler();
        private AspNetUserRolesHandler UserRolesHandler = new AspNetUserRolesHandler();
        private SchoolHandler schoolHandler = new SchoolHandler();
        // GET: UserManagement
        public ActionResult Roles()
        {
            ///ViewBag.SchoolId=rolesHandler.GetSchoolsByGroupId()

                ViewBag.schoolId = new SelectList(schoolHandler.GetAllByCompanyID(companyId), "SchoolID", "SchoolName"+lang);

            return PartialView(rolesHandler.GetAllByCompanyId(companyId));
        }

        public ActionResult CreateRoles()
        {
            ViewBag.schoolId = new SelectList(schoolHandler.GetAllByCompanyID(companyId), "SchoolID", "SchoolName" + lang);
            return PartialView("_CreateRoles");
        }
        [HttpPost]
        public ActionResult CreateRoles(AspNetRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                //// model.CompanyID = companyId;
                ////rolesHandler.CreateRole(model);

                //if (!roleManager.RoleExists(model.RoleName))
                //{
                //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                //    role.Name = model.RoleName;
                //    roleManager.Create(role);

                //}
                rolesHandler.Create(model);

                TempData["Msg"] = 1;
                return PartialView("_ListRoles", rolesHandler.GetAllByCompanyId(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_ListRoles", rolesHandler.GetAllByCompanyId(companyId));
        }

        [HttpPost]
        public ActionResult GetEditRoles(string id)
        {
            ViewBag.schoolId = new SelectList(schoolHandler.GetAllByCompanyID(companyId), "SchoolID", "SchoolName" + lang);

            var model = rolesHandler.GetById(id);

            model.schoolId = rolesHandler.GetSchoolsByGroupId(model.ID);

            return PartialView("_EditRoles",model);
        }
        [HttpPost]
        public ActionResult EditRoles(AspNetRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                //// model.CompanyID = companyId;
                ////rolesHandler.CreateRole(model);

                //if (!roleManager.RoleExists(model.RoleName))
                //{
                //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                //    role.Name = model.RoleName;
                //    roleManager.Create(role);

                //}
                rolesHandler.Create(model);

                TempData["Msg"] = 1;
                return PartialView("_ListRoles", rolesHandler.GetAllByCompanyId(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_ListRoles", rolesHandler.GetAllByCompanyId(companyId));
        }


        [HttpGet]
        public ActionResult DeleteRoles(string id)
        {
            return PartialView("_DeleteRoles", id);

        }

        [HttpPost, ActionName("DeleteRoles")]
        public ActionResult ConfirmDeleteRoles(string id)
        {
            if (ModelState.IsValid)
            {

                rolesHandler.Delete(id);

                TempData["Msg"] = 1;
                return PartialView("_ListRoles", rolesHandler.GetAllByCompanyId(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_ListRoles", rolesHandler.GetAllByCompanyId(companyId));
        }


        public ActionResult UserRoles()
        {
            return PartialView(UserRolesHandler.GetAll());
        }



    }
}