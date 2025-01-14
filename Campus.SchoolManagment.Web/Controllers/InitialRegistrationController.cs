using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Campus.SchoolManagment.Web.Controllers
{
    //[Filters.Compress]
    public class InitialRegistrationController : BaseController
    {
        AdmInitialRegistrationHandler initialRegistration = new AdmInitialRegistrationHandler();
        NationalityHandler _Nationalityhandler = new NationalityHandler();
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        // GET: InitialRegistration
        public ActionResult Index()
        {
            List<GradeList> results = GetGradesList();

            ViewBag.GradeID = new SelectList(results, "GradeID", "Gname");

            return PartialView();
        }

        private List<GradeList> GetGradesList()
        {
            string query = string.Format(@"SELECT DISTINCT AdmGradesSchool.GradeID,AdmGradesSchool.gradeOrig, AdmGradesSchool.GradeSchoolName{0} as Gname
                            FROM AdmGradesSchool 
                            inner join admStudyear
                            ON admStudyear.YearID = AdmGradesSchool.YearID
                            WHERE (admStudyear.CurrentYear=1)
                            order by AdmGradesSchool.gradeOrig", lang.Substring(0, 1));


            var results = Context.Database.SqlQuery<GradeList>(query).ToList();
            return results;
        }

        public class GradeList
        {
            public int GradeID { get; set; }
            public string Gname { get; set; }

        }

        public ActionResult GetStudents(int id)
        {

            return PartialView("_List", initialRegistration.GetAllByGrades(id));
        }


        [HttpGet]
        public ActionResult Create()
        {
            List<GradeList> results = GetGradesList();

            ViewBag.GradeIDList = new SelectList(results, "GradeID", "Gname");
            ViewBag.NationalityList = new SelectList(_Nationalityhandler.GetAllByCompanyID(companyId), "NationalityID", "Nationality" + lang);
            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(AdmInitialStudRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                initialRegistration.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", initialRegistration.GetAllByGrades(model.GradeID));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", initialRegistration.GetAllByGrades(model.GradeID));
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            List<GradeList> results = GetGradesList();

            ViewBag.GradeIDList = new SelectList(results, "GradeID", "Gname");
            ViewBag.NationalityList = new SelectList(_Nationalityhandler.GetAllByCompanyID(companyId), "NationalityID", "Nationality" + lang);
            return PartialView("_Edit", initialRegistration.GetById(id));

        }

        [HttpPost]
        public ActionResult Edit(AdmInitialStudRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                initialRegistration.Update(model);
                TempData["Msg"] = 1;
                return PartialView("_List", initialRegistration.GetAllByGrades(model.GradeID));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", initialRegistration.GetAllByGrades(model.GradeID));


         
        }




        [HttpGet]
        public ActionResult Delete(int id)
        {
            
            return PartialView("_Delete",id);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var gradeID = initialRegistration.DeleteReturnGradeID(id);
            TempData["Msg"] = 3;
            
            return PartialView("_List", initialRegistration.GetAllByGrades(gradeID));
        }


    }
}