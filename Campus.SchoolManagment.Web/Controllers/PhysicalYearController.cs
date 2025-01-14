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
    public class PhysicalYearController : BaseController
    {
        private PhysicalYearHandler _PhysicalYearHandler = new PhysicalYearHandler();


        // GET: PhysicalYear
        public ActionResult Index()
        {


            if (companyId != 0)

            {
                 return PartialView(_PhysicalYearHandler.GetAllByCompany(companyId, schoolId).Where(x => x.IsCurrent == true).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }


        public ActionResult Create()
        {
            // return PartialView(_PhysicalYearHandler.Create());
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(PhysicalYearViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyID = companyId;
                model.SchoolID = schoolId;
                _PhysicalYearHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _PhysicalYearHandler.GetAllByCompany(companyId, schoolId).Where(x => x.IsCurrent == true));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _PhysicalYearHandler.GetAllByCompany(companyId, schoolId).Where(x => x.IsCurrent == true));

        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }
        /// <summary>
        /// Close Physical Year
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _PhysicalYearHandler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _PhysicalYearHandler.GetAllByCompany(companyId, schoolId).Where(x => x.IsCurrent == true));
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _PhysicalYearHandler.GetAllByCompany(companyId, schoolId).Where(x => x.IsCurrent == true));
            }
        }


        public ActionResult Edit(int id)
        {
            PhysicalYearViewModel _model = _PhysicalYearHandler.GetById(id);

            return PartialView("_Edit", _model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhysicalYearViewModel model)
        {
            if (ModelState.IsValid)
            {
                _PhysicalYearHandler.Update(model);

                TempData["Msg"] = 2;
                return PartialView("_List", _PhysicalYearHandler.GetAllByCompany(companyId, schoolId).Where(x => x.IsCurrent == true));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _PhysicalYearHandler.GetAllByCompany(companyId, schoolId).Where(x => x.IsCurrent == true));
        }



    }
}