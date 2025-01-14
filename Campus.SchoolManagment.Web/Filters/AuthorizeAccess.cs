using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Campus.SchoolManagment.Web.Controllers;
using Campus.School.Management.Logic.BusinessLayer;
using Campus.School.Management.Logic.DataBaseLayer;
using Campus.School.Management.Logic.BusinessLayer.Handler;

namespace Campus.SchoolManagment.Web.Filters
{
    public class AuthorizeAccess : ActionFilterAttribute
    {
        public string Feature { get; set; }
        public string PageName { get; set; }


        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        private SchoolUserHandler _UserHandler = new SchoolUserHandler();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var _dbUser = Statistics.GetLogiccookie();

            string userAspId = filterContext.RequestContext.HttpContext.User.Identity.GetUserId();
            string roleId = _UserHandler.GetRoleByMemberShipId(userAspId);
            string controlName; bool hasPermission = false;

            if (PageName != null)
            {
                controlName = PageName;
            }
            else
            {
                controlName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            }

            switch (Feature)
            {
                case "View":
                    hasPermission = Context.UserPermissions.Where(c => c.RoleID == roleId && c.MenuPage.PageName == controlName && c.ViewAct == true).Count() > 0 ? true : false;
                    break;
                case "Create":
                    hasPermission = Context.UserPermissions.Where(c => c.RoleID == roleId && c.MenuPage.PageName == controlName && c.CreateAct == true).Count() > 0 ? true : false;
                    break;
                case "Edit":
                    hasPermission = Context.UserPermissions.Where(c => c.RoleID == roleId && c.MenuPage.PageName == controlName && c.EditAct == true).Count() > 0 ? true : false;
                    break;
                case "Delete":
                    hasPermission = Context.UserPermissions.Where(c => c.RoleID == roleId && c.MenuPage.PageName == controlName && c.DeleteAct == true).Count() > 0 ? true : false;
                    break;
                default:
                    hasPermission = false;
                    break;
            }

            if (!hasPermission)
            {
                if (filterContext.Controller.GetType() != typeof(BaseController))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Error",
                        action = "NoAccess"      
                    }));
                }

            }

            base.OnActionExecuting(filterContext);
        }
    }
}