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
    [Authorize]
    public class AccountNotespayableController : BaseController
    {

        AccountNotespayableHandler _AccountNotespayable = new AccountNotespayableHandler();

        AccountTreasuryHandler _TreasuryHandler = new AccountTreasuryHandler();

        AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();

        AccountTreeHandler _TreeHandler = new AccountTreeHandler();


        public ActionResult Index()
        {
             return PartialView(_AccountNotespayable.GetAll());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Treasury = new SelectList(_TreasuryHandler.GetAll(), "ID", "TreasuryName"+lang);
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4), "AccountTreeID", "AccountNameEN");
            
            return PartialView("_Create", _AccountNotespayable.Create());
        }


        [HttpPost]
        public JsonResult SearchTreeCode(int AccountTreeID)
        {

            var code = _TreeHandler.GetById(AccountTreeID).AccountTreeCode;
            return Json(code, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchAccountTree(string AccountTreeCode)
        {
            var list = _TreeHandler.GetAll().Where(x => x.AccountTreeCode.Contains(AccountTreeCode) && x.AccountTreeParent != null &&  x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4);
            ViewBag.TreeList = new SelectList(list, "AccountTreeID", "AccountName"+lang);
            return PartialView("_GetTreeList");

        }






        [HttpPost]
        public ActionResult Create(AccountNotespayableViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountNotespayable.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _AccountNotespayable.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _AccountNotespayable.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _AccountNotespayable.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _AccountNotespayable.GetAll());
        }


        public ActionResult Edit(int id)
        {
            ViewBag.Treasury = new SelectList(_TreasuryHandler.GetAll(), "ID", "TreasuryName"+lang);
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4), "AccountTreeID", "AccountName"+lang);

            return PartialView("_Edit", _AccountNotespayable.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountNotespayableViewModel model)
        {
            if (ModelState.IsValid)
            {
                _AccountNotespayable.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _AccountNotespayable.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _AccountNotespayable.GetAll());
        }


    }
}