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
    public class BusCostController : Controller
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();

        private BusCostHandler _BuscostHandler = new BusCostHandler();
        private BusHandler _busHandler = new BusHandler();

        public ActionResult Index()
        {
             return PartialView(_BuscostHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Create()
        {
            string query = @" SELECT B.* FROM [dbo].[Buses] B
                              LEFT OUTER join [dbo].[AccBusCosts] C
                                ON B.BusID=C.BusID
                                WHERE C.BusNo IS  NULL";

            ViewBag.bus = new SelectList(Context.Database.SqlQuery<BusViewModel>(query).ToList(), "BusID", "BusNo");

            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(BusCostViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BuscostHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _BuscostHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _BuscostHandler.GetAll());
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _BuscostHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _BuscostHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _BuscostHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusCostViewModel model)
        {
            if (ModelState.IsValid)
            {
                _BuscostHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _BuscostHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _BuscostHandler.GetAll());
        }
    }
}