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
    [Authorize(Roles = "SuperAdmin")]
    public class NationalityController : Controller
    {
        private NationalityHandler _NationalityHandler = new NationalityHandler();

        public ActionResult Index()
        {
            int companyId = 0;

            if (Session["CompanyID"] != null)
            {
                companyId = int.Parse(Session["CompanyID"].ToString());
            }

             return PartialView(_NationalityHandler.GetAllByCompanyID(companyId));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("_Create");
        }



        [HttpPost]
        public ActionResult Create(NationalityViewModel model)
        {
            if (ModelState.IsValid)
            {
                int companyId = 0;

                if (Session["CompanyID"] != null)
                {
                    companyId = int.Parse(Session["CompanyID"].ToString());

                }
                model.CompanyID = companyId;
                _NationalityHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _NationalityHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _NationalityHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _NationalityHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _NationalityHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _NationalityHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NationalityViewModel model)
        {
            if (ModelState.IsValid)
            {
                _NationalityHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _NationalityHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _NationalityHandler.GetAll());
        }

    }
}