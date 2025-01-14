using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    public class HolidayTypesController : BaseController
    {
        private HolidayTypesHandler _HolidayTypesHandler = new HolidayTypesHandler();

        private int companyId = 0;

        public HolidayTypesController()
        {
            // int companyId = 0;

            if (System.Web.HttpContext.Current.Session["CompanyID"] != null)
            {
                companyId = int.Parse(System.Web.HttpContext.Current.Session["CompanyID"].ToString());

            }
        }


        // GET: HolidayTypes
        public ActionResult Index()
        {
            return PartialView(_HolidayTypesHandler.GetAllByCompanyID(companyId));
        }


        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(HolidayTypesViewModel model)
        {
            if (ModelState.IsValid)
            {
               // model.CompanyID = companyId;
                _HolidayTypesHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _HolidayTypesHandler.GetAllByCompanyID(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _HolidayTypesHandler.GetAllByCompanyID(companyId));
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _HolidayTypesHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _HolidayTypesHandler.GetAllByCompanyID(companyId));
        }


        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _HolidayTypesHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HolidayTypesViewModel model)
        {
            if (ModelState.IsValid)
            {
                _HolidayTypesHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _HolidayTypesHandler.GetAllByCompanyID(companyId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _HolidayTypesHandler.GetAllByCompanyID(companyId));
        }


    }
}