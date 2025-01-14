using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Campus.SchoolManagment.Web.Controllers
{
    public class JournalTypesController : BaseController
    {
        // GET: journalTypes
        JournalTypesHandler journalTypesHandler = new JournalTypesHandler();

        public ActionResult Index()
        {
            if (companyId != 0)

            {
                return PartialView(journalTypesHandler.GetAllByCompany(companyId));
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }

        }
        [HttpGet]
        public ActionResult Create()
        {
           
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(JournalTypesViewModal model)
        {
            if (ModelState.IsValid)
            {
                int companyId = 0;

                if (Session["CompanyID"] != null)
                {
                    companyId = int.Parse(Session["CompanyID"].ToString());

                }
                model.companyId = companyId;

                journalTypesHandler.Create(model);

                TempData["Msg"] = 1;

                return PartialView("_List", journalTypesHandler.GetAllByCompany(companyId));
            }
            TempData["Msg"] = 4;
            return PartialView("_List", journalTypesHandler.GetAllByCompany(companyId));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            journalTypesHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", journalTypesHandler.GetAllByCompany(companyId));
        }

        public ActionResult Edit(int id)
        {
           

            return PartialView("_Edit", journalTypesHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JournalTypesViewModal model)
        {
            if (ModelState.IsValid)
            {
                journalTypesHandler.Update(model);
                TempData["Msg"] = 2;
                return PartialView("_List", journalTypesHandler.GetAllByCompany(companyId));

            }

            TempData["Msg"] = 4;
            return PartialView("_List", journalTypesHandler.GetAllByCompany(companyId));
        }



    }
}