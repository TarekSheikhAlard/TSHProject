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
   
    public class DepartmentTreeController : BaseController
    {
        private DepartmentTreeHandler _DepartmentTreeHandler = new DepartmentTreeHandler();

        [Filters.Compress]
        public ActionResult Index()
        {
            return PartialView(_DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == null).ToList());
        }
        [Filters.Compress]
        public ActionResult GetRoot()
        {
            return PartialView("_List", _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == null).ToList());
        }
        [Filters.Compress]
        public ActionResult Create(int? level, int? _parentID)
        {
          
            ViewBag.level = level;

            DepartmentTreeViewModel _Model = _DepartmentTreeHandler.Create();

            _Model.DepartmentTreeParentID = _parentID;

            return PartialView("_Create", _Model);
        }

        [HttpPost]
        [Filters.Compress]
        public ActionResult Create(DepartmentTreeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _DepartmentTreeHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == model.DepartmentTreeParentID).ToList());
            }

            TempData["Msg"] = 4;
            return PartialView("_List", _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == model.DepartmentTreeParentID).ToList());
        }


        [Filters.Compress]
        public ActionResult Edit(int id)
        {
            

            DepartmentTreeViewModel _Tree = _DepartmentTreeHandler.GetById(id);

            _Tree.check = false;
            return PartialView("_Edit", _Tree);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Filters.Compress]
        public ActionResult Edit(DepartmentTreeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _DepartmentTreeHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == model.DepartmentTreeParentID).ToList());
            }

            TempData["Msg"] = 4;
            return PartialView("_List", _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == model.DepartmentTreeParentID).ToList());
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
            int? parentID = _DepartmentTreeHandler.GetById(id).DepartmentTreeParentID;
            try
            {

                _DepartmentTreeHandler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == parentID).ToList());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == parentID).ToList());
            }

        }




        public ActionResult TreeList()
        {
            return PartialView("_TreeList");
        }


        public string GetChildDepartmentTreePageMenu()
        {
            return _DepartmentTreeHandler.GetChildDepartmentTreeMenu(false);

        }



        public ActionResult GetChild(int id)
        {
            var list = _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == id).ToList();
            if (list == null || list.Count == 0)
            {
                ViewBag.Parentlevel = _DepartmentTreeHandler.GetById(id).DepartmentTreeLevel;

                return PartialView("_List", list);
            }
            ViewBag.Parentlevel = list.FirstOrDefault().DepartmentTreeParent.DepartmentTreeLevel;

            return PartialView("_List", list);
        }

        [HttpPost]
        public JsonResult CheckDepartmentExist(string DepartmentNameAR, bool check)
        {
            if (DepartmentNameAR != null && check == true)
            {
                if (_DepartmentTreeHandler.GetAll().Any(x => x.DepartmentNameAR == DepartmentNameAR))
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



        public ActionResult GetDepartments()
        {
            ViewBag.MaindepartmentTree = new SelectList(_DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeLevel == 1).OrderBy(x => x.DepartmentTreeID), "DepartmentTreeID", "DepartmentName" + lang.ToUpper());

            return PartialView("_DepartmentTree");
        }


        [HttpPost]
        public JsonResult GetBranchsDepartment(int id)
        {

            var child = _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeID.Equals(id)).FirstOrDefault();

            //  if (System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft)

            dynamic list;
            if (lang.ToLower().Equals("ar"))
                 list = _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == id).Select(x => new { x.DepartmentTreeID, x.DepartmentNameAR }).ToList();
            //list = _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == child.DepartmentTreeID).Select(x => new { x.DepartmentTreeID, x.DepartmentNameAR }).ToList();
            else
             list = _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID==id).Select(x => new { x.DepartmentTreeID, x.DepartmentNameEN }).ToList();
            //list = _DepartmentTreeHandler.GetAll().Where(x => x.DepartmentTreeParentID == child.DepartmentTreeID).Select(x => new { x.DepartmentTreeID, x.DepartmentNameEN }).ToList();


            return Json(list, JsonRequestBehavior.AllowGet);


        }




        //-----------------------unused code------------------------------

        public string GetChildDepartmentTreeMenu()
        {
            return _DepartmentTreeHandler.GetChildDepartmentTreeMenu(true);

        }
        //-----------------------------------------------------

    }
}