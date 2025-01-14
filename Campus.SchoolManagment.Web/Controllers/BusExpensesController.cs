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
    [Filters.Compress]
    public class BusExpensesController : Controller
    {
       private BusExpensesHandler _BusExpensesHandler = new BusExpensesHandler();

        private BusExpensesItemHandler _BusExpensesItemHandler = new BusExpensesItemHandler ();
        private EmployeeHandler _EmployeeHandler = new EmployeeHandler();
        private BusHandler _BusHandler = new BusHandler();

        // GET: AccountDailyJournal
        public ActionResult Index()
        {
            // ViewBag.Countery = new SelectList(_CountryHandler.GetAll(), "ID", "CounteryEnglishName");
            var list = _BusExpensesHandler.GetAll().ToList();
             return PartialView(list);
        }

        public ActionResult CreateList()
        {
            var list = _BusExpensesHandler.GetAll().ToList();
            return PartialView("_CreateList", list);

        }

        [HttpPost]
        public ActionResult GetDetail(int id)
        {
            var list = _BusExpensesHandler.GetById(id);
            return PartialView("_DetailList", list._BusExpensesItems);

        }

        public ActionResult Create()
        {
            ViewBag.Employee_ID = new SelectList(_EmployeeHandler.GetAll().Where(x=> x.jobID == 14), "Employee_ID", "NameA");
            ViewBag.BusID = new SelectList(_BusHandler.GetAll(), "BusID", "BusNo");

            return PartialView("_Create",_BusExpensesHandler.Create());
        }

        [HttpPost]
        public ActionResult Create(BusExpenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BusExpensesHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _BusExpensesHandler.GetAll());
            }
            TempData["Msg"] = 4;

            return PartialView("_List", _BusExpensesHandler.GetAll());

        
        }

        [HttpPost]
        public ActionResult AddDetail(int ID)
        {
            var detail = _BusExpensesItemHandler.Create();
            detail.BusExpenses_ID = ID;
           
            return PartialView("_ListItem", detail);
        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _BusExpensesHandler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _BusExpensesHandler.GetAll());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _BusExpensesHandler.GetAll());
            }
           
        }


        public ActionResult Edit(int id)
        {
            BusExpenseViewModel _BusExpenses = _BusExpensesHandler.GetById(id);
            ViewBag.Employee_ID = new SelectList(_EmployeeHandler.GetAll().Where(x => x.jobID == 14), "Employee_ID", "NameA", _BusExpenses.Employee_ID);
            ViewBag.BusID = new SelectList(_BusHandler.GetAll(), "BusID", "BusNo", _BusExpenses.BusID);
            return PartialView("_Edit", _BusExpenses);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusExpenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BusExpensesHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _BusExpensesHandler.GetAll());
            }
            TempData["Msg"] = 4;

            return PartialView("_List", _BusExpensesHandler.GetAll());


        }

        
    }
}