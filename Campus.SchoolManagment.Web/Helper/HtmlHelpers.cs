﻿using Campus.SchoolManagment.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Helper
{
  
    public static class HtmlHelpers
    {

    
        public static string IsActive(this HtmlHelper htmlHelper, string controller, string action=null)
        {
             var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && action == routeAction);

            return returnActive ? "active" : "";
        }
        public static string IsVisible(this HtmlHelper htmlHelper, string controller, string action = null)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && action == routeAction);

            return returnActive ? "visible active" : "";
        }



    }
}