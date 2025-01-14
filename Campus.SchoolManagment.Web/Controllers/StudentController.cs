using Campus.School.Management.Logic.BusinessLayer;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using Campus.SchoolManagment.Web.Helper;
using Campus.SchoolManagment.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using NReco.PdfGenerator;
using System.Net;
using System.Threading.Tasks;

namespace Campus.SchoolManagment.Web.Controllers
{
    //[Filters.Compress]
    public class StudentController : BaseController
    {
        AdmInitialRegistrationHandler initialRegistration = new AdmInitialRegistrationHandler();
        StudentHandler _StudentHandler = new StudentHandler();
        AccFeeItemHandler _AccFeeItemHandler = new AccFeeItemHandler();
        BusStudentOccupationHandler _BusStudentOccupationHandler = new BusStudentOccupationHandler();
        NationalityHandler Nationalityhandler = new NationalityHandler();
        StudyYearHandler studyyearhandler = new StudyYearHandler();
        //AcademicYearHandler academicyearhandler = new AcademicYearHandler();
        //ClassRoomHandler _ClassRoomHandler = new ClassRoomHandler();
        //AdmStudent_ClassHandler _Student_ClassHandler = new AdmStudent_ClassHandler();
        //------------------------------------------------------------------------//
        BusHandler _busHandler = new BusHandler();
        BusCostHandler busCostHandler = new BusCostHandler();
        AccStudentConfigHandler _AccStudentConfigHandler = new AccStudentConfigHandler();
        //AccStudentConfigurationHandler _AccStudentConfigrationHandler = new AccStudentConfigurationHandler();
        //ccountStudentFeesListHandler _AccountStudentFeesListHandler = new AccountStudentFeesListHandler();
        //-----------------------------------------------------------------------//
        AccStudentFeesHandler _Handler = new AccStudentFeesHandler();
        AccStudentConfigHandler _studConfig = new AccStudentConfigHandler();
        //BusCostHandler _BusCostHandler = new BusCostHandler();
        //----------------------------------------------------------------------------------------
        //create user Account
        private SchoolUserController _UserController = new SchoolUserController();
        private AccountController _AccountController = new AccountController();
        ApplicationDbContext dbMembership = new ApplicationDbContext();
        //------------------------------------------------------------------------------------

        BankCurrencyHandler _bankCurrency = new BankCurrencyHandler();
        EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();
        AccountTreasuryHandler _TreasuryHandler = new AccountTreasuryHandler();
        AccountTreeHandler _TreeHandler = new AccountTreeHandler();
        BankBranchHandler _HandlerBankBranch = new BankBranchHandler();
        private AccountTreeHandler _AccountTreeHandler = new AccountTreeHandler();
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        AccStudentFeeHandler accStudentFee = new AccStudentFeeHandler();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        ReceiptHandler receiptHandler = new ReceiptHandler();
        StudentTransactionsHandler transactions = new StudentTransactionsHandler();
        private TransportBusesHandler _TransportBuses = new TransportBusesHandler();

        public ActionResult Index(int? Flage)
        {

            if (schoolId != 0)

            {
                ViewBag.Flage = Flage;


                ViewBag.YearID = new SelectList(studyyearhandler.GetAll(), "YearID", "YearName" + lang);
                ViewBag.CurrentYear = studyyearhandler.GetAll().Where(x => x.CurrentYear == 1).Select(x => x.YearID).FirstOrDefault() ;


                TempData["Flag"] = Flage;

                return PartialView();
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }





        }

        [Filters.Compress]
        public object StudentList(int GradeID = 0, string YearID = "null")
        {
            ViewBag.Flage = TempData["Flag"];
            TempData["Flag"] = ViewBag.Flage;
            List<StudentViewModel> List = new List<StudentViewModel>();
            if (!(GradeID == 0 && YearID.Equals("null")))
            {
                List = _StudentHandler.GetAllByGrades(GradeID, YearID);
            }
            else
            {

                var currentYear = studyyearhandler.GetAll().Where(x => x.IsDefault == true).Select(x => x.YearID).FirstOrDefault();

                List = _StudentHandler.GetAll().Where(x => x.YearID == currentYear).ToList();

            }
            object jObj = new
            {
                data = List
            };

            //var model = _StudentHandler.GetAll();
            return JsonConvert.SerializeObject(jObj, Formatting.None);

            //return PartialView("_List", model);
        }

        public ActionResult FeePayment()
        {


            return PartialView();
        }

        public ActionResult GetStudentsFeePayment(long id, int GradeID = 0)
        {
            List<AccStudentFeeViewModel> model = getStudentFees(id, GradeID);

            return PartialView("_FeePaymentTable", model);
        }

        private List<AccStudentFeeViewModel> getStudentFees(long id, int GradeID = 0)
        {
            string query = string.Empty;


            if (GradeID == 0)
            {
                query = "select * from dbo.GetFeePaymentTable(@p0) order by [priority]";
            }
            else
            {
                query = "select * from dbo.GetFeePaymentTable(@p0) order by [priority]";

            }

            var model = Context.Database.SqlQuery<AccStudentFeeViewModel>(query, id).ToList();

            return model;
        }

        //private decimal getMinimumFees(long id, int GradeID = 0)
        //{


        //}


        public object getAllStudents()
        {

            //ViewBag.Students = new SelectList(_StudentHandler.GetAll(),"StudentAcdID","NameEn");
            dynamic studentsObj = new
            {

                IsError = false,
                data = _StudentHandler.GetAll().Select(x => new
                {
                    value = x.StudentAcdID,
                    name = x.NameEn

                })

            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(studentsObj);
        }



        public JsonResult PopulateGradeList(int id)
        {

            string query = @"SELECT DISTINCT AdmGradesSchool.GradeID,AdmGradesSchool.gradeOrig, AdmGradesSchool.GradeSchoolNameE as Gname
                            FROM AdmGradesSchool 
                            inner join admStudyear
                           ON admStudyear.YearID = AdmGradesSchool.YearID
                            WHERE (AdmGradesSchool.YearID = @p0)
                            order by AdmGradesSchool.gradeOrig";


            var results = Context.Database.SqlQuery<GradeList>(query, id).ToList();

            //ViewBag.GradeID = new SelectList(results, "GradeID", "GradeSchoolNameE");

            return Json(results, JsonRequestBehavior.AllowGet);
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
            public string GnameE { get; set; }


        }


        //public ActionResult StudentList(int GradeID, string YearID)
        //{
        //    ViewBag.Flage = TempData["Flag"];
        //    TempData["Flag"] = ViewBag.Flage;

        //    var model = _StudentHandler.GetAllByGrades(GradeID, YearID);
        //    //var model = _StudentHandler.GetAll();
        //    return PartialView("_List", model);
        //}

        public ActionResult PlacementTest()
        {
            ViewBag.Flage = TempData["Flag"];
            TempData["Flag"] = ViewBag.Flage;

            List<GradeList> results = GetGradesList();
            ViewBag.GradeID = new SelectList(results, "GradeID", "Gname");

            //var model = _StudentHandler.GetAll().Take(5).ToList() ;
            //var model = _StudentHandler.GetAll();
            return PartialView("PlacementTest");
        }

        [HttpGet]
        public ActionResult PlacementTestStudents(int id)
        {

            return PartialView("_PlacementResultList", initialRegistration.GetAllByGrades(id));
        }



        public ActionResult SaveResult(int id, string result, int grade)
        {
            initialRegistration.SaveResult(id, result);

            return PartialView("_PlacementResultList", initialRegistration.GetAllByGrades(grade));
        }




        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.Nationality = new SelectList(Nationalityhandler.GetAll(), "NationalityID", "NationalityEn");

            //string query = @"SELECT DISTINCT AdmGradesSchool.GradeID,AdmGradesSchool.gradeOrig, AdmGradesSchool.GradeSchoolNameE as GnameE
            //                FROM AdmGradesSchool 
            //                inner join AdmSemsters
            //                ON AdmSemsters.GradeID = AdmGradesSchool.GradeID
            //                WHERE (AdmGradesSchool.YearID ='8')
            //                order by AdmGradesSchool.gradeOrig";


            //_StudentHandler.RegisterStudent(model);

            var results = GetGradesList();

            ViewBag.Grade = new SelectList(results, "GradeID", "Gname");
            ViewBag.Year = new SelectList(studyyearhandler.GetAll(), "YearID", "YearName" + lang);


            var model = _StudentHandler.Create(id);

            model.initId = id;



            return PartialView("_Create", model);
        }

        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {

                TempData["xc"] = ImageFile;

            }

            return Json("sucess");

        }

        [HttpPost]
        public ActionResult Create(StudentViewModel model)
        {
            ViewBag.Flage = 1;
            if (ModelState.IsValid)
            {

                HttpPostedFileBase ImageFile = TempData["xc"] as HttpPostedFileBase;

                string Name = Path.GetFileNameWithoutExtension(ImageFile.FileName);

                string vExtension = Path.GetExtension(ImageFile.FileName);

                string ImageName = Name + "_" + Guid.NewGuid() + vExtension;

                string physicalPath = Server.MapPath(Statistics.StudentImagesPath + ImageName);
                ImageFile.SaveAs(physicalPath);

                model.Student_Img = ImageName;

                //create Student
                _StudentHandler.RegisterStudent(model);

                // create User
                string userName = model.NameEn;
                //SchoolUserViewModel _dbUser = _AccountController.Getcookie();
                //SchoolUserViewModel _schoolUser = new SchoolUserViewModel()
                //{
                //    Name = userName,
                //    Email = model.Mail,
                //    Password = model.Password,
                //    ConfirmPassword = model.ConfirmPassword,
                //    SchoolID = _dbUser.SchoolID,
                //    RoleID = dbMembership.Roles.Where(x => x.Name == "Student").FirstOrDefault().Id,
                //};
                //int _userSaved = _UserController.CreateUser(_schoolUser);
                // if (_userSaved == 1)
                //{
                //TempData["Msg"] = 1;
                //return PartialView("_List", _StudentHandler.GetAll());
                //}
                // else if (_userSaved == 5)
                // {
                //_StudentHandler.Delete(model.ID);
                //TempData["Msg"] = 5;
                //return PartialView("_List", _StudentHandler.GetAll());
                //}
                //else
                //  {
                // delete Student if user not save
                //    _StudentHandler.Delete(model.ID);
                //    TempData["Msg"] = 4;
                //    return PartialView("_List", _StudentHandler.GetAll());
                //}


                return PartialView("_PlacementResultList", initialRegistration.GetAllByGrades((int)model.GradeID));
            }
            else
            {

                ModelState.AddModelError(String.Empty, "Validation Errors , Try Again...");
            }
            TempData["Msg"] = 4;

            return PartialView("_PlacementResultList", initialRegistration.GetAllByGrades((int)model.GradeID));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            ViewBag.Flage = 1;
            _StudentHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _StudentHandler.GetAll());
        }

        public ActionResult Edit(int id)
        {
            var model = _StudentHandler.GetById(id);
            //model.check = false;
            ViewBag.Nationality = new SelectList(Nationalityhandler.GetAll(), "NationalityID", "NationalityEn");
            // ViewBag.Grade = new SelectList(academicyearhandler.GetAll(), "AcademicID", "AcadmicName");
            //ViewBag.Year = new SelectList(studyyearhandler.GetAll(), "YearID", "YearName"+lang);
            //ViewBag.ClassRoom = new SelectList(_ClassRoomHandler.GetAll().Where(x=>x.AcademicYearID==model.GradeID), "ClassID", "ClassName");

            return PartialView("_Edit", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel model)
        {
            if (ModelState.ContainsKey("Password"))
                ModelState["Password"].Errors.Clear();
            if (ModelState.ContainsKey("ConfirmPassword"))
                ModelState["ConfirmPassword"].Errors.Clear();

            ViewBag.Flage = 1;
            if (ModelState.IsValid)
            {
                if (TempData["xc"] != null)
                {
                    HttpPostedFileBase ImageFile = TempData["xc"] as HttpPostedFileBase;

                    string Name = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string vExtension = Path.GetExtension(ImageFile.FileName);
                    string ImageName = Name + "_" + Guid.NewGuid() + vExtension;

                    string physicalPath = Server.MapPath(Statistics.StudentImagesPath + ImageName); ImageFile.SaveAs(physicalPath);

                    model.Student_Img = ImageName;
                    _StudentHandler.Update(model);
                    TempData["Msg"] = 2;
                    return PartialView("_List", _StudentHandler.GetAll());

                }
                else
                {
                    _StudentHandler.Update(model);
                    TempData["Msg"] = 2;
                    return PartialView("_List", _StudentHandler.GetAll());
                }

            }


            TempData["Msg"] = 4;
            return PartialView("_List", _StudentHandler.GetAll());
        }


        public ActionResult StudentDetails(int id)
        {

            string externalConnectionString = Context.AdmSchools.Where(x => x.SchoolID == _dbUser.SchoolID).Select(x => x.ConnectionString).SingleOrDefault().ToString();

            ExternalSchoolContext ExternalContext = new ExternalSchoolContext(externalConnectionString);

            string query = @"SELECT StudAcdID StudentAcdID,
                              +' '+ NameFt
                              +' '+ NameM
                              +' '+ NameFm NameEn
                               ,ISNULL([Address],' Not Specified')[Address]
                               ,Nationalities.NationalityEn
                              ,Mobile
                              ,Mail
                          FROM [campuserp].[dbo].[AdmStudents]
                          left join Nationalities 
                         on AdmStudents.Nationality=Nationalities.NationalityID
                          Where StudAcdID = @p0 ";



            var student = ExternalContext.Database.SqlQuery<StudentViewModel>(query, id).SingleOrDefault();

            ViewBag.StudentName = student.NameEn;
            ViewBag.Nationality = student.NationalityEn;
            ViewBag.Address = student.Address;
            ViewBag.Mobile = student.Mobile;
            ViewBag.Mail = student.Mail;


            List<AccStudentFeeViewModel> model = getStudentFees(id);

            return PartialView("_StudentDetails", model);
        }


        //[HttpPost]
        //public JsonResult GetClass(int id)
        //{

        //    var list = _ClassRoomHandler.GetAll().Where(x => x.AcademicYearID == id).Select(x => new { x.ClassID, x.ClassName }).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);

        //}


        //[HttpPost]
        // public JsonResult classSpace(int ClassID, int YearID, bool check)
        // {
        //     if (ClassID != 0 && YearID != 0 && check == true)
        //     {
        //         var _classroomCount = _ClassRoomHandler.GetById(ClassID).StudentCount;
        //         int count = _Student_ClassHandler.GetAll().Where(x => x.ClassID == ClassID && x.YearID == YearID).Count();
        //         if (count >= _classroomCount)
        //         {
        //             return Json(false, JsonRequestBehavior.AllowGet);
        //         }

        //         else
        //         {
        //             return Json(true, JsonRequestBehavior.AllowGet);

        //         }
        //     }

        //     return Json(true, JsonRequestBehavior.AllowGet);
        // }


        [HttpGet]
        public ActionResult Configuration(long StudentRefId)
        {
            ViewBag.Flage = 2;

            //  ViewBag.FeeItemId = new SelectList(FeeList(StudentRefId), "AccountTreeID", "AccountName" + lang.ToUpper());

            ViewBag.Ref_Id = StudentRefId;

            ViewBag.BusId = new SelectList(_busHandler.GetAll(), "BusID", "BusNo");

            //AccStudentConfigViewModel model = new AccStudentConfigViewModel();
            //model.Ref_Id = StudentRefId;

            //var studentconfig = _AccStudentConfigHandler.GetAll();//.Where(x=>x.StudentAcdID==StudentAcdID);
            //var _AccountStudentFeesList = _AccountStudentFeesListHandler.GetAll().Where(x => x.AcademicYearID == GradeID).FirstOrDefault();

            //if (studentconfig == null)
            //{
            //    var model = _AccStudentConfigrationHandler.Create();

            //    model.StudentAcdID = StudentAcdID;
            //    model.AcademicYear = GradeName;
            //    if (_AccountStudentFeesList == null)
            //    {
            //        model.BooksPrice = 0; model.StudyFees = 0;  model.BooksNetPrice = 0;  model.StudyNetFees = 0;
            //    }
            //    else
            //    {
            //     model.BooksPrice = _AccountStudentFeesList.BooksFees;
            //     model.BooksNetPrice = _AccountStudentFeesList.BooksFees;
            //     model.StudyFees = _AccountStudentFeesList.StudyFees;
            //     model.StudyNetFees = _AccountStudentFeesList.StudyFees;
            //    }

            //    return PartialView("_Configuration", model);

            //}

            //else
            //{
            return PartialView("_Configuration");

            //}

        }

        public object FeeDropdownList(long id)
        {
            //ViewBag.bus = new SelectList(_busHandler.GetAll(), "BusID", "BusNo");
            // ViewBag.FeeItemId = new SelectList(_AccFeeItemHandler.GetAll(), "ID", "Name_"+lang.ToUpper());

            var list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == 1035 && (_AccStudentConfigHandler.GetAll().Where(c => c.Ref_Id == id).Select(c => c.FeeItemId).ToArray().Contains(x.AccountTreeID) == false))
                 .Select(q => new
                 {
                     value = q.AccountTreeID,
                     name = lang.ToLower() == "ar" ? q.AccountNameAR : q.AccountNameEN
                 });



            return JsonConvert.SerializeObject(list);
        }








        [HttpGet]
        public PartialViewResult GetConfigList(long id)
        {

            var studentconfig = _AccStudentConfigHandler.GetAllByRefId(id);

            return PartialView("_ConfigurationList", studentconfig);
        }

        [HttpPost]
        public ActionResult Configuration(AccStudentConfigViewModel model)
        {
            ViewBag.Flage = 2;
            model.ID = 0;
            if (ModelState.IsValid)
            {

                if (model.ID == 0)
                {
                    _AccStudentConfigHandler.Create(model);
                    //TempData["Msg"] = 1;
                }
                //else
                //{
                //    _AccStudentConfigrationHandler.Update(model);
                //    TempData["Msg"] = 2;
                //}
                var studentconfig = _AccStudentConfigHandler.GetAll().Where(x => x.Ref_Id == model.Ref_Id);
                ViewBag.StudentRefId = model.Ref_Id;

                return PartialView("_ConfigurationList", studentconfig);
            }

            else
            {


                //TempData["Msg"] = 4;
                var studentconfig = _AccStudentConfigHandler.GetAll().Where(x => x.Ref_Id == model.Ref_Id);
                ViewBag.StudentRefId = model.Ref_Id;

                return PartialView("_ConfigurationList", studentconfig);
            }
        }

        [HttpPost]
        public ActionResult BusConfiguration(AccStudentConfigViewModel model)
        {
            ViewBag.Flage = 2;
            model.ID = 0;
            if (ModelState.IsValid)
            {

                if (model.ID == 0)
                {
                    model.FeeItemId = 7; //readonly value only for bus fee


                    model.Description = string.Format("Bus Fee Student ref Id :{0}, more details--({1})", model.Ref_Id, model.Description);

                    _AccStudentConfigHandler.Upsert(model);

                    BusStudentOccupationViewModel busStudent = new BusStudentOccupationViewModel();

                    busStudent.Ref_Id = model.Ref_Id;
                    busStudent.BusID = model.BusID;
                    busStudent.MorningTrip = model.MorningTrip;
                    busStudent.EviningTrip = model.EveningTrip;

                    _BusStudentOccupationHandler.Upsert(busStudent);

                    //TempData["Msg"] = 1;
                }
                //else
                //{
                //    _AccStudentConfigrationHandler.Update(model);
                //    TempData["Msg"] = 2;
                //}
                var studentconfig = _AccStudentConfigHandler.GetAll().Where(x => x.Ref_Id == model.Ref_Id);
                ViewBag.StudentRefId = model.Ref_Id;

                return PartialView("_ConfigurationList", studentconfig);
            }

            else
            {


                //TempData["Msg"] = 4;
                var studentconfig = _AccStudentConfigHandler.GetAll().Where(x => x.Ref_Id == model.Ref_Id);
                ViewBag.StudentRefId = model.Ref_Id;

                return PartialView("_ConfigurationList", studentconfig);
            }
        }


        public object GetBusFees(int id)
        {

            dynamic jobj = new { };
            var busCost = busCostHandler.GetByBusId(id);
            if (busCost != null)
            {
                jobj = new
                {
                    busCost.BusID,
                    busCost.SingleTripCost,
                    busCost.DoubleTripCost

                };
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(jobj);
        }

        public ActionResult EditConfig(int id)
        {
            var model = _AccStudentConfigHandler.GetById(id);

            return PartialView("_EditConfig", model);

        }

        [HttpPost]
        public ActionResult EditConfig(AccStudentConfigViewModel model)
        {

            _AccStudentConfigHandler.Update(model);

            //ViewBag.FeeItemId = new SelectList(_AccFeeItemHandler.GetAll(), "ID", "Name_" + lang.ToUpper());
            var studentconfig = _AccStudentConfigHandler.GetAll().Where(x => x.Ref_Id == model.Ref_Id);
            ViewBag.StudentRefId = model.Ref_Id;

            // TempData["Msg"] = 2;

            return PartialView("_ConfigurationList", studentconfig);

        }


        [HttpGet]
        public ActionResult DeleteConfig(int id)
        {
            return PartialView("_DeleteConfig", id);

        }

        [HttpPost, ActionName("DeleteConfig")]
        public JsonResult ConfrimDeleteConfig(int id)
        {
            ViewBag.Flage = 2;
            _AccStudentConfigHandler.Delete(id);

            //TempData["Msg"] = 3;
            return Json(id);
        }

        public ActionResult BusCostSearch(int BusID, string TripType)
        {
            decimal? cost = 0;
            //var _BusCost = _BusCostHandler.GetAll().Where(x => x.BusID == BusID).FirstOrDefault();

            //if (_BusCost != null)
            //{
            //    if (TripType == "Oneway")
            //    {
            //        cost = _BusCost.SingleTripCost;
            //    }

            //    else if(TripType == "Twoway")
            //    {
            //        cost = _BusCost.DoubleTripCost;
            //    }

            //}

            return Json(cost, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult FeeSelect(int StudentRefId)
        {
            ViewBag.Flage = 3;

            ViewBag.StudentConfigrationID = new SelectList(_AccStudentConfigHandler.GetConfigsByStudent(StudentRefId), "ID", "FeeItemName");
            ViewBag.StudentRefId = StudentRefId;
            return PartialView("_FeeSelect");
        }

        [HttpGet]
        public ActionResult FeeSearch(int ConfigID)
        {
            ViewBag.Flage = 3;
            var list = _Handler.Create(ConfigID);


            //list.YearID = YearID;
            //list.GradeID =  GradeID;


            list.StudentConfigrationID = ConfigID;
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter" + lang);
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "Name" + lang);
            ViewBag.Currency = new SelectList(_bankCurrency.GetAll(), "BankCurrencyID", "Currency_Type");
            ViewBag.Treasury = new SelectList(_TreasuryHandler.GetAll(), "ID", "TreasuryName" + lang);
            ViewBag.BranchList = new SelectList(_HandlerBankBranch.GetAll(), "ID", "BranchName" + lang);

            return PartialView("_PartialFees", list);
        }

        //deprecated
        public ActionResult getAllFees(long? StudentAcdID)
        {
            string query = @"SELECT CAST(SUM(config.Amount) AS decimal) totalBeforeTax ,CAST(SUM(config.Amount+Config.Tax) AS DECIMAL )TotalAmount,CAST(Sum(fees.PaidAmount) AS DECIMAL) PaidAmount,CAST (SUM(config.Amount-fees.PaidAmount) AS DECIMAL )remainderAmount from
            [AccStudentConfig] Config
            inner join
            [AccStudentFees] fees
            on Config.id  = fees.1
            where Config.Ref_Id= '1003'";

            AccStudentFeesViewModel model = new AccStudentFeesViewModel();

            using (var context = new SchoolManagmentDBEntities())
            {

                model = context.Database.SqlQuery<AccStudentFeesViewModel>(query).FirstOrDefault();
                model.IsPaid = true;
            }
            return PartialView("_PartialFees", model); ;

        }

        [HttpPost]
        public ActionResult CreateFees(AccStudentFeesViewModel model)
        {
            ViewBag.Flage = 3;

            model.AccStudentConfig = _Handler.GetConfigration(model.StudentConfigrationID);  //_studConfig.GetById(model.StudAcdID);
            //model.StudentConfigrationID = model.AccStudentConfigration.ID;
            model.AccStudentFee = _Handler.GetAllFeeByConfig(model.StudentConfigrationID);

            if (ModelState.IsValid)
            {
                _Handler.Create(model);
                //model.AccStudentFee = _Handler.GetAll(model.StudAcdID);
                ViewBag.flage = 3;
                // return PartialView("_PartialGrid", model);
                var list = _Handler.Create(model.StudentConfigrationID);



                return PartialView("_PartialGrid", list);
            }
            else
            {
                var list = _Handler.Create(model.StudentConfigrationID);
                TempData["Msg"] = 4;
                return PartialView("_PartialGrid", list);
            }
        }

        public async Task<ActionResult> PayFees(List<AccStudentFeeViewModel> model, StudentPayment details)
        {

            var receipt = accStudentFee.Create(model, details.StudentAcdID, details.amountPaid, details.amountBalance, details.CashAccount);

            ViewBag.StudentAcdID = details.StudentAcdID;
            ViewBag.ReceiptID = receipt.ReceiptID;

            string html = RenderViewToString(this.ControllerContext, "_Receipt", receipt);

            receiptHandler.SaveReceipt(html);

            receipt.email = "mohd007masood@gmail.com";
            receipt.pemail = "mohd404masood@gmail.com";

            if (details.emailBoth)
            {

                await SendDocumentAsync(receipt.email, html, receipt.ReceiptID.ToString(), receipt.pemail);

            }
            else if (details.emailStudent)
            {
                await SendDocumentAsync(receipt.email, html, receipt.ReceiptID.ToString());

            }
            else if (details.emailParent)
            {
                await SendDocumentAsync(receipt.pemail, html, receipt.ReceiptID.ToString());
            }


            //return Content(html);
            return PartialView("_Receipt", receipt);
        }

        public class StudentPayment
        {
            public int amountPaid { get; set; }
            public int amountBalance { get; set; }
            public int CashAccount { get; set; }
            public int StudentAcdID { get; set; }
            public bool emailStudent { get; set; } = false;
            public bool emailParent { get; set; } = false;
            public bool emailBoth { get; set; } = false;
        }

        [ActionName("SendDocument")]
        public async Task<JsonResult> SendDocumentAsync(string to,string html, string ReceiptID, string cc = "NULL")
        {
            try
            {
                // var body = "<p>Email From: {0} ({1})</div>{2}</div><div>{3}</div>";
                var message = new MailMessage();

                message.To.Add(new MailAddress(to));
                if (!(string.IsNullOrEmpty(cc)) && !cc.Equals("NULL"))
                { message.CC.Add(new MailAddress(cc)); }
              
                message.From = new MailAddress("campus.erp@outlook.com");  // replace with valid value

                message.Subject = "Student Payment Receipt No#" + ReceiptID;


                message.Body = "Please find the attached Receipt";//string.Format(body, model.firstname, model.email, orderTitle, orderListBuilder.ToString());


                message.Attachments.Add(new Attachment(GetPdf(html, "Receipt Of Payment"), new System.Net.Mime.ContentType("application/pdf")));

                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;
                    var credential = new NetworkCredential
                    {
                        UserName = "campus.erp@outlook.com",  // replace with valid value
                        Password = "P@ssword@!@#"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);

                }
                return Json(true); // JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }




        }
        //Dev warning this method already referenced in document controller 
        //make it a common function to avoid multiple referenced function
        public Stream GetPdf(string html, string title)
        {
            var Renderer = new HtmlToPdfConverter();
            byte[] _bufferPDF;
            Renderer.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
            Renderer.Margins.Bottom = 20;
            Renderer.Margins.Top = 30;
            Renderer.Margins.Left = 10;
            Renderer.Margins.Right = 10;

            Renderer.Size = NReco.PdfGenerator.PageSize.A4;

            Renderer.PageHeaderHtml = string.Format(@"<div style="" font-family:Arial;"">
                              <b style="" text-align:left;  "">Campus ERP</b>
                              <br/>
                              <b>Date: </b><span style="" text-align:left;"">{0}</span>
                              <div style="" text-align:center; font-size:16pt;"">{1}</div>
                              <hr/>
                              <br/>
                              </div>", DateTime.Now, title);



            Renderer.PageFooterHtml = @"<div style='text-align:center'><em style='color:rgba(0, 0, 0, 0.87); font-size:12pt'>page <span  class=""page"" ></span>  of <span class=""topage""></span></em></div>";



            _bufferPDF = Renderer.GeneratePdf(html);

            Stream stream = new MemoryStream(_bufferPDF);

            return stream;

        }


        public async Task<ActionResult>PayFeesAutomatic(List<automateFeeDetails> model, StudentPayment details)
        {

            var receipt = accStudentFee.CreateFeeAutomatic(model, details.StudentAcdID, details.amountPaid, details.CashAccount);

            ViewBag.StudentAcdID = details.StudentAcdID;
            ViewBag.ReceiptID = receipt.ReceiptID;

            string html = RenderViewToString(this.ControllerContext, "_Receipt", receipt);

            receiptHandler.SaveReceipt(html);


            receipt.email = "mohd007masood@gmail.com";
            receipt.pemail = "mohd404masood@gmail.com";

            if (details.emailBoth)
            {

                await SendDocumentAsync(receipt.email, html, receipt.ReceiptID.ToString(), receipt.pemail);

            }
            else if (details.emailStudent)
            {
                await SendDocumentAsync(receipt.email, html, receipt.ReceiptID.ToString());

            }
            else if (details.emailParent)
            {
                await SendDocumentAsync(receipt.pemail, html, receipt.ReceiptID.ToString());
            }


            //return Content(html);
            return PartialView("_Receipt", receipt);
        }

        public ActionResult StudentTransactions()
        {
            List<StudentTransactionsViewModel> model = new List<StudentTransactionsViewModel>();
            return PartialView("_Transaction", model);

        }

        public object getStudentTransactions(int id)
        {
            var data = transactions.GetAll(id);

            var jObj = new
            {
                data
            };
            return JsonConvert.SerializeObject(jObj, Formatting.None);

        }

        public object getFeeDetailsAutomatic(long id)
        {
            string query = @"select AccountTreeID,AccountNameEN AccountName,CAST((TotalAmount / installmentPeriod) * AutomaticCount AS DECIMAL(18, 2)) Amount,AutomaticCount ,CASE WHEN TotalAmount <= PaidAmount THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END IsPaid, CAST(0 AS BIT) IsSelected from dbo.GetFeePaymentTable(@p0)
                WHERE TotalAmount != 0
                order by[priority]";

            var feeDetails = Context.Database.SqlQuery<automateFeeDetails>(query, id).ToList();

            var obj = new { data = feeDetails };
            return JsonConvert.SerializeObject(obj);
        }

        [HttpPost]
        public ActionResult DeleteFees(int id, int StudentAcdID, int YearID, int GradeID)
        {

            _Handler.Delete(id);
            TempData["Msg"] = 3;

            ViewBag.Flage = 3;
            var list = _Handler.Create(StudentAcdID);
            list.YearID = YearID;
            list.GradeID = GradeID;
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenterAR");
            ViewBag.employee = new SelectList(_EmployeeHandler.GetAll(), "Employee_ID", "NameA");
            ViewBag.Currency = new SelectList(_bankCurrency.GetAll(), "BankCurrencyID", "Currency_Type");
            ViewBag.Treasury = new SelectList(_TreasuryHandler.GetAll(), "ID", "TreasuryName");
            ViewBag.BranchList = new SelectList(_HandlerBankBranch.GetAll(), "ID", "BranchName");

            return PartialView("_PartialFees", list);
        }



        /********* New Transport Module ***********/
        [HttpGet]
        public ActionResult GetTransport(int id)
        {
            ViewBag.SchoolX = _dbUser.Location.ToString().Split(',')[0];
            ViewBag.SchoolY = _dbUser.Location.ToString().Split(',')[1];
            ViewBag.AllBusLocation = JsonConvert.SerializeObject(_TransportBuses.GetAll().Select(x => new { Locations = x.FarestPoint }).ToList());
            ViewBag.StudentLocation = ""; //_StudentHandler.GetById(id).Location;

            return PartialView("_AddTransport");
        }




        /******************************************/

        public ActionResult GetAccounts2()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName" + lang);

            return PartialView("_AccountTree");
        }

        public ActionResult GetAccounts1()
        {
            ViewBag.MainaccountTree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 1).OrderBy(x => x.AccountTreeID), "AccountTreeID", "AccountName" + lang);

            return PartialView("_AccountTree1");
        }




        [HttpPost]
        public JsonResult GetBranchsAccount(int id)
        {

            dynamic list;
            if (lang.ToLower().Equals("ar"))
                list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).Select(x => new { x.AccountTreeID, x.AccountNameAR }).ToList();
            else
                list = _TreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).Select(x => new { x.AccountTreeID, x.AccountNameEN }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }
        [Filters.FileDownload]
        public ActionResult ExportAll(int GradeID, string YearID)
        {
            var list = _StudentHandler.GetAllByGrades(GradeID, YearID).ToList();

            return new ExcelResult(list, string.Format("Students-{1}-{0}", DateTime.Now, list.FirstOrDefault().GradeName), "Students of " + list.FirstOrDefault().GradeName,

                new string[] {
               "StudentAcdID",
                "YearName",
                "GradeName",
                "NameEn",
                "ArabicName",
                "AdmDate",
                "BirthDate",
                "BirthPlace",
                "Address",
                "Sex",
                "NationalityID",
                "NationalityEn",
                "IqamaNo",
                "IqamaStartDate",
                "IqamaEndDate",
                "PassportNo",
                "PassportStartDate",
                "PassEndDate",
                "Mobile",
                "Tel",
                "Mail",
                "ParentJob",
                "ParentAddress",
                "ParentTell",
                "Requirments",
                "Orignalfees",
                "LastEduShcool1",
                "LastEduShcool2",
                "LastEduCity1",
                "LastEduCity2",
                "LastEduDateFrom1",
                "LastEduDateFrom2",
                "LastEduFull1",
                "LastEduFull2",
                "PersonStm",
                "Refrences",
                "ParentMail",
                "Parentmobile",
                "IqamaPlace",
                "IsArchives",
                "PrevSchool",
                "ParentEqmEndDate"
                });

        }

        [Filters.FileDownload]
        public ActionResult ExportPdfAll(int GradeID, string YearID)
        {

            var list = _StudentHandler.GetAllByGrades(GradeID, YearID).ToList();
            string html = RenderViewToString(this.ControllerContext, "List_Pdf", list);


            return new PDFResult(html, "Students - " + list.FirstOrDefault().GradeName, "All Students of Grade: " + list.FirstOrDefault().GradeName + " and Year: " + list.FirstOrDefault().YearName);

        }

        [Filters.FileDownload]
        public ActionResult ExportPdfDetail(int id)
        {
            var list = _StudentHandler.GetById(id);
            string html = RenderViewToString(this.ControllerContext, "Detail_Pdf", list);

            return new PDFResult(html, "Student - " + list.NameEn, "Details of Student : " + list.NameEn + " -  Grade: " + list.GradeName);

        }

        [Filters.FileDownload]
        public ActionResult ExportReceipt(int id)
        {

            string html = receiptHandler.GetReceipt(id); //RenderViewToString(this.ControllerContext, "Detail_Pdf", list);

            return new PDFResult(html, "Receipt" + id, "Receipt");
        }

    }
}