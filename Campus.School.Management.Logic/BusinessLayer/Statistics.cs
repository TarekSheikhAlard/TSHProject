using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Campus.School.Management.Logic.BusinessLayer
{
    public static class Statistics
    {
        public const string StudentImagesPath = "~/Content/Images/Student_Img/";
        public const string SchoolImagesPath = "~/Content/Images/SchoolImages/";
        public const string ImgPath = "~/Content/Img/";
        public static readonly string JournalAttachedDocumentPath = "~/Content/Uploads/";///String.Format("~/Content/Uploads/{0}/{1}/journal/", Logic.CompanyID.ToString(),Logic.SchoolID.ToString()) ?? string.Empty;
        public static int CurrentSchoolID { get; set; }
        public static string UserName { get; set; }

        public static  SchoolUserViewModel Logic = GetLogiccookie();
        public static SchoolUserViewModel GetLogiccookie()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["dbusr"];
            return cookie != null ? JsonConvert.DeserializeObject<SchoolUserViewModel>(cookie.Value) : new SchoolUserViewModel();
        }
    }
    
}
