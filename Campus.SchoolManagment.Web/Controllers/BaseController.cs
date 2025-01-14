using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public int companyId = 0;
        public int schoolId = 5016;
        public int yearId = 0;
        public string lang = "En";
        private UserPermissionHandler userPermissionHandler = new UserPermissionHandler();
        private SchoolUserHandler _UserHandler = new SchoolUserHandler();
        //StudyYearHandler _StudyYearHandler = new StudyYearHandler();

        public BaseController()
        {

            if (System.Web.HttpContext.Current.Session["CompanyID"] != null)
            {
                companyId =int.Parse(System.Web.HttpContext.Current.Session["CompanyID"].ToString());

            }
          

            if (System.Web.HttpContext.Current.Session["SchoolID"] != null)
            {
                schoolId = int.Parse(System.Web.HttpContext.Current.Session["SchoolID"].ToString());

            }
            if (companyId!=0&&schoolId!=0)
            {
               // yearId=_StudyYearHandler.GetAllByCompany(companyId, schoolId).Where(y=>y.IsDefault==true).FirstOrDefault().YearID;
            }

            lang = Utility.getCultureCookie(false);
            ViewData["lang"] = lang.ToLower();
        }



        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }


        //protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        //{


        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var roleId = _UserHandler.GetRoleByMemberShipId(User.Identity.GetUserId());
        //        var Permissions = userPermissionHandler.GetByRoleId(roleId).Where(c=>c.ViewAct==false).Select(c=>c.PageID).ToList();
        //        //var Permissions = userPermissionHandler.GetNoAccessPagesByRoleId(roleId);

        //        HttpCookie Librel = new HttpCookie("CampusXLibrel");
        //        Librel.Value = JsonConvert.SerializeObject(Permissions);
        //        Librel.Expires = DateTime.Now.AddHours(1);
        //        Response.SetCookie(Librel);
        //    }
        //    return base.BeginExecuteCore(callback, state);
        //}


    }
}