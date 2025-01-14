using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class AccStudentConfigHandler:IRepository<AccStudentConfigViewModel>
    {

        private SchoolManagmentDBEntities context = new SchoolManagmentDBEntities();

        public AccStudentConfigViewModel Create()
        {
            AccStudentConfigViewModel model = new AccStudentConfigViewModel();

            return model;

        }

        public void Create(AccStudentConfigViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AccStudentConfig _AccStudentConfig = new AccStudentConfig()
            {
                AccountTreeID=model.FeeItemId,
                //FeeItemId=model.FeeItemId,
              //  Amount=model.Amount,
                Discount=model.Discount,
               // Tax= model.Tax,
                Description=model.Description,
                CreatedBy=_dbUser.ID.ToString(),
                CreatedDate= DateTime.Now,
                Ref_Id=model.Ref_Id,
                IsActive=true,
                SchoolID=_dbUser.SchoolID
                
            };
            context.AccStudentConfigs.Add(_AccStudentConfig);
            context.SaveChanges();        
        }

        public void Upsert(AccStudentConfigViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            var bussFee = context.AccStudentConfigs.Where(x => x.AccountTreeID == 7 && x.Ref_Id == model.Ref_Id).FirstOrDefault();

            if (bussFee != null)
            {
               
                //bussFee.Amount = model.Amount;
                //bussFee.Tax = model.Tax;
                bussFee.Discount = model.Discount;
                bussFee.Description = model.Description;
                bussFee.ModifiedBy = _dbUser.ID.ToString();
                bussFee.ModifiedDate = DateTime.Now;
                bussFee.IsActive = true;
            }
            else
            {

                AccStudentConfig _AccStudentConfig = new AccStudentConfig()
                {
                    AccountTreeID = model.FeeItemId,
                   // Amount = model.Amount,
                    Discount = model.Discount,
                    //Tax = model.Tax,
                    Description = model.Description,
                    CreatedBy = _dbUser.ID.ToString(),
                    CreatedDate = DateTime.Now,
                    Ref_Id = model.Ref_Id,
                    IsActive = true

                };
                context.AccStudentConfigs.Add(_AccStudentConfig);
               
            }
            context.SaveChanges();
        }







        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccStudentConfig = context.AccStudentConfigs.Where(x => x.ID == ID).FirstOrDefault();

            _AccStudentConfig.IsActive = false;
            _AccStudentConfig.DeleteBy = _dbUser.ID.ToString();
            _AccStudentConfig.DeletedDate = DateTime.Now;

            context.SaveChanges();
            return true;
        }

        public List<AccStudentConfigViewModel> GetAll()
        {
            var lang = Utility.getCultureCookie(false).ToLower();
            return context.AccStudentConfigs.Where(x => x.IsActive == true)
                .Select(x => new AccStudentConfigViewModel
             {
                 ID=x.ID,
                 FeeItemId=x.AccountTreeID,
                 FeeItemName = lang == "ar" ? x.AccountTree.AccountNameAR : x.AccountTree.AccountNameEN,
                 //FeeItemName = x.AccFeeItem.NAME_EN,
                 //Amount = x.Amount,
                 Discount = x.Discount,
                 //Tax = x.Tax,
                 Description = x.Description,
                 CreatedBy = x.CreatedBy,
                 CreatedDate = x.CreatedDate,
                 StudentAcdID = x.StudentReference.StudAcdID,
                 Ref_Id =x.Ref_Id 

             }).ToList();
        }
        public List<AccStudentConfigViewModel> GetAllByRefId(long id)
        {
            var lang = Utility.getCultureCookie(false).ToLower();
            return context.AccStudentConfigs.Where(x => x.IsActive == true && x.Ref_Id==id)
                .Select(x => new AccStudentConfigViewModel
                {
                    ID = x.ID,
                    FeeItemId = x.AccountTreeID,
                    FeeItemName= lang=="ar" ? x.AccountTree.AccountNameAR:x.AccountTree.AccountNameEN,
                    Discount = x.Discount, 
                    Description = x.Description,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    StudentAcdID = x.StudentReference.StudAcdID,
                    Ref_Id = x.Ref_Id

                }).ToList();
        }


        public AccStudentConfigViewModel GetById(int ID)
        {
            var lang = Utility.getCultureCookie(false).ToLower();
            return context.AccStudentConfigs.Where(x => x.ID == ID)
              .Select(x => new AccStudentConfigViewModel
              {
                  ID=x.ID,
                  //FeeItemName = x.AccFeeItem.NAME_EN,
                  FeeItemName = lang == "ar" ? x.AccountTree.AccountNameAR : x.AccountTree.AccountNameEN,
                  Discount = x.Discount,
                  //Tax = x.Tax,
                  Description = x.Description,
                  StudentAcdID = x.StudentReference.StudAcdID,
                  Ref_Id=x.Ref_Id


              }).FirstOrDefault();
           
        }

        public List<AccStudentConfigViewModel> GetConfigsByStudent(int ID)
        {
            string lang = Utility.getCultureCookie(false).ToLower();

            return context.AccStudentConfigs.Where(x => x.Ref_Id == ID && x.IsActive==true)
              .Select(x => new AccStudentConfigViewModel
              {
                  ID = x.ID,
                  //FeeItemName = lang=="ar"?x.AccFeeItem.NAME_AR: x.AccFeeItem.NAME_EN,
                  
                  Discount = x.Discount,
                  
                  Description = x.Description,
                  StudentAcdID = x.StudentReference.StudAcdID,
                  Ref_Id=x.Ref_Id
              }).ToList();
        }

        public AccStudentConfigViewModel Update(AccStudentConfigViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _AccStudentConfig = context.AccStudentConfigs.Where(x => x.ID == model.ID).FirstOrDefault();
   
            _AccStudentConfig.Discount = model.Discount;
            _AccStudentConfig.Description = model.Description;
            _AccStudentConfig.ModifiedBy = _dbUser.ID.ToString();
            _AccStudentConfig.ModifiedDate = DateTime.Now;

            context.SaveChanges();
            return model;
        }
    }
}
