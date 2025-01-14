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
    public class AccountNotesReceivableController : BaseController
    {
        AccountNotesReceivableHandler _NotesReceivableHandler = new AccountNotesReceivableHandler();

        AccountTreasuryHandler _TreasuryHandler = new AccountTreasuryHandler();

        AccountCostCenterHandler _CostCenterHandler = new AccountCostCenterHandler();

        AccountTreeHandler _TreeHandler = new AccountTreeHandler();


        public ActionResult Index()
        {
             return PartialView(_NotesReceivableHandler.GetAll());
        }



        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Treasury = new SelectList(_TreasuryHandler.GetAll(), "ID", "TreasuryName"+lang);
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4), "AccountTreeID", "AccountNameEN");

            return PartialView("_Create", _NotesReceivableHandler.Create());
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
            var list = _TreeHandler.GetAll().Where(x => x.AccountTreeCode.Contains(AccountTreeCode) && x.AccountTreeParent != null && x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4);
            ViewBag.TreeList = new SelectList(list, "AccountTreeID", "AccountName"+lang);
            return PartialView("_GetTreeList");

        }



        [HttpPost]
        public ActionResult Create(AccountNotesReceivableViewModel model)
        {
            if (ModelState.IsValid)
            {
                _NotesReceivableHandler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _NotesReceivableHandler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _NotesReceivableHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _NotesReceivableHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _NotesReceivableHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {
            ViewBag.Treasury = new SelectList(_TreasuryHandler.GetAll(), "ID", "TreasuryName"+lang);
            ViewBag.CostCenter = new SelectList(_CostCenterHandler.GetAll(), "ID", "CostCenter"+lang);
            ViewBag.Tree = new SelectList(_TreeHandler.GetAll().Where(x => x.AccountTreeLevel == 3 || x.AccountTreeLevel == 4), "AccountTreeID", "AccountName"+lang);

            return PartialView("_Edit", _NotesReceivableHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountNotesReceivableViewModel model)
        {
            if (ModelState.IsValid)
            {
                _NotesReceivableHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _NotesReceivableHandler.GetAll());

            }

            TempData["Msg"] = 4;
            return PartialView("_List", _NotesReceivableHandler.GetAll());
        }
    }
}