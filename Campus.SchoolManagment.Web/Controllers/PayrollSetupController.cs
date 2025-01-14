using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Campus.SchoolManagment.Web.Helper.SemanticControls;


namespace Campus.SchoolManagment.Web.Controllers
{
    [Authorize]
    public class PayrollSetupController : BaseController
    {
        EmployeeBenfitsOtherHandler _employeeBenfits = new EmployeeBenfitsOtherHandler();

        AccountTreeHandler _AccountTreeHandler = new AccountTreeHandler();

        private int companyId = 0;
        // GET: PayrollSetup
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Benefits()
        {
            var model = _employeeBenfits.GetAllByCompany(companyId, 1);

            ViewBag.type = 1;

            return PartialView("_BenefitsAndOthers", model);
        }
        public ActionResult Deductions()
        {
            var model = _employeeBenfits.GetAllByCompany(companyId, 2);

            ViewBag.type = 2;

            return PartialView("_BenefitsAndOthers", model);
        }
        public ActionResult Taxes()
        {
            var model = _employeeBenfits.GetAllByCompany(companyId, 3);

            ViewBag.type = 3;

            return PartialView("_BenefitsAndOthers", model);
        }

        public ActionResult Gov()
        {
            var model = _employeeBenfits.GetAllByCompany(companyId, 4);

            ViewBag.type = 3;

            return PartialView("_BenefitsAndOthers", model);
        }







        public ActionResult Create(int id)
        {

            ViewBag.AccountTreeList = _AccountTreeHandler.quickAccountTreeListFull().Select(x => new DropdownList
            {
                name = x.name,
                value = x.value.ToString(),
                text = x.text
            }).ToList();

            ViewBag.type = id;

            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeBenefitsOthersViewModel model)
        {
            if (ModelState.IsValid)
            {
                // model.CompanyID = companyId;
                _employeeBenfits.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_BenefitsAndOthers", _employeeBenfits.GetAllByCompany(companyId, model.Type));
            }
            TempData["Msg"] = 4;
            return PartialView("_BenefitsAndOthers", _employeeBenfits.GetAllByCompany(companyId, model.Type));
        }



        public ActionResult Edit(int id)
        {
            ViewBag.AccountTreeList = _AccountTreeHandler.quickAccountTreeListFull().Select(x => new DropdownList
            {
                name = x.name,
                value = x.value.ToString(),
                text = x.text
            }).ToList();
            var model = _employeeBenfits.GetById(id);
            ViewBag.type = model.Type;
            return PartialView("_Edit", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeBenefitsOthersViewModel model)
        {
            if (ModelState.IsValid)
            {
                _employeeBenfits.Update(model);
                TempData["Msg"] = 2;


                return PartialView("_BenefitsAndOthers", _employeeBenfits.GetAllByCompany(companyId, model.Type));

            }

            TempData["Msg"] = 4;
            return PartialView("_BenefitsAndOthers", _employeeBenfits.GetAllByCompany(companyId, model.Type));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var type = _employeeBenfits.GetById(id).Type;

            ViewBag.type = type;

            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var type = _employeeBenfits.GetById(id).Type;
            var result = _employeeBenfits.Delete(id);

            
            ViewBag.type = type;
            if (result)
                TempData["Msg"] = 3;
            else
                TempData["Msg"] = 5;

            return PartialView("_BenefitsAndOthers", _employeeBenfits.GetAllByCompany(companyId, type));
        }

    }

}