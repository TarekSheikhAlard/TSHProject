using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using System.Web.Configuration;
using System.Configuration;
using Newtonsoft.Json;

namespace Campus.SchoolManagment.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

          

            }
        protected void Application_AcquireRequestState()
        {
            HttpContext context = HttpContext.Current;
            if (context != null & context.Request.Cookies != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["_lang"];
                if (cookie != null && cookie.Value != null)
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cookie.Value);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                }
            }


        }

    }
}
