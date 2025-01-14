using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult cookie()
        {
             return PartialView();
        }

        //public ActionResult Change( )
        //{
        //    // public ActionResult Change(String Languageabbreviations)
        //    //if (Languageabbreviations != null)
        //    //{
        //    //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Languageabbreviations);
        //    //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(Languageabbreviations);
        //    //}

        //    //HttpCookie cookie = new HttpCookie("Language");
        //    //cookie.Value = Languageabbreviations;
        //    //Response.Cookies.Add(cookie);

        //   return PartialView( );

        //}


    }
}