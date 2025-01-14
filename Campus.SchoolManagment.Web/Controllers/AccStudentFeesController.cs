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
    public class AccStudentFeesController : Controller
    {
        AccStudentFeesHandler _Handler = new AccStudentFeesHandler();
        AccStudentConfigHandler _studConfig = new AccStudentConfigHandler();


        public ActionResult Index()
        {
             return PartialView();
        }

        [HttpGet]
        public ActionResult FeeSearch(int StudentAcdID)
        {
             var list = _Handler.Create(StudentAcdID);
            return PartialView("_PartialFees", list);
        }


        [HttpGet]
        public ActionResult Create()
        {
             return PartialView();// (_Handler.Create(StudentAcdID));

        }


        //[HttpPost]
        //public ActionResult Create(AccStudentFeesViewModel model)
        //{

        //    model.AccStudentConfigration = _Handler.GetConfigration(model.StudAcdID);  //_studConfig.GetById(model.StudAcdID);
        //    model.StudentConfigrationID = model.AccStudentConfigration.ID;
        //   model.AccStudentFee = _Handler.GetAll(model.StudAcdID);
        //    if (ModelState.IsValid)
        //    {
        //        _Handler.Create(model);
        //        model.AccStudentFee = _Handler.GetAll(model.StudAcdID);
        //        // return FeeSearch(int.Parse(model.StudAcdID.ToString()));
        //        //return PartialView("_PartialGrid", model);
        //        ViewBag.flage = 3;
        //        return PartialView("../Student/_List", model);
        //    }

            
        //     return PartialView(model);
        //}

    }
}