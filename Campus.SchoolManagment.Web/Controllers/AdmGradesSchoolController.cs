using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Employee")]
    public class AdmGradesSchoolController : BaseController
    {
        // GET: AdmGradesSchool
        GradesSchoolHandler _gradeschool = new GradesSchoolHandler();


        public ActionResult Index( )
        {
             return PartialView(_gradeschool.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Nationality = new SelectList(_gradeschool.GetAll(), "NationalityID", "Nationality"+lang);
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(GradesSchoolViewModel model)
        {
            if (ModelState.IsValid)
            {
                _gradeschool.Create(model);
                TempData["Msg"] = 1;
                return PartialView("_List", _gradeschool.GetAll());
            }
            TempData["Msg"] = 4;
            return PartialView("_List", _gradeschool.GetAll());
        }



    }
}



//public class CityController : Controller
//{


//    private CityHandler _CityHandler = new CityHandler();
//    private NationalityHandler _NationalityHandler = new NationalityHandler();

//    public ActionResult Index()
//    {
//         return PartialView(_CityHandler.GetAll());
//    }

//    [HttpGet]
//    public ActionResult Create()
//    {
//        ViewBag.Nationality = new SelectList(_NationalityHandler.GetAll(), "NationalityID", "NationalityEn");
//        return PartialView("_Create");
//    }



//    [HttpPost]
//    public ActionResult Create(CityViewModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            _CityHandler.Create(model);
//            TempData["Msg"] = 1;
//            return PartialView("_List", _CityHandler.GetAll());
//        }
//        TempData["Msg"] = 4;
//        return PartialView("_List", _CityHandler.GetAll());
//    }


//    [HttpGet]
//    public ActionResult Delete(int id)
//    {
//        return PartialView("_Delete", id);

//    }


//    [HttpPost, ActionName("Delete")]
//    public ActionResult ConfirmDelete(int id)
//    {
//        _CityHandler.Delete(id);
//        TempData["Msg"] = 3;
//        return PartialView("_List", _CityHandler.GetAll());
//    }


//    public ActionResult Edit(int id)
//    {
//        ViewBag.Nationality = new SelectList(_NationalityHandler.GetAll(), "NationalityID", "NationalityEn");

//        return PartialView("_Edit", _CityHandler.GetById(id));

//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public ActionResult Edit(CityViewModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            _CityHandler.Update(model);
//            TempData["Msg"] = 2;
//            return PartialView("_List", _CityHandler.GetAll());

//        }

//        TempData["Msg"] = 4;
//        return PartialView("_List", _CityHandler.GetAll());
//    }



//}
//}