using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.SchoolManagment.Web.Filters;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Filters.Compress]
    [Authorize]
    public class AccFeeItemController : Controller
    {
        private AccFeeItemHandler accFeeItem = new AccFeeItemHandler();
        // GET: AccFeeItem
        public ActionResult Index()
        {
            return PartialView(accFeeItem.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("_Create");
        }


        [HttpPost]
        public ActionResult Create(AccFeeItemViewModel model)
        {
            if (ModelState.IsValid)
            {
             
                 accFeeItem.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", accFeeItem.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", accFeeItem.GetAll());
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            accFeeItem.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", accFeeItem.GetAll());
        }


    }
}