using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.SchoolManagment.Web.Helper;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Campus.SchoolManagment.Web.Controllers
{
    public class EmployeeAccountReportController : BaseController
    {
        private AccountTreeHandler _AccountTreeHandler = new AccountTreeHandler();
        AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();
        // GET: EmployeeAccountReport
        public ActionResult Index()
        {
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAllByCompany(companyId, schoolId).Where(x=>x.Comment== "Cost Center Of Empployee"), "ID", "CostCenter" + lang);

            ViewBag.Account = new SelectList(_AccountTreeHandler.GetAll().Where(x=>x.AccountTreeParentID==21), "AccountTreeID", "AccountName" + lang);

            return PartialView();
        }
        [HttpPost]
        public object Create(EmployeeAccountReportInputs model) {

            return model;

        }

    }
}