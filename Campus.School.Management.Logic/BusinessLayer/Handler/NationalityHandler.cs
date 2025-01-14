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
    public class NationalityHandler : IRepository<NationalityViewModel>
    {
        private SchoolManagmentDBEntities Context = new SchoolManagmentDBEntities();
        SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();


        public void Create(NationalityViewModel model)
        {
           
            Nationality _Nationality = new Nationality()
            {
                NationalityAr = model.NationalityAr,
                NationalityEn = model.NationalityEn,
                CompanyID    =  model.CompanyID,
                CreatedbyID = _dbUser.ID,
                CreatedDate = DateTime.Now,
                DeletedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            Context.Nationalities.Add(_Nationality);
            Context.SaveChanges();
        }

        public bool Delete(int ID)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Nationality = Context.Nationalities.Where(x => x.IsDeleted == false && x.NationalityID == ID).FirstOrDefault();
            if (_Nationality != null)
            {
                _Nationality.DeletedbyID = _dbUser.ID;
                _Nationality.IsDeleted = true;
                _Nationality.DeletedDate = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<NationalityViewModel> GetAll()
        {
            return Context.Nationalities.Where(x => x.IsDeleted == false).Select(c => new NationalityViewModel
            {
                NationalityAr = c.NationalityAr,
                NationalityEn = c.NationalityEn,
                NationalityID = c.NationalityID,

            }).ToList();
        }

        public List<NationalityViewModel> GetAllByCompanyID(int CompanyId)
        {
            return Context.Nationalities
                          .Where(x => x.IsDeleted == false && x.CompanyID == _dbUser.CompanyID)
                          .OrderByDescending(x => x.NationalityID)
                          .Select(c => new NationalityViewModel
                            {
                                NationalityAr = c.NationalityAr,
                                NationalityEn = c.NationalityEn,
                                NationalityID = c.NationalityID,

                            }).ToList();
                        }
        public NationalityViewModel GetById(int ID)
        {
            return Context.Nationalities.Where(c => c.IsDeleted == false && c.NationalityID == ID).Select(c => new NationalityViewModel
            {
                NationalityAr = c.NationalityAr,
                NationalityEn = c.NationalityEn,
                NationalityID = c.NationalityID,

            }).FirstOrDefault();
        }

        public NationalityViewModel Update(NationalityViewModel model)
        {
            SchoolUserViewModel _dbUser = Statistics.GetLogiccookie();
            var _Nationality = Context.Nationalities.Where(x => x.IsDeleted == false && x.NationalityID == model.NationalityID).FirstOrDefault();
            _Nationality.NationalityEn = model.NationalityEn;
            _Nationality.NationalityAr = model.NationalityAr;
            _Nationality.ModifiedbyID = _dbUser.ID;
            _Nationality.ModifiedDate = DateTime.Now;
            Context.SaveChanges();
            return model;
        }



        public NationalityViewModel Create()
        {
            throw new NotImplementedException();
        }
    }
}
