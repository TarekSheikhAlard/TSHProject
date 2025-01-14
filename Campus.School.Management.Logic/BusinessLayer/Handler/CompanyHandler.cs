using Campus.School.Management.Logic.BusinessLayer.Abstract;
using Campus.School.Management.Logic.BusinessLayer.ViewModel;
using Campus.School.Management.Logic.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Web;
using Newtonsoft.Json;
using Microsoft.SqlServer;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Campus.School.Management.Logic.BusinessLayer.Handler
{
    public class CompanyHandler : IRepository<CompanyViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();


        public CompanyViewModel Update(CompanyViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Company = Context.AdmCompanies.Where(c => c.CompanyID == model.CompanyID&& c.IsDeleted==false).FirstOrDefault();
            _Company.CompanyMobile = model.CompanyMobile;
            _Company.CompanyNameAr = model.CompanyNameAr;
            _Company.CompanyNameEn = model.CompanyNameEn;
            _Company.CompanyPhone = model.CompanyPhone;
            _Company.Logo = model.Logo;
            _Company.ModifiedbyID = _dbUser.ID;
            _Company.ModifiedDate = DateTime.Now;
            _Company.showArabic = model.showArabic;
            _dbUser.isArabic = model.showArabic;
            Setcookie(_dbUser);

            Context.SaveChanges();
            return model;

        }
        private void Setcookie(SchoolUserViewModel dbusr)
        {
            string _user = JsonConvert.SerializeObject(dbusr);
            var cookie = new HttpCookie("dbusr", _user)
            {
                Expires = DateTime.Now.AddYears(1)
            };
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public List<CompanyViewModel> GetAll()
        {
            return Context.AdmCompanies.Where(c=>c.IsDeleted==false).Select(c => new CompanyViewModel
            {
                CompanyID = c.CompanyID,
                CompanyMobile = c.CompanyMobile,
                CompanyNameAr = c.CompanyNameAr,
                CompanyNameEn = c.CompanyNameEn,
                CompanyPhone = c.CompanyPhone,
                Logo = c.Logo,
                showArabic = c.showArabic

            }).ToList();

        }

        public CompanyViewModel GetById(int ID)
        {
            return Context.AdmCompanies.Where(c => c.CompanyID == ID && c.IsDeleted==false).Select(c => new CompanyViewModel
            {
                CompanyID = c.CompanyID,
                CompanyMobile = c.CompanyMobile,
                CompanyNameAr = c.CompanyNameAr,
                CompanyNameEn = c.CompanyNameEn,
                CompanyPhone = c.CompanyPhone,
                Logo = c.Logo,
                showArabic = c.showArabic



            }).FirstOrDefault();
        }

        public void Create(CompanyViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            AdmCompany _Company = new AdmCompany
            {
                CompanyMobile = model.CompanyMobile,
                CompanyNameAr = model.CompanyNameAr,
                CompanyNameEn = model.CompanyNameEn,
                CompanyPhone = model.CompanyPhone,
                Logo = model.Logo,
                CreatedDate =DateTime.Now,
                CreatedbyID = _dbUser.ID,
                ModifiedDate =DateTime.Now,
                DeletedDate =DateTime.Now,
                showArabic = model.showArabic


            };
            Context.AdmCompanies.Add(_Company);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Company = Context.AdmCompanies.Where(c => c.CompanyID == ID&& c.IsDeleted==false).FirstOrDefault();
            if (_Company != null)
            {
                _Company.DeletedbyID = _dbUser.ID;
                _Company.IsDeleted = true;
                _Company.DeletedDate = DateTime.Now;
               
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public CompanyViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
