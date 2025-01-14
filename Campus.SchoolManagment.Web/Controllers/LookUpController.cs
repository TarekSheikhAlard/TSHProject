using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    public class LookUpController : Controller
    {
        private SchoolHandler _handler = new SchoolHandler();

        [HttpPost]
        public JsonResult GetSchoolList(string companyID)
        {
            List<SchoolViewModel> lstcity = new List<SchoolViewModel>();
            int companyId = Convert.ToInt32(companyID);
            var lstSchool = _handler.GetByCompanyID(companyId);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstSchool);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}