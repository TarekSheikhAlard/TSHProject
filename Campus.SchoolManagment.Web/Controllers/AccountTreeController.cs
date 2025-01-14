 using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Newtonsoft.Json;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.SchoolManagment.Web.Helper;
using static Campus.SchoolManagment.Web.Helper.SemanticControls;

namespace Campus.SchoolManagment.Web.Controllers
{

    [Authorize]
    public class AccountTreeController : BaseController
    {
        private AccountTreeHandler _AccountTreeHandler = new AccountTreeHandler();
        private BankCurrencyHandler _BankCurrencyHandler = new BankCurrencyHandler();
        SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();

       
        public ActionResult Index()
        {
            return PartialView();
        }

        //[Filters.Compress]
        public ActionResult Create()
        {
            

            //ViewBag.AccountType = new SelectList(AccountTypeList(),"value","name");
          //  ViewBag.level = level;
          
            AccountTreeViewModel _Model = _AccountTreeHandler.Create();

            List<DropdownList> CreditDebitFlags = new List<DropdownList>();

            CreditDebitFlags.Add(new DropdownList
            {
                name = "Credit",
                value = "C"

            });
            CreditDebitFlags.Add(new DropdownList
            {
                name = "Debit",
                value = "D"

            });

            ViewBag.CreditDebitFlags = CreditDebitFlags.ToList();
            // _Model.AccountTreeParentID = _parentID;
            return PartialView("_Create", _Model);
        }

        [HttpPost]
        [Filters.Compress]
        public ActionResult Create(AccountTreeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountTreeHandler.Create(model);
             
             

                TempData["Msg"] = 1;
                return PartialView("_List", _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == model.AccountTreeParentID).ToList());
            }
         
            TempData["Msg"] = 4;
            
            return PartialView("_List", _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == model.AccountTreeParentID).ToList());
        }


        //[Filters.Compress]
        public ActionResult Edit(int id)
        {
            ViewBag.Currency = new SelectList(_BankCurrencyHandler.GetAll(), "BankCurrencyID", "Currency_Type");

            AccountTreeViewModel _Tree = _AccountTreeHandler.GetById(id);
        
            _Tree.check = false;

            List<DropdownList> CreditDebitFlags = new List<DropdownList>();

            CreditDebitFlags.Add(new DropdownList
            {
                name = "Credit",
                value = "C"

            });
            CreditDebitFlags.Add(new DropdownList
            {
                name = "Debit",
                value = "D"

            });

            ViewBag.CreditDebitFlags = CreditDebitFlags.ToList();
            return PartialView("_Edit", _Tree);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       [Filters.Compress]
        public ActionResult Edit(AccountTreeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountTreeHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == model.AccountTreeParentID).ToList());
            }

            TempData["Msg"] = 4;
            return PartialView("_List", _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == model.AccountTreeParentID).ToList());
        }
        

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }

        [HttpPost, ActionName("Delete")]
      
        public ActionResult DeleteConfirmed(int id)
        {
            int? parentID = _AccountTreeHandler.GetById(id).AccountTreeParentID;
            try
            {

                var result =_AccountTreeHandler.Delete(id);
                if (result) {
                    TempData["Msg"] = 3;
                }
                else {
                    TempData["Msg"] = 5;
                }
                return PartialView("_List", _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == parentID).ToList());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == parentID).ToList());
            }

        }




        public ActionResult TreeList()
        {
            return PartialView("_TreeList");
        }


        public string GetChildAccountTreePageMenu()
        {
            return _AccountTreeHandler.GetChildAccountTreeMenu(false);

        }

      

        public ActionResult GetChild(int id = 0)
        {
            List<AccountTreeViewModel> list;
            if (id != 0)
            {

                 list = _AccountTreeHandler.GetAll().Where(x => x.AccountTreeParentID == id).ToList();
                if (list == null || list.Count == 0)
                {
                    ViewBag.Parentlevel = _AccountTreeHandler.GetById(id).AccountTreeLevel;

                    return PartialView("_List", list);
                }
                ViewBag.Parentlevel = list.FirstOrDefault().AccountTreeParent.AccountTreeLevel;

            }
            else {

                 list = _AccountTreeHandler.GetAll().Where(x=>x.AccountTreeCode!=null &&x.AccountTreeCode!="").OrderBy(x=>x.AccountTreeCode).ToList();
            }
            return PartialView("_List", list);
        }

        [HttpPost]
        public JsonResult CheckAccountExist(string AccountNameEn, bool check =true)
        {
            if (AccountNameEn != null && check == true)
            {
                if (_AccountTreeHandler.GetAll().Any(x => x.AccountNameEN == AccountNameEn))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CheckAccountEnExist(string AccountNameAR, bool check1 = true)
        {
            if (AccountNameAR != null && check1 == true)
            {
                if (_AccountTreeHandler.GetAll().Any(x => x.AccountNameAR == AccountNameAR || x.AccountNameEN == AccountNameAR))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public object quickAccountTreeList(int id = 0,int index=-1) {
            List<quickAccountTreeListModel> list;

            if (id ==-1)
            {
                list = context.Database.SqlQuery<quickAccountTreeListModel>("select * from [AccountTreeDropdownList](@p0,1)", schoolId, id).ToList();
            }
            else if (id != 0)
            {
                list = context.Database.SqlQuery<quickAccountTreeListModel>("select * from [AccountTreeDropdownListFilter](@p0,@p1)", schoolId,id).ToList();
            }
            else {

                list = context.Database.SqlQuery<quickAccountTreeListModel>("select * from AccountTreeDropdownList(@p0,1)", schoolId).ToList();
            }

            var obj = new
            {
                list,
                index
            };

            return JsonConvert.SerializeObject(obj);
        }
        public class quickAccountTreeListModel {

            public int value { get; set; }
            public string name { get; set; }
            public string text { get; set; }
        }


        public object quickAccountTreeListFull()
        {
            return JsonConvert.SerializeObject(_AccountTreeHandler.quickAccountTreeListFull());
        }



        [HttpGet]
        public object AccountTypeList()
        {
            List<quickAccountTreeListModel> list;


            list = context.Database.SqlQuery<quickAccountTreeListModel>("select value, name,text from AccountTypeDropDown(@p0)", schoolId).ToList();

            //if (lang.ToLower() == "ar") {
            //    list = context.Database.SqlQuery<AccountTypeListModel>("select AccountTreeID as value,AccountNameAR as name from AccountTypeDropDown(@p0)", schoolId).ToList();
            //}
            //else {
            //    list = context.Database.SqlQuery<AccountTypeListModel>("select AccountTreeID as value,AccountNameEN as name  from AccountTypeDropDown(@p0)", schoolId).ToList();
            //}

            var obj = new
            {
                list
               
            };

            return JsonConvert.SerializeObject(obj);


      
            //var obj = new
            //{
            //    list
            //};

            //return JsonConvert.SerializeObject(obj);
        }
        public class AccountTypeListModel
        {

            public int value { get; set; }
            public string name { get; set; }
           
        }

        [Filters.FileDownload]
        public ActionResult ExportExcel()
        {
            var list = _AccountTreeHandler.GetAll().ToList();

            return new ExcelResult(list, "Chart of Accounts", "Chart of Accounts");

        }

        [Filters.FileDownload]
        public ActionResult ExportPdf()
        {
            var list = _AccountTreeHandler.GetAll().ToList();
            string html = RenderViewToString(this.ControllerContext, "_List_Pdf", list);

            //return new ExcelResult(list, "Chart of Accounts", "Chart of Accounts");
            return new PDFResult(html, "Chart of Accounts", "Chart of Accounts");

        }

        //[Filters.FileDownload]
        //public ActionResult ExportPdfAll(int GradeID, string YearID)
        //{

        //    var list = _StudentHandler.GetAllByGrades(GradeID, YearID).ToList();
        //    string html = RenderViewToString(this.ControllerContext, "List_Pdf", list);


        //    return new PDFResult(html, "Students - " + list.FirstOrDefault().GradeName, "All Students of Grade: " + list.FirstOrDefault().GradeName + " and Year: " + list.FirstOrDefault().YearName);

        //}


        //-----------------------unused code------------------------------

        public string GetChildAccountTreeMenu()
        {
             return _AccountTreeHandler.GetChildAccountTreeMenu(true);
        
        }
        //-----------------------------------------------------
   
    }
}