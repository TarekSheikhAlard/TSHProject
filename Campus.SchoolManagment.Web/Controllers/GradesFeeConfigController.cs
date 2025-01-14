using Campus.School.Management.Logic.BusinessLayer;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using Campus.SchoolManagment.Web.Helper;
using Campus.SchoolManagment.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class GradesFeeConfigController : BaseController
    {
        AccountTreeHandler AccountTree = new AccountTreeHandler();
        StudyYearHandler studyyearhandler = new StudyYearHandler();

        GardesFeeConfigHandler gardesFeeConfig = new GardesFeeConfigHandler();
        SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        // GET: GradesFeeConfig
        public ActionResult Index()
        {
            if (schoolId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var query = string.Format(@"Select config.Id,config.GradeID,tree.AccountTreeID,tree.AccountName{2} AccountTreeName,config.Amount,config.[Priority],tree.SchoolID from [dbo].[GradesFeeConfig] config 
                      right outer join AccountTree tree 
                      on config.[AccountTreeID] = tree.AccountTreeID 
                      and config.SchoolID = tree.SchoolID
                      and config.GradeID ='{0}'
                      Where tree.AccountTreeParentID='27'
                      and tree.SchoolID='{1}' and tree.AccountNameEn like '%FeesRevenue'
                      order by config.[Priority] 
                    ", "7", schoolId, lang);


            ViewBag.YearID = new SelectList(studyyearhandler.GetAll(), "YearID", "YearName" + lang);

           


           // var results = gardesFeeConfig.GetAllByGradeId(GradeID);

            return View();
        }

        public ActionResult GetFeeConfig(int GradeID )
        {
            var lang = Utility.getCultureCookie(false);

            InstallemtTypeSelectList(lang);

            

          string isCurrentYearQuery=  @" SELECT 1    
                                          FROM [campuserp].[dbo].[AdmGradesSchool] grades
                                          inner join [campuserp].[dbo].[admStudyear] studyyear
                                          on grades.YearID = studyyear.YearID
                                          where studyyear.CurrentYear =1
                                           and grades.GradeID = @p0";


           var isCurrentYear= Context.Database.SqlQuery<int>(isCurrentYearQuery, GradeID).ToList();

           ViewBag.isCurrentYear= isCurrentYear.Count() ==0?false:true;

            return PartialView("_FeeConfig", gardesFeeConfig.GetAllByGradeId(GradeID));



        }

        private void InstallemtTypeSelectList(string lang)
        {
            var query = @"SELECT Id, NameEn,NameAr,IsActive,Period,AutomaticCount From [dbo].[FeeInstallmentTypes] where IsActive=1 ";


            ViewBag.FeeInstallmentType = new SelectList(Context.Database.SqlQuery<FeeInstallmentType>(query).ToList(), "Id", "Name" + lang);
        }

        public ActionResult FeesReport(string id) {

            var query = "select * from [FeesReportBySchool](@p0) UNION ALL select 'Total', sum(Amount) ,Grade,'',GradeID from [FeesReportBySchool](@p0) group by GradeID,Grade order by AccountTreeID,GradeID";
            var report = Context.Database.SqlQuery<FeeReportViewModel>(query, id).ToList();

            return PartialView("FeesReport",report);
        }

        public ActionResult Save(List<GradesFeeConfigViewModel> configList)
        {

            gardesFeeConfig.Upsert(configList);

            InstallemtTypeSelectList(lang);

            string isCurrentYearQuery = @" SELECT 1    
                                          FROM [campuserp].[dbo].[AdmGradesSchool] grades
                                          inner join [campuserp].[dbo].[admStudyear] studyyear
                                          on grades.YearID = studyyear.YearID
                                          where studyyear.CurrentYear =1
                                           and grades.GradeID = @p0";

            var isCurrentYear = Context.Database.SqlQuery<int>(isCurrentYearQuery, configList.FirstOrDefault().GradeID).ToList();

            ViewBag.isCurrentYear = isCurrentYear.Count() == 0 ? false : true;

            return PartialView("_FeeConfig" , gardesFeeConfig.GetAllByGradeId(configList.First().GradeID));
        }

    }
}