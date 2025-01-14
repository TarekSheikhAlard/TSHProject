using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.School.Management.Logic.BusinessLayer.Handler;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.BusinessLayer;
using System.IO;
using System.Net;
using Campus.SchoolManagment.Web.Models;
using System.Web.Script.Serialization;

namespace Campus.SchoolManagment.Web.Controllers
{
    //[Filters.Compress]
   // [Authorize(Roles = "SuperAdmin")]
    public class SchoolController : BaseController
    {
        private SchoolHandler _handler = new SchoolHandler();
        private SchoolUserController _UserController = new SchoolUserController();

        ApplicationDbContext dbMembership = new ApplicationDbContext();

        private CompanyHandler _CompanyHandler = new CompanyHandler();
        private CityHandler _CityHandler = new CityHandler();

        // GET: School
        public ActionResult Index()
        {
            int companyId = 0;

            if (Session["CompanyID"] != null)
            {
                companyId = int.Parse(Session["CompanyID"].ToString());

            }

            if (User.IsInRole("SuperAdmin"))
            {
                return PartialView(_handler.GetAllByCompanyID(companyId));
            }
            else
            {
                return PartialView(_handler.GetAllByCompanyID(companyId).Where(x=>x.SchoolID==schoolId));
            }       
        }

        public ActionResult Create(int? ID)
        {
            int companyId = 0;

            if (Session["CompanyID"]!=null)
            {
                 companyId = int.Parse(Session["CompanyID"].ToString());

            }

            ViewBag.CityID = new SelectList(_CityHandler.GetAll(), "ID", "CityEn");

            ViewBag.CompanyID = new SelectList(_CompanyHandler.GetAll(), "CompanyID", "CompanyNameEn", companyId);


            var model = (ID == null) ? (_handler.Create()) : _handler.GetById(ID.Value);

            return PartialView("_Create", model);
        }
        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {

                TempData["photo"] = ImageFile;

            }

            return Json("sucess");

        }
        [HttpPost]
        public ActionResult Create(SchoolViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (TempData["photo"] != null)
                {   
                    HttpPostedFileBase ImageFile = TempData["photo"] as HttpPostedFileBase;

                    string Name = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string vExtension = Path.GetExtension(ImageFile.FileName);

                    string ImageName = Name + "_" + Guid.NewGuid() + vExtension;
                    string physicalPath = Server.MapPath(Statistics.SchoolImagesPath + ImageName);
                    ImageFile.SaveAs(physicalPath);
                    model.Logo = ImageName;

                    string userName ="Admin " +  model.SchoolNameEn;
                    if (model.IsNew)
                    {
                     model =  _handler.CreateSchool(model);
                        
                        //  SchoolUserViewModel _schoolUser = new SchoolUserViewModel() {
                        //      Name = userName,
                        //      Email = model.Email,
                        //      Password = model.Password,
                        //      ConfirmPassword = model.ConfirmPassword,

                        //      SchoolID = model.SchoolID,
                        //      RoleID = dbMembership.Roles.Where(x=> x.Name == "Admin").FirstOrDefault().Id,
                        //  };
                        //int _userSaved =  _UserController.CreateUser(_schoolUser);
                        //  if (_userSaved==1)
                        //  {

                        //  TempData["Msg"] = 1;
                        //  return PartialView("_List", _handler.GetAll());
                        //  }
                        //  else if (_userSaved == 5)
                        //  {
                        //      _handler.Delete(model.SchoolID);
                        //      TempData["Msg"] = 5;
                        //      return PartialView("_List", _handler.GetAll());
                        //  }
                        //  else
                        //  {
                        //      _handler.Delete(model.SchoolID);
                        //      TempData["Msg"] = 4;
                        //      return PartialView("_List", _handler.GetAll());
                        //  }
                    }
                 
                }
                TempData["Msg"] = 1;

                if (User.IsInRole("SuperAdmin"))
                {
                    return PartialView(_handler.GetAllByCompanyID(companyId));
                }
                else
                {
                    return PartialView(_handler.GetAllByCompanyID(companyId).Where(x => x.SchoolID == schoolId));
                }
            }

            TempData["Msg"] = 4;

            if (User.IsInRole("SuperAdmin"))
            {
                return PartialView(_handler.GetAllByCompanyID(companyId));
            }
            else
            {
                return PartialView(_handler.GetAllByCompanyID(companyId).Where(x => x.SchoolID == schoolId));
            }
          
        }
        public ActionResult Edit(int ID)
        {
            var model = _handler.GetById(ID);
            ViewBag.CityID = new SelectList(_CityHandler.GetAll(), "ID", "CityEn", model.CityID);
            ViewBag.CompanyID = new SelectList(_CompanyHandler.GetAll(), "CompanyID", "CompanyNameEn", model.CompanyID);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(SchoolViewModel model)
        {
            if (ModelState.ContainsKey("Password"))
                ModelState["Password"].Errors.Clear();
            if (ModelState.ContainsKey("ConfirmPassword"))
                ModelState["ConfirmPassword"].Errors.Clear();
            if (ModelState.IsValid)
            {
                SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

                if (TempData["photo"] != null)
                {
                    HttpPostedFileBase ImageFile = TempData["photo"] as HttpPostedFileBase;

                    string Name = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string vExtension = Path.GetExtension(ImageFile.FileName);

                    string ImageName = Name + "_" + Guid.NewGuid() + vExtension;
                    string physicalPath = Server.MapPath(Statistics.SchoolImagesPath + ImageName);
                    ImageFile.SaveAs(physicalPath);
                    model.Logo = ImageName;

                    _handler.Update(model);
                    TempData["Msg"] = 2;

                    

                    if (model.Url != _dbUser.Url)
                        Session["SchoolUrl"]=model.Url;



                    if (User.IsInRole("SuperAdmin"))
                    {
                        return PartialView("_List",_handler.GetAllByCompanyID(companyId));
                    }
                    else
                    {
                        return PartialView("_List", _handler.GetAllByCompanyID(companyId).Where(x => x.SchoolID == schoolId));
                    }

                }
                else
                {
                    _handler.Update(model);
                    if (model.Url != _dbUser.Url)
                        Session["SchoolUrl"] = model.Url;
                    TempData["Msg"] = 2;

                    if (User.IsInRole("SuperAdmin"))
                    {
                        return PartialView("_List", _handler.GetAllByCompanyID(companyId));
                    }
                    else
                    {
                        return PartialView("_List", _handler.GetAllByCompanyID(companyId).Where(x => x.SchoolID == schoolId));
                    }

                }
            }
            TempData["Msg"] = 4;

            if (User.IsInRole("SuperAdmin"))
            {
                return PartialView("_List", _handler.GetAllByCompanyID(companyId));
            }
            else
            {
                return PartialView("_List", _handler.GetAllByCompanyID(companyId).Where(x => x.SchoolID == schoolId));
            }

        }

        public ActionResult SchoolCity()
        {
            ViewBag.CityID = new SelectList(_CityHandler.GetAll(), "ID", "CityAr");
            ViewBag.SchoolID = new List<SelectListItem>();
            //var model =  _handler.Create();

             return PartialView();
        }


        [HttpGet]
        public JsonResult GetChildSchool(int id)
        {
            var list = _handler.GetAll().Where(x => x.CityID == id).Select(x => new
            {
                x.SchoolID,
                x.SchoolNameAr
            }).ToList();
            // var Pol = new SelectList(list, "AccountTreeID", "AccountTreeName");
            return new JsonResult() { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: AdmStages/Delete/5
        public ActionResult Delete(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return PartialView("_Delete", ID);

        }

        // POST: Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            try
            {
                _handler.Delete(ID);
                TempData["Msg"] = 3;

                if (User.IsInRole("SuperAdmin"))
                {
                    return PartialView(_handler.GetAllByCompanyID(companyId));
                }
                else
                {
                    return PartialView(_handler.GetAllByCompanyID(companyId).Where(x => x.SchoolID == schoolId));
                }
            }
            catch (Exception)
            {
                TempData["Msg"] = 4;

                if (User.IsInRole("SuperAdmin"))
                {
                    return PartialView(_handler.GetAllByCompanyID(companyId));
                }
                else
                {
                    return PartialView(_handler.GetAllByCompanyID(companyId).Where(x => x.SchoolID == schoolId));
                }
            }
        }

    
    }
}