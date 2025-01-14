using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.SchoolManagment.Web.Helper;
using Campus.School.Management.Logic.BusinessLayer;
using Newtonsoft.Json;
using static Campus.SchoolManagment.Web.Helper.SemanticControls;

namespace Campus.SchoolManagment.Web.Controllers
{
    public class TransportController : BaseController
    {
        private TransportDriverHandler _TransportDriver = new TransportDriverHandler();
        private TransportSupervisorHandler _TransportSupervisor = new TransportSupervisorHandler();
        private TransportBusesHandler _TransportBuses = new TransportBusesHandler();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
        // GET: Transport
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Driver() {

            return PartialView(_TransportDriver.GetAll());
        }

        [HttpGet]
        public ActionResult CreateDriver()
        {

            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateDriver(TransportDriverViewModel model)
        {
            if (ModelState.IsValid)
            {
                _TransportDriver.Create(model);
                TempData["Msg"] = 1;
                return PartialView("ListDriver",_TransportDriver.GetAll());
            }
            TempData["Msg"] = 4;

            return PartialView("ListDriver", _TransportDriver.GetAll());
        }

        [HttpGet]
        public ActionResult EditDriver(int id)
        {

            return PartialView(_TransportDriver.GetById(id));
        }

        [HttpPost]
        public ActionResult EditDriver(TransportDriverViewModel model)
        {
            if (ModelState.IsValid)
            {
                _TransportDriver.Update(model);
                TempData["Msg"] = 2;
                return PartialView("ListDriver", _TransportDriver.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("ListDriver", _TransportDriver.GetAll());
        }

        [HttpGet]
        public ActionResult DeleteDriver(int id)
        {
            return PartialView(id);
        }

        [HttpPost, ActionName("DeleteDriver")]
        public ActionResult ConfirmDeleteDriver(int id)
        {
            _TransportDriver.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("ListDriver", _TransportDriver.GetAll());
      
        }


        public ActionResult Supervisor()
        {

            return PartialView(_TransportSupervisor.GetAll());
        }
        [HttpGet]
        public ActionResult CreateSupervisor()
        {
           
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateSupervisor(TransportSupervisorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _TransportSupervisor.Create(model);
                TempData["Msg"] = 1;
                return PartialView("ListSupervisor", _TransportSupervisor.GetAll());
            }
            TempData["Msg"] = 4;

            return PartialView("ListSupervisor", _TransportSupervisor.GetAll());

           
        }

        [HttpGet]
        public ActionResult EditSupervisor(int id)
        {

            return PartialView(_TransportSupervisor.GetById(id));
        }

        [HttpPost]
        public ActionResult EditSupervisor(TransportSupervisorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _TransportSupervisor.Update(model);
                TempData["Msg"] = 2;
                return PartialView("ListSupervisor", _TransportSupervisor.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("ListSupervisor", _TransportSupervisor.GetAll());
        }

        [HttpGet]
        public ActionResult DeleteSupervisor(int id)
        {
            return PartialView(id);

        }


        [HttpPost, ActionName("DeleteSupervisor")]
        public ActionResult ConfirmDeleteSupervisor(int id)
        {
            _TransportSupervisor.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("ListSupervisor",_TransportSupervisor.GetAll());
        }



        public ActionResult Bus() {


            return PartialView(_TransportBuses.GetAll());
        }

        [HttpGet]
        public ActionResult CreateBus()
        {
            ViewBag.SchoolX = _dbUser.Location.ToString().Split(',')[0];
            ViewBag.SchoolY = _dbUser.Location.ToString().Split(',')[1];
            ViewBag.DriverList = _TransportDriver.GetAllFreeDrivers().Select(x => new DropdownList
            {
                name = x.DriverNameEn,
                value = x.Id.ToString(),
                text = x.DriverNameEn
            }).ToList();
            ViewBag.SupervisorList = _TransportSupervisor.GetAll().Select(x => new DropdownList
            {
                name = x.SupervisorNameEn,
                value = x.Id.ToString(),
                text = x.SupervisorNameEn
            }).ToList();

            ViewBag.AllBusLocation = JsonConvert.SerializeObject(_TransportBuses.GetAll().Select(x => new { Locations =x.FarestPoint }).ToList());

            TransportBusesViewModel model = new TransportBusesViewModel();

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult CreateBus(TransportBusesViewModel ModelValues)
        {
            if (ModelState.IsValid)
            {
                _TransportBuses.Create(ModelValues);
                TempData["Msg"] = 1;
                return PartialView("ListBus", _TransportBuses.GetAll());
            }
            TempData["Msg"] = 4;

            return PartialView("ListBus", _TransportBuses.GetAll());
      
        }

        [HttpGet]
        public ActionResult EditBus(int id)
        {
            ViewBag.SchoolX = _dbUser.Location.ToString().Split(',')[0];
            ViewBag.SchoolY = _dbUser.Location.ToString().Split(',')[1];
            ViewBag.DriverList = _TransportDriver.GetAll().Select(x => new DropdownList
            {
                name = x.DriverNameEn,
                value = x.Id.ToString(),
                text = x.DriverNameEn
            }).ToList();
            ViewBag.SupervisorList = _TransportSupervisor.GetAll().Select(x => new DropdownList
            {
                name = x.SupervisorNameEn,
                value = x.Id.ToString(),
                text = x.SupervisorNameEn
            }).ToList();

            ViewBag.AllBusLocation = JsonConvert.SerializeObject(_TransportBuses.GetAll().Where(x=>x.Id !=id).Select(x => new { Locations = x.FarestPoint }).ToList());

            TransportBusesViewModel model = new TransportBusesViewModel();

            return PartialView(_TransportBuses.GetById(id));
        }

        [HttpPost]
        public ActionResult EditBus(TransportBusesViewModel modelValues)
        {

            if (ModelState.IsValid)
            {
                _TransportBuses.Update(modelValues);
                TempData["Msg"] = 2;
                return PartialView("ListBus", _TransportBuses.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("ListBus", _TransportBuses.GetAll());
        }

        [HttpGet]
        public ActionResult DeleteBus(int id)
        {
            return PartialView(id);
        }

        [HttpPost, ActionName("DeleteBus")]
        public ActionResult ConfirmDeleteBus(int id)
        {
            _TransportBuses.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("ListBus",_TransportBuses.GetAll());
        }

    }
}