using Campus.School.Management.Logic.BusinessLayer;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Controllers
{
   
  [Filters.Compress]
    [Authorize(Roles = "SuperAdmin")]
    public class CompanyController : Controller
    {
        CompanyHandler _CompanyHandler = new CompanyHandler();

        public ActionResult Index()
        {
            return PartialView(_CompanyHandler.GetAll());
        }


        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("_Create");
        }


        [HttpPost]
        public ActionResult Create(CompanyViewModel model)
        {
             if (ModelState.IsValid)
            {
                if (TempData["xc"] != null)
                {
                    HttpPostedFileBase ImageFile = TempData["xc"] as HttpPostedFileBase;

                    string Name = Path.GetFileNameWithoutExtension(ImageFile.FileName);

                    string vExtension = Path.GetExtension(ImageFile.FileName);

                    string ImageName = Name + "_" + Guid.NewGuid() + vExtension;

                 
                    string physicalPath = Server.MapPath(Statistics.SchoolImagesPath + ImageName);
                    ImageFile.SaveAs(physicalPath);
                   
                    model.Logo = ImageName;
                    _CompanyHandler.Create(model);
                    TempData["Msg"] = 1;
                    return PartialView("_List", _CompanyHandler.GetAll());

                }
               
            }

            TempData["Msg"] = 4;
            return PartialView("_List", _CompanyHandler.GetAll());
        }



        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {

                TempData["xc"] = ImageFile;
       
            }

            return Json("sucess");

        }




        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", id);

        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _CompanyHandler.Delete(id);
            TempData["Msg"] = 3;
            return PartialView("_List", _CompanyHandler.GetAll());
        }


        public ActionResult Edit(int id)
        {

            return PartialView("_Edit", _CompanyHandler.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (TempData["xc"] != null)
                {
                    HttpPostedFileBase ImageFile = TempData["xc"] as HttpPostedFileBase;

                    string Name = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string vExtension = Path.GetExtension(ImageFile.FileName);
                    string ImageName = Name + "_" + Guid.NewGuid() + vExtension;

                    string physicalPath = Server.MapPath(Statistics.SchoolImagesPath + ImageName);
                    ImageFile.SaveAs(physicalPath);

                    model.Logo = ImageName;
                    _CompanyHandler.Update(model);
                    TempData["Msg"] = 2;
                    return PartialView("_List", _CompanyHandler.GetAll());

                }
                else
                {
                    _CompanyHandler.Update(model);
                    TempData["Msg"] = 2;
                    return PartialView("_List", _CompanyHandler.GetAll());
                }

            }


            TempData["Msg"] = 4;
            return PartialView("_List", _CompanyHandler.GetAll());
        }
    }
}