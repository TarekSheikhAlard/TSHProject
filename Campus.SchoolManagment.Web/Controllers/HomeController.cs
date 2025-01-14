using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.SchoolManagment.Web.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    //[Authorize(Roles = "Admin,SuperAdmin,Employee")]
    [Authorize]
    public class HomeController : BaseController
    {
        private SchoolHandler _SchoolHandler = new SchoolHandler();
        [Authorize]
        public ActionResult Index()
        {
           // string name = SingletoneUser.Instance.GetUser().Name;
             return View();
        }
        public ActionResult Login()
        {
          
            ViewBag.SchoolID = new SelectList(_SchoolHandler.GetAll(), "SchoolID", "SchoolNameAr");
             return View();
        }

       
        public ActionResult AccountPage()
        {        
             return View();
        }

        public ActionResult ChangeLanguage(string lang, string returnUrl)
        {
            if (lang != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }

            HttpCookie cookie = new HttpCookie("_lang");
            cookie.Value = lang;
            Response.Cookies.Add(cookie);

            return Redirect(returnUrl);
        }


        public JsonResult ChangeTheme(string theme)
        {
            if (theme == null)
            {
                theme = "default";
            }

            HttpCookie cookie = new HttpCookie("_theme");
            cookie.Value = theme;
            Response.Cookies.Add(cookie);

            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}