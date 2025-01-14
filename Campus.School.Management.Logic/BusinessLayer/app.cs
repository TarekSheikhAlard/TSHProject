using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Campus.School.Management.Logic.BusinessLayer
{
  public class Utility
    {
        public enum SalaryItemType
        {
            //Deduction = 1,
            //Bonus = 2

            خصومات = 1,
            مكافات = 2
        }


        public static string getCultureCookie(bool inverse)
        {
            string cultureName;

            HttpCookie cultureCookie = HttpContext.Current.Request.Cookies["_lang"];

            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = "en";

            if (cultureName.ToLower().Equals("ar-eg")) cultureName = "ar"; 

            if (inverse) cultureName = cultureName.ToLower().Equals("en") ? "ar-EG" : "en";

            return cultureName.ToUpperInvariant();

        }



        public static bool IsNullEmptyOrWhiteSpace(string value)
        {

            if (String.IsNullOrEmpty(value) && String.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            return false;
        }

        public static string getThemeCookie()
        {
            string theme;

            HttpCookie themeCookie = HttpContext.Current.Request.Cookies["_theme"];

            if (themeCookie != null)
                theme = themeCookie.Value;
            else
                theme = "blue";

            return theme.ToLower();

        }

        
    }
}
