using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class SchoolHandler : IRepository<SchoolViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

        public SchoolViewModel Update(SchoolViewModel model)
        {

            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _School = Context.AdmSchools.Where(c => c.SchoolID == model.SchoolID).FirstOrDefault();
            if (_School != null)
            {
                _School.Address = model.Address;
                _School.CityID = model.CityID;
                // _School.CompanyID = model.CompanyID;
                _School.Email = model.Email;
                _School.Facebook = model.Facebook;
                _School.Fax = model.Fax;
                _School.Logo = model.Logo;
                _School.Mobile = model.Mobile;
                _School.phone = model.phone;
                _School.PostCode = model.PostCode;
                _School.SchoolNameAr = model.SchoolNameAr;
                _School.SchoolNameEn = model.SchoolNameEn;
                _School.SmsSender = model.SmsSender;
                _School.Website = model.Website;
                _School.whatsApp = model.whatsApp;
                _School.LicenseNo = model.LicenseNo;
                _School.SchSuperVision = model.SchSuperVision;
                _School.ModifiedDate = DateTime.Now;
                _School.ModifiedbyID = _dbUser.ID;
                _School.Url = model.Url;
                _School.YMap = model.Location.Split(',')[1];
                _School.XMap = model.Location.Split(',')[0];
                Context.SaveChanges();
            }
            return model;

        }

        public List<SchoolViewModel> GetAll()
        {
            var lang = Utility.getCultureCookie(false);
            return Context.AdmSchools.Where(x => x.IsDeleted == false).Select(c => new SchoolViewModel
            {
                SchoolID = c.SchoolID,
                Address = c.Address,
                CityID = c.CityID,
                CityName = lang.ToLower() == "ar" ? c.AdmCity.CityAr : c.AdmCity.CityEn,
                Email = c.Email,
                Facebook = c.Facebook,
                Fax = c.Fax,
                Mobile = c.Mobile,
                phone = c.phone,
                PostCode = c.PostCode,
                SchoolNameAr = c.SchoolNameAr,
                SchoolNameEn = c.SchoolNameEn,
                CompanyID = c.CompanyID,
                SmsSender = c.SmsSender,
                Website = c.Website,
                Logo = c.Logo,
                LicenseNo = c.LicenseNo,
                SchSuperVision = c.SchSuperVision,
                Url = c.Url,
                Location = c.XMap + "," + c.YMap


            }).ToList();
        }


        public List<SchoolViewModel> GetAllByCompanyID(int companyId)
        {

            var lang = Utility.getCultureCookie(false);
            return Context.AdmSchools
                          .Where(x => x.IsDeleted == false && x.CompanyID == companyId && x.SchoolID == _dbUser.SchoolID)
                          .OrderByDescending(x => x.SchoolID)
                          .Select(c => new SchoolViewModel
                          {
                              SchoolID = c.SchoolID,
                              Address = c.Address,
                              CityID = c.CityID,
                              CityName = lang == "Ar" ? c.AdmCity.CityAr : c.AdmCity.CityEn,
                              Email = c.Email,
                              Facebook = c.Facebook,
                              Fax = c.Fax,
                              Mobile = c.Mobile,
                              phone = c.phone,
                              PostCode = c.PostCode,
                              SchoolNameAr = c.SchoolNameAr,
                              SchoolNameEn = c.SchoolNameEn,
                              CompanyID = c.CompanyID,
                              SmsSender = c.SmsSender,
                              Website = c.Website,
                              Logo = c.Logo,
                              LicenseNo = c.LicenseNo,
                              SchSuperVision = c.SchSuperVision,
                              Url = c.Url,
                              Location = c.XMap + "," + c.YMap
                          }).ToList();

        }
        public List<SchoolViewModel> GetByCompanyID(int companyID)
        {
            return Context.AdmSchools.Where(x => x.IsDeleted == false && x.CompanyID == companyID).Select(c => new SchoolViewModel
            {
                SchoolID = c.SchoolID,
                Address = c.Address,
                CityID = c.CityID,
                CityName = c.AdmCity.CityEn,
                Email = c.Email,
                Facebook = c.Facebook,
                Fax = c.Fax,
                Mobile = c.Mobile,
                phone = c.phone,
                PostCode = c.PostCode,
                SchoolNameAr = c.SchoolNameAr,
                SchoolNameEn = c.SchoolNameEn,
                CompanyID = c.CompanyID,
                SmsSender = c.SmsSender,
                Website = c.Website,
                Logo = c.Logo,
                LicenseNo = c.LicenseNo,
                SchSuperVision = c.SchSuperVision,
                Url = c.Url,
                Location = c.XMap + "," + c.YMap
            }).ToList();

        }

        public SchoolViewModel GetById(int ID)
        {
            return Context.AdmSchools.Where(c => c.SchoolID == ID && c.IsDeleted == false).Select(c => new SchoolViewModel
            {
                SchoolID = c.SchoolID,
                Address = c.Address,
                CityID = c.CityID,
                Email = c.Email,
                Facebook = c.Facebook,
                Fax = c.Fax,
                Mobile = c.Mobile,
                phone = c.phone,
                PostCode = c.PostCode,
                SchoolNameAr = c.SchoolNameAr,
                SchoolNameEn = c.SchoolNameEn,
                CompanyID = c.CompanyID,
                SmsSender = c.SmsSender,
                Website = c.Website,
                whatsApp = c.whatsApp,
                Logo = c.Logo,
                IsNew = false,
                CityName = c.AdmCity.CityEn,
                CompanyName = c.AdmCompany.CompanyNameEn,
                LicenseNo = c.LicenseNo,
                SchSuperVision = c.SchSuperVision,
                Url = c.Url,
                Location = c.XMap + "," + c.YMap
            }).FirstOrDefault();
        }

        public void Create(SchoolViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();

            AdmSchool _School = new AdmSchool
            {
                Address = model.Address,
                CityID = model.CityID,
                Email = model.Email,
                Facebook = model.Facebook,
                Fax = model.Fax,
                Mobile = model.Mobile,
                phone = model.phone,
                PostCode = model.PostCode,
                SchoolNameAr = model.SchoolNameAr,
                SchoolNameEn = model.SchoolNameEn,
                CompanyID = model.CompanyID,
                SmsSender = model.SmsSender,
                Website = model.Website,
                whatsApp = model.whatsApp,
                Logo = model.Logo,
                LicenseNo = model.LicenseNo,
                SchSuperVision = model.SchSuperVision,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Url = model.Url,
                YMap = model.Location.Split(',')[1],
                XMap = model.Location.Split(',')[0]
            };

            Context.AdmSchools.Add(_School);
            Context.SaveChanges();
        }

        public SchoolViewModel CreateSchool(SchoolViewModel model)
        {

            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AdmSchool _School = new AdmSchool
            {
                Address = model.Address,
                CityID = model.CityID,
                Email = model.Email,
                Facebook = model.Facebook,
                Fax = model.Fax,
                Mobile = model.Mobile,
                phone = model.phone,
                PostCode = model.PostCode,
                SchoolNameAr = model.SchoolNameAr,
                SchoolNameEn = model.SchoolNameEn,
                CompanyID = model.CompanyID,
                SmsSender = model.SmsSender,
                Website = model.Website,
                whatsApp = model.whatsApp,
                Logo = model.Logo,
                LicenseNo = model.LicenseNo,
                SchSuperVision = model.SchSuperVision,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                Url = model.Url,
                YMap = model.Location.Split(',')[1],
                XMap = model.Location.Split(',')[0]
            };
            Context.AdmSchools.Add(_School);
            Context.SaveChanges();
            model.SchoolID = _School.SchoolID;

            Context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                   String.Format(@"SET identity_insert  AccountTree ON 
                    INSERT INTO AccountTree (AccountTreeID,[AccountNameAR],[AccountNameEN],[AccountTreeCode],[AccountTreeParentID],[AccountTreeLevel], [CreatedDate],[CreatedbyID],[ModifiedDate],[ModifiedbyID],  [DeletedDate],[DeletedbyID],[IsDeleted],[PageUrl],[BankCurrencyID], [SchoolID] ) 
                    SELECT AccountTreeID, [AccountNameAR],[AccountNameEN],[AccountTreeCode],[AccountTreeParentID],[AccountTreeLevel],
                    [CreatedDate],[CreatedbyID],[ModifiedDate],[ModifiedbyID],
                    [DeletedDate],[DeletedbyID],[IsDeleted],
                    [PageUrl],[BankCurrencyID],'{0}' [SchoolID] 
                    FROM [dbo].[AccountTree]  
                    WHERE AccountTreeLevel =1 OR AccountTreeLevel =2 
                    AND USEFORSTATIC = 1
                    SET identity_insert  AccountTree OFF
                    ", model.SchoolID));

            Context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                   String.Format(@"  SET identity_insert  [InventoryTree] ON 
                            INSERT INTO [InventoryTree]  ([InventoryTreeID],[InventoryNameAR],[InventoryNameEN],[InventoryTreeCode],[InventoryTreeParentID],[InventoryTreeLevel],[CreatedDate],[CreatedbyID],[ModifiedDate],[ModifiedbyID],[DeletedDate],[DeletedbyID],[IsDeleted],[SchoolID],[Notes])
                              SELECT 
                           [InventoryTreeID], [InventoryNameAR]
                          ,[InventoryNameEN]
                          ,[InventoryTreeCode]
                          ,[InventoryTreeParentID]
                          ,[InventoryTreeLevel]
                          ,[CreatedDate]
                          ,[CreatedbyID]
                          ,[ModifiedDate]
                          ,[ModifiedbyID]
                          ,[DeletedDate]
                          ,[DeletedbyID]
                          ,[IsDeleted]
                          ,'{0}' [SchoolID]
                          ,[Notes]
  FROM [dbo].[InventoryTree]
  where [InventoryTreeLevel] =1 AND USEFORSTATIC = 1
 SET identity_insert  [InventoryTree] OFF 
", model.SchoolID));

            return model;
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _School = Context.AdmSchools.Where(c => c.SchoolID == ID).FirstOrDefault();
            if (_School != null)
            {
                _School.IsDeleted = true;
                _School.DeletedbyID = _dbUser.ID;
                _School.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;

        }

        public SchoolViewModel Create()
        {
            SchoolViewModel model = new SchoolViewModel();
            model.IsNew = true;
            return model;
        }
    }
}