using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
   // [Filters.Compress]
    public class EmployeeAssetsController : BaseController
    {
        private EmployeeAssetsHandler _employeeAssetsHandler = new EmployeeAssetsHandler();
        private AssetTreeHandler _AssetTreeHandler = new AssetTreeHandler();
        // GET: EmployeeAssets
        public EmployeeAssetsController()
        {
            // int companyId = 0;

            if (System.Web.HttpContext.Current.Session["CompanyID"] != null)
            {
                companyId = int.Parse(System.Web.HttpContext.Current.Session["CompanyID"].ToString());

            }
        }
        public ActionResult Index(int id)
        {
            ViewBag.empid = id;
            return PartialView();
           // return PartialView(_employeeAssetsHandler.GetAllByEmployeeId(id));
        }
        public ActionResult GetList(int empid)
        {
           // return PartialView(empid);
            return PartialView("_List",_employeeAssetsHandler.GetAllByEmployeeId(empid));
        }


        [HttpGet]
        public ActionResult Create(int empid)
        {
            EmployeeAssetsViewModel model = new EmployeeAssetsViewModel();
            model.Employee_ID = empid;

            return PartialView("_Create",model);
        }

        [HttpPost]
        public ActionResult Create(EmployeeAssetsViewModel model)
        {
            if (ModelState.IsValid)
            {           
                _employeeAssetsHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _employeeAssetsHandler.GetAllByEmployeeId(model.Employee_ID));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _employeeAssetsHandler.GetAllByEmployeeId(model.Employee_ID));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _employeeAssetsHandler.Delete(id);
            var x= _employeeAssetsHandler.GetEmployeeIdFromRecId(id);
            TempData["Msg"] = 3;


            return PartialView("_List", _employeeAssetsHandler.GetAllByEmployeeId(x.Employee_ID));
        }


        public ActionResult Edit(int id)
        {
            
            return PartialView("_Edit", _employeeAssetsHandler.GetById(id));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeAssetsViewModel model)
        {
            if (ModelState.IsValid)
            {
                _employeeAssetsHandler.Update(model);
                TempData["Msg"] = 2;
               

                return PartialView("_List", _employeeAssetsHandler.GetAllByEmployeeId(model.Employee_ID));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _employeeAssetsHandler.GetAllByEmployeeId(model.Employee_ID));
        }

        public ActionResult GetAsset()
        {
            ViewBag.MainassetTree = new SelectList(_AssetTreeHandler.GetAll().Where(x => x.AssetTreeLevel == 1).OrderBy(x => x.AssetTreeID), "AssetTreeID", "AssetName" + lang);

            return PartialView("_AssetTree");
        }

        [HttpPost]
        public JsonResult GetBranchsAsset(int id)
        {

            dynamic list;
            if (lang.ToLower().Equals("ar"))
                list = _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == id).Select(x => new { x.AssetTreeID, x.AssetNameAR }).ToList();
            else
                list = _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == id).Select(x => new { x.AssetTreeID, x.AssetNameEN }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }



    }
}