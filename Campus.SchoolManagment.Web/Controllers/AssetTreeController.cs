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

    [Authorize]
    public class AssetTreeController : BaseController
    {
        private AssetTreeHandler _AssetTreeHandler = new AssetTreeHandler();
        private BankCurrencyHandler _BankCurrencyHandler = new BankCurrencyHandler();
        private DepartmentHandler _DepartmentHandler = new DepartmentHandler();


       [Filters.Compress]
        public ActionResult Index()
        {
            if (companyId != 0)

            {
                return PartialView(_AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == null).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
            
        }
        [Filters.Compress]
        public ActionResult GetRoot()
        {
            return PartialView("_List",_AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == null).ToList());
        }
        [Filters.Compress]
        public ActionResult Create(int ?level, int ?_parentID)
        {
            ViewBag.Currency = new SelectList(_BankCurrencyHandler.GetAll(), "BankCurrencyID", "Currency_Type");
           


            ViewBag.level = level;
          
            AssetTreeViewModel _Model = _AssetTreeHandler.Create();

            _Model.AssetTreeParentID = _parentID;

            return PartialView("_Create", _Model);
        }

        [HttpPost]
        [Filters.Compress]
        public ActionResult Create(AssetTreeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AssetTreeHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == model.AssetTreeParentID).ToList());
            }
         
            TempData["Msg"] = 4;
            return PartialView("_List", _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == model.AssetTreeParentID).ToList());
        }


        [Filters.Compress]
        public ActionResult Edit(int id)
        {
            ViewBag.Currency = new SelectList(_BankCurrencyHandler.GetAll(), "BankCurrencyID", "Currency_Type");

            AssetTreeViewModel _Tree = _AssetTreeHandler.GetById(id);
        
            _Tree.check = false;
            return PartialView("_Edit", _Tree);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Filters.Compress]
        public ActionResult Edit(AssetTreeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AssetTreeHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == model.AssetTreeParentID).ToList());
            }

            TempData["Msg"] = 4;
            return PartialView("_List", _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == model.AssetTreeParentID).ToList());
        }

        [Filters.Compress]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }

        [HttpPost, ActionName("Delete")]
        [Filters.Compress]
        public ActionResult DeleteConfirmed(int id)
        {
            int? parentID = _AssetTreeHandler.GetById(id).AssetTreeParentID;
            try
            {

                _AssetTreeHandler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == parentID).ToList());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == parentID).ToList());
            }

        }




        public ActionResult TreeList()
        {
            return PartialView("_TreeList");
        }


        public string GetChildAssetTreePageMenu()
        {
            return _AssetTreeHandler.GetChildAssetTreeMenu(false);

        }

      

        public ActionResult GetChild(int id)
        {
            var list = _AssetTreeHandler.GetAll().Where(x => x.AssetTreeParentID == id).ToList();
            if (list == null || list.Count == 0)
            {
                ViewBag.Parentlevel = _AssetTreeHandler.GetById(id).AssetTreeLevel;

                return PartialView("_List", list);
            }
            ViewBag.Parentlevel = list.FirstOrDefault().AssetTreeParent.AssetTreeLevel;

            return PartialView("_List", list);
        }

        [HttpPost]
        public JsonResult CheckAssetExist(string AssetNameAR, bool check)
        {
            if (AssetNameAR != null && check == true)
            {
                if (_AssetTreeHandler.GetAll().Any(x => x.AssetNameAR == AssetNameAR))
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




        //-----------------------unused code------------------------------
    
        public string GetChildAssetTreeMenu()
        {
             return _AssetTreeHandler.GetChildAssetTreeMenu(true);
        
        }
        //-----------------------------------------------------
   
    }
}