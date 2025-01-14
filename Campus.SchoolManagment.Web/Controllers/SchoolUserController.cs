using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.BusinessLayer;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Campus.SchoolManagment.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Campus.SchoolManagment.Web.Controllers
{
   // [Filters.Compress]
    public class SchoolUserController : BaseController
    {
        private SchoolHandler _SchoolHandler = new SchoolHandler();
        private SchoolUserHandler _Handler = new SchoolUserHandler();
        ApplicationDbContext dbMembership = new ApplicationDbContext();
        
        private AspNetRolesHandler rolesHandler = new AspNetRolesHandler();

        //private AccountSalaryItemHandler _SalaryItemHandler = new AccountSalaryItemHandler ();
        //private EmployeeHandler _EmpHandler = new EmployeeHandler ();

        // GET: AccountEmployeeSalaryItem
        public ActionResult Index()
        {
             var list = _Handler.GetAllByCompany(companyId).ToList();
             return PartialView(list);
        }

        public ActionResult Create()
        {
           var lang= Utility.getCultureCookie(false);
            
            ViewBag.SchoolID = new SelectList(_SchoolHandler.GetAll(), "SchoolID", "SchoolName"+lang);
            ViewBag.RoleId = new SelectList(rolesHandler.GetAllByCompanyId(companyId), "ID", "RoleName");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(SchoolUserViewModel model)
        {
            if (ModelState.ContainsKey("RoleID"))
                ModelState["RoleID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                UserStore<ApplicationUser> _store = new UserStore<ApplicationUser>(dbMembership);
                ApplicationUserManager UserManager = new ApplicationUserManager(_store);
                ApplicationUser membershipUser = new ApplicationUser();

                membershipUser.UserName = model.Email;
                membershipUser.Email = model.Email;
               
                var result = UserManager.CreateAsync(membershipUser, model.Password);
                if (result.Result.Succeeded)
                {
                    //string tempRole = dbMembership.Roles.Find(model.RoleID).Name;

                    //result = UserManager.AddToRolesAsync(membershipUser.Id, tempRole);
                    //if (result.Result.Succeeded)
                    //{
                        model.AspUserID = membershipUser.Id;
                        // model.CreatedbyID = _Handler.GetByMemberShipId(User.Identity.GetUserId());
                        _Handler.Create(model);

                        TempData["Msg"] = 1;
                        return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());
                    //}
                    //else
                    //{
                    //    TempData["Msg"] = 4;
                    //    return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());
                    //}
                }
                else
                {
                    TempData["Msg"] = 4;
                    return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());
                }
            }

            TempData["Msg"] = 4;
            return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());
        }


        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                var AspUserID = _Handler.GetById(id).AspUserID;
                _Handler.Delete(id);
                //_Handler.Delete(id, _Handler.GetByMemberShipId(User.Identity.GetUserId()));
                dbMembership.Users.Remove(dbMembership.Users.Find(AspUserID));
                dbMembership.SaveChanges();

                TempData["Msg"] = 3;
                return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());
            }
            catch (Exception ex)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());
            }
        }

        public ActionResult Edit(int id)
        {
            SchoolUserViewModel _item = _Handler.GetById(id);
            ViewBag.SchoolID = new SelectList(_SchoolHandler.GetAll(), "SchoolID", "SchoolNameEn", _item.SchoolID);
            ViewBag.RoleId = new SelectList(rolesHandler.GetAllByCompanyId(companyId), "ID", "RoleName");
            return PartialView("_Edit", _item);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SchoolUserViewModel model)
        {
            if (ModelState.ContainsKey("Password"))
                ModelState["Password"].Errors.Clear();

            if (ModelState.IsValid)
            { //Update Aspnet Users
                ApplicationUser usr = dbMembership.Users.Find(model.AspUserID);


                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                try
                {
                    usr.UserName = model.Email;
                    dbMembership.SaveChanges();
                    var result = userManager.SetEmail(model.AspUserID, model.Email);
                }
                catch (Exception)
                {
                    TempData["Msg"] = 5;
                    return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());
                }


                UserStore<ApplicationUser> _store = new UserStore<ApplicationUser>(dbMembership);
                ApplicationUserManager UserManager = new ApplicationUserManager(_store);

                if (UserManager.IsInRole(usr.Id, dbMembership.Roles.Find(model.RoleID).Name) == false)
                {
                    var resultRemove = UserManager.RemoveFromRoles(usr.Id, dbMembership.Roles.Find(usr.Roles.FirstOrDefault().RoleId).Name);
                    if (resultRemove.Succeeded)
                    {
                        var resultAdd = UserManager.AddToRole(model.AspUserID, dbMembership.Roles.Find(model.RoleID).Name);
                        if (resultAdd.Succeeded == false)
                        {
                            TempData["Msg"] = 4;
                            return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());

                        }

                    }
                    else
                    {
                        TempData["Msg"] = 4;
                        return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());

                    }
                }


                // model.ModifiedbyID =  _Handler.GetByMemberShipId(User.Identity.GetUserId());
                _Handler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _Handler.GetAllByCompany(companyId).ToList());

        }

        [HttpGet]
        public PartialViewResult ChangePassword(string Id)
        {
            UserStore<ApplicationUser> _store = new UserStore<ApplicationUser>(dbMembership);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(_store);
            var User = dbMembership.Users.Single(u => u.Id == Id);
            ChangeUserPassword Model = new ChangeUserPassword();
            Model.UserName = User.UserName;
            Model.UserId = User.Id;
            ModelState.Clear();
            return PartialView("ChangePassword", Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangeUserPassword Model)
        {
            ModelState.Remove("OldEmail");
            ModelState.Remove("NewEmail");

            if (ModelState.IsValid)
            {
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());

                userManager.RemovePassword(Model.UserId);
                var result = userManager.AddPassword(Model.UserId, Model.NewPassword);
                if (result.Succeeded)
                {
                    TempData["Msg"] = 2;
                    return PartialView("_List", _Handler.GetAll());
                }
                else
                {

                    TempData["Msg"] = 4;
                    return PartialView("_List", _Handler.GetAll());
                }
            }
            else
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _Handler.GetAll());
            }

        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public int CreateUser(SchoolUserViewModel model)
        {
            if (ModelState.ContainsKey("RoleID"))
                ModelState["RoleID"].Errors.Clear();
            if (ModelState.IsValid)
            {
                UserStore<ApplicationUser> _store = new UserStore<ApplicationUser>(dbMembership);
                ApplicationUserManager UserManager = new ApplicationUserManager(_store);
                ApplicationUser membershipUser = new ApplicationUser();

                membershipUser.UserName = model.Email;
                membershipUser.Email = model.Email;

                var result = UserManager.CreateAsync(membershipUser, model.Password);
                if (result.Result.Succeeded)
                {
                    string tempRole = dbMembership.Roles.Find(model.RoleID).Name;

                    result = UserManager.AddToRolesAsync(membershipUser.Id, tempRole);
                    if (result.Result.Succeeded)
                    {
                        model.AspUserID = membershipUser.Id;
                        //model.CreatedbyID = 1;//_Handler.GetByMemberShipId(User.Identity.GetUserId());
                        _Handler.Create(model);
                        return 1;
                    }
                    else
                    {

                        return 4;
                    }
                }
                else
                {

                    return 5;
                }
            }


            return 4;
        }

        public JsonResult CheckEmail(string Email)
        {
           return CheckEmailExist(Email);
        }
        public JsonResult Checkmail(string mail)
        {
            return CheckEmailExist(mail);
        }
        public JsonResult CheckEmailExist(string Email)
        {
            UserStore<ApplicationUser> _store = new UserStore<ApplicationUser>(dbMembership);
            ApplicationUserManager UserManager = new ApplicationUserManager(_store);
            ApplicationUser membershipUser = new ApplicationUser();

            membershipUser = UserManager.FindByEmail(Email);
            if (membershipUser == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);//not fire validator
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);// fire validator

            }
        }
    }
}