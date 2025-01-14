using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.BusinessLayer;
using System.Threading;
using System.Globalization;
using System.Net;

namespace Campus.SchoolManagment.Web.Controllers
{
    //  [Filters.Compress]
    [Authorize]
    public class BusController : Controller
    {
        private BusHandler _Bushandler = new BusHandler();
        private BusTripHandler _BusTripHandler = new BusTripHandler();
        private EmployeeHandler _EmployeeHandler = new EmployeeHandler();


        // GET: School
        public ActionResult Index()
        {
             return PartialView(_Bushandler.GetAll());
        }

        public ActionResult Create()
        {

            return PartialView("_Create", _Bushandler.Create());
        }

        [HttpPost]
        public ActionResult Create(BusViewModel bus)
        {
            if (ModelState.IsValid)
            {
                _Bushandler.Create(bus);

                TempData["Msg"] = 1;
                return PartialView("_List", _Bushandler.GetAll());
            }
            TempData["Msg"] = 4;

            return PartialView("_List", _Bushandler.GetAll());



        }
        public ActionResult Edit(int ID)
        {


            return PartialView("_Edit", _Bushandler.GetById(ID));
        }

        [HttpPost]
        public ActionResult Edit(BusViewModel bus)
        {
            if (ModelState.IsValid)
            {
                _Bushandler.Update(bus);
            
            TempData["Msg"] = 2;
            return PartialView("_List", _Bushandler.GetAll());
        }
        TempData["Msg"] = 4;

            return PartialView("_List", _Bushandler.GetAll());


        }

    // GET: AdmStages/Delete/5
    public ActionResult Delete(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return PartialView("_Delete", ID);
        }

        // POST: Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            _Bushandler.Delete(ID);

            try
            {
                _Bushandler.Delete(ID);
                TempData["Msg"] = 3;
                return PartialView("_List", _Bushandler.GetAll());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _Bushandler.GetAll());
            }
        }
        
        [HttpPost]
        public ActionResult AddBusTrip(int? BusID)
        {
            var BusTrip = _BusTripHandler.Create();
            BusTrip.BusID = BusID;
           
            return PartialView("_BusTripItem", BusTrip);
        }

    }
}