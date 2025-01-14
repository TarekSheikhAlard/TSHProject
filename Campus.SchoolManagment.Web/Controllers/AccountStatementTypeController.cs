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
    public class AccountStatementTypeController : Controller
    {

        AccountStatementTypeHandler _Handler = new AccountStatementTypeHandler();

        public ActionResult Index()
        {
             return PartialView(_Handler.GetAll());
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(AccountStatementTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _Handler.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _Handler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _Handler.GetAll());

        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _Handler.Delete(id);
                TempData["Msg"] = 3;
                return PartialView("_List", _Handler.GetAll());
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;
                return PartialView("_List", _Handler.GetAll());
            }
        }

        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", _Handler.GetById(id));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountStatementTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _Handler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", _Handler.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _Handler.GetAll());

        }


    }
}