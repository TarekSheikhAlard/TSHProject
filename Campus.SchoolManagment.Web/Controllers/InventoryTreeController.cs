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
    //[Filters.Compress]
    public class InventoryTreeController : BaseController
    {
        private InventoryTreeHandler _InventoryTreeHandler = new InventoryTreeHandler();



       
        public ActionResult Index()
        {
            return PartialView(_InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == null).ToList());
        }
        
        public ActionResult GetRoot()
        {
            return PartialView("_List", _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == null).ToList());
        }
       
        public ActionResult Create(int? level, int? _parentID)
        {
           
            ViewBag.level = level;

            InventoryTreeViewModel _Model = _InventoryTreeHandler.Create();

            _Model.InventoryTreeParentID = _parentID;

            return PartialView("_Create", _Model);
        }

        [HttpPost]
        public ActionResult Create(InventoryTreeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _InventoryTreeHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == model.InventoryTreeParentID).ToList());
            }

            TempData["Msg"] = 4;
            return PartialView("_List", _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == model.InventoryTreeParentID).ToList());
        }


       
        public ActionResult Edit(int id)
        {
           

            InventoryTreeViewModel _Tree = _InventoryTreeHandler.GetById(id);

            _Tree.check = false;
            return PartialView("_Edit", _Tree);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InventoryTreeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _InventoryTreeHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == model.InventoryTreeParentID).ToList());
            }

            TempData["Msg"] = 4;
            return PartialView("_List", _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == model.InventoryTreeParentID).ToList());
        }


        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            int? parentID = _InventoryTreeHandler.GetById(id).InventoryTreeParentID;
            try
            {

                _InventoryTreeHandler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == parentID).ToList());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == parentID).ToList());
            }

        }




        public ActionResult TreeList()
        {
            return PartialView("_TreeList");
        }


        public string GetChildInventoryTreePageMenu()
        {
            return _InventoryTreeHandler.GetChildInventoryTreeMenu(false);

        }



        public ActionResult GetChild(int id)
        {
            var list = _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == id).ToList();
            if (list == null || list.Count == 0)
            {
                ViewBag.Parentlevel = _InventoryTreeHandler.GetById(id).InventoryTreeLevel;

                return PartialView("_List", list);
            }
            ViewBag.Parentlevel = list.FirstOrDefault().InventoryTreeParent.InventoryTreeLevel;

            return PartialView("_List", list);
        }

        [HttpPost]
        public JsonResult CheckInventoryExist(string InventoryNameAR, bool check)
        {
            if (InventoryNameAR != null && check == true)
            {
                if (_InventoryTreeHandler.GetAll().Any(x => x.InventoryNameAR == InventoryNameAR))
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

        public ActionResult GetInventory()
        {
            ViewBag.MainInventoryTree = new SelectList(_InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeLevel == 1).OrderBy(x => x.InventoryTreeID), "InventoryTreeID", "InventoryName" + lang.ToUpper());

            return PartialView("_InventoryTree");
        }


        [HttpPost]
        public JsonResult GetBranchsInventory(int id)
        {

            var child = _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeID.Equals(id)).FirstOrDefault();

            //  if (System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft)

            dynamic list;
            if (lang.ToLower().Equals("ar"))
                list = _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == id).Select(x => new { x.InventoryTreeID, x.InventoryNameAR }).ToList();
            //list = _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == child.DepartmentTreeID).Select(x => new { x.DepartmentTreeID, x.DepartmentNameAR }).ToList();
            else
                list = _InventoryTreeHandler.GetAll().Where(x => x.InventoryTreeParentID == id).Select(x => new { x.InventoryTreeID, x.InventoryNameEN }).ToList();
            //list = _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == child.DepartmentTreeID).Select(x => new { x.DepartmentTreeID, x.DepartmentNameEN }).ToList();


            return Json(list, JsonRequestBehavior.AllowGet);


        }



        //-----------------------unused code------------------------------

        public string GetChildInventoryTreeMenu()
        {
            return _InventoryTreeHandler.GetChildInventoryTreeMenu(true);

        }
        //-----------------------------------------------------

    }
}